#include <stddef.h>
#include "Treap.h"
#include <stdio.h>

Treap::Treap()
{
  count = 0;
  treapTree = NULL;
}

int Treap::get_tree_left_count(BaseTreapNode* node)
{
  return (node->get_left() != NULL) ? node->get_left()->get_count() : 0;
}

BaseTreapNode* Treap::root()
{
  return treapTree;
}

void Treap::insert(int idx, int data)
{
  insert_node(idx, data);
}

BaseTreapNode* Treap::insert_node(int idx, int data)
{
  BaseTreapNode* node = create_node(data);
  treapTree = insert_node(idx, node, treapTree);
  count++;
  return node;
}

int Treap::get_count()
{
  return  count;
}

int Treap::get_elem(int idx)
{
  BaseTreapNode* treeNode = treapTree;
  
  while (treeNode != NULL)
    {
      int leftTreeCount = get_tree_left_count(treeNode);
      
      if (idx == leftTreeCount)
	{
	  return treeNode->get_value();
	}
      if (idx < leftTreeCount)
	{
	  treeNode = treeNode->get_left();
	}
      else
	{
	  idx -= leftTreeCount + 1;
	  treeNode = treeNode->get_right();
	}
    }
  throw ("Treap key was not found");
}

void Treap::set_elem(int idx, int value)
{
  set_node(treapTree, idx, value);
}

void Treap::DeleteNode(int idx)
{
  treapTree = deleteNode(idx, treapTree);
  count--;
}

BaseTreapNode* Treap::create_node(int data)
{
  return new BaseTreapNode(data);
}

BaseTreapNode* Treap::deleteNode(int idx, BaseTreapNode* node)
{
  int leftTreeCount = this->get_tree_left_count(node);
  
  if (idx < leftTreeCount)
    {
      node->set_left(deleteNode(idx, node->get_left()));
      node->updateAggregate();
      return node;
    }
  if (idx > leftTreeCount)
    {
      node->set_right(deleteNode(idx - leftTreeCount - 1, node->get_right()));
      node->updateAggregate();
      return node;
    }
  
  node = node->deleteRoot();
  
  return node;
}

BaseTreapNode* Treap::insert_node(int idx, BaseTreapNode* node, BaseTreapNode* tree)
{
  if (tree == NULL)
    {
      return node;
    }
  
  int leftTreeCount = get_tree_left_count(tree);
  if (idx - leftTreeCount <= 0)
    {
      tree->set_left(insert_node(idx, node, tree->get_left()));
      if (tree->get_left()->get_priority() < tree->get_priority())
	{
	  tree = tree->rotateRight();
	}
    }
  else
    {
      tree->set_right(insert_node(idx - leftTreeCount - 1, node, tree->get_right()));
      if(tree->get_right()->get_priority() < tree->get_priority())
        {
	  tree = tree->rotateLeft();
        }
    } 
  
  return tree;
}

void Treap::set_node(BaseTreapNode* node, int idx, int value)
{
  int left_tree_count = get_tree_left_count(node);
  if (idx == left_tree_count)
    {
      node->set_value(value);
      node->updateAggregate();
      return; 
    }
  if (idx < left_tree_count)
    {
      set_node(node->get_left(), idx, value);
      node->updateAggregate();
      return ;
    }
  set_node(node->get_right(), idx - (left_tree_count + 1), value);
  node->updateAggregate();
}

Treap::~Treap()
{
  if (treapTree != NULL)
    {
      delete treapTree;
    }
}
