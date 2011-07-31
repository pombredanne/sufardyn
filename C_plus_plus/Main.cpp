#include <iostream>
#include "BaseTreapNode.cpp"
#include "AggregateTreap.cpp"
#include "AddMonoid.cpp"
#include "InvertedSuffixArray.h"
#include "InverseIntTreap.cpp"

using namespace std;

int main()
{
	char *p = "mississippi";

	InvertedSuffixArray * array = new InvertedSuffixArray();
	array->AddString(p);
	array->print();


	return 0;
}
