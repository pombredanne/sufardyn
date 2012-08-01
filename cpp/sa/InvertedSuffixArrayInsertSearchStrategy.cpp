#include "AbstractBinarySearchStrategy.h"
#include "InvertedSuffixArray.h"
#include "InvertedSuffixArrayInsertSearchStrategy.h"

InvertedSuffixArrayInsertSearchStrategy::InvertedSuffixArrayInsertSearchStrategy(InvertedSuffixArray* array, int posLongestSuffix, char ch)
{
  this->array = array;
  this->posLongestSuffix = posLongestSuffix;
  this->ch = ch;
}

bool InvertedSuffixArrayInsertSearchStrategy::Condition(int pos)
{
  int idx = array->array->get_elem(pos);
  
  char fstCh = array->str->getElem(array->str->Length() - 1 - idx);
  if (fstCh == ch)
    {
      if (idx == 0)
	{
	  return 1;
	}
      
      int inverse = array->array->GetInverse(idx - 1);
      
      return posLongestSuffix > inverse;
    }
  
  return fstCh < ch;
}
