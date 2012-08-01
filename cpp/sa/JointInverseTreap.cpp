#include "JointInverseTreap.h"
#include "stddef.h"
#include "AbstractMonoid.h"

JointInverseTreap::JointInverseTreap()
{
    treapTree = NULL;
    count = 0;
}

JointTreapNode* JointInverseTreap::root()
{
    return treapTree;
}

int JointInverseTreap::get_tree_left_count(JointTreapNode* node)
{
    return (node->get_left() != NULL) ? node->get_left()->get_count() : 0;
}

void JointInverseTreap::insert(int idx, int value, int lcpValue)
{
	JointTreapNode* node = insert_node_internal(idx, value, lcpValue);
	dict[value] = node;
}

JointTreapNode* JointInverseTreap::insert_node_internal(int idx, int value, int lcpValue)
{
    JointTreapNode* node = new JointTreapNode(value, lcpValue);
    treapTree = insertNode(idx, node, treapTree);
    treapTree->set_parent(NULL);
    count++;
    return node;
}

int JointInverseTreap::get_count()
{
    return count;
}

int JointInverseTreap::get_elem(int idx)
{
    JointTreapNode* treeNode = treapTree;
    
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

int JointInverseTreap::get_lcp(int idx)
{
    JointTreapNode* treeNode = treapTree;
    
    while (treeNode != NULL)
    {
        int leftTreeCount = get_tree_left_count(treeNode);
        
        if (idx == leftTreeCount)
        {
            return treeNode->get_lcp();
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


void JointInverseTreap::set_elem(int idx, int value)
{
    set_node_value(treapTree, idx, value);
}

void JointInverseTreap::set_lcp(int idx, int lcpValue)
{
    set_lcp_value(treapTree, idx, lcpValue);
}

void JointInverseTreap::deleteNode(int idx)
{
    treapTree = deleteNode(idx, treapTree);
    count--;
	dict.erase(idx);
}

int JointInverseTreap::Aggregate(int l, int r)
{
    return Aggregate(root(), l, r);
}

int JointInverseTreap::GetInverse(int value)
{
	return this->GetInverse(dict[value]);
}

int JointInverseTreap::GetInverse(JointTreapNode* node)
{
    JointTreapNode* input = node;		
    
    int res = this->get_tree_left_count(input);
    
    while (input->get_parent() != NULL)
    {
        JointTreapNode* parent = input->get_parent();
        
        if ((parent->get_right()) == input)
        {
            res += this->get_tree_left_count(parent) + 1;
        }
        
        input = parent;
    }
    
    return res;
}

void JointInverseTreap::Move(int source, int dest)
{
    int value = get_elem(source);
    int lcp = get_lcp(source);
    deleteNode(source);
    JointTreapNode* node = insert_node_internal(dest, value, lcp);
    dict[value] = node;
}


JointTreapNode* JointInverseTreap::deleteNode(int idx, JointTreapNode* node)
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

JointTreapNode* JointInverseTreap::insertNode(int idx, JointTreapNode* node, JointTreapNode* tree)
{
    if (tree == NULL)
    {
        return node;
    }
    
    int leftTreeCount = get_tree_left_count(tree);
    if (idx - leftTreeCount <= 0)
    {
        tree->set_left(insertNode(idx, node, tree->get_left()));
        JointTreapNode* left = tree->get_left();
        if (left != NULL && (left->get_priority() < tree->get_priority()))
        {
            tree = tree->rotateRight();
        }
    }
    else
    {
        tree->set_right(insertNode(idx - leftTreeCount - 1, node, tree->get_right()));
        JointTreapNode* right = tree->get_right();
        if (right != NULL && (right->get_priority() < tree->get_priority()))
        {
            tree = tree->rotateLeft();
        }
    } 
    
    return tree;
}

void JointInverseTreap::set_node_value(JointTreapNode* node, int idx, int value)
{
    int left_tree_count = get_tree_left_count(node);
    if (idx == left_tree_count)
    {
        node->set_value(value);
        return; 
    }
    if (idx < left_tree_count)
    {
        set_node_value(node->get_left(), idx, value);
        return ;
    }
    set_node_value(node->get_right(), idx - (left_tree_count + 1), value);
}


void JointInverseTreap::set_lcp_value(JointTreapNode* node, int idx, int lcpValue)
{
    int left_tree_count = get_tree_left_count(node);
    if (idx == left_tree_count)
    {
        node->set_lcp_value(lcpValue);
        node->updateAggregate();
        return; 
    }
    if (idx < left_tree_count)
    {
        set_lcp_value(node->get_left(), idx, lcpValue);
        node->updateAggregate();
        return ;
    }
    set_lcp_value(node->get_right(), idx - (left_tree_count + 1), lcpValue);
    node->updateAggregate();
}

int JointInverseTreap::Aggregate(JointTreapNode* node, int l, int r)
{
    int left_count = this->get_tree_left_count(node);
    
    if (l == r && l == left_count)
    {
        return node->get_lcp();
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
    
    return AggregateFromNode(node, l, r);
}

int JointInverseTreap::AggregateFromNode(JointTreapNode* node, int l, int r)
{
    int res = node->get_value();
    
    JointTreapNode* lNode = node->get_left();
    while (lNode != NULL)
    {
        int treeLeftCount = this->get_tree_left_count(lNode);
        
        if (l < treeLeftCount)
        {
            if (lNode->get_right() != NULL)
            {
                res = operation(res, lNode->get_right()->get_aggregate());
            }
            res = operation(res, lNode->get_value());
            lNode = lNode->get_left();
        }
        else if (l == treeLeftCount)
        {
            if (lNode->get_right() != NULL)
            {
                res = operation(res, lNode->get_right()->get_aggregate());
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
    
    JointTreapNode* rNode = node->get_right();
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
                res = operation(res, rNode->get_left()->get_aggregate());
            }
            res = operation(res, rNode->get_value());
            break;
        }
        else if (r > treeLeftCount)
        {
            if (rNode->get_left() != NULL)
            {
                res = operation(res, rNode->get_left()->get_aggregate());
            }
            
            res = operation(res, rNode->get_value());
            
            rNode = rNode->get_right();
            r -= treeLeftCount + 1;
        }
    }
    
    return res;
}

JointTreapNode* JointInverseTreap::get_node(int idx)
{
    return dict[idx];
}

JointInverseTreap::~JointInverseTreap()
{
    if (treapTree != NULL)
    {
        delete treapTree;
    }
}











