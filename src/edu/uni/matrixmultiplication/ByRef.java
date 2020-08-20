package edu.uni.matrixmultiplication;

public class ByRef<T>
{
	private T value;
	
	public ByRef()
	{
		value = null;
	}
	
	public void setValue(T value)
	{
		this.value = value;
	}
	
	public T getValue()
	{
		return value;
	}

}
//predava stoynost po referenciq
