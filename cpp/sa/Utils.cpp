#include "Utils.h"

char* Utils::GenerateRandomString(int length)
{
    char* res = new char[length + 2];
    
    for (int i = 0; i < length; i++)
    {
        res[i] = 'A' + rand() % 26;
    }
    
    res[length] = '$';
    res[length + 1] = 0;
    
    return res;
}