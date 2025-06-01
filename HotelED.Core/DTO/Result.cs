namespace HotelED.Core.DTO;

public class Result
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;

    public static Result Ok()
    {
        return new Result { Success = true };
    }

    public static Result Fail(string msg)
    {
        return new Result { Success = false, Message = msg };
    }
}