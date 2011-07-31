#pragma once

#include "BaseTreapNode.cpp"

class ParentTreapNode : public BaseTreapNode
{
private:
	ParentTreapNode* parent;

public:
    ParentTreapNode() {
    }
    
	ParentTreapNode(int value) : BaseTreapNode(value)
	{
		parent = NULL;
	}

	void set_left(BaseTreapNode* node)
	{
		BaseTreapNode::set_left(node);	

		if (node != NULL)
		{
			((ParentTreapNode *) node)->set_parent(this);
		}
	}

	void set_right(BaseTreapNode* node)
	{
		BaseTreapNode::set_right(node);	

		if (node != NULL)
		{
			((ParentTreapNode *) node)->set_parent(this);
		}
	}

	void set_parent(ParentTreapNode* node)
	{
		parent = node;
	}

	ParentTreapNode* get_parent()
	{
		return parent;
	}

	BaseTreapNode* rotateLeft()
	{
		ParentTreapNode* parent1 = parent;
		BaseTreapNode* node = BaseTreapNode::rotateLeft();
		((ParentTreapNode*) node)->set_parent(parent1);
		return node;
	}

	BaseTreapNode* rotateRight()
	{
		ParentTreapNode* parent1 = parent;
		BaseTreapNode* node = BaseTreapNode::rotateRight();
		((ParentTreapNode*) node)->set_parent(parent1);
		return node;
	}
};
