using System.Collections.Generic;

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

    public class PagedResult<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int Code { get; set; }
        public IEnumerable<T>? Data { get; set; }

        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public static PagedResult<T> Fail(string message, int code = 500) => new PagedResult<T> { IsSuccess = false, Message = message, Code = code };
        public static PagedResult<T> Success(IEnumerable<T> data, int page, int pageSize, int totalCount) =>
            new() { IsSuccess = true, Data = data, PageSize = pageSize, Page = page, TotalCount = totalCount, TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize) };
    }
}
