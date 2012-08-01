#include "gtest/gtest.h"
#include "InvertedSuffixArray.h"
#include "ImplicitSuffixArray.h"
#include "AggregateTreap.h"
#include "StaticSuffixArray.h"
#include <stdlib.h>
#include <algorithm>
#include "JointInverseTreap.h"
#include <iostream>
#include "Utils.h"

TEST(StressTest, ImplicitSuffixArray)
{
    for (int i = 0; i < 10000; i++) 
    {
        const char* p = Utils::GenerateRandomString(100);
        
        StaticSuffixArray sas;
        sas.AddString(p);
        
        ImplicitSuffixArray isa;
        
        isa.AddString(p);
        
        bool res = sas.Equals(isa);
        
        if (!res)
        {
            cout << p;
            EXPECT_TRUE(false);
        }
        
        delete[] p;
    }
    
    EXPECT_TRUE(true);
}

int main(int argc, char** argv)
{
    testing::InitGoogleTest(&argc, argv);
    
    return RUN_ALL_TESTS();
}
