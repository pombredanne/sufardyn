#pragma once

#include "InverseTreap.cpp"

class InverseIntTreap
{
private:
	InverseTreap treap;
	ParentTreapNode* dict[MAX_TREAP_NODES];

public: 
	InverseIntTreap()
	{
	}

	void Move(int source, int dest)
	{
		int value = treap.get_elem(source);
		treap.DeleteNode(source);
		BaseTreapNode* node = treap.insert_node(dest, value);
		dict[value] = (ParentTreapNode *) node;
	}

	void Insert(int idx, int value)
	{
		BaseTreapNode* node = treap.insert_node(idx, value);
		dict[value] = (ParentTreapNode*) node;
	}

	int get_elem(int idx)
	{
		return treap.get_elem(idx);
	}

	void set_elem(int idx, int value)
	{
		DeleteElem(idx);
		Insert(idx, value);
	}

	void DeleteElem(int idx)
	{
		dict[idx] = NULL;
		treap.DeleteNode(idx);
	}

	int GetInverse(int value)
	{
		return treap.GetInverse(dict[value]);
	}
};
