using UnityEngine;
using System;

public static class ExtensionMethods
{
    public static int RandomIndex(this Array array) => UnityEngine.Random.Range(0, array.Length);

    public static T GetRandomElement<T>(this T[] array) => array[array.RandomIndex()];
}