package edu.uni.matrixmultiplication;

public class FileFormatException extends Exception
{
	public FileFormatException(String message)
	{
		super(message);
	}
}
//hvurlq se kogato formatut na faila koyto chetem e nevaliden
//naprimer ochakvame na purviq red 3 polojitelni celi chisla
//ako tova ne e taka throwvame tozi exception