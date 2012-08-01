#pragma once
#include "AbstractBinarySearchStrategy.h"

class BinarySearch
{
 public:
  static int BinarySearchFirst(int left, int right, AbstractBinarySearchStrategy* strategy)
  {
    int l = left;
    int r = right;
    
    while (l < r)
      {
	int m = (l + r) / 2;
	if (strategy->Condition(m))
	  {
	    l = m + 1;
	  }
	else
	  {
	    r = m;
	  }
      }
    
    return l;
  }
  
  static int BinarySearchLast(int left, int right, AbstractBinarySearchStrategy* strategy)
  {
    int l = left;
    int r = right;
    
    while (l < r)
      {
	int m = (l + r + 1) / 2;
	if (strategy->Condition(m))
	  {
	    l = m;
	  }
	else
	  {
	    r = m - 1;
	  }
      }
    
    return l;
  }
};
