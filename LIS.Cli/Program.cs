using System;
using LIS.Lib;

class Program
{
    // Entry point of the application
    // Reads input from command line arguments or standard input, computes the longest contiguous increasing sequence, and prints the result.
    static void Main(string[] args)
    {
        string input;
        // Read input from command line arguments or standard input
        // If command line arguments are provided, join them into a single string.
        if (args.Length > 0)
        {
            // Join command line arguments into a single string, separated by spaces
            input = string.Join(' ', args);
        }
        else
        {
            input = Console.In.ReadToEnd();
        }
        // Call the LIS solver and print the result
        // If input is null, pass an empty string to avoid exceptions
        var outStr = LisSolver.LongestIncreasingSubsequence(input ?? string.Empty);
        Console.WriteLine(outStr);
    }
}
