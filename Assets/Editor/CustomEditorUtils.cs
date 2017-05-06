using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class CustomEditorUtils {

    public static void Header(string label)
    {
        GUILayout.Space(10);
        GUILayout.Label(label, EditorStyles.boldLabel);
    }

    public static Texture2D TextureField(string label, string tooltip, Texture2D obj)
    {
        return (Texture2D)EditorGUILayout.ObjectField(
            new GUIContent(label, tooltip),
            obj,
            typeof(Texture2D),
            false,
            GUILayout.Height(EditorGUIUtility.singleLineHeight));
    }

    public static bool Button(string text)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        var b = GUILayout.Button(text);
        GUILayout.Space(5);
        GUILayout.EndHorizontal();
        return b;
    }

    public static int MinIntField(string label, string tooltip, int val, int min)
    {
        var i = EditorGUILayout.IntField(new GUIContent(label, tooltip), val);
        if (i < min) i = min;
        return i;
    }

    public static ListState<T> List<T>(string label, ListState<T> list, Func<int, T, T> populateFunc)
    {
        list.collapsed = EditorGUILayout.Foldout(list.collapsed, label);
        if (list.collapsed)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            GUILayout.BeginVertical();

            var size = EditorGUILayout.DelayedIntField(new GUIContent("Size"), list.size);
            if (size < 0)
                size = 0;

            // check if list size changed
            if (list.size != size)
            {
                // old array is empty
                if (list.size == 0)
                {
                    list.data = new T[size];
                    list.rowsCollapsed = new bool[size];
                }   
                else
                {
                    list.rowsCollapsed = ResizeArray(list.rowsCollapsed, size);
                    list.data = ResizeArray(list.data, size);
                }
                list.size = size;
            }
            
            // populate the list
            for (var i = 0; i < list.size; i++)
            {
                list.rowsCollapsed[i] = EditorGUILayout.Foldout(list.rowsCollapsed[i], "Element " + i);
                if (list.rowsCollapsed[i])
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    GUILayout.BeginVertical();
                    list.data[i] = populateFunc(i, list.data[i]);
                    GUILayout.EndVertical();
                    GUILayout.EndHorizontal();
                    
                }
            }

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        return list;
    ;}

    private static T[] ResizeArray<T>(T[] source, int targetSize)
    {
        var newData = new T[targetSize];
        Array.Copy(source, newData, Math.Min(source.Length, targetSize));
        return newData;
    }

    public class ListState<T>
    {
        public bool collapsed;
        public int size;
        public bool[] rowsCollapsed = new bool[0];
        public T[] data = new T[0];
    }
}
