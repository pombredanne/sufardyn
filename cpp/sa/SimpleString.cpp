#include "SimpleString.h"
#include <string.h>

SimpleString::SimpleString(const char* s)
{
    len = strlen(s);
    this->s = new char[len + 1];
    strncpy(this->s, s, len);
    this->s[len] = 0;
}

SimpleString::SimpleString(String& str)
{
    len = str.Length();
    s = new char[len + 1];
    
    for (int i = 0; i < len; i++)
    {
        s[i] = str.getElem(i);
    }
    
    s[len] = 0;
}

int SimpleString::Length()
{
    return len;
}

char SimpleString::getElem(int idx)
{
    return s[idx];
}

SimpleString::~SimpleString()
{
    delete[] s;
}