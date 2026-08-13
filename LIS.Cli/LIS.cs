using System;
using System.Collections.Generic;

namespace LIS.Lib
{
    public static class LisSolver
    {
        // Returns the longest contiguous strictly increasing run, preferring the earliest run on ties.
        public static string LongestIncreasingSubsequence(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            // Split the input string into tokens based on whitespace characters, removing empty entries
            
            var tokens = input.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var nums = new List<long>(tokens.Length);
            
            // Parse tokens into long integers, throwing an exception for invalid tokens
            foreach (var t in tokens)
            {
                if (long.TryParse(t, out var v)) nums.Add(v);
                else throw new FormatException($"Invalid integer token: '{t}'");
            }
            
            int n = nums.Count;
            if (n == 0) return string.Empty;
            if (n == 1) return nums[0].ToString();

            int bestStart = 0;
            int bestLength = 1;
            int currentStart = 0;

            for (int i = 1; i < n; i++)
            {
                if (nums[i] <= nums[i - 1])
                {
                    currentStart = i;
                }

                int currentLength = i - currentStart + 1;
                if (currentLength > bestLength)
                {
                    bestStart = currentStart;
                    bestLength = currentLength;
                }
            }

            return string.Join(' ', nums.GetRange(bestStart, bestLength));
        }
    }
}
