#pragma once

class InvertedSuffixArrayInsertSearchStrategy : public AbstractBinarySearchStrategy
{
 private :
  InvertedSuffixArray* array;
  int posLongestSuffix;
  char ch;
  
public: 	
  InvertedSuffixArrayInsertSearchStrategy(InvertedSuffixArray* array, int posLongestSuffix, char ch);
  bool Condition(int pos);
};
