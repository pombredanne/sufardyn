#include"Treap.cpp"

class StringStream
{
private:
	char* a;
public:
	StringStream()
	{
		a = new char[MAX_TREAP_NODES];
		Length = 0;
	}

	void AppendChar(char ch)
	{
		a[Length] = ch;
		Length++;
	}

	int Length;
	char getElem(int idx)
	{
		return a[Length - idx - 1];
	}
};

