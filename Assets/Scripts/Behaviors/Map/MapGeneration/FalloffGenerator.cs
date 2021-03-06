﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FalloffGenerator
{
    public static float a = 2f;
    public static float b = 3.2f;
    public static float[,] GenerateFalloffMap(int width, int height)
    {
        float[,] map = new float[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float x = i / (float)width * 2 - 1;
                float y = j / (float)height * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                value = Smoothout(value, a, b);
                map[i, j] = value;
            }
        }

        return map;
    }

    private static float Smoothout(float value, float a, float b)
    {
        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}
