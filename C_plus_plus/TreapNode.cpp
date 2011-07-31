#pragma once

#include "BaseTreapNode.cpp"
#include "AbstractMonoid.cpp"

class TreapNode : public BaseTreapNode
{
private:
	int aggregate;

public:

	TreapNode(int value) : BaseTreapNode(value)
	{
		aggregate = value;
	}

	int get_aggregate()
	{
		return aggregate;
	}

	virtual void updateAggregate()
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

	virtual BaseTreapNode* rotateLeft()
	{
		BaseTreapNode* node = BaseTreapNode::rotateLeft();
		if (node != NULL)
		{
			node->updateAggregate();
		}

		return node;
	}

	virtual BaseTreapNode* rotateRight()
	{
		BaseTreapNode* node = BaseTreapNode::rotateRight();
		if (node != NULL)
		{
			node->updateAggregate();
		}

		return node;
	}

protected:
	virtual void update()
	{
		BaseTreapNode::update();
		updateAggregate();
	}
};