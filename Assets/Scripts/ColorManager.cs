using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;
    public Color[] CellColors;

    [Space(5)] public Color NumbersDarkColor;
    public Color NumbersLightColor;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}