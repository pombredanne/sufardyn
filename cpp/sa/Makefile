UNAME := $(shell uname)

ifeq ($(UNAME), Linux)
GTEST_INCLUDE = /usr/include/gtest
GTEST_LIB = /usr/lib/libgtest.a
endif
ifeq ($(UNAME), Darwin)
GTEST_INCLUDE = /opt/local/include
GTEST_LIB = /opt/local/lib/libgtest.a
endif


SOURCES = AggregateTreap.cpp BaseTreapNode.cpp InverseTreap.cpp InverseIntTreap.cpp InvertedSuffixArray.cpp InvertedSuffixArrayInsertSearchStrategy.cpp ParentTreapNode.cpp InvertedStringStream.cpp Treap.cpp TreapNode.cpp StaticSuffixArray.cpp JointTreapNode.cpp JointInverseTreap.cpp String.cpp SimpleString.cpp ImplicitSuffixArray.cpp StringStream.cpp SuffixArray.cpp Utils.cpp

OBJS = AggregateTreap.o BaseTreapNode.o InverseTreap.o InverseIntTreap.o InvertedSuffixArray.o InvertedSuffixArrayInsertSearchStrategy.o ParentTreapNode.o InvertedStringStream.o Treap.o TreapNode.o StaticSuffixArray.o JointTreapNode.o JointInverseTreap.o String.o SimpleString.o ImplicitSuffixArray.o StringStream.o SuffixArray.o Utils.o

main: all
	g++ -c Main.cpp
	g++ Main.o $(OBJS) -o main

all:
	g++ -c $(SOURCES)

clean:
	rm -f *.o
	rm -f main
	rm -f tests
	rm -f *~
	rm -f stress

test: all
	g++ -c -I /opt/local/include/ Test.cpp
	g++ -I $(GTEST_INCLUDE) Test.o $(OBJS) $(GTEST_LIB) -lpthread -o tests
	./tests

stress: all
	g++ -c -I /opt/local/include/ StressTest.cpp
	g++ -I $(GTEST_INCLUDE) StressTest.o $(OBJS) $(GTEST_LIB) -lpthread -o stress
	./stress

valgrind: test
	valgrind ./tests