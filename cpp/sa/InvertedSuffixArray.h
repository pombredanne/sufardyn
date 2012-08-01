#pragma once
#include "InverseIntTreap.h"
#include "InvertedStringStream.h"
#include <iostream>
#include "SuffixArray.h"

using namespace std;

class InvertedSuffixArray : public SuffixArray
{
public:
    InverseIntTreap* array;
    void AddChar(char ch);
    void AddString(const char *p);
    InvertedStringStream* str;
    InvertedSuffixArray();
    ~InvertedSuffixArray();
    int GetElem(int i);
    int GetLcp(int i);
    int Length();
    String& GetString();
    friend ostream& operator<<(ostream &out, InvertedSuffixArray &array);
};
