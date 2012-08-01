#include "Treap.h"
#include "BaseTreapNode.h"
#include "TreapNode.h"
#include "AggregateTreap.h"
#include "AbstractMonoid.h"
#include <stddef.h>

AggregateTreap::AggregateTreap() 
{
}

int AggregateTreap::Aggregate(int l, int r)
{
  return Aggregate(root(), l, r);
}

BaseTreapNode* AggregateTreap::create_node(int data)
{
  return new TreapNode(data);
}

int AggregateTreap::Aggregate(BaseTreapNode* node, int l, int r)
{
  int left_count = this->get_tree_left_count(node);

  if (l == r && l == left_count)
    {
      return node->get_value();
    }

  if (l < left_count && r < left_count)
    {
      return Aggregate(node->get_left(), l, r);
    }

  if (l > left_count && r > left_count)
    {
      int shift = left_count + 1;
      return Aggregate(node->get_right(), l - shift, r - shift);
    }
  
  return AggregateFromNode((TreapNode *)node, l, r);
}

int AggregateTreap::AggregateFromNode(TreapNode* node, int l, int r)
{
  int res = node->get_value();
  
  BaseTreapNode* lNode = node->get_left();
  while (lNode != NULL)
    {
      int treeLeftCount = this->get_tree_left_count(lNode);
      
      if (l < treeLeftCount)
	{
	  if (lNode->get_right() != NULL)
	    {
	      res = operation(res, ((TreapNode *)lNode->get_right())->get_aggregate());
	    }
	  res = operation(res, lNode->get_value());
	  lNode = lNode->get_left();
	}
      else if (l == treeLeftCount)
	{
	  if (lNode->get_right() != NULL)
	    {
	      res = operation(res, ((TreapNode *)lNode->get_right())->get_aggregate());
	    }
	  res = operation(res, lNode->get_value());
	  break;
	}
      else if (l > treeLeftCount)
	{
	  lNode = lNode->get_right();
	  l -= treeLeftCount + 1;
	}
    }
  
  BaseTreapNode* rNode = node->get_right();
  r -= this->get_tree_left_count(node) + 1;
  
  while (r >= 0 && rNode != NULL)
    {
      int treeLeftCount = this->get_tree_left_count(rNode);
      
      if (r < treeLeftCount)
	{
	  rNode = rNode->get_left();
	}
      else if (r == treeLeftCount)
	{
	  if (rNode->get_left() != NULL)
	    {
	      res = operation(res, ((TreapNode *)rNode->get_left())->get_aggregate());
	    }
	  res = operation(res, rNode->get_value());
	  break;
	}
      else if (r > treeLeftCount)
	{
	  if (rNode->get_left() != NULL)
	    {
	      res = operation(res, ((TreapNode *)rNode->get_left())->get_aggregate());
	    }
	  
	  res = operation(res, rNode->get_value());
	  
	  rNode = rNode->get_right();
	  r -= treeLeftCount + 1;
	}
    }
  
  return res;
}
