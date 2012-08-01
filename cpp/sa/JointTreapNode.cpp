#include <stddef.h>
#include "stdlib.h"
#include "JointTreapNode.h"
#include "AbstractMonoid.h"

JointTreapNode::JointTreapNode(int value, int lcpValue)
{
    this->value = value;
    this->lcpValue = lcpValue;
    left = NULL;
    right = NULL;
    parent = NULL;
    count = 1;
    aggregate = lcpValue;
    priority = rand();
}

JointTreapNode* JointTreapNode::get_left()
{
    return left;
}

JointTreapNode* JointTreapNode::get_right()
{
    return right;
}

JointTreapNode* JointTreapNode::get_parent()
{
    return parent;
}

void JointTreapNode::set_left(JointTreapNode* node)
{
    left = node;
    if (node != NULL)
    {
        node->set_parent(this);
    }
    update();
}

void JointTreapNode::set_right(JointTreapNode* node)
{
    right = node;
    if (node != NULL)
    {
        node->set_parent(this);    
    }
    update();
}

void JointTreapNode::set_parent(JointTreapNode *node)
{
    parent = node;
}

int JointTreapNode::get_value()
{
    return value;
}

void JointTreapNode::set_value(int value)
{
    this->value = value;
}

void JointTreapNode::set_lcp_value(int lcpValue)
{
    this->lcpValue = lcpValue;
    updateAggregate();
}

int JointTreapNode::get_lcp()
{
    return lcpValue;
}

int JointTreapNode::get_count()
{
    return count;
}

int JointTreapNode::get_priority()
{
    return priority;
}

JointTreapNode* JointTreapNode::rotateLeft()
{
    JointTreapNode* temp  = right;
    set_right(right->left);
    temp->set_left(this);
    temp->updateAggregate();
    return temp;
}

JointTreapNode* JointTreapNode::rotateRight()
{
    JointTreapNode* temp  = left;
    set_left(left->right);
    temp->set_right(this);
    temp->updateAggregate();
    return temp;
}

JointTreapNode* JointTreapNode::deleteRoot()
{
    JointTreapNode* temp;
    
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

void JointTreapNode::updateAggregate()
{
    aggregate = this->get_lcp();
    if (this->get_left() != NULL)
    {
        aggregate = operation(aggregate, (this->get_left())->get_aggregate());
    }
    if (this->get_right() != NULL)
    {
        aggregate = operation(aggregate, (this->get_right())->get_aggregate());
    }
}

int JointTreapNode::get_aggregate()
{
    return aggregate;
}

void JointTreapNode::update()
{
    update_count();
    updateAggregate();
}

void JointTreapNode::update_count()
{
    count = (left != NULL ? left->count : 0) + 1 + (right != NULL ? right->count : 0);
}

JointTreapNode::~JointTreapNode()
{
    if (left != NULL)
    {
        delete left;
    }
    
    if (right != NULL)
    {
        delete right;
    }
}























