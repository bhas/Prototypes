using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FbxImporter : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        var importer = assetImporter as ModelImporter;
        var name = importer.assetPath.ToLower();
        if (name.Substring(name.Length - 4, 4) == ".fbx")
        {
            importer.globalScale = 10000 * 500;
            importer.generateAnimations = ModelImporterGenerateAnimations.None;
            importer.importMaterials = false;
            importer.importAnimation = false;
            importer.importBlendShapes = false;
            importer.importTangents = ModelImporterTangents.None;
        }
    }
}
