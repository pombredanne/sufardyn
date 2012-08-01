#include "ImplicitSuffixArray.h"
#include "StringStream.h"
#include <algorithm>
#include <string.h>

ImplicitSuffixArray::ImplicitSuffixArray()
{
    array = new JointInverseTreap();
    str = new StringStream();
    array_state.first = 0;
    array_state.second = NULL;
}

ImplicitSuffixArray::~ImplicitSuffixArray()
{
    delete str;
    delete array;
}

ImplicitSuffixArray& ImplicitSuffixArray::operator=(SuffixArray& other)
{
    if (this == &other)
    {
        return *this;
    }
        
    delete array;
    delete str;
    
    array = new JointInverseTreap();
    str = new StringStream();
    
    for (int i = 0; i < other.Length(); i++)
    {
        int elem = other.GetElem(i);
        
        int lcpValue = other.GetLcp(i);   
        array->insert(i, elem, lcpValue);
        str->AppendChar(other.GetString().getElem(i));
    }
    
    array_state = make_pair(0, array->root());

    return *this;
}

int ImplicitSuffixArray::Length()
{
    return array->get_count();
}

int ImplicitSuffixArray::GetElem(int i)
{
    return array->get_elem(i);
}

int ImplicitSuffixArray::GetLcp(int i)
{
    return array->get_lcp(i);
}

String& ImplicitSuffixArray::GetString()
{
    return *str;
}

std::pair<int, int> ImplicitSuffixArray::Search(const char *p)
{
	std::pair<int, JointTreapNode*> value = SearchNode(p);
	return make_pair(value.first, value.second->get_value());
}


std::pair<int, JointTreapNode*> ImplicitSuffixArray::SearchNode(const char *p)
{
    size_t len = strlen(p);
    std::pair<int, JointTreapNode*> state = make_pair(0, array->root());
    
    while (state.first < len)
    {
        std::pair<int, JointTreapNode*> new_state = AdvanceByChar(state, p[state.first]);
        if (new_state == state)
        {
            break;
        }
        state = new_state;
    }
    
    return state;
}

int lcpNewNodeRight(JointTreapNode* node, JointTreapNode* new_node)
{
    if (new_node->get_left() == NULL)
    {
        return node->get_lcp();
    }
    else
    {
        return std::min(node->get_lcp(), new_node->get_left()->get_aggregate());
    }
}

int lcpNewNodeLeft(JointTreapNode* node, JointTreapNode* new_node)
{
    if (new_node->get_right() == NULL)
    {
        return new_node->get_lcp();
    }
    else
    {
        return std::min(new_node->get_lcp(), new_node->get_right()->get_aggregate());
    }
}

std::pair<int, JointTreapNode*> ImplicitSuffixArray::MoveDown(std::pair<int, JointTreapNode*> state, char p)
{
    JointTreapNode* node = state.second;
    int matched = state.first;
    
    char c = str->getElem(node->get_value() + matched);
    if (p == c)
    {
        matched++;
    }
    else if (p > c)
    {
        JointTreapNode* new_node = node->get_right();
        if (new_node == NULL)
        {
            return make_pair(matched, node);
        }
        
        while (lcpNewNodeRight(node, new_node) < matched)
        {
            new_node = new_node->get_left();
            if (new_node == NULL) {
                return make_pair(matched, node);
            }
        }
        
        node = new_node;
    }
    else
    {
        JointTreapNode* new_node = node->get_left();
        if (new_node == NULL)
        {
            return make_pair(matched, node);
        }
        
        while (lcpNewNodeLeft(node, new_node) < matched)
        {
            new_node = new_node->get_right();
            if (new_node == NULL) {
                return make_pair(matched, node);
            }
        }
        
        node = new_node;
    }
    
    return make_pair(matched, node);
}

