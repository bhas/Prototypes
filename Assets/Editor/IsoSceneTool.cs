using UnityEngine;
using UnityEditor;
using UnityEditorInternal.VersionControl;

public class LevelToolWindow : EditorWindow
{
    private int height;
    private Texture2D surfaceTex;
    private Texture2D heightTex;
    private bool showColors = true;
    private bool hasCorrectDims, hasTextures;
    private int colorLength;
    private CustomEditorUtils.ListState<PaletteEntry> colorPalette = new CustomEditorUtils.ListState<PaletteEntry>();

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Isometric/Level tool")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(LevelToolWindow), false, "Level");
    }

    void OnGUI()
    {
        GUILayout.Space(10);
        height = CustomEditorUtils.MinIntField("Max Height", "The maximum height the terrain can have", height, 1);

        AddSectionTextures();
        AddSectionColors();

        using (new EditorGUI.DisabledScope(!hasTextures || !hasCorrectDims))
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (CustomEditorUtils.Button("Generate Level"))
            {
                GenerateMap();
            }
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
        }
    }

    private void AddSectionColors()
    {
        CustomEditorUtils.Header("Colors");

        CustomEditorUtils.List("Color Palette", colorPalette, Populate);
    }

    private PaletteEntry Populate(int i, PaletteEntry oldVal)
    {
        oldVal.color = EditorGUILayout.ColorField("Color", oldVal.color);
        oldVal.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", oldVal.prefab, typeof(GameObject), false);
        return oldVal;
    }

    private void AddSectionTextures()
    {
        CustomEditorUtils.Header("Textures");
        surfaceTex = CustomEditorUtils.TextureField("Surface Texture",
            "A RGB texture that indicates the surface type of the terrain. Each color will be mapped to a matching surface type.",
            surfaceTex);
        heightTex = CustomEditorUtils.TextureField("Height Texture",
            "A grayscale texture that indicates the height of the terrain. It maps it from 0 (black) to max height (" +
            height + ") (white).", heightTex);


        hasTextures = surfaceTex != null && heightTex != null;
        if (hasTextures)
        {
            hasCorrectDims = surfaceTex.width == heightTex.width ||
                             surfaceTex.height == heightTex.height;

            if (!hasCorrectDims)
                EditorGUILayout.HelpBox("Textures must have same size", MessageType.Error);
            else
                EditorGUILayout.HelpBox(
                    string.Format("Map size: {0} x {1} x {2}", surfaceTex.width, height, surfaceTex.height),
                    MessageType.Info);
        }
        else
        {
            EditorGUILayout.HelpBox("Missing textures", MessageType.Error);
        }
    }

    private void GenerateMap()
    {
        var data = surfaceTex.GetPixels();

        for (var x = 0; x < surfaceTex.width; x++)
        {
            for (var y = 0; y < surfaceTex.height; y++)
            {
                var c = data[x][y];
                var x2 = 0;
            }
        }

        Debug.Log("Baaaaam");
    }

    public struct PaletteEntry
    {
        public Color color;
        public GameObject prefab;
    }
}