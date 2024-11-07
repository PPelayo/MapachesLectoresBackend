using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

public class DataResult<TSuccess>
{
    public bool IsSuccess { get; protected set; }

    public bool IsFailure => !IsSuccess;
    
    private Success _successResult;

    private Failure _failureResult;

    public  Success SuccessResult
    {
        get => IsSuccess ? _successResult : throw new Exception("No puedes acceder al resultado cuando ha sido fallido");
        protected set
        {
            _successResult = value;
        }
    }

    public Failure FailureResult
    {
        get => !IsSuccess ? _failureResult : throw new Exception("No puedes acceder al error cuando ha sido validado");
        protected set
        {
            _failureResult = value;
        }
    }

    private DataResult(TSuccess data)
    {
        IsSuccess = true;
        SuccessResult = new Success(data);
    }

    private DataResult(IError error)
    {
        IsSuccess = false;
        FailureResult = new Failure(error);
    }

    public static DataResult<TSuccess> CreateSuccess(TSuccess data)
    {
        return new DataResult<TSuccess>(data);
    }

    public static DataResult<TSuccess> CreateFailure(IError error)
    {
        return new DataResult<TSuccess>(error);
    }

    public DataResult<TResult> Map<TResult>(Func<TSuccess, TResult> predicate)
    {
        return IsFailure 
            ? DataResult<TResult>.CreateFailure(FailureResult.Error) 
            : DataResult<TResult>.CreateSuccess(predicate(SuccessResult.Data));
    } 
    
    public IActionResult ActionResultHanlder(
        Func<TSuccess, IActionResult> onSuccess,
        Func<IError, IActionResult> onFailure
    )
    {
        return IsSuccess 
            ? onSuccess(SuccessResult.Data) 
            : onFailure(FailureResult.Error);
    }


    public sealed class Success(TSuccess data)
    {
        public TSuccess Data { get; } = data;
    }
    
    public sealed class Failure(IError error)
    {
        public IError Error { get; } = error;
    }
    
}