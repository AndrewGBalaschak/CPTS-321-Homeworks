using System;
using System.Numerics;
using System.Text;

public class FibonacciTextReader : TextReader
{
    // Current Fibonacci number, used to calculate the next Fibonacci number
    private BigInteger current;

    // Previous Fibonacci number, used to calculate the next Fibonacci number
    private BigInteger previous;

    // Number of Fibonacci numbers to calculate through (inclusive)
    private int maxLines;

    // Current line number
    private int line;

    /// <summary>
    /// Creates a FibonacciTextReader object
    /// </summary>
    /// <param name="maxLines">Number of Fibonacci numbers to calculate through (inclusive)</param>
    public FibonacciTextReader(int maxLines)
    {
        // Initializing with these values removes the need for checking special cases 0 and 1
        this.current = 0;
        this.previous = 1;
        this.maxLines = maxLines;
        this.line = 0;
    }

    /// <summary>
    /// Override the ReadLine method from TextReader
    /// </summary>
    /// <returns></returns>
    public override string ReadLine()
    {
        // Check if the line number exceeds the maximum
        if (line >= maxLines)
        {
            // Return null to indicate the end of the stream
            return null;
        }

        // Save the current number as a string for return
        string result = current.ToString();

        // Compute the next Fibonacci number by adding the current and previous numbers
        BigInteger next = current + previous;

        // Update the previous, current, and line numbers
        previous = current;
        current = next;
        line++;

        return result;
    }

    /// <summary>
    /// Override the ReadToEnd method from TextReader
    /// </summary>
    /// <returns>A string of fibonacci numbers, each on a new, numbered line</returns>
    public override string ReadToEnd()
    {
        StringBuilder sb = new StringBuilder();
        string fibonacciNumber;

        // Loop until ReadLine returns null
        while ((fibonacciNumber = this.ReadLine()) != null)
        {
            // Append the line number, Fibonacci number, and a new line character to the string builder
            // The line number needs an offset since it is incremented in ReadLine()
            // Fibonacci numbers are 0-indexed, so the first 50 numbers will be in the range 0-49
            sb.Append(line - 1 + ": " + fibonacciNumber);

            // Only include newline char if it is not the last line
            if (line < maxLines)
            {
                sb.Append('\n');
            }
        }
        return sb.ToString();
    }
}