package edu.uni.matrixmultiplication;

import java.util.EventListener;

public interface MatrixThreadEventListener extends EventListener
{
	public void threadStarted(MatrixThreadEvent e);
	public void threadFinished(MatrixThreadEvent e);
	public void allThreadsFinished(MatrixThreadEvent e);
}
//interface koyto dava info za tova kakvi sabitiq mogat da se sluchat na edna matrica
