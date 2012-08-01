#pragma once
#include "BaseTreapNode.h"

class ParentTreapNode : public BaseTreapNode
{
 private:
  ParentTreapNode* parent;
	
 public:
  ParentTreapNode(int value);
  void set_left(BaseTreapNode* node);
  void set_right(BaseTreapNode* node);
  void set_parent(ParentTreapNode* node);
  ParentTreapNode* get_parent();
  BaseTreapNode* rotateLeft();
  BaseTreapNode* rotateRight();
};
