﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = new GameManager();

    private GameManager() { }

    public static GameManager GetInstance()
    {
        return instance;
    }
    #endregion

    [SerializeField]
    bool b_blitzMode;

    public bool getBlitzMode()
    {
        return b_blitzMode;
    }
    public void setBlitzMode(bool _value)
    {
        b_blitzMode = _value;
    }

    // Use this for initialization
    void Start()
    {
        b_blitzMode = false;
    }

    // Update is called once per frame
    void Update()
    {

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