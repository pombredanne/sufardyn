#pragma once
#include "BaseTreapNode.h"

class TreapNode : public BaseTreapNode
{
 private:
  int aggregate;

 public:
  TreapNode(int value);
  int get_aggregate();
  virtual void updateAggregate();
  virtual BaseTreapNode* rotateLeft();
  virtual BaseTreapNode* rotateRight();
  
 protected:
  virtual void update();
};
