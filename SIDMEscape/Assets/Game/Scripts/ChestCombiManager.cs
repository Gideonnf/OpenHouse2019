﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Runtime.InteropServices;

public class ChestCombiManager : MonoBehaviour
{
    [SerializeField]
    GameObject go_clock; // Clock GO for time to set combi

    int[,] arr_chestCombi;
    int[,] arr_chestBlitzCombi;

    // input buffer to compare code;
    public List<int> arr_testingCombi;

    // Start is called before the first frame update
    void Start()
    {
        arr_chestBlitzCombi = new int[,] { { 0, 3, 2, 5 }, { 1, 1, 1, 2 }, { 2, 0, 0, 8 } };

        arr_chestCombi = new int[, ] { { 5, 6, 5, 3 }, { 2, 6, 3, 6 }, { 1, 7, 4, 8 } };
        arr_testingCombi = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (arr_testingCombi.Count == 4)
        {
            if (!GameManager.GetInstance().b_blitzMode)
            {
                if (arr_testingCombi.ToArray().SequenceEqual(arr_chestCombi.GetRow((int)go_clock.GetComponent<ClockRandomiser>().n_clockStates - 1)))
                {
                    Debug.LogError("Do not panic, it works");
                }
                else
                {
                    arr_testingCombi.Clear();
                }
            }
            else
            {
                if (arr_testingCombi.ToArray().SequenceEqual(arr_chestBlitzCombi.GetRow((int)go_clock.GetComponent<ClockRandomiser>().n_clockStates - 1)))
                {
                    Debug.LogError("Do not panic, it works");
                }
                else
                {
                    arr_testingCombi.Clear();
                }
            }

        }
    }

}

public static class ArrayExt
{
    public static T[] GetRow<T>(this T[,] array, int row)
    {
        if (!typeof(T).IsPrimitive)
            throw new InvalidOperationException("Not supported for managed types.");

        if (array == null)
            throw new ArgumentNullException("array");

        int cols = array.GetUpperBound(1) + 1;
        T[] result = new T[cols];

        int size;

        if (typeof(T) == typeof(bool))
            size = 1;
        else if (typeof(T) == typeof(char))
            size = 2;
        else
            size = Marshal.SizeOf<T>();

        Buffer.BlockCopy(array, row * cols * size, result, 0, cols * size);

        return result;
    }
}
