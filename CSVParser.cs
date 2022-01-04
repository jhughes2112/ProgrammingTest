using System.IO;
using System.Diagnostics;

//-------------------
// This particular flavor of Comma-Separated-Value parser is specifically expected to handle noisy/bad data formats.
// The kinds of misbehaving data includes: any text OUTSIDE the quoted characters are ignored, variable delimiters,
// backslash-escaped content is respected inside quotes only, multiple valid content blocks should be concatenated
// within a column, and sometimes the quotes don't balance.  Handle it.
//-------------------
public class CSVParser
{
	// This can be called repeatedly on a block of text with carriage returns in it.  The parser stops at each line as it reaches \n's.
	static string[] Parse(string csv, char delimiter, char quote)
	{
		//***** Make this work, and return the array of strings, one per comma separated field.
		return new string[0];
	}

	// This verifies the output is correct.
	static void CheckEqual(string[] a, string[] b)
	{
		Debug.Assert(a.Length==b.Length, $"Length mismatch {a.Length} != {b.Length}");
		for (int i=0; i<a.Length; i++)
		{
			Debug.Assert(a[i] == b[i], $"Content mismatch index {i} {a[i]} != {b[i]}");
		}
	}

	public static int Main()
	{
		string[] csvLines = File.ReadAllLines("../../sometext.csv");
		string[] parsedOutput1 = Parse(csvLines[0], ',', '"');
		string[] correctOutput1 = new string[] { "simple", "input", "line" };
		CheckEqual(correctOutput1, parsedOutput1);
		
		string[] parsedOutput2 = Parse(csvLines[1], ',', '"');
		string[] correctOutput2 = new string[] { "this,", "is", "a", "", "", "valid\t ", "input with unclosed quotes" };
		CheckEqual(correctOutput2, parsedOutput2);

		string[] parsedOutput3 = Parse(csvLines[2], ',', '"');
		string[] correctOutput3 = new string[] { " this ", "is", "also", "", "", "valid to embed a \\backslash", "ora\n", "" };
		CheckEqual(correctOutput3, parsedOutput3);

		string[] parsedOutput4 = Parse(csvLines[2], 'x', '\'');
		string[] correctOutput4 = new string[] { "this", "is", "\"", "extreme!" };
		CheckEqual(correctOutput4, parsedOutput4);

		return 0;
	}
}