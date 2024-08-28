using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ScriptParser
{
    public static List<string> ParseString(string input)
    {
        // ���Խ��� ����Ͽ� ���ȣ�� �߰�ȣ ������ �ؽ�Ʈ�� ����
        Regex regex = new Regex(@"\[(.*?)\]|\{(.*?)\}");
        MatchCollection matches = regex.Matches(input);

        List<string> parsedStrings = new List<string>();

        foreach (Match match in matches)
        {
            // match.Groups[1]�� ���ȣ ���� �ؽ�Ʈ, match.Groups[2]�� �߰�ȣ ���� �ؽ�Ʈ
            string extractedString = match.Groups[1].Value != "" ? match.Groups[1].Value : match.Groups[2].Value;
            parsedStrings.Add(extractedString);
        }

        return parsedStrings;
    }
}
