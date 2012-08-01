#include "String.h"

String::String()
{
}

ostream& operator<<(ostream& out, String& stream)
{
    for (int i = 0; i < stream.Length(); i++)
    {
        out << stream.getElem(i);
    }
    
    out << endl;
    
    return out;
}