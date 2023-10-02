using System;
using System.Diagnostics;
using System.Text;

namespace Homework_2
{
    public static class DistinctNumbers
    {

        /// <summary>
        /// Generates a list of random integers
        /// </summary>
        /// <param name="lower">The lower bound of random integers (inclusive)</param>
        /// <param name="upper">The upper bound of random integers (inclusive)</param>
        /// <param name="length">The length of the list</param>
        /// <returns>A list of integers</returns>
        public static List<int> GenerateRandomInts(int lower, int upper, int length)
        {
            // Create a new Random object
            Random random = new Random();

            // Create a new List object
            List<int> randomNumbers = new List<int>();

            // Loop quantity times
            for (int i = 0; i < length; i++)
            {
                // Generate a random integer in the range [lower, upper]
                int number = random.Next(lower, upper + 1);

                // Add the number to the list
                randomNumbers.Add(number);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Runs three distinct approaches to count the number of unique integers in a list
        /// </summary>
        /// <param name="randomNumbers">A list of integers</param>
        public static string RunDistinctIntegers(List<int> list)
        {
            // StringBuilder for building strings
            StringBuilder sb = new StringBuilder();

            // Approach 1:
            // Create and start timer
            Stopwatch hashTime = new Stopwatch();
            hashTime.Start();

            int hashCount = HashSetCount(list);

            // Stop timer
            hashTime.Stop();

            // Add approach 1 results to StringBuilder
            sb.AppendLine("Approach 1: Using a hash set");
            sb.AppendLine("\tNumber of distinct integers: " + hashCount);
            sb.AppendLine("\tElapsed time in milliseconds: " + hashTime.ElapsedMilliseconds);
            sb.AppendLine("\tThe time complexity of this approach is O(n) because we loop through the list once and add each element to the hash set which takes constant time to add.");
            sb.AppendLine("\tSince this is a hash set, it does not allow duplicate entries, so hashing the same number multiple times effectively removes it from the count.");


            // Approach 2:
            // Create and start timer
            Stopwatch falseTime = new Stopwatch();
            falseTime.Start();

            int falseCount = FalsifyCount(list);

            // Stop timer
            falseTime.Stop();

            // Add approach 2 results to StringBuilder
            sb.AppendLine("Approach 2: Using a falsifying count");
            sb.AppendLine("\tNumber of distinct integers: " + falseCount);
            sb.AppendLine("\tElapsed time in milliseconds: " + falseTime.ElapsedMilliseconds);
            sb.AppendLine("\tThe time complexity of this method is O(n^2) since we are looping through the list twice.");
            sb.AppendLine("\tHere, I am using a falsification style count where initially we assume all numbers are unique.");
            sb.AppendLine("\tNext, we loop through the list, observe a number, and check through the rest of the list to make sure the number is unique.");
            sb.AppendLine("\tIf a match is found, we can subtract one from the count since we know the number we are at is not unique.");


            //Approach 3:
            // Create and start timer
            Stopwatch sortTime = new Stopwatch();
            sortTime.Start();

            int sortCount = SortCount(list);

            // Stop timer
            sortTime.Stop();

            // Add approach 3 results to StringBuilder
            sb.AppendLine("Approach 3: Sorting the list");
            sb.AppendLine("\tNumber of distinct integers: " + sortCount);
            sb.AppendLine("\tElapsed time in milliseconds: " + sortTime.ElapsedMilliseconds);
            sb.AppendLine("\tThe time complexity for this is nlog(n) average case for the sort, and O(n) for the counting.");
            sb.AppendLine("\tSince the list is sorted, counting is much more efficient, I just check each number with the number that follows it, if they are different then I add 1 to the count.");

            return sb.ToString();
        }

        /// <summary>
        /// Hashes each entry in the list and counts the unique entries
        /// </summary>
        /// <param name="list">A list of integers</param>
        /// <returns>Number of unique list entries</returns>
        public static int HashSetCount(List<int> list)
        {
            // Create a new HashSet object
            HashSet<int> hashSet = new HashSet<int>();

            // Loop through the list
            foreach (int number in list)
            {
                // Add the number to the hash set
                hashSet.Add(number);
            }

            return hashSet.Count;
        }

        /// <summary>
        /// Counts the number of unique integers in the input
        /// </summary>
        /// <param name="list">A list of integers</param>
        /// <returns>Number of unique list entries</returns>
        public static int FalsifyCount(List<int> list)
        {
            // Create a counter
            int count = list.Count();

            // Loop through the list
            for (int i = 0; i < list.Count() - 1; i++)
            {
                // Loop through the latter half of the list (relative to outer loop)
                for (int j = i + 1; j < list.Count(); j++)
                {
                    // If we have a duplicate, there is one less unique number
                    if (list[i] == list[j])
                    {
                        count--;
                        break;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Sorts the input list and counts it
        /// </summary>
        /// <param name="list">A list of integers</param>
        /// <returns>Number of unique list entries</returns>
        public static int SortCount(List<int> list)
        {
            // Create counter
            int Count = 0;

            // Sort the list using the built-in sorting functionality
            list.Sort();

            // Loop through the list
            for (int i = 0; i < list.Count() - 1; i++)
            {
                // We need to account for the very last number being unique
                if (i == 0)
                {
                    Count++;
                }
                // Check if current number is distinct from next number, if true then there is one more unique number
                if (list[i] != list[i + 1])
                {
                    Count++;
                }
            }

            return Count;
        }
    }
}