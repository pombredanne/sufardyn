#include "InvertedSuffixArray.h"
#include <iostream>
#include "JointInverseTreap.h"
#include "AggregateTreap.h"
#include <map>
#include "StaticSuffixArray.h"
#include "ImplicitSuffixArray.h"

using namespace std;

int main()
{
    const char* p = "mississippi$";
    

    StaticSuffixArray sas;
    sas.AddString(p);
    
    ImplicitSuffixArray isa;
    
    isa.AddString(p);
    
    cout << isa;
    cout << sas.Equals(isa) << endl;
    
    
    return 0;
}
