#pragma once
#include"AbstractMonoid.cpp"

class AddMonoid : public AbstractMonoid
{
public:
	
	int NeutralElement()
	{
		return 0;
	}

	int Operation(int fst, int snd)
	{
		return fst + snd;
	}
};