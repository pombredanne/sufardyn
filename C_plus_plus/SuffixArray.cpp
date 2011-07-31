#include "InverseIntTreap.cpp"
#include "AggregateTreap.cpp"
#include "BinarySearch.cpp"
#include <string>

#include "SuffixArrayBinarySearchFirstStrategy.cpp"
#include "SuffixArrayBinarySearchLastStrategy.cpp"

using namespace std;


/*
SuffixArray::SuffixArray()
	{
		lcp = new AggregateTreap();
		arr = new InverseIntTreap();
	}
*/

int SuffixArray::SearchFirstIndex(string str)
    {
		return BinarySearch::BinarySearchFirst(0, len - 1, new SuffixArrayBinarySearchFirstStrategy(this, str));
	}

int SuffixArray::SearchLastIndex(string str)
    {
		return BinarySearch::BinarySearchFirst(0, len - 1, new SuffixArrayBinarySearchLastStrategy(this, str));
	}

int SuffixArray::SearchFirst(string str)
    {
		int index = SearchFirstIndex(str);
		
		int elem = arr->get_elem(index);
		string substr = str.substr(elem, min((int)str.length(), this->len - elem));

		if (substr.compare(str) == 0)
		{
			return index;
		}

		return -1;
	}
