#pragma once
#include "Treap.h"
#include "TreapNode.h"

class AggregateTreap : public Treap
{
 public:
  AggregateTreap();
  int Aggregate(int l, int r);

 protected:
  virtual BaseTreapNode* create_node(int data);

 private:
  int Aggregate(BaseTreapNode* node, int l, int r);
  int AggregateFromNode(TreapNode* node, int l, int r);	
};
