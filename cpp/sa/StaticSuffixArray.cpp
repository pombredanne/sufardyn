#include <algorithm>
#include "StaticSuffixArray.h"
#include <iostream>
#include <cstring>
#include <memory.h>
#include "SimpleString.h"

using namespace std;

StaticSuffixArray::StaticSuffixArray()
{
    len = 0;
}

void StaticSuffixArray::AddString(const char* s)
{
    if (len > 0)
    {
        throw exception();
    }
    
    str = new SimpleString(s);
    
    len = strlen(s);
    sa = new int[len];
    lcp = new int[len];
    pos = new int[len];
    BuildArray();
    BuildLcp();
}

StaticSuffixArrayComparer::StaticSuffixArrayComparer(StaticSuffixArray* array)
{
    this->array = array;
}

bool StaticSuffixArrayComparer::operator()(int i, int j)
{
    if (array->pos[i] != array->pos[j])
    {
        return array->pos[i] < array->pos[j];
    }
    i += array->gap;
    j += array->gap;
    return (i < array->len && j < array->len) ? array->pos[i] < array->pos[j] : i > j;
}

void StaticSuffixArray::BuildArray()
{
    int* tmp = new int[len];
    
    memset(tmp, 0, len * sizeof(int));
    
    for (int i = 0; i < len; i++)
    {
        sa[i] = i;
        pos[i] = str->getElem(i);
    }
    
    StaticSuffixArrayComparer comparer(this);
    
    for (gap = 1;; gap *= 2)
    {
        sort(sa, sa + len, comparer);
        for(int i = 0; i < len - 1; i++)
        { 
            tmp[i + 1] = tmp[i] + comparer(sa[i], sa[i + 1]);
        }
        
        for (int i = 0; i < len; i++)
        {
            pos[sa[i]] = tmp[i];
        }
        
        if (tmp[len - 1] == len - 1) 
        {
            break;
        }
    }

	delete[] tmp;
}

int StaticSuffixArray::Length()
{
    return len;
}

int StaticSuffixArray::GetElem(int i)
{
    return sa[i];
}

int StaticSuffixArray::GetLcp(int i)
{
    return lcp[i];
}

void StaticSuffixArray::BuildLcp()
{
    int k = 0;
    for (int i = 0; i < len; ++i) 
    {
        if (pos[i] != len - 1)
        {
            for (int j = sa[pos[i] + 1]; str->getElem(i + k) == str->getElem(j + k);)
            {
                ++k;
            }
            
            lcp[pos[i]] = k;
            if (k)
            {
                --k;
            }
        }
    }

	lcp[len - 1] = 0;
} 

String& StaticSuffixArray::GetString()
{
    return *(str);
}

StaticSuffixArray::~StaticSuffixArray()
{
    delete str;
    delete[] sa;
    delete[] lcp;
    delete[] pos;
}

ostream& operator<<(ostream &out, StaticSuffixArray &array)
{
    for (int i = 0; i < array.Length(); i++)
    {
        int value = array.GetElem(i);
        out << value << " ";
    }
    out << endl;
    
    
    for (int i = 0; i < array.Length() - 1; i++)
    {
        int value = array.GetLcp(i);
        out << value << " ";
    }
    
    out << "-" << endl;
    
    return out;
}
