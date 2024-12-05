#include "pch.h"
#include "C:\VSProjects\Sample-Test1\MyTestSuite\tester.cpp"

TEST(TestCaseName, TestName) {
  EXPECT_EQ("qhA=", tester("AA10")); // Valid Test.
  EXPECT_EQ("q1B=", tester("AA10")); // Invalid Test.
  EXPECT_TRUE(true);
}

TEST(TestCaseName, TestName) {
	EXPECT_EQ("686974207468652062756c6c277320657965", tester("686974207468652062756c6c277320657965")); // Valid Test.
	EXPECT_EQ("686974207468652062756c6c277320657965AB", tester("686974207468652062756c6c277320657965")); // Invalid Test.
	// Loop tests
	for (int i = 0; i < 20; ++i) {
		EXPECT_EQ("686974207468652062756c6c277320657965AB" + char(i), tester("686974207468652062756c6c277320657965"));
	}

	EXPECT_TRUE(true);
}

TEST(TestCaseName, TestName) {
	EXPECT_EQ(" vA", tester()); // Correct Showcase (Inserted during test).
	EXPECT_EQ(" vA", tester()); // Incorrect Showcase (Inserted during test).
	EXPECT_TRUE(true);
}

TEST(TestCaseName, TestName) {
	EXPECT_EQ("Now that party is jumping\n", tester());
	EXPECT_EQ("n", tester()); 
	EXPECT_EQ("Hello World", tester()); 
	EXPECT_EQ("This is an encrypt test", tester());
	EXPECT_TRUE(true);
}

TEST(TestCaseName, TestName) {
	EXPECT_EQ("0b3637272a2b2e636222ce69692a2e69692a23693a23693a2a3c6324202d62363443c2a26223242727665272a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f", tester()); // Correct Showcase (Inserted during test).
	EXPECT_TRUE(true);
}

TEST(TestCaseName, TestName) {
	EXPECT_EQ("gloe1vn", tester()); // Unsuccessful(?) Challenge 6 Appears to have errors.
	EXPECT_TRUE(true);
}

TEST(TestCaseName, TestName) {
	EXPECT_EQ("YELLOW SUBMARINE", tester());
	EXPECT_TRUE(true);
}

TEST(TestCaseName, TestName) {
	EXPECT_EQ("d8836a0875edea57508758554326957e87585ad96620192877890a9889d1203a0192938377894813655e5368397", tester());
	EXPECT_TRUE(true);
}

