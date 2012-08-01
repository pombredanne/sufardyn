#pragma once
#include "SuffixArray.h"
#include "SimpleString.h"

class StaticSuffixArray : public SuffixArray
{
    friend class StaticSuffixArrayComparer;
    
private:
    SimpleString* str;
    size_t len;
    int* sa;
    int* lcp;
    int* pos;
    int gap;
    void BuildArray();
    void BuildLcp();
    
public:
    int Length();
    int GetElem(int i);
    int GetLcp(int i);
    StaticSuffixArray();
    void AddString(const char* s);
    String& GetString();
    ~StaticSuffixArray();
    friend ostream& operator<<(std::ostream &out, StaticSuffixArray &array);
};

class StaticSuffixArrayComparer
{
private:
    StaticSuffixArray* array;
public:
    StaticSuffixArrayComparer(StaticSuffixArray* array);
    bool operator()(int i, int j);
};
