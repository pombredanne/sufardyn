#pragma once
#include <stack>
#include "Treap.h"
#include "ParentTreapNode.h"

using namespace std;

class InverseTreap : public Treap
{
 protected:
  virtual BaseTreapNode* create_node(int data);
  
 public:
  int GetInverse(ParentTreapNode* node);
 
 protected:
  BaseTreapNode* deleteNode(int idx, BaseTreapNode* tNode);
};
