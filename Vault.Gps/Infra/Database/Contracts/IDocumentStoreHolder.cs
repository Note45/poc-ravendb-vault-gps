using Raven.Client.Documents;

namespace vault_gps.Infra.Database.Contracts;

public interface IDocumentStoreHolder
{
    public IDocumentStore CreateStore();
}