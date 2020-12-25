package edu.uni.matrixmultiplication;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.nio.file.Files;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Random;

public class Test
{
	private static Date startTime = null;
	private static HashMap<String, String> params = new HashMap<>();
	private static Matrix result = null;
	private static MatrixThreadEventListener e = new MatrixThreadEventListener()
	{
		
		@Override
		public void threadStarted(MatrixThreadEvent e)
		{
			if(MatrixThread.quietMode())
				return;
			Date d = e.getEventTime();
			MatrixThread t = (MatrixThread)e.getSource();
			SimpleDateFormat sdf = new SimpleDateFormat("HH:mm:ss:SSS");
			System.out.printf("Thread %d started at %s\n", t.getIndex(), sdf.format(d));
		}
		
		@Override
		public void threadFinished(MatrixThreadEvent e)
		{
			if(MatrixThread.quietMode())
				return;
			Date d = e.getEventTime();
			MatrixThread t = (MatrixThread)e.getSource();
			SimpleDateFormat sdf = new SimpleDateFormat("HH:mm:ss:SSS");
			System.out.printf("Thread %d finished at %s\n", t.getIndex(), sdf.format(d));
		}
		
		@Override
		public void allThreadsFinished(MatrixThreadEvent e)
		{
			System.out.printf("Ends at %s\n", new SimpleDateFormat("HH:mm:ss:SSS").format(e.getEventTime()));
			System.out.printf("All work was finished in %d milliseconds\n", e.getEventTime().getTime() - startTime.getTime());
			if(params.containsKey("-o") && result != null)
			{
				try
				{
					result.writeToFile(params.get("-o"));
				} 
				catch (FileNotFoundException ex)
				{
					// TODO Auto-generated catch block
					ex.printStackTrace();
				}
			}
			if(!params.containsKey("-o") && result != null)
			{
				System.out.println(result);
			}
			System.exit(0);
		}
	};

	public static void main(String[] args)
	{	
		int incrementor = 2;
		for(int i = 0; i<args.length; i+=incrementor)
		{
			if(!args[i].equals("-q"))
			{
				params.put(args[i], args[i+1]);
				incrementor = 2;
			}
			else
			{
				params.put(args[i], "");
				incrementor = 1;
			}
		}
		
		
		params.put("-m", "100");
		params.put("-n", "100");
		params.put("-k", "100");
		params.put("-t", "10");
		params.put("-q", "");
		params.put("-o", "dupe.txt");
		
		
		int m = -1, n = -1, k = -1, t = -1;
		String fileName = null;
		
		if(params.containsKey("-m"))
			m = Integer.parseInt(params.get("-m"));
		if(params.containsKey("-n"))
			n = Integer.parseInt(params.get("-n"));
		if(params.containsKey("-k"))
			k = Integer.parseInt(params.get("-k"));
		if(params.containsKey("-t"))
			t = Integer.parseInt(params.get("-t"));
		if(params.containsKey("-i"))
			fileName = params.get("-i");
		if(params.containsKey("-q"))
			MatrixThread.quietMode(true);
		
		ByRef<Matrix> first = new ByRef<>();
		ByRef<Matrix> second = new ByRef<>();
		
		
		if(m != -1 && n != -1 && k != -1 && fileName == null)
		{
			second.setValue(new Matrix(n, k));
			second.getValue().generateRandomMatrix(0, 10);
			first.setValue(new Matrix(m, n));
			first.getValue().generateRandomMatrix(0, 10);
			/*System.out.println(first.getValue());
			System.out.println();
			System.out.println(second.getValue());*/
			result = new Matrix(m, k);
		}
		else if(fileName != null && m == -1 && n == -1 && k == -1)
		{
			try
			{
				readMatricesFromFile(fileName, first, second);
				/*System.out.println(first.getValue());
				System.out.println();
				System.out.println(second.getValue());*/
			
			} catch (IOException e)
			{
				e.printStackTrace();
			} catch (FileFormatException e)
			{
				e.printStackTrace();
			}
			result = new Matrix(first.getValue().getNumberOfRows(), second.getValue().getNumberOfColumns());
		}
		else
		{
			System.out.print("Wrong parameters passed to application!");
		}
		
		if(t == -1)
			t = 1;
		
		startTime = new Date();
		SimpleDateFormat sdf = new SimpleDateFormat("HH:mm:ss:SSS");
		System.out.printf("Starts at %s\n", sdf.format(startTime));
		
		for(int i = 0; i<t; i++)
		{
			MatrixThread matrixThread = new MatrixThread(first.getValue(), second.getValue(), i, t, result);
			matrixThread.addMatrixThreadListener(e);
			Thread tm = new Thread(matrixThread);
			tm.start();
		}
	}

	private static void readMatricesFromFile(String fileName, ByRef<Matrix> first, ByRef<Matrix> second) throws IOException, FileFormatException
	{
		List<String> rows = Files.readAllLines(new File(fileName).toPath());
		String[] firstRow = rows.get(0).trim().split(" ");
		if(firstRow.length != 3)
		{
			throw new FileFormatException("The dimensions of the matrices must be 3 integer not negative and not zero values.");
		}
		int m = Integer.parseInt(firstRow[0]);
		int n = Integer.parseInt(firstRow[1]);
		int k = Integer.parseInt(firstRow[2]);
		
		first.setValue(new Matrix(m, n));
		second.setValue(new Matrix(n, k));
		
		int column = 0;
		for(int row = 1; row<=m; row++)
		{
			String[] numbers = rows.get(row).trim().split(" ");
			for(String number : numbers)
			{
				first.getValue().setValueAt(Double.parseDouble(number), row-1, column++);
			}
			column = 0;
		}
		
		for(int row = m+1; row<m+n; row++)
		{
			String[] numbers = rows.get(row).trim().split(" ");
			for(String number : numbers)
			{
				second.getValue().setValueAt(Double.parseDouble(number), row-m-1, column++);
			}
			column = 0;
		}
	} 	
}
