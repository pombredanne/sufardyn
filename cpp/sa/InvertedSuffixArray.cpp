#include "InvertedSuffixArray.h"
#include "BinarySearch.h"
#include "InvertedSuffixArrayInsertSearchStrategy.h"
#include "SuffixArray.h"
#include <iostream>

using namespace std;

InvertedSuffixArray::InvertedSuffixArray()
{
    array = new InverseIntTreap();
    str = new InvertedStringStream();
}

void InvertedSuffixArray::AddChar(char ch)
{
    if (str->Length() == 0)
    {
        array->Insert(0, 0);
        str->AppendChar(ch);
		
        return;
    }
    
    int pos = array->GetInverse(str->Length() - 1);
    
    InvertedSuffixArrayInsertSearchStrategy strategy = InvertedSuffixArrayInsertSearchStrategy(this, pos, ch);
    
    int first = BinarySearch::BinarySearchFirst(0, str->Length(), &strategy);
    
    array->Insert(first, str->Length());
    str->AppendChar(ch);
}

void InvertedSuffixArray::AddString(const char* p)
{
    const char *c = p;
    while (*c)
    {
        c++;
    }
    
    while (c != p)
    {
        c--;
        AddChar(*c);
    }
}

int InvertedSuffixArray::GetElem(int i)
{
    return Length() - 1 - array->get_elem(i);
}

int InvertedSuffixArray::Length()
{
    return str->Length();
}

int InvertedSuffixArray::GetLcp(int i)
{
    throw exception();
}

String& InvertedSuffixArray::GetString()
{
    return *str;
}

InvertedSuffixArray::~InvertedSuffixArray()
{
    delete array;
    delete str;
}

ostream& operator<<(ostream &out, InvertedSuffixArray &array)
{
    out << *array.str;
    
    for (int i = 0; i < array.str->Length(); i++)
    {
        int value = array.GetElem(i);
        out << value << " ";
    }
    
    return out;
}
