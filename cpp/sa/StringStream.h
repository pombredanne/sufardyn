#pragma once

#include "InvertedStringStream.h"

class StringStream : public InvertedStringStream
{
public:
    StringStream();
    StringStream(String& str);
    StringStream(char* s);
    char getElem(int idx);
    ~StringStream();
};