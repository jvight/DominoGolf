using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataController : MonoBehaviour
{
    public string GameName = "DominoGolf";
    // Start is called before the first frame update    
    public static List<int> GetArrayForKey(string key)
    {
        string[] value = PlayerPrefs.GetString(key).Split(char.Parse(","));
        List<int> list = new List<int>();
        if (value.Length == 1) { return list; }
        for (int i = 0; i < value.Length; i++)
        {
            list.Add(Int32.Parse(value[i]));
        }
        return list;
    }

    public static void SetArrayForKey(string key, List<int> data)
    {
        string str = "";
        for (int i = 0; i < data.Count; i++)
        {
            str += data[i].ToString();
            if(i < data.Count) {
                str += ",";
            }   
        }
        PlayerPrefs.SetString(key, str);
    }
}
