#include "StringStream.h"
#include <string.h>

char StringStream::getElem(int idx)
{
    return a[idx];
}

StringStream::StringStream() : InvertedStringStream()
{
}

StringStream::StringStream(String& str) : InvertedStringStream(str)
{
}

StringStream::StringStream(char* s)
{
    this->a = s;
    this->length = strlen(s);
}

StringStream::~StringStream()
{
}