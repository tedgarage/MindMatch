using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethods
{

    #region DATE TIME

    public static DateTime ToDateTime(this string date)
    {
        return Convert.ToDateTime(date);
    }

    public static string ToFormattedDateTime(this DateTime input)
    {
        string result = input.ToString("dd MMM yyyy, hh:mm tt");

        return result;
    }

    public static string ToFormattedDate(this DateTime input)
    {
        string result = input.ToString("dd, MMM yyyy");

        return result;
    }

    public static string ToRelativeDate(this DateTime input)
    {
        TimeSpan oSpan = DateTime.UtcNow.Subtract(input);
        double TotalMinutes = oSpan.TotalMinutes;
        string Suffix = " ago";

        if (TotalMinutes < 0.0)
        {
            TotalMinutes = Math.Abs(TotalMinutes);
            Suffix = " from now";
        }

        var aValue = new SortedList<double, Func<string>>();
        aValue.Add(0.75, () => "less than a min");
        aValue.Add(1.5, () => "about a min");
        aValue.Add(45, () => string.Format("{0} min", Math.Round(TotalMinutes)));
        aValue.Add(90, () => "about an hour");
        aValue.Add(1440, () => string.Format("about {0} hr", Math.Round(Math.Abs(oSpan.TotalHours)))); // 60 * 24
        aValue.Add(2880, () => "a day"); // 60 * 48
        aValue.Add(43200, () => string.Format("{0} days", Math.Floor(Math.Abs(oSpan.TotalDays)))); // 60 * 24 * 30
        aValue.Add(86400, () => "about a month"); // 60 * 24 * 60
        aValue.Add(525600, () => string.Format("{0} mon", Math.Floor(Math.Abs(oSpan.TotalDays / 30)))); // 60 * 24 * 365 
        aValue.Add(1051200, () => "about a year"); // 60 * 24 * 365 * 2
        aValue.Add(double.MaxValue, () => string.Format("{0} yrs", Math.Floor(Math.Abs(oSpan.TotalDays / 365))));

        return aValue.First(n => TotalMinutes < n.Key).Value.Invoke() + Suffix;
    }

    #endregion

    #region UI

    public static void ScrollToTop(this ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }

    public static void ScrollToBottom(this ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }

    #endregion

    public static Rect GetWorldRect(this RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        // Get the bottom left corner.
        Vector3 position = corners[0];

        Vector2 size = new Vector2(
            rectTransform.lossyScale.x * rectTransform.rect.size.x,
            rectTransform.lossyScale.y * rectTransform.rect.size.y);

        return new Rect(position, size);
    }

    public static Texture2D DeCompress(this Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }

    public static void SetSpriteForEachChild(this Transform parent, Sprite sprite)
    {
        foreach (Transform child in parent)
        {
            child.GetComponent<Image>().sprite = sprite;
        }
    }

}

public static class EnumExtensions
{
    public static string ToStringValue<TEnum>(this TEnum enumValue) where TEnum : Enum
    {
        return Enum.GetName(typeof(TEnum), enumValue);
    }
    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}

public static class ListExtensions
{
    private static System.Random random = new System.Random();

    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static T GetRandomElement<T>(this T[] array)
    {
        int rand = UnityEngine.Random.Range(0, array.Length);

        T element = array[rand];

        return element;
    }

    public static T GetRandomElement<T>(this List<T> list)
    {
        int rand = UnityEngine.Random.Range(0, list.Count);


        T element = list[rand];

        return element;
    }

    public static List<T> GetRandomElements<T>(this List<T> totalList, int numElements)
    {
        if (numElements >= totalList.Count)
        {
            throw new ArgumentException("Number of elements requested is greater than or equal to the total list size.");
        }

        List<T> randomElements = new List<T>();
        System.Random random = new System.Random();

        while (randomElements.Count < numElements)
        {
            int randomIndex = random.Next(totalList.Count);

            if (!randomElements.Contains(totalList[randomIndex]))
            {
                randomElements.Add(totalList[randomIndex]);
            }
        }

        return randomElements;
    }
}

#region COLOR
public static class ColorExtensions
{
    public static Color ToColor(this string hex, float alpha = 1)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        Color color = new Color(r / 255f, g / 255f, b / 255f);
        color.a = alpha;

        return color;
    }


    public static Color ChangeAlpha(this Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

}
#endregion