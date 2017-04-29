using System;
using UnityEngine;

[Serializable]
public class Test
{
    public Color color;
    public GameObject prefab;
}

public class ImageProcessor : MonoBehaviour
{
    public Test[] array;
    public int[] ints;

    [Header("The dimensions of the textures")]
    public int width;
    public int height;

    [Header("The textures")]
    [Tooltip("A RGB texture that indicates the surface type of the terrain. Each color will be mapped to a matching surface type.")]
    public Texture2D surfaceTex;
    [Tooltip("A grayscale texture that indicates the height of the terrain. It maps it from minHeigt (white) to maxHeight (black).")]
    public Texture2D heightTex;
    [Tooltip("The generated output will be stored in this texture.")]
    public Texture2D destText;

	// Use this for initialization
	void Start ()
	{
	    FixTextureFormat();

	}

    public void FixTextureFormat()
    {
        destText = new Texture2D(width, height, TextureFormat.RGHalf, false);
        

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                destText.SetPixel(i, j, Color.black);
            }
        }
        destText.Apply();
    }
}
