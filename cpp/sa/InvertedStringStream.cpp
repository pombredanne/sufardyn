#include "InvertedStringStream.h"

using namespace std;

void InvertedStringStream::init()
{
    a = new char[1048576];
    length = 0;    
}

InvertedStringStream::InvertedStringStream() : String()
{
    init();
}

InvertedStringStream::InvertedStringStream(String& str)
{
    init();
    for (int i = 0; i < str.Length(); i++)
    {
        AppendChar(str.getElem(i));
    }
}

int InvertedStringStream::Length()
{
    return length;
} 

void InvertedStringStream::AppendChar(char ch)
{
    a[length] = ch;
    a[length + 1] = 0;
    length++;
}

char InvertedStringStream::getElem(int idx)
{
    return a[length - idx - 1];
}

InvertedStringStream::~InvertedStringStream()
{
    delete[] a;
}

