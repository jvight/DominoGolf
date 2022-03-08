using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataController : MonoBehaviour
{
    public string GameName = "DominoGolf";
    // Start is called before the first frame update    
    public static List<int> getArrayForKey(string key)
    {
        string[] value = PlayerPrefs.GetString(key).Split(char.Parse(","));
        List<int> list = new List<int>();
        for (int i = 0; i < value.Length; i++)
        {
            list.Add(Int32.Parse(value[i]));
        }
        return list;
    }

    public static void setArrayForKey(string key, string defaultValue)
    {
        PlayerPrefs.SetString(key, defaultValue);
    }
}
