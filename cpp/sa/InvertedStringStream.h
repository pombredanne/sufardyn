#pragma once

#include "String.h"

using namespace std;

class InvertedStringStream : public String
{
private:
    void init();
protected:
    char* a;
    int length;
public:
    InvertedStringStream();
    InvertedStringStream(String& str);
    void AppendChar(char ch);
    int Length();
    virtual char getElem(int idx);
    ~InvertedStringStream();
};

