#include <stddef.h>
#include "BaseTreapNode.h"
#include "ParentTreapNode.h"

ParentTreapNode::ParentTreapNode(int value) : BaseTreapNode(value)
{
  parent = NULL;
}

void ParentTreapNode::set_left(BaseTreapNode* node)
{
  BaseTreapNode::set_left(node);	
  
  if (node != NULL)
    {
      ((ParentTreapNode *) node)->set_parent(this);
    }
}

void ParentTreapNode::set_right(BaseTreapNode* node)
{
  BaseTreapNode::set_right(node);	

  if (node != NULL)
    {
      ((ParentTreapNode *) node)->set_parent(this);
    }
}

void ParentTreapNode::set_parent(ParentTreapNode* node)
{
  parent = node;
}

ParentTreapNode* ParentTreapNode::get_parent()
{
  return parent;
}

BaseTreapNode* ParentTreapNode::rotateLeft()
{
  ParentTreapNode* parent1 = parent;
  BaseTreapNode* node = BaseTreapNode::rotateLeft();
  ((ParentTreapNode*) node)->set_parent(parent1);
  return node;
}

BaseTreapNode* ParentTreapNode::rotateRight()
{
  ParentTreapNode* parent1 = parent;
  BaseTreapNode* node = BaseTreapNode::rotateRight();
  ((ParentTreapNode*) node)->set_parent(parent1);
  return node;
}
