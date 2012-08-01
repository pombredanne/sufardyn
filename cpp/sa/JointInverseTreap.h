#pragma once

#include "JointTreapNode.h"
#include <map>

using namespace std;

class JointInverseTreap 
{
private:
    int count;
    JointTreapNode* treapTree;
    map <int, JointTreapNode*> dict;
    
public:
    JointInverseTreap();
    ~JointInverseTreap();
    
    int get_tree_left_count(JointTreapNode* node);
    JointTreapNode* root();
    void insert(int idx, int value, int lcp);
    JointTreapNode* insert_node_internal(int idx, int value, int lcpValue);
    int get_count();
    int get_elem(int idx);
    int get_lcp(int idx);
    void set_elem(int idx, int value);
    void set_lcp(int idx, int lcpValue);
    void deleteNode(int idx);
    int Aggregate(int l, int r);
    int GetInverse(JointTreapNode* node);
    int GetInverse(int value);
    void Move(int source, int dest);
    JointTreapNode* get_node(int idx);
protected:
    JointTreapNode* deleteNode(int idx, JointTreapNode* node);
    
private:
    JointTreapNode* insertNode(int idx, JointTreapNode* node, JointTreapNode* tree);
    void set_node_value(JointTreapNode* node, int idx, int value);
    void set_lcp_value(JointTreapNode* node, int idx, int lcpValue);
    int Aggregate(JointTreapNode* node, int l, int r);
    int AggregateFromNode(JointTreapNode* node, int l, int r);
    
};