std::pair<int, JointTreapNode*> ImplicitSuffixArray::AdvanceByChar(std::pair<int, JointTreapNode*> input_state, char p)
{
    std::pair<int, JointTreapNode*> state = input_state;
    
    do
    {
        std::pair<int, JointTreapNode*> new_state = MoveDown(state, p);
        if (new_state == state)
        {
            break;
        }
        state = new_state;
    }
    while (state.first <= input_state.first);
    
    return state;
}

JointTreapNode* left_parent(JointTreapNode* node)
{
    JointTreapNode* new_node = node->get_parent();
    if (new_node == NULL) {
        return NULL;
    }
    while (new_node->get_right() != node) {
        node = new_node;
        new_node = node->get_parent();
        if (new_node == NULL) {
            return NULL;
        }
    }
    return new_node;
}

JointTreapNode* right_parent(JointTreapNode* node)
{
    JointTreapNode* new_node = node->get_parent();
    if (new_node == NULL) {
        return NULL;
    }
    while (new_node->get_left() != node) {
        node = new_node;
        new_node = node->get_parent();
        if (new_node == NULL) {
            return NULL;
        }
    }
    return new_node;
}

void ImplicitSuffixArray::AddChar(char p)
{
    str->AppendChar(p);
    
    if (array_state.second == NULL)
    {
        array->insert(0, 0, 0);
        array_state = make_pair(0, array->root());
        return;
    }
    
    while (true)
    {
        std::pair<int, JointTreapNode*> state = array_state;

        state = AdvanceByChar(state, p);

        if (state.first == array_state.first)
        {
            char c = str->getElem(state.second->get_value() + state.first);
            
            int idx = array->GetInverse(state.second);
            
            Mode mode;
            
            if (p < c)
            {
                array->insert(idx, array->get_count(), state.first);
                mode = LEFT;
            }
            else
            {
                int cur_lcp = state.second->get_lcp();
                array->insert(idx + 1, array->get_count(), cur_lcp);
                array->set_lcp(idx, state.first);
                mode = RIGHT;
            }
            
            if (state.first == 0)
            {
                array_state = make_pair(0, array->root());
                break;
            }
            else
            {
                int new_idx = array->GetInverse(state.second->get_value() + 1);
                
                std::pair<int, JointTreapNode*> new_state = make_pair(state.first - 1, array->get_node(array->get_elem(new_idx)));
                new_state = RestoreState(new_state, mode);
                
                array_state = new_state;
            }
        }
        else
        {
            array_state = state;
            break;
        }
    }
}

std::pair<int, JointTreapNode*> ImplicitSuffixArray::RestoreState(std::pair<int, JointTreapNode*> state, Mode mode)
{
    JointTreapNode* node = state.second;
    int matched = state.first;
    
    while (true)
    {
        JointTreapNode* ancestor = NULL;
        int lcp = -1;
        
        if (mode == RIGHT)
        {
            ancestor = right_parent(node);
        }
        else if (mode == LEFT)
        {
            ancestor = left_parent(node);
        }
        
        if (ancestor == NULL) {
            break;
        }
        
        if (mode == RIGHT)
        {
            lcp = lcpNewNodeLeft(ancestor, node);
        }
        else if (mode == LEFT)
        {
            lcp = lcpNewNodeRight(ancestor, node);
        }
        
        if (lcp < matched) {
            break;
        }
        
        node = ancestor;
    }
    
    return make_pair(matched, node);
}

void ImplicitSuffixArray::AddString(const char *p)
{
    const char *c = p;
    while (*c)
    {
        AddChar(*c);
        c++;
    }
}

ostream& operator<<(ostream &out, ImplicitSuffixArray &array)
{
    for (int i = 0; i < array.Length(); i++)
    {
        int value = array.GetElem(i);
        out << value << " ";
    }
    out << endl;
    
    
    for (int i = 0; i < array.Length() - 1; i++)
    {
        int value = array.GetLcp(i);
        out << value << " ";
    }
    
    out << "-" << endl;
    
    return out;
}