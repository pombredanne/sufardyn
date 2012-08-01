#include "SuffixArray.h"

bool SuffixArray::Equals(SuffixArray &other)
{
    if (Length() != other.Length())
    {
        return false;
    }
    
    for (int i = 0; i < Length(); i++)
    {
        if (GetElem(i) != other.GetElem(i) || GetLcp(i) != other.GetLcp(i))
        {
            return false;
        }
    }
    
    return true;
}