#pragma once

#include <stdio.h>
#include <stdlib.h>

#define MAX_TREAP_NODES 10000000

class BaseTreapNode
{
private:
	int priority;
	BaseTreapNode* left;
	BaseTreapNode* right;
	int value;
	int count;

public:

    BaseTreapNode() {
    }
    
	BaseTreapNode(int value)
	{
		priority = rand();
		this->value = value;
		left = NULL;
		right = NULL;
		count = 1;
	}
	
	BaseTreapNode* get_left()
	{
		return left;
	}

	virtual void set_left(BaseTreapNode* node)
	{
		left = node;
		update();
	}

	BaseTreapNode* get_right()
	{
		return right;
	}

	virtual void set_right(BaseTreapNode* node)
	{
		right = node;
		update();
	}

	int get_value()
	{
		return value;
	}

	void set_value(int value)
	{
		this->value = value;
	}

	int get_count()
	{
		return count;
	}

	int get_priority()
	{
		return priority;
	}

	virtual BaseTreapNode* rotateLeft()
	{
		BaseTreapNode* temp  = right;
		set_right(right->left);
		temp->set_left(this);
		return temp;
	}
	
	virtual BaseTreapNode* rotateRight()
	{
		BaseTreapNode* temp  = left;
		set_left(left->right);
		temp->set_right(this);
		return temp;
	}


    BaseTreapNode* deleteRoot()
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

	virtual void updateAggregate()
	{
	}

protected:
	virtual void update()
	{
		update_count();
	}


private: 
	void update_count()
	{
		count = (left != NULL ? left->count : 0) + 1 + (right != NULL ? right->count : 0);
	}

};















