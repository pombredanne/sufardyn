#pragma once
#include <cstddef>
#include "String.h"

class SuffixArray
{
public:
    //  int SearchFirstIndex(string str);
    //int SearchLastIndex(string str);
    //int SearchFirst(string str);
    virtual int GetElem(int i) = 0;
    virtual int GetLcp(int i) = 0;
    virtual int Length() = 0;
    virtual String& GetString() = 0;
    bool Equals(SuffixArray& other);
};
