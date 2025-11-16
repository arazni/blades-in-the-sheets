namespace Models.Common;
public class ErrorResult<TValue, TException>
where TException : Exception
{
	public TValue? Value { get; init; }
	public TException? Error { get; init; }
	public bool IsValue { get; init; }
	public bool IsError { get; init; }
	public ErrorResult(TValue value)
	{
		Value = value;
		IsValue = true;
	}

	public ErrorResult(TException exception)
	{
		Error = exception;
		IsError = true;
	}

	public void Match(Action<TValue> valueAction, Action<TException> errorAction)
	{
		if (IsValue)
			valueAction(Value!);
		else
			errorAction(Error!);
	}

	public T Switch<T>(Func<TValue, T> valueFunction, Func<TException, T> errorFunction)
	{
		if (IsValue)
			return valueFunction(Value!);
		else
			return errorFunction(Error!);
	}
}
