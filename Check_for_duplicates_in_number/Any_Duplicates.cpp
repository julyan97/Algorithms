/******************************************************************************

Welcome to GDB Online.
GDB online is an online compiler and debugger tool for C, C++, Python, Java, PHP, Ruby, Perl,
C#, VB, Swift, Pascal, Fortran, Haskell, Objective-C, Assembly, HTML, CSS, JS, SQLite, Prolog.
Code, Compile, Run and Debug online from anywhere in world.

*******************************************************************************/
#include <stdio.h>
#include <string.h>
#include <iostream>

using namespace std;

int count(int number)
{
    
    int temp = number;
    int counter = 0;
    
    while(temp != 0)
    {
        temp = temp/10;
        counter++;
    }
    return counter;
    
}

bool any_reapet_numbers(int size,int number)
{
    int* array = new int[size];
    int temp = number;
    int index =  0;
    while(temp != 0)
    {
        int right = temp % 10;
        array[index] = right;
        temp = temp/10;
        index++;
    }
    for (int i = 0; i < size ; i++) {
        for (int j = i+1; j < size ; j++) {
            if(array[i] == array[j])
            return true;
        }
    }
    return false;
}

int main()
{
    int number;
    cin >> number;
    cout << any_reapet_numbers(count(number),number);
}
