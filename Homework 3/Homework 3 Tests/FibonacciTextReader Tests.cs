using Homework_3;

namespace Homework_3_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestFibonacci50()
        {
            FibonacciTextReader fib = new FibonacciTextReader(50);
            Assert.AreEqual(
                "0: 0\n1: 1\n2: 1\n3: 2\n4: 3\n5: 5\n6: 8\n7: 13\n8: 21\n9: 34\n10: 55\n11: 89\n12: 144\n13: 233\n14: 377\n15: 610\n16: 987\n17: 1597\n18: 2584\n19: 4181\n20: 6765\n21: 10946\n22: 17711\n23: 28657\n24: 46368\n25: 75025\n26: 121393\n27: 196418\n28: 317811\n29: 514229\n30: 832040\n31: 1346269\n32: 2178309\n33: 3524578\n34: 5702887\n35: 9227465\n36: 14930352\n37: 24157817\n38: 39088169\n39: 63245986\n40: 102334155\n41: 165580141\n42: 267914296\n43: 433494437\n44: 701408733\n45: 1134903170\n46: 1836311903\n47: 2971215073\n48: 4807526976\n49: 7778742049",
                fib.ReadToEnd()
                );
        }

        [Test]
        public void TestFibonacci100()
        {
            FibonacciTextReader fib = new FibonacciTextReader(100);
            Assert.AreEqual(
                "0: 0\n1: 1\n2: 1\n3: 2\n4: 3\n5: 5\n6: 8\n7: 13\n8: 21\n9: 34\n10: 55\n11: 89\n12: 144\n13: 233\n14: 377\n15: 610\n16: 987\n17: 1597\n18: 2584\n19: 4181\n20: 6765\n21: 10946\n22: 17711\n23: 28657\n24: 46368\n25: 75025\n26: 121393\n27: 196418\n28: 317811\n29: 514229\n30: 832040\n31: 1346269\n32: 2178309\n33: 3524578\n34: 5702887\n35: 9227465\n36: 14930352\n37: 24157817\n38: 39088169\n39: 63245986\n40: 102334155\n41: 165580141\n42: 267914296\n43: 433494437\n44: 701408733\n45: 1134903170\n46: 1836311903\n47: 2971215073\n48: 4807526976\n49: 7778742049\n50: 12586269025\n51: 20365011074\n52: 32951280099\n53: 53316291173\n54: 86267571272\n55: 139583862445\n56: 225851433717\n57: 365435296162\n58: 591286729879\n59: 956722026041\n60: 1548008755920\n61: 2504730781961\n62: 4052739537881\n63: 6557470319842\n64: 10610209857723\n65: 17167680177565\n66: 27777890035288\n67: 44945570212853\n68: 72723460248141\n69: 117669030460994\n70: 190392490709135\n71: 308061521170129\n72: 498454011879264\n73: 806515533049393\n74: 1304969544928657\n75: 2111485077978050\n76: 3416454622906707\n77: 5527939700884757\n78: 8944394323791464\n79: 14472334024676221\n80: 23416728348467685\n81: 37889062373143906\n82: 61305790721611591\n83: 99194853094755497\n84: 160500643816367088\n85: 259695496911122585\n86: 420196140727489673\n87: 679891637638612258\n88: 1100087778366101931\n89: 1779979416004714189\n90: 2880067194370816120\n91: 4660046610375530309\n92: 7540113804746346429\n93: 12200160415121876738\n94: 19740274219868223167\n95: 31940434634990099905\n96: 51680708854858323072\n97: 83621143489848422977\n98: 135301852344706746049\n99: 218922995834555169026",
                fib.ReadToEnd()
                );
        }

        [Test]
        public void TestFibonacci0()
        {
            FibonacciTextReader fib = new FibonacciTextReader(0);
            Assert.AreEqual(
                "",
                fib.ReadToEnd()
                );
        }

        [Test]
        public void TestFibonacciNegative()
        {
            FibonacciTextReader fib = new FibonacciTextReader(-1);
            Assert.AreEqual(
                "",
                fib.ReadToEnd()
                );
        }
    }
}