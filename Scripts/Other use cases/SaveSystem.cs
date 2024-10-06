using System;
using System.Reflection;
using UnityEngine;

public class SaveSystem
{
    public static void SaveObjectState(object obj)
    {
        FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            // Save the field's value
            object value = field.GetValue(obj);
            Debug.Log($"Saving: {field.Name} = {value}");
        }
    }

    public static void LoadObjectState(object obj)
    {
        FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            // Load from a file or database
            object savedValue = null;
            field.SetValue(obj, savedValue);
        }
    }
}
