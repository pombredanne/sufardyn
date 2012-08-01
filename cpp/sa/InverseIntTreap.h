#pragma once
#include "InverseTreap.h"
#include <map>

using namespace std;

class InverseIntTreap
{
private:
  InverseTreap* treap;
  map <int, ParentTreapNode*> dict;
  
public: 
  InverseIntTreap();
  void Move(int source, int dest);
  void Insert(int idx, int value);
  int get_elem(int idx);
  void set_elem(int idx, int value);
  void DeleteElem(int idx);
  int GetInverse(int value);
  ~InverseIntTreap();
};
