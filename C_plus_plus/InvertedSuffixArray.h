#include "InverseIntTreap.cpp"
#include "StringStream.cpp"
#pragma once

class InvertedSuffixArray
{
public:
	InverseIntTreap* array;
	void AddChar(char ch);
	void AddString(char *p);
	StringStream* str;
	InvertedSuffixArray();
	void print();
};
