namespace Ambev.DeveloperEvaluation.Domain.Results
{
    public class Result
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int Code { get; set; }

        public static Result Fail(string message, int code = 500) => new Result { IsSuccess = false, Message = message, Code = code };
        public static Result Success(string message = "") => new Result { IsSuccess = true, Message = message };
    }
}
