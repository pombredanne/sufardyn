#pragma once
#include "BaseTreapNode.h"

class Treap
{
 private:
  int count;
  BaseTreapNode * treapTree;
  
 public:
  Treap();
  int get_tree_left_count(BaseTreapNode* node);
  BaseTreapNode* root();
  void insert(int idx, int data);
  BaseTreapNode* insert_node(int idx, int data);
  int get_count();
  int get_elem(int idx);
  void set_elem(int idx, int value);
  void DeleteNode(int idx);
  ~Treap();
  
 protected:
  virtual BaseTreapNode* create_node(int data);
  virtual BaseTreapNode* deleteNode(int idx, BaseTreapNode* node);
  
 private:
  
  BaseTreapNode* insert_node(int idx, BaseTreapNode* node, BaseTreapNode* tree);
  void set_node(BaseTreapNode* node, int idx, int value);
};
