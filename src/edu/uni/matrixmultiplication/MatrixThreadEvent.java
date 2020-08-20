package edu.uni.matrixmultiplication;

import java.util.Date;
import java.util.EventObject;

public class MatrixThreadEvent extends EventObject
{
	
	private Date eventTime;
	
	
	public MatrixThreadEvent(MatrixThread source, Date eventTime)
	{
		super(source);
		this.eventTime = eventTime;
	}
	
	public Date getEventTime()
	{
		return eventTime;
	}
	//subitie koeto pazi tochna data v koqto se e sluchilo
}
