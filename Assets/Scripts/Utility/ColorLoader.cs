using System.Collections;
using UnityEngine;

public static class ColorLoader
{

    public static Color HexToColor(string hex, float alpha = 1.0f)
    {
        Color color = Color.black;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            color.a = alpha;
        }
        else
        {
            Debug.LogWarning("A Hexadecimal value to color parsing failed!");
        }

        return color;
    }

}