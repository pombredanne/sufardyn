#pragma once
#include "SuffixArray.h"
#include "JointInverseTreap.h"
#include "StringStream.h"
#include <iostream>

enum Mode {
    LEFT = -1,
    RIGHT = 1
};

int lcpNewNodeRight(JointTreapNode* node, JointTreapNode* new_node);
int lcpNewNodeLeft(JointTreapNode* node, JointTreapNode* new_node);
JointTreapNode* left_parent(JointTreapNode* node);
JointTreapNode* right_parent(JointTreapNode* node);

class ImplicitSuffixArray : public SuffixArray
{
private:
    JointInverseTreap* array;
    StringStream* str;
    std::pair<int, JointTreapNode*> SearchNode(const char *p);
    std::pair<int, JointTreapNode*> MoveDown(std::pair<int, JointTreapNode*> state, char p);
    std::pair<int, JointTreapNode*> array_state;

public:
    ImplicitSuffixArray();
    ~ImplicitSuffixArray();
    int GetElem(int i);
    int GetLcp(int i);
    int Length();
    String& GetString();    
    ImplicitSuffixArray& operator=(SuffixArray &other);
    friend ostream& operator<<(std::ostream &out, ImplicitSuffixArray &array);
    std::pair<int, int> Search(const char *p);
    std::pair<int, JointTreapNode*> AdvanceByChar(std::pair<int, JointTreapNode*> state, char p);
    std::pair<int, JointTreapNode*> RestoreState(std::pair<int, JointTreapNode*> state, Mode mode);
	void AddChar(char p);
    void AddString(const char* p);
};
