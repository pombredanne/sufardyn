#include "gtest/gtest.h"
#include "InvertedSuffixArray.h"
#include "ImplicitSuffixArray.h"
#include "AggregateTreap.h"
#include "StaticSuffixArray.h"
#include <stdlib.h>
#include <algorithm>
#include "JointInverseTreap.h"
#include <iostream>

bool EqualsEither(int what, int val1, int val2)
{
	return what == val1 || what == val2;
}
#define EXPECT_EITHER(w,v1,v2) EXPECT_PRED3(EqualsEither,w,v1,v2)

void ArrayEqualTest(const char* p)
{
    StaticSuffixArray t;
    t.AddString(p);    
    ImplicitSuffixArray imm;
    imm.AddString(p);
        
    EXPECT_TRUE(t.Equals(imm));
}

TEST(ImplicitSuffixArray, BuildTest)
{
    ArrayEqualTest("aaaaaaaaaaa$");

    ArrayEqualTest("a$");
    
    ArrayEqualTest("mississippi$");
    
    ArrayEqualTest("abracadabra$");
    
    ArrayEqualTest("zazazazazaz$");
    
    ArrayEqualTest("abc$");
    
    ArrayEqualTest("$");

    ArrayEqualTest("banana$");
    
    {
        StaticSuffixArray t;
        t.AddString("banana");    
        ImplicitSuffixArray imm;
        imm = t;
        imm.AddChar('b');
        imm.AddChar('a');
        imm.AddChar('z');
        
        StaticSuffixArray sas;
        sas.AddString("bananabaz");
        
        EXPECT_TRUE(sas.Equals(imm));
    }
    
    
    {
        StaticSuffixArray t;
        t.AddString("banana");    
        ImplicitSuffixArray imm;
        imm = t;
        imm.AddChar('b');
        imm.AddChar('a');
        imm.AddChar('a');
        
        StaticSuffixArray sas;
        sas.AddString("bananabaa");
        
        for (int i = 0; i < sas.Length() - 1; i++) 
        {
            EXPECT_EQ(imm.GetElem(i), sas.GetElem(i + 1));
            EXPECT_EQ(imm.GetLcp(i), sas.GetLcp(i + 1));
        }
    }
}

TEST(ImplicitSuffixArray, SearchTest)
{
    StaticSuffixArray st;
    st.AddString("banana");
    
    ImplicitSuffixArray im;
    
    im = st;
    
    {
        std::pair<int, int> value = im.Search("ab");
        
        EXPECT_EQ(value.first, 1);
        EXPECT_EITHER(value.second, 5, 3);
    }
    
    {
        std::pair<int, int> value = im.Search("anaz");
        
        EXPECT_EQ(value.first, 3);
        EXPECT_EQ(value.second, 1);
    }
    
    {
        std::pair<int, int> value = im.Search("ana");
        
        EXPECT_EQ(value.first, 3);
    }
    
    {
        std::pair<int, int> value = im.Search("nana");
        
        EXPECT_EQ(value.first, 4);
        EXPECT_EQ(value.second, 2);
    }
    
    {
        std::pair<int, int> value = im.Search("az");
        
        EXPECT_EQ(value.first, 1);
        EXPECT_EQ(value.second, 1);
    }
    
    {
        std::pair<int, int> value = im.Search("z");
        
        EXPECT_EQ(value.first, 0);
        EXPECT_EQ(value.second, 2);
    }
    
    {
        std::pair<int, int> value = im.Search("!");
        
        EXPECT_EQ(value.first, 0);
        EXPECT_EQ(value.second, 5);
    }
    
    {
        std::pair<int, int> value = im.Search("aa");
        
        EXPECT_EQ(value.first, 1);
        EXPECT_EITHER(value.second, 5, 3);
    }
    
    {
        std::pair<int, int> value = im.Search("banana");
        
        EXPECT_EQ(value.first, 6);
        EXPECT_EQ(value.second, 0);
    }
    
    {
        std::pair<int, int> value = im.Search("naz");
        
        EXPECT_EQ(value.first, 2);
        EXPECT_EQ(value.second, 2);
    }
    
}


TEST(JointInverseTreap, Sum)
{
    const int N = 100;
    
    JointInverseTreap t;
    for (int i = 0; i < N; i++)
    {
        t.insert(i, i, i);
    }
    
    for (int i = 0; i < N; i++)
    {
        for (int j = i; j < N; j++)
        {
            EXPECT_EQ(t.Aggregate(i, j), i);
        }
    }
}

TEST(InvertedSuffixArray, Mississippi)
{
    InvertedSuffixArray array;
    array.AddString("mississippi");
    
    EXPECT_EQ(array.GetElem(0), 10);
    EXPECT_EQ(array.GetElem(1), 7);
    EXPECT_EQ(array.GetElem(2), 4);
    EXPECT_EQ(array.GetElem(3), 1);
    EXPECT_EQ(array.GetElem(4), 0);
    EXPECT_EQ(array.GetElem(5), 9);
    EXPECT_EQ(array.GetElem(6), 8);
    EXPECT_EQ(array.GetElem(7), 6);
    EXPECT_EQ(array.GetElem(8), 3);
    EXPECT_EQ(array.GetElem(9), 5);
    EXPECT_EQ(array.GetElem(10), 2);
}

TEST(StaticSuffixArray, Banana)
{
    StaticSuffixArray array;
    array.AddString("banana");
    EXPECT_EQ(array.GetElem(0), 5);
    EXPECT_EQ(array.GetElem(1), 3);
    EXPECT_EQ(array.GetElem(2), 1);
    EXPECT_EQ(array.GetElem(3), 0);
    EXPECT_EQ(array.GetElem(4), 4);
    EXPECT_EQ(array.GetElem(5), 2);
}

TEST(AggregateTreap, RandAggregate)
{
    AggregateTreap t;
    
    int MAX = 100000;
    int LEN = 1000;
    

    
    for (int i = 0; i < LEN; i++)
    {
        t.insert(i, rand() % MAX);
    }
    
    for (int i = 0; i < LEN; i++)
    {
        int m = MAX;
        for (int j = i; j < LEN; j++)
        {
            m = std::min(m, t.get_elem(j));
            
            EXPECT_EQ(m, t.Aggregate(i, j));
        }
    }
}

int main(int argc, char** argv)
{
    testing::InitGoogleTest(&argc, argv);
 
    if (argc == 1)
    {
        srand(42);
    }
    else
    {
        for (int i = 0; i < 100; i++)
        {
            srand(i);
            cout << "srand = " << i << endl;
            if (RUN_ALL_TESTS() != 0)
            {
                return 1;
            }
        }
    }
    
    return RUN_ALL_TESTS();
}
