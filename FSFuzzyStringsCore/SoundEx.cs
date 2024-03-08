
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FSFuzzyStrings
{
	/// <summary>
	/// Description of SoundEx.
	/// </summary>
	public class SoundEx
	{
		public SoundEx()
		{
		}
		
		public static string Do(string word)
		{
			const int MaxSoundexCodeLength = 4;

			var soundexCode = new StringBuilder();
			var previousWasHOrW = false;

			word = Regex.Replace(
				word == null ? string.Empty : word.ToUpper(),
				@"[^\w\s]",
				string.Empty);

			if (string.IsNullOrEmpty(word))
				return string.Empty.PadRight(MaxSoundexCodeLength, '0');

			soundexCode.Append(word.First());

			for (var i = 1; i < word.Length; i++) {
				var numberCharForCurrentLetter =
					GetCharNumberForLetter(word[i]);

				if (i == 1 &&
				    numberCharForCurrentLetter ==
				    GetCharNumberForLetter(soundexCode[0]))
					continue;

				if (soundexCode.Length > 2 && previousWasHOrW &&
				    numberCharForCurrentLetter ==
				    soundexCode[soundexCode.Length - 2])
					continue;

				if (soundexCode.Length > 0 &&
				    numberCharForCurrentLetter ==
				    soundexCode[soundexCode.Length - 1])
					continue;

				soundexCode.Append(numberCharForCurrentLetter);

				previousWasHOrW = "HW".Contains(word[i]);
			}

			return soundexCode
                .Replace("0", string.Empty)
                    .ToString()
                        .PadRight(MaxSoundexCodeLength, '0')
                            .Substring(0, MaxSoundexCodeLength);
		}

		private static char GetCharNumberForLetter(char letter)
		{
			if ("BFPV".Contains(letter))
				return '1';
			if ("CGJKQSXZ".Contains(letter))
				return '2';
			if ("DT".Contains(letter))
				return '3';
			if ('L' == letter)
				return '4';
			if ("MN".Contains(letter))
				return '5';
			if ('R' == letter)
				return '6';

			return '0';
		}
		
		
		/// <summary>
		/// Soundex-encodes the given text
		/// </summary>
		/// <param name="text">Text to be encoded</param>
		/// <returns></returns>
		public static string SoundEx2(string text)
		{
								//  ABCDEFGHIJKLMNOPQRSTUVWXYZ
			const string _values = "01230120022455012623010202";
    		const int EncodingLength = 4;
			char prevChar = ' ';

			// Normalize input
			text = text.ToUpper();
			text = text.Replace(" ","");
			text = FSLibraryCore.TextUtil.OnlyAlfaNumeric(text);
			if (text.Length == 0)
				return text;

			// Write result to StringBuilder
			StringBuilder builder = new StringBuilder();
			builder.Append(text[0]);
			for (int i = 1;
            i < text.Length && builder.Length < EncodingLength;
            i++) {
				// Get digit for this letter
				char c = _values[text[i] - 'A'];

				// Add if not zero or same as last character
				if (c != '0' && c != prevChar) {
					builder.Append(c);
					prevChar = c;
				}
			}

			// Pad with trailing zeros
			while (builder.Length < EncodingLength)
				builder.Append('0');

			// Return result
			return builder.ToString();
		}

	}
}
