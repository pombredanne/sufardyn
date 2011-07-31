#pragma once

#include"Treap.cpp"
#include"ParentTreapNode.cpp"


class InverseTreap : public Treap
{
protected:
    ParentTreapNode _nodes[MAX_TREAP_NODES];
    int _node_cnt;
    
	virtual BaseTreapNode* create_node(int data)
	{
        _nodes[_node_cnt] = ParentTreapNode(data);
		return &_nodes[_node_cnt++];
	}

public:

    InverseTreap() {
        _node_cnt = 0;
    }
    
	int GetInverse(ParentTreapNode* node)
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

protected:
	BaseTreapNode* deleteNode(int idx, BaseTreapNode* tNode)
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



};
