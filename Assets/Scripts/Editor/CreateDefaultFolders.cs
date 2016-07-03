using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

internal class CreateDefaultFolders
{
    [MenuItem("Playing Minds/Create Default Folders")]
    private static void CreateFolders()
    {
        CreateDirectory("Animations");
        CreateDirectory("External Assets");
        CreateDirectory("Materials");
        CreateDirectory("Models");
        CreateDirectory("Prefabs");
        CreateDirectory("Scripts");
        CreateDirectory("Shaders");
        CreateDirectory("Audio");
        CreateDirectory("Audio/SFX");
        CreateDirectory("Audio/Music");
        CreateDirectory("Textures");
        CreateDirectory("Sprites");
        CreateDirectory("Scenes");
        CreateDirectory("Scenes/Main");
        CreateDirectory("Scenes/Sandbox");
        AssetDatabase.Refresh();
    }

    private static void CreateDirectory(string name)
    {
        string path = Path.Combine(Application.dataPath, name);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}