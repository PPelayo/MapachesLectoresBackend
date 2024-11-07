namespace MapachesLectoresBackend.Core.Presentation.Dtos;

public class BaseResponse
{
    public int StatusCode { get; set; }

    public bool IsValid { get; set; }

    public object? Error { get; set; }

    public object? Result { get; set; }
    
    public static BaseResponse CreateSuccess(int code, object result)
    {
        return new BaseResponse
        {
            StatusCode = code,
            IsValid = true,
            Result = result
        };
    }
    
    public static BaseResponse CreateError(int code, object error)
    {
        return new BaseResponse
        {
            StatusCode = code,
            IsValid = false,
            Error = error
        };
    }
};