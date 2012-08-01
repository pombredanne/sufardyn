#pragma once

class BaseTreapNode
{
 private:
  int priority;
  BaseTreapNode* left;
  BaseTreapNode* right;
  int value;
  int count;
  
public: 
  BaseTreapNode(int value);	
  BaseTreapNode* get_left();
  virtual void set_left(BaseTreapNode* node);
  BaseTreapNode* get_right();
  virtual void set_right(BaseTreapNode* node);
  int get_value();
  void set_value(int value);
  int get_count();
  int get_priority();
  virtual BaseTreapNode* rotateLeft();
  virtual BaseTreapNode* rotateRight();
  BaseTreapNode* deleteRoot();
  virtual void updateAggregate();
  ~BaseTreapNode();
 
protected:
  virtual void update();
 private: 
  void update_count();
};















