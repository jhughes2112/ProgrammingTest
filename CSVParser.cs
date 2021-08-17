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
		// "simple","input","line"
		string line1 = @"""simple"",""input"",""line""";
		string[] output1 = new string[] { "simple", "input", "line" };
		CheckEqual(output1, Parse(line1, ',', '"'));
		
		// "this," ,"is", "a",,,"valid\t" " ","input with unclosed quotes
		string line2 = @"""this,"" ,""is"", ""a"",,,""valid\t"" "" "",""input with unclosed quotes";
		string[] output2 = new string[] { "this,", "is", "a", "", "", "valid\t ", "input with unclosed quotes" };
		CheckEqual(output2, Parse(line2, ',', '"'));

		// " this " ,"is", " also " ignored text,,, more ignored text "valid to embed a \\backslash","or" "a" "\n",
		string line3 = @""" this "" ,""is"", "" also "" ignored text,,, more ignored text ""valid to embed a \\backslash"",""or"" ""a"" ""\n"",";
		string[] output3 = new string[] { " this ", "is", "also", "", "", "valid to embed a \\backslash", "or", "a", "\n", "" };
		CheckEqual(output3, Parse(line3, ',', '"'));

		// 'this'x \\\'is' x '"'x 'extreme!'
		string line4 = @"'this'x \\\'is' x '""'x 'extreme!' ";
		string[] output4 = new string[] { "this", "is", "\"", "extreme!" };
		CheckEqual(output4, Parse(line4, 'x', '\''));

		return 0;
	}
}