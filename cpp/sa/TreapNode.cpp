#include <stddef.h>
#include "BaseTreapNode.h"
#include "AbstractMonoid.h"
#include "TreapNode.h"

TreapNode::TreapNode(int value) : BaseTreapNode(value)
{
  aggregate = value;
}

int TreapNode::get_aggregate()
{
  return aggregate;
}

void TreapNode::updateAggregate()
{
  aggregate = this->get_value();
  if (this->get_left() != NULL)
    {
      aggregate = operation(aggregate, ((TreapNode*)this->get_left())->get_aggregate());
    }
  if (this->get_right() != NULL)
    {
      aggregate = operation(aggregate, ((TreapNode*)this->get_right())->get_aggregate());
    }
}

BaseTreapNode* TreapNode::rotateLeft()
{
  BaseTreapNode* node = BaseTreapNode::rotateLeft();
  if (node != NULL)
    {
      node->updateAggregate();
    }
  
  return node;
}

BaseTreapNode* TreapNode::rotateRight()
{
  BaseTreapNode* node = BaseTreapNode::rotateRight();
  if (node != NULL)
    {
      node->updateAggregate();
    }
  
  return node;
}

void TreapNode::update()
{
  BaseTreapNode::update();
  updateAggregate();
}
