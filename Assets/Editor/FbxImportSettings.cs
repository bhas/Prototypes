using UnityEditor;

public class FbxImporter : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        var importer = assetImporter as ModelImporter;
        var name = importer.assetPath.ToLower();
        if (name.Substring(name.Length - 4, 4) == ".fbx")
        {
            // ==============  Model =============
            // Meshes
            importer.globalScale = 1;
            importer.useFileUnits = false;
            importer.meshCompression = ModelImporterMeshCompression.Off;
            importer.isReadable = false;
            importer.optimizeMesh = true;
            importer.importBlendShapes = false;
            importer.addCollider = false;
            importer.keepQuads = false;
            importer.weldVertices = true;
            importer.swapUVChannels = false;
            importer.generateSecondaryUV = false;
            // Normals & Tangents
            importer.importNormals = ModelImporterNormals.Import;
            importer.importTangents = ModelImporterTangents.None;
            // Materials
            importer.importMaterials = true;
            importer.materialName = ModelImporterMaterialName.BasedOnMaterialName;
            importer.materialSearch = ModelImporterMaterialSearch.Local;

            // ==============  Rig =============
            importer.animationType = ModelImporterAnimationType.None;

            // ==============  Animations =============
            importer.importAnimation = false;
        }
    }
}
