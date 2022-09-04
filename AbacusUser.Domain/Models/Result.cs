using FluentValidation.Results;

namespace AbacusUser.Domain.Models;

public record Result
{
    public Result()
    {
        Errors = new();
    }
    public bool Success { get; set; }
    public List<string> Errors { get; set; }
}

public record Result<T> : Result
{
    public Result() : base() { }
    public Result(T data, IEnumerable<string>? errors = null)
    {
        Success = true;
        Data = data;
        Errors = errors?.ToList() ?? new();
    }

    public Result(T data, string error) : base()
    {
        Success = true;
        Data = data;
        Errors.Add(error);
    }

    public T? Data { get; set; }
    public static Result<T> Ok(T data, IEnumerable<string>? errors = null) => new(data, errors);
    public static Result<T> Ok(T data, string error) => new(data, error);

    public static implicit operator Result<T>(ErrorResult result) => new() { Errors = result.Errors };
}

public record ErrorResult : Result
{
    public ErrorResult(string error) : base()
    {
        Errors.Add(error);
    }

    public ErrorResult(IEnumerable<string> errors)
    {
        Errors = errors.ToList();
    }

    public ErrorResult(List<ValidationFailure> failures)
    {
        Errors = failures.Select(x => x.ErrorMessage).ToList();
    }
}
