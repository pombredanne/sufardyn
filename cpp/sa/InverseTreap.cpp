#include <stack>
#include"Treap.h"
#include"ParentTreapNode.h"
#include "InverseTreap.h"

using namespace std;

BaseTreapNode* InverseTreap::create_node(int data)
{
  return new ParentTreapNode(data);
}

int InverseTreap::GetInverse(ParentTreapNode* node)
{
  ParentTreapNode* input = node;		
  
  int res = this->get_tree_left_count(input);
  
  while (input->get_parent() != NULL)
    {
      ParentTreapNode* parent = input->get_parent();
      
      if ((parent->get_right()) == input)
	{
	  res += this->get_tree_left_count(parent) + 1;
	}
      
      input = parent;
    }
  
  return res;
}

BaseTreapNode* InverseTreap::deleteNode(int idx, BaseTreapNode* tNode)
{
  int leftTreeCount = this->get_tree_left_count(tNode);
  
  if (idx < leftTreeCount)
    {
      tNode->set_left(deleteNode(idx, tNode->get_left()));
      tNode->updateAggregate();
      return tNode;
    }
  if (idx > leftTreeCount)
    {
      tNode->set_right(deleteNode(idx - leftTreeCount - 1, tNode->get_right()));
      tNode->updateAggregate();
      return tNode;
    }
  
  ParentTreapNode* node = (ParentTreapNode *)tNode;  
  ParentTreapNode* parent = node->get_parent();
  
  tNode = tNode->deleteRoot();
  if (tNode != NULL)
    {
      ((ParentTreapNode *)tNode)->set_parent(parent);
    }
  
  return tNode;
}
