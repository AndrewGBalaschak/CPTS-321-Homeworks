// Create a new binary search tree
BST tree = new BST();

// Prompt the user to enter a list of integer numbers on a single line
Console.WriteLine("Please enter a list of integers in the range [0,100] on a single line, separated by spaces:");

// Read the user's input as a string
string input = Console.ReadLine();

// Split the input string by spaces and convert each element to an integer
int[] numbers = Array.ConvertAll(input.Split(' '), int.Parse);

// Insert each number into the binary search tree
foreach (int number in numbers)
{
    tree.InsertNode(number);
}

// Display the numbers in sorted order
Console.WriteLine("The numbers in sorted order are: ");
tree.PrintInOrder();
Console.WriteLine();

// Display the statistics about the tree
Console.WriteLine("Tree Statistics:");
Console.WriteLine("\tNumber of nodes: " + tree.Count());
Console.WriteLine("\tNumber of levels: " + tree.Levels());
Console.WriteLine("\tTheoretical minimum number of levels: " + tree.MinLevels());