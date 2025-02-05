using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    //static char[] TRIM_CHARS = { '\"' };
    static char[] TRIM_CHARS = { '\"' };


    public static List<Dictionary<string, object>> ReadFileFromResources(string _filePath)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(_filePath) as TextAsset;
        string dataText = data.text;
        var lines = Regex.Split(dataText, LINE_SPLIT_RE);
        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);

        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                header[j] = header[j].TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalValue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalValue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalValue = f;
                }
                entry[header[j]] = finalValue;
            }
            list.Add(entry);
        }
        return list;
    }
}