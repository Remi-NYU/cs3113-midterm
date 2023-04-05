using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Records
{
    public struct Record
    {
        public float time;
        public bool isSet;
    }

    public static Record[] records = new Record[4];
    public static bool initialized = false;

    public static void Initialize()
    {
        if (!initialized)
        {
            for (int i = 0; i < records.Length; i++)
            {
                records[i].isSet = false;
            }
            initialized = true;
        }
    }

    public static void SetRecord(int level, float time)
    {
        int index = level - 1;
        if (records[index].isSet)
        {
            if (time < records[index].time)
            {
                records[index].time = time;
            }
        }
        else
        {
            records[index].time = time;
            records[index].isSet = true;
        }
    }

    public static float GetRecord(int level)
    {
        int index = level - 1;
        if (records[index].isSet)
        {
            return records[index].time;
        }
        else
        {
            return -1;
        }
    }
}
