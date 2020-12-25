package edu.uni.matrixmultiplication;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.util.Random;

public class Matrix
{
	private double[][] matrix;
	private int rows;
	private int columns;
	
	public Matrix()
	{
		matrix = new double[][]{{1, 0}, {0, 1}};//E
		rows = 2;
		columns = 2;
	}
	
	public Matrix(int rows, int columns)
	{
		matrix = new double[rows][columns];
		this.rows = rows;
		this.columns = columns;
	}
	
	public int getNumberOfRows()
	{
		return rows;
	}
	
	public int getNumberOfColumns()
	{
		return columns;
	}
	
	public void generateRandomMatrix(int from, int to)
	{
		Random r = new Random();
		for(int i = 0; i<matrix.length; i++)
		{
			for(int j = 0; j<matrix[i].length; j++)
			{
				matrix[i][j] = from + r.nextInt((to-from+1));
			}
		}
	}
	
	public void generateRandomMatrix()
	{
		generateRandomMatrix(0, 1);
	}
	
	//doesn't return copy of row
	public double[] getRow(int rowIndex)
	{
		return matrix[rowIndex];
	}
	
	//returns copy of column
	public double[] getColumn(int columnIndex)
	{
		double[] column = new double[matrix.length];
		for(int i = 0; i<column.length; i++)
		{
			column[i] = matrix[i][columnIndex];
		}
		return column;
	}
	
	public void setValueAt(double value, int row, int column)
	{
		matrix[row][column] = value;
	}
	
	@Override
	public String toString()
	{
		StringBuilder builder = new StringBuilder();
		for(int i = 0; i<matrix.length; i++)
		{
			for(int j = 0; j<matrix[i].length; j++)
			{
				builder.append(matrix[i][j]);
				builder.append(" ");
			}
			builder.append("\r\n");
		}
		return builder.toString();
	}
	
	public void writeToFile(String fileName) throws FileNotFoundException
	{
		File f = new File(fileName);
		PrintWriter writer = new PrintWriter(f);
		writer.format("%d %d\r\n", getNumberOfRows(), getNumberOfRows());
		writer.append(toString());
		writer.flush();
		writer.close();
	}
}
//opisva matrica s broy redove i koloni, posredstvom metodi dava vuzmojnost tq da se generira
//na random princip i da se zapishe v textov file


