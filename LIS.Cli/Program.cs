using System;
using LIS.Lib;

/// <summary>
/// Command-line entry point for the contiguous increasing-sequence solver.
/// </summary>
class Program
{
    /// <summary>
    /// Reads input from command-line arguments or standard input, then prints
    /// the longest contiguous strictly increasing sequence.
    /// </summary>
    static void Main(string[] args)
    {
        string input;

        if (args.Length > 0)
        {
            // Command-line values arrive as separate arguments, so combine
            // them into the single input string expected by the solver.
            input = string.Join(' ', args);
        }
        else
        {
            // Reading standard input also supports piped or redirected input.
            input = Console.In.ReadToEnd();
        }

        var outStr = LisSolver.LongestIncreasingSubsequence(input ?? string.Empty);
        Console.WriteLine(outStr);
    }
}
