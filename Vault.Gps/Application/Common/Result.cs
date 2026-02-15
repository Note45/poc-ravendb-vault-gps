namespace vault_gps.Application.Common;

/// <summary>
/// Representa o resultado de uma operação, encapsulando sucesso ou falha
/// </summary>
public abstract record Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }

    protected Result(bool isSuccess, string? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new SuccessResult();
    public static Result Failure(string error) => new FailureResult(error);

    public sealed record SuccessResult : Result
    {
        public SuccessResult() : base(true) { }
    }

    public sealed record FailureResult : Result
    {
        public FailureResult(string error) : base(false, error) { }
    }
}

/// <summary>
/// Representa o resultado de uma operação com um valor de sucesso
/// </summary>
public abstract record Result<T> : Result
{
    public T? Value { get; }

    protected Result(bool isSuccess, T? value = default, string? error = null)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new SuccessResult(value);
    public static Result<T> Failure(string error) => new FailureResult(error);

    public sealed record SuccessResult : Result<T>
    {
        public SuccessResult(T value) : base(true, value) { }
    }

    public sealed record FailureResult : Result<T>
    {
        public FailureResult(string error) : base(false, error: error) { }
    }
}




