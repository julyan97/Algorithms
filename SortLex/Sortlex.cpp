#include <stdio.h>
#include<iostream>
#include<string.h>
#include <ctype.h>

using namespace std;

bool is_bigger(char* first, char* second)
{
    int size1 = strlen(first);
    int size2 = strlen(second); 
    int min = size1 < size2 ? size1 : size2;
           
    for (int i = 0; i < min; i++) {
        if(first[i] > second[i])return true;
    }
    return false;
}

bool SelectionSortLex(char**  list,int size)
{
    for (int i = 0; i < size - 1; i++) {
       for (int j = i+1; j < size; j++) {
          if(is_bigger(list[i],list[j]))
          {
            char* temp = list[i]; 
            list[i] = list[j]; 
            list[j] = temp;    
          }
       }
    }
}

bool BubbleSortLex(char**  list,int size)
{
    for (int i = 0; i < size - 1; i++) {
       for (int j = 0; j < size - i - 1; j++) {
          if(is_bigger(list[j],list[j + 1]))
          {
            char* temp = list[j]; 
            list[j] = list[j + 1]; 
            list[j + 1] = temp;    
          }
       }
    }
}

int CastedString(const char* str)
{
    int result = 0;
    int size = strlen(str);
    for (int i = 0; i < size; i++) {
        result += (int)str[i];
    }
    return result;
}

int main()
{
    char* list[4] ={strdup("abd"),strdup("z"),strdup("jkl"),strdup("abc")};
    SelectionSortLex(list,4);
    for (int i = 0; i < 4; i++) {
        cout<<list[i]<<endl;
    }
    return 0;
}
