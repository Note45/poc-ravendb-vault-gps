using Raven.Client.Documents.Indexes;
using vault_gps.Application.Queries;
using vault_gps.Contracts.Models;

namespace vault_gps.Infra.Database.Indexes;

public class GpsPositionByAggregateId: AbstractIndexCreationTask<GpsPositionItem, GpsPositionAggregateResult>
{
    public GpsPositionByAggregateId()
    {
        Map = items => from item in items
            select new
            {
                item.AggregateId,
                item.EventType,
                item.Latitude,
                item.Longitude,
                item.UpdateTime,
                TotalEvents = 1
            };

        Reduce = results => from result in results
            group result by result.AggregateId into g
            let last = g.OrderByDescending(x => x.UpdateTime).First()
            select new GpsPositionAggregateResult
            {
                AggregateId = g.Key,
                EventType = last.EventType,
                Latitude = last.Latitude,
                Longitude = last.Longitude,
                UpdateTime = g.Max(x => x.UpdateTime),
                TotalEvents = g.Sum(x => x.TotalEvents)
            };
    }
}