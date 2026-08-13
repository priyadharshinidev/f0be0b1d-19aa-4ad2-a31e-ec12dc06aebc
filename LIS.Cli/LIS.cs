using System;
using System.Collections.Generic;

// This file contains the core implementation for the longest increasing
// sequence calculation. The method receives all values as one string,
// validates and parses the values, scans them from left to right, and
// returns the longest contiguous strictly increasing run.

namespace LIS.Lib
{
    /// <summary>
    /// Provides the logic for finding the longest contiguous strictly
    /// increasing sequence in a whitespace-separated list of integers.
    ///
    /// A sequence is contiguous, which means that values must appear next to
    /// each other in the original input. Values cannot be skipped or reordered.
    /// A sequence is strictly increasing when every value is greater than the
    /// value immediately before it.
    ///
    /// For example, given the input "6 1 5 9 2", the increasing runs are
    /// "6", "1 5 9", and "2". The result is "1 5 9" because it is the
    /// longest run.
    ///
    /// When multiple runs have the same maximum length, the run that appears
    /// earliest in the input is returned. This behavior is implemented by
    /// updating the stored result only when a strictly longer run is found.
    ///
    /// The implementation processes the input in one pass. Its time complexity
    /// is O(n), and its space complexity is O(n), where n is the number of
    /// parsed input values.
    /// </summary>
    public static class LisSolver
    {
        /// <summary>
        /// Returns the longest contiguous strictly increasing run, preferring
        /// the earliest run when multiple runs have the same length.
        ///
        /// The input may contain integer values separated by spaces, tabs, or
        /// new lines. Repeated whitespace is ignored.
        ///
        /// Empty or whitespace-only input returns an empty string. A single
        /// integer is returned unchanged because it is a valid one-value run.
        ///
        /// If any token is not a valid 64-bit integer, a
        /// <see cref="FormatException"/> is thrown identifying the invalid
        /// token.
        ///
        /// Example:
        /// Input:  "6 1 5 9 2"
        /// Output: "1 5 9"
        ///
        /// The returned values are separated by a single space.
        /// </summary>
        public static string LongestIncreasingSubsequence(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            // Split on all supported whitespace characters and tabs, ignoring empty tokens. This allows the input to be formatted
            // with newlines or tabs, and it also allows multiple spaces between values.
            var tokens = input.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var nums = new List<long>(tokens.Length);

            // Parse every token so invalid input fails with a useful error.
            foreach (var t in tokens)
            {
                // Parse the token as a 64-bit integer. If parsing fails, throw a FormatException with the invalid token.
                if (long.TryParse(t, out var v)) nums.Add(v);
                else throw new FormatException($"Invalid integer token: '{t}'");
            }
            // The input is now validated and parsed. Scan the values to find the longest contiguous strictly increasing run.
            int n = nums.Count;
            if (n == 0) return string.Empty;
            // A single value is a valid run, so return it unchanged.
            if (n == 1) return nums[0].ToString();

            // Keep only the run boundaries; equal-length runs retain the earlier result.
            int bestStart = 0;
            int bestLength = 1;
            int currentStart = 0;
            // O(n) scan of the input values to find the longest contiguous strictly increasing run.
            // Scan each adjacent pair from left to right. The current run
            // continues only when the new value is greater than the previous value.
            for (int i = 1; i < n; i++)
            {
                // A smaller or equal value breaks the increasing run. The
                // current value becomes the first value of the next run.
                if (nums[i] <= nums[i - 1])
                {
                    currentStart = i;
                }

                // Calculate the length of the run ending at the current index.
                int currentLength = i - currentStart + 1;

                // Store a new best run only when it is longer than the previous
                // best run. Equal-length runs are ignored, so the earliest run wins.
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
