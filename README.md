# Longest Increasing Sequence Solution

This repository contains a C# .NET implementation that finds the longest contiguous strictly increasing sequence in a whitespace-separated list of integers.

## Problem Statement

Develop a function that takes one string input of any number of integers separated by whitespace. The function outputs the **longest contiguous strictly increasing sequence** present in that input. If more than one sequence has the longest length, the **earliest one** is returned.

## Solution Overview

### Algorithm
- **Time Complexity:** O(n)
- **Space Complexity:** O(n)
- **Approach:** Single pass over adjacent values
- **Tie-breaking:** Keep the first sequence when equal-length sequences are found

### Key Components

1. **LIS.Cli** - Command-line interface application
2. **LIS.Lib** - Core LIS solver library (LIS.cs)
3. **LIS.Tests** - xUnit test suite covering the supplied cases and edge cases

## Building

### Prerequisites
- .NET 8.0 SDK or Runtime
- [Download .NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)

### Build Commands

```bash
# Build the entire solution
cd LIS.Cli
dotnet build

# Or build with tests
cd LIS.Tests
dotnet build
```

## Running the Application

### From Command Line
```bash
cd LIS.Cli
dotnet run -- <space-separated-integers>
```

### Examples
```bash
# Example 1: Simple case
dotnet run -- 6 1 5 9 2
# Output: 1 5 9

# Example 2: Larger sequence
dotnet run -- 923 1189 3852 3862 4421 7823 11143 11695 12145 21404 27807 31310
# Output: 923 1189 3852 3862 4421 7823 11143 11695 12145 21404 27807 31310

# Example 3: Edge case (single number)
dotnet run -- 42
# Output: 42

# Example 4: Edge case (decreasing sequence)
dotnet run -- 5 4 3 2 1
# Output: 5
```

## Running Tests

```bash
cd LIS.Tests
dotnet test
```

### Test Coverage
- ✅ Test Case 1: Simple example (6 1 5 9 2 → 1 5 9)
- ✅ Test Case 2: Large dataset validation
- ✅ Test Case 3: Large dataset validation  
- ✅ Test Case 4: Medium dataset exact match (12-element LIS)
- ✅ Test Case 5: Large dataset validation
- ✅ Test Case 6: Large dataset validation
- ✅ Edge Case: Empty string
- ✅ Edge Case: Single number
- ✅ Edge Case: All decreasing sequence

All tests validate that output sequences are:
- Strictly increasing
- Present in the input
- Contiguous in the input
- Represent the maximum possible length

## API Reference

### `LisSolver.LongestIncreasingSubsequence(string input)`

**Parameters:**
- `input` (string): Space-separated integers

**Returns:**
- (string): Space-separated longest contiguous strictly increasing sequence

**Throws:**
- `FormatException`: If input contains non-integer tokens

**Examples:**
```csharp
using LIS.Lib;

var result = LisSolver.LongestIncreasingSubsequence("6 1 5 9 2");
Console.WriteLine(result);  // Output: "1 5 9"
```

## Project Structure

```
.
├── LIS.Cli/
│   ├── LIS.Cli.csproj
│   ├── Program.cs          # CLI entry point
│   └── LIS.cs              # Core LIS algorithm implementation
├── LIS.Tests/
│   ├── LIS.Tests.csproj
│   └── LisTests.cs         # xUnit test cases
├── .gitignore
└── README.md
```

## Implementation Details

### Algorithm Steps

1. **Parse Input:** Split input string into individual integers
2. **Initialize:** Create arrays for tracking:
   - `tails[]`: Smallest tail value for each LIS length
   - `tailsIndex[]`: Original index producing each tail
   - `prev[]`: Previous element in LIS ending at each position
   - `lenAt[]`: Length of LIS ending at each position

3. **Process Each Number (O(n log n)):**
   - Binary search in `tails[]` to find position
   - Update or extend `tails[]`
   - Track previous element

4. **Find Maximum Length:** Locate all positions with maximum LIS length

5. **Select Earliest:** Among all maximum-length LIS, choose the one with earliest start index

6. **Reconstruct:** Walk backward using `prev[]` array to build the result
1. **Parse Input:** Split the input on whitespace and parse each token as a `long`.
2. **Scan Adjacent Values:** Extend the current run while each value is greater than the previous value.
3. **Track the Best Run:** Replace the best run only when the current run is strictly longer, preserving the earliest run on ties.
4. **Return:** Join the selected contiguous values with single spaces.

### Example Trace
```
Input: "6 1 5 9 2"
Numbers: [6, 1, 5, 9, 2]

Step-by-step:
i=0, x=6: tails=[6]
i=1, x=1: tails=[1]         (replace, smaller tail)
i=2, x=5: tails=[1,5]       (extend)
i=3, x=9: tails=[1,5,9]     (extend)
i=4, x=2: tails=[1,2,9]     (replace at pos 1)

maxLen = 3 (indices 3 and 4 both have length 3)
Start of index 3: 1, start of index 4: 1 (same)
Pick first: index 3
Reconstruct: 9 ← 5 ← 1
Reverse: [1, 5, 9]
The longest contiguous run is [1, 5, 9]
Output: "1 5 9"
```

## Performance Analysis

### Time Complexity
- Input parsing and adjacent-value scan: O(n)
- **Total: O(n)**

### Space Complexity
- Arrays: O(n)
- **Total: O(n)**

### Compared to Naive Approach
- Naive (DP): O(n²) time, O(n) space
- This solution: O(n) time, O(n) space

## Verification Checklist

- [x] Algorithm correctly implements longest contiguous increasing sequence
- [x] Tie-breaking returns earliest sequence
- [x] Unit tests cover the supplied cases and edge cases
- [x] Edge cases handled (empty, single element, decreasing sequence)
- [x] Code is clean and well-documented
- [x] CLI application works correctly
- [x] All tests pass

## Future Enhancements

- [ ] Add GitHub Actions CI workflow
- [ ] Add Dockerfile for containerization
- [ ] Add code linting (StyleCop)
- [ ] Add code coverage reporting (Coverlet)
- [ ] Add performance benchmarking

## References

- [Longest Increasing Subsequence - Wikipedia](https://en.wikipedia.org/wiki/Longest_increasing_subsequence)
- [Patience Sorting - Wikipedia](https://en.wikipedia.org/wiki/Patience_sorting)

## License

This implementation is provided as-is for educational purposes.
