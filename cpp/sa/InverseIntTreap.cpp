#include "InverseTreap.h"
#include "InverseIntTreap.h"
#include <map>

using namespace std;
InverseIntTreap::InverseIntTreap()
{
  treap = new InverseTreap();
}

void InverseIntTreap::Move(int source, int dest)
{
  int value = treap->get_elem(source);
  treap->DeleteNode(source);
  BaseTreapNode* node = treap->insert_node(dest, value);
  dict[value] = (ParentTreapNode *) node;
}

void InverseIntTreap::Insert(int idx, int value)
{
  BaseTreapNode* node = treap->insert_node(idx, value);
  dict[value] = (ParentTreapNode*) node;
}

int InverseIntTreap::get_elem(int idx)
{
  return treap->get_elem(idx);
}

void InverseIntTreap::set_elem(int idx, int value)
{
  DeleteElem(idx);
  Insert(idx, value);
}

void InverseIntTreap::DeleteElem(int idx)
{
  dict.erase(idx);
  treap->DeleteNode(idx);
}

int InverseIntTreap::GetInverse(int value)
{
  return treap->GetInverse(dict[value]);
}

InverseIntTreap::~InverseIntTreap()
{
  delete treap;
}
