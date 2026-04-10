using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace FallenEditorTool
{
    public static class LayoutFinder
    {
        public static List<string> GetCustomLayoutNames()
        {
            // Windows: %AppData%/Unity/Editor-5.x/Preferences
            // macOS: ~/Library/Preferences/Unity/Editor-5.x
            // Linux: ~/.config/unity3d/Preferences
            //maybe this are path...
            string preferencesPath = InternalEditorUtility.unityPreferencesFolder;
            string layoutsPath = Path.Combine(preferencesPath, "Layouts", "default");


            if (!Directory.Exists(layoutsPath))
            {
                Debug.LogWarning("Layouts directory not found at: " + layoutsPath);
                return new List<string>();
            }
            return Directory.GetFiles(layoutsPath, "*.wlt")
                .Select(Path.GetFileNameWithoutExtension)
                .ToList();
        }
    } 
}
