using System;

public class BST
{
	private BSTNode root;

	public BST()
	{
		this.root = null;
	}

	/// <summary>
	/// Adds a node to the binary tree
	/// </summary>
	/// <param name="value">value to add</param>
	/// <returns> 
	/// 0 = Duplicate value, node not added
	/// 1 = Node added successfully
	/// 2 = Tree was empty, node added as root
	/// </returns>
	public int InsertNode(int value)
	{
		// No root, early return for optimization
		if(root == null)
		{
			this.root = new BSTNode(value);
			return 2;
		}

        BSTNode current = this.root;
        BSTNode parent = null;

		// Search through tree until we have an empty space to put the new value
		while (current != null)
		{
			// No duplicates, early return for optimization
			if (current.value == value)
			{
				return 0;
			}

			if (value < current.value)
			{
				parent = current;
				current = current.left;
			}

            // By the pidgeonhole principle, value > current.value
            else
            {
                parent = current;
                current = current.right;
            }
		}

		// Create a new node with desired value
		BSTNode newNode = new BSTNode(value);

		// Create refernce to new node from parent node
		if(value < parent.value)
		{
			parent.left = newNode;
		}
		else
		{
			parent.right = newNode;
		}

		return 0;
	}

    /// <summary>
    /// Prints the tree to the console in sorted order
    /// </summary>
    public void PrintInOrder()
    {
        PrintInOrder(this.root); // Call the recursive method on the root node
    }

    /// <summary>
    /// Recursively traverses a subtree starting at node in sorted order and prints to the console (in-order traversal)
    /// </summary>
    /// <param name="node">node at which to start recursively printing</param>
    private void PrintInOrder(BSTNode node)
    {
        // Base case: if the node is null, return
        if (node == null)
        {
            return;
        }

        // Recursive case: print the left subtree, then the node, then the right subtree
        PrintInOrder(node.left);			// Print the left subtree
        Console.Write(node.value + " ");	// Print the node's value
        PrintInOrder(node.right);			// Print the right subtree
    }

    /// <summary>
    /// Calculates the number of nodes in the tree
    /// </summary>
    /// <returns>
    /// Number of nodes in the tree
    /// </returns>
    public int Count()
    {
        return Count(this.root); // Call the recursive method on the root node
    }

    /// <summary>
    /// Recursively counts the number of nodes in a subtree
    /// </summary>
    /// <param name="node">node at which to start recursively counting</param>
    /// <returns>
    /// Number of nodes in the subtree starting at node
    /// </returns>
    private int Count(BSTNode node)
    {
        // Base case: if the node is null, return 0
        if (node == null)
        {
            return 0;
        }

        // Recursive case: return 1 plus the sum of counts of left and right subtrees
        return 1 + Count(node.left) + Count(node.right);
    }

    /// <summary>
    /// Calculates the number of levels in the tree (height)
    /// </summary>
    /// <returns>
    /// Number of levels in the tree (height)
    /// </returns>
    public int Levels()
    {
        return Levels(this.root); // Call the recursive method on the root node
    }

    /// <summary>
    /// Recursively counts the number of levels in a subtree (height)
    /// </summary>
    /// <param name="node"></param>
    /// <returns>
    /// Height of the subtree starting a node
    /// </returns>
    private int Levels(BSTNode node)
    {
        // Base case: if the node is null, return 0
        if (node == null)
        {
            return 0;
        }

        // Recursive case: return 1 plus the maximum of levels of left and right subtrees
        return 1 + Math.Max(Levels(node.left), Levels(node.right));
    }

    /// <summary>
    /// Calculates the theoretical minimum number of levels the tree could have
    /// </summary>
    /// <returns>
    /// Theoretical minimum number of levels the tree could have
    /// </returns>
    public int MinLevels()
    {
       // A tree's theoretical minimum number of levels is proportional to the logbase 2 of the number of nodes in the tree plus one, rounded up to the nearest integer
       return (int)Math.Ceiling(Math.Log2(this.Count() + 1));
    }

}