#pragma once
#include"AbstractMonoid.cpp"

class MinMonoid : public AbstractMonoid
{
public:
	
	int NeutralElement()
	{
		return -2147483648;
	}

	int Operation(int fst, int snd)
	{
		return (fst < snd ? fst : snd);
	}
};