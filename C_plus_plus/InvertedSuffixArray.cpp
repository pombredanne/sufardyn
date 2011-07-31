#include "InvertedSuffixArray.h"
#include "BinarySearch.cpp"
#include "InvertedSuffixArrayInsertSearchStrategy.cpp"

#include <stdio.h>

#pragma once

InvertedSuffixArray::InvertedSuffixArray()
{
	array = new InverseIntTreap();
	str = new StringStream();
}


void InvertedSuffixArray::AddChar(char ch)
{
	if (str->Length == 0)
	{
		array->Insert(0, 0);
		str->AppendChar(ch);

		return;
	}

	int pos = array->GetInverse(str->Length - 1);

	InvertedSuffixArrayInsertSearchStrategy strategy(this, pos, ch);

	int first = BinarySearch::BinarySearchFirst(0, str->Length, &strategy);

	array->Insert(first, str->Length);
	str->AppendChar(ch);
}



void InvertedSuffixArray::AddString(char* p)
{
	char *c = p;
	while (*c)
	{
		c++;
	}

	while (c != p)
	{
		c--;
		AddChar(*c);
	}

	AddChar(*p);
}

void InvertedSuffixArray::print()
{
	for (int i = 0; i < str->Length; i++)
	{
		printf("%d ", array->get_elem(i));
	}
	printf("\n");
}