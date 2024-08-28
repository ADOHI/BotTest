using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ScriptParser
{
    public static List<string> ParseString(string input)
    {
        // 정규식을 사용하여 대괄호와 중괄호 사이의 텍스트를 추출
        Regex regex = new Regex(@"\[(.*?)\]|\{(.*?)\}");
        MatchCollection matches = regex.Matches(input);

        List<string> parsedStrings = new List<string>();

        foreach (Match match in matches)
        {
            // match.Groups[1]은 대괄호 안의 텍스트, match.Groups[2]는 중괄호 안의 텍스트
            string extractedString = match.Groups[1].Value != "" ? match.Groups[1].Value : match.Groups[2].Value;
            parsedStrings.Add(extractedString);
        }

        return parsedStrings;
    }
}
