using UnityEngine;
using UnityEditor;

public class IsoLevelToolWindow : EditorWindow
{
    private LevelToolInput input = new LevelToolInput();
    private CustomEditorUtils.ListState<LevelToolInput.PaletteEntry> colorPalette = new CustomEditorUtils.ListState<LevelToolInput.PaletteEntry>();
    private Vector2 scrollPos;

    // Add menu item
    [MenuItem("Isometric/Level tool")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(IsoLevelToolWindow), false, "Level");
    }

    // The window gets created here
    void OnGUI()
    {
        GUILayout.Space(10);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        input.maxHeight = CustomEditorUtils.MinIntField("Max Height", "The maximum height the terrain can have", input.maxHeight, 1);

        // Textures
        CustomEditorUtils.Header("Textures");
        input.surfaceTex = CustomEditorUtils.TextureField("Surface Texture",
            "A RGB texture that indicates the surface type of the terrain. Each color will be mapped to a matching surface type.",
            input.surfaceTex);
        input.heightTex = CustomEditorUtils.TextureField("Height Texture",
            "A grayscale texture that indicates the height of the terrain. It maps it from 0 (black) to max height (" +
            input.maxHeight + ") (white).", input.heightTex);

        // Colors
        CustomEditorUtils.Header("Colors");
        CustomEditorUtils.List("Color Palette", colorPalette, Populate);

        EditorGUILayout.EndScrollView();

        // Error/info box
        var error = ValidateInput();
        if (error == null)
        {
            EditorGUILayout.HelpBox(
                string.Format("Map size: {0} x {1} x {2}", input.surfaceTex.width, input.maxHeight,
                    input.surfaceTex.height),
                MessageType.Info);
        }
        else
            EditorGUILayout.HelpBox(error, MessageType.Error);

        // Button
        using (new EditorGUI.DisabledScope(error != null))
        {
            if (CustomEditorUtils.Button("Generate Level"))
            {
                input.colorPalette = colorPalette.data;
                IsoLevelTool.GenerateMap(input);
            }
        }

        
        GUILayout.Space(10);
    }

    // Validate input and return error message (returns null if the input is valid)
    private string ValidateInput()
    {
        // if textures are not set
        if (input.surfaceTex == null || input.heightTex == null)
            return "Missing textures";
        
        // if textures does not share same dimensions
        if (input.surfaceTex.width != input.heightTex.width || input.surfaceTex.height != input.heightTex.height)
            return "Textures must have same size";

        // no colors set
        if (colorPalette.size == 0)
            return "Color palette must contain at least one color.";
        

        return null;
    }

    private LevelToolInput.PaletteEntry Populate(int i, LevelToolInput.PaletteEntry oldVal)
    {
        oldVal.color = EditorGUILayout.ColorField("Color", oldVal.color);
        oldVal.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", oldVal.prefab, typeof(GameObject), false);
        return oldVal;
    }
}