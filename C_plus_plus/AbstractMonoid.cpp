#pragma once

#define operation(a, b) ((a) < (b)) ? (a) : (b)
//#define operation(a, b) ((a) + (b))

class AbstractMonoid
{
public :
	virtual int NeutralElement() = 0; 

	virtual int Operation(int fst, int snd) = 0;
};
