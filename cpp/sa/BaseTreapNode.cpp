#include <stdio.h>
#include <stdlib.h>
#include "BaseTreapNode.h"

BaseTreapNode::BaseTreapNode(int value)
{
  priority = rand();
  this->value = value;
  left = NULL;
  right = NULL;
  count = 1;
}

BaseTreapNode* BaseTreapNode::get_left()
{
  return left;
}

void BaseTreapNode::set_left(BaseTreapNode* node)
{
  left = node;
  update();
}

BaseTreapNode* BaseTreapNode::get_right()
{
  return right;
}

void BaseTreapNode::set_right(BaseTreapNode* node)
{
  right = node;
  update();
}

int BaseTreapNode::get_value()
{
  return value;
}

void BaseTreapNode::set_value(int value)
{
  this->value = value;
}

int BaseTreapNode::get_count()
{
  return count;
}

int BaseTreapNode::get_priority()
{
  return priority;
}

BaseTreapNode* BaseTreapNode::rotateLeft()
{
  BaseTreapNode* temp  = right;
  set_right(right->left);
  temp->set_left(this);
  return temp;
}

BaseTreapNode* BaseTreapNode::rotateRight()
{
  BaseTreapNode* temp  = left;
  set_left(left->right);
  temp->set_right(this);
  return temp;
}


BaseTreapNode* BaseTreapNode::deleteRoot()
{
  BaseTreapNode* temp;
  
  if(left == NULL)
    {
      return right;
    }

  if(right == NULL)
    {
      return left;
    }
  
  if(left->priority < right->priority)
    {
      temp        = rotateRight();
      temp->right  = deleteRoot();
    }
  else
    {
      temp        = rotateLeft();
      temp->left   = deleteRoot();
    }
  return temp;
}

void BaseTreapNode::updateAggregate()
{
}

void BaseTreapNode::update()
{
  update_count();
}

void BaseTreapNode::update_count()
{
  count = (left != NULL ? left->count : 0) + 1 + (right != NULL ? right->count : 0);
}

BaseTreapNode::~BaseTreapNode()
{
  if (left != NULL)
    {
      delete left;
    }

  if (right != NULL)
    {
      delete right;
    }
}
