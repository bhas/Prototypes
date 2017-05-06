using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class IsoLevelTool
{

    public static void GenerateMap(LevelToolInput input)
    {
        // process texture data
        var paletteColors = input.colorPalette.Select(entry => entry.color).ToArray();
        var surfaces = ProcessSurfaceTexture(input.surfaceTex, paletteColors);
        var heights = ProcessHeightTexture(input.heightTex, input.maxHeight);

        // generate blocks
        var parent = new GameObject("Surface");
        for (var x = 0; x <  input.surfaceTex.width; x++)
        {
            for (var y = 0; y < input.surfaceTex.height; y++)
            {
                var index = surfaces[x, y];
                var h = heights[x, y];
                var prefab = input.colorPalette[index].prefab;
                if (prefab != null)
                {
                    var pos = new Vector3(x, 0, y);
                    var obj = GameObject.Instantiate(prefab, pos * 0.2f, Quaternion.identity, parent.transform);
                    obj.transform.localScale = new Vector3(1, h+1, 1);
                }
            }
        }
    }

    // maps all colors into an index in the palette
    private static int[,] ProcessSurfaceTexture(Texture2D texture, Color[] palette)
    {
        var result = new int[texture.width, texture.height];
        for (var x = 0; x < texture.width; x++)
        {
            for (var y = 0; y < texture.height; y++)
            {
                result[x, y] = MapColor(texture.GetPixel(x, y), palette);
            }
        }

        return result;
    }

    // maps all colors into a height between 0 and maxHeight (inclusive)
    private static int[,] ProcessHeightTexture(Texture2D texture, int maxHeight)
    {
        var result = new int[texture.width, texture.height];
        for (var x = 0; x < texture.width; x++)
        {
            for (var y = 0; y < texture.height; y++)
            {
                var gray = texture.GetPixel(x, y).grayscale;
                result[x, y] = (int)(gray * maxHeight);
            }
        }

        return result;
    }

    // maps an abitrary color c to the best match in the color palette. Returns the index of the best color
    private static int MapColor(Color c, Color[] palette)
    {
        var minDiff = ColorDiff(c, palette[0]);
        var index = 0;
        if (minDiff == 0)
            return index;

        for (var i = 1; i < palette.Length; i++)
        {
            var diff = ColorDiff(c, palette[i]);

            if (diff < 0.001)
                return i;

            if (diff < minDiff)
            {
                index = i;
                minDiff = diff;
            }
        }

        return index;
    }

    // returns the difference between two colors
    private static float ColorDiff(Color c1, Color c2)
    {
        return Mathf.Abs(c1.r - c2.r) + Mathf.Abs(c1.g - c2.g) + Mathf.Abs(c1.b - c2.b);
    }
}

public class LevelToolInput
{
    public int maxHeight;
    public Texture2D surfaceTex;
    public Texture2D heightTex;
    public bool optimizeMesh;
    public PaletteEntry[] colorPalette;

    public struct PaletteEntry
    {
        public Color color;
        public GameObject prefab;
    }
}
