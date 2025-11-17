namespace Models.Common;
public class ErrorResult<TValue, TError>
{
	public TValue? Value { get; init; }
	public TError? Error { get; init; }
	public bool IsValue { get; init; }
	public bool IsError { get; init; }
	public ErrorResult(TValue value)
	{
		Value = value;
		IsValue = true;
	}

	public ErrorResult(TError exception)
	{
		Error = exception;
		IsError = true;
	}

	public void Match(Action<TValue> valueAction, Action<TError> errorAction)
	{
		if (IsValue)
			valueAction(Value!);
		else
			errorAction(Error!);
	}

	public T Switch<T>(Func<TValue, T> valueFunction, Func<TError, T> errorFunction)
	{
		if (IsValue)
			return valueFunction(Value!);
		else
			return errorFunction(Error!);
	}
}
