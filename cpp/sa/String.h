#pragma once

#include <iostream>

using namespace std;

class String
{
public:
    String();
    String(String& str);
    virtual int Length() = 0;
    virtual char getElem(int idx) = 0;
    friend ostream& operator<<(ostream& out, String& stream);
};