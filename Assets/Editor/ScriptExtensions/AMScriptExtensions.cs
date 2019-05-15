﻿using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.ProjectWindowCallback;
using System.Text;
using System.Collections.Generic;

// https://bitbucket.org/liortal/code-templates/src/928d36fa990897e02be2cedbc0946150a4a58a71/Assets/CodeTemplates/Editor/CodeTemplates.cs?at=master&fileviewer=file-view-default

public class AMScriptExtensions : Editor
{
    /// <summary>
    /// The C# script icon.
    /// </summary>
    private static Texture2D scriptIcon = (EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D);

    [MenuItem("Assets/Create/AM C# Script", priority = 0)]
    private static void CreateScript()
    {
        CodeTemplates.CreateFromTemplate("NewScript.cs", @"Assets/Editor/ScriptExtensions/Template/ScriptTemplate.txt");
    }

    [MenuItem("Assets/Create/AM C# Interface", priority = 0)]
    private static void CreateInterface()
    {
        CodeTemplates.CreateFromTemplate("NewInterface.cs", @"Assets/Editor/ScriptExtensions/Template/InterfaceTemplate.txt");
    }

    [MenuItem("Assets/Create/AM MonoBehaviour C# Script", priority = 0)]
    private static void CreateMonoBehaviourScript()
    {
        CodeTemplates.CreateFromTemplate("NewMonoBehaviourScript.cs", @"Assets/Editor/ScriptExtensions/Template/MonoBehaviourTemplate.txt");
    }
}

public class DoCreateCodeFile : EndNameEditAction
{
    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        Object o = CodeTemplates.CreateScript(pathName, resourceFile);
        ProjectWindowUtil.ShowCreatedAsset(o);
    }
}

/// <summary>
/// Editor class for creating code files from templates.
/// </summary>
public class CodeTemplates
{
    /// <summary>
    /// The C# script icon.
    /// </summary>
    private static Texture2D scriptIcon = (EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D);

    internal static UnityEngine.Object CreateScript(string pathName, string templatePath)
    {
        string newFilePath = Path.GetFullPath(pathName);
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);
        string className = NormalizeClassName(fileNameWithoutExtension);

        string templateText = string.Empty;

        if (File.Exists(templatePath))
        {
            using (var sr = new StreamReader(templatePath))
            {
                templateText = sr.ReadToEnd();
            }

            UTF8Encoding encoding = new UTF8Encoding(true, false);

            Debug.Log(newFilePath);
            string namespaceStr = null;
            List<string> splitedPath = new List<string>(newFilePath.Split('\\', '/'));
            var assetsIndex = splitedPath.IndexOf("Assets");

            if (splitedPath.Contains("Scripts"))
            {
                splitedPath.Remove("Scripts");
            }

            for (int index = assetsIndex + 1; index < splitedPath.Count; index++)
            {
                var path = splitedPath[index];
                if (path.Contains(".cs"))
                {
                    continue;
                }
                else if (path.Contains("-"))
                {
                    path = path.Replace('-', '_');
                }
                namespaceStr += $".{path}";
            }

            templateText = templateText.Replace("#SCRIPTNAME#", className);
            templateText = templateText.Replace("#NAMESPACE#", $"AM{namespaceStr}");

            using (var sw = new StreamWriter(newFilePath, false, encoding))
            {
                sw.Write(templateText);
            }

            AssetDatabase.ImportAsset(pathName);

            return AssetDatabase.LoadAssetAtPath(pathName, typeof(Object));
        }
        else
        {
            Debug.LogError(string.Format("The template file was not found: {0}", templatePath));

            return null;
        }
    }

    private static string NormalizeClassName(string fileName)
    {
        return fileName.Replace(" ", string.Empty);
    }

    /// <summary>
    /// Creates a new code file from a template file.
    /// </summary>
    /// <param name="initialName">The initial name to give the file in the UI</param>
    /// <param name="templatePath">The full path of the template file to use</param>
    public static void CreateFromTemplate(string initialName, string templatePath)
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            ScriptableObject.CreateInstance<DoCreateCodeFile>(),
            initialName,
            scriptIcon,
            templatePath);
    }
}
