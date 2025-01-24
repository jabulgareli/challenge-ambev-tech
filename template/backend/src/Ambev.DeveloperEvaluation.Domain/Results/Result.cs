namespace Ambev.DeveloperEvaluation.Domain.Results
{
    public class Result
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int Code { get; set; }

        public static Result Fail(string message, int code = 500) => new() { IsSuccess = false, Message = message, Code = code };
        public static Result Success(string message = "") => new() { IsSuccess = true, Message = message };
    }

    public class DataResult<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int Code { get; set; }

        public T? Data { get; set; }

        public static DataResult<T> Fail(string message, int code = 500) => new() { IsSuccess = false, Message = message, Code = code };
        public static DataResult<T> Success(T data, string message = "") => new() { Data = data,  IsSuccess = true, Message = message };
    }

    public class PaginatedResult<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int Code { get; set; }
        public T? Data { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public static PaginatedResult<T> Fail(string message, int code = 500) => new PaginatedResult<T> { IsSuccess = false, Message = message, Code = code };
        public static PaginatedResult<T> Success(T data, int pageNumber, int pageSize, int totalCount) =>
            new() { IsSuccess = true, Data = data, PageSize = pageSize, CurrentPage = pageNumber, TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize) };
    }
}
