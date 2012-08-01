#pragma once

#include "String.h"

class SimpleString : public String
{
private:
    char* s;    
    size_t len;
public:
    SimpleString(const char* s);
    SimpleString(String& str);
    int Length();
    char getElem(int idx);
    ~SimpleString();
};