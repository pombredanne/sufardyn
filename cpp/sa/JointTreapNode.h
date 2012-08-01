#pragma once

class JointTreapNode 
{
private:
    int priority;
    JointTreapNode* left;
    JointTreapNode* right;
    JointTreapNode* parent;
    int value;
    int count;
    int aggregate;
    int lcpValue;
    
public: 
    JointTreapNode(int value, int lcpValue);	
    JointTreapNode* get_left();
    virtual void set_left(JointTreapNode* node);
    JointTreapNode* get_right();
    virtual void set_right(JointTreapNode* node);
    int get_value();
    int get_lcp();
    void set_value(int value);
    void set_lcp_value(int lcpValue);
    int get_count();
    int get_priority();
    int get_aggregate();
    virtual JointTreapNode* rotateLeft();
    virtual JointTreapNode* rotateRight();
    void set_parent(JointTreapNode* node);
    JointTreapNode* get_parent();
    
    JointTreapNode* deleteRoot();
    virtual void updateAggregate();
    ~JointTreapNode();
    
protected:
    virtual void update();
private: 
    void update_count();
};
