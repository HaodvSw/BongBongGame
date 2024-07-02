using System.Collections;
using UnityEngine;

public static class Util
{
    public static string KEY_LANGUAGE = "KEY_LANGUAGE";
    public static string KEY_SOUND = "KEY_SOUND";
    public static string KEY_GUIDE = "KEY_GUIDE";

    public static string[] RemoveArray(string[] input, int index)
    {
        string[] result = new string[input.Length - 1];
        for (int i = 0; i < input.Length - 1; i++)
        {
            if (i < index)
            {
                result[i] = input[i];
            }
            else
            {
                result[i] = input[i + 1];
            }
        }
        return result;
    }

    public static void SetData(string value, string keyValue)
    {
        string loadValue = PlayerPrefs.GetString(keyValue);
        if (!value.Equals(loadValue))
        {
            PlayerPrefs.SetString(keyValue, value);
            PlayerPrefs.Save();
        }
    }

    public static string getData(string keyValue)
    {
        return PlayerPrefs.GetString(keyValue);
        
    }

 
}
