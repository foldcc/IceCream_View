using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;

public static class IceCreamCodeTemplateEditor
{
    private static Texture2D scriptIcon = (EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D);

    [MenuItem("Assets/Create/IceCreamView/IceCream ViewModule C# Script", false, 89)]
    private static void CreateViewModuleCode()
    {
        string[] guids = AssetDatabase.FindAssets("ViewAbstractModuleTemplate.cs");
        if (guids.Length == 0)
        {
            Debug.LogError("ViewAbstractModuleTemplate.cs.txt not found in asset database");
            return;
        }
        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        CreateFromTemplate("NewViewModule.cs", path);
    }

    [MenuItem("Assets/Create/IceCreamView/IceCream View C# Script", false, 89)]
    private static void CreateViewCode()
    {
        string[] guids = AssetDatabase.FindAssets("ViewAbstractTemplate.cs");
        if (guids.Length == 0)
        {
            Debug.LogError("ViewAbstractTemplate.cs.txt not found in asset database");
            return;
        }
        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        CreateFromTemplate("NewView.cs", path);
    }

    public static void CreateFromTemplate(string initialName, string templatePath)
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            ScriptableObject.CreateInstance<DoCreateCodeFile>(),
            initialName,
            scriptIcon,
            templatePath
        );
    }

    public class DoCreateCodeFile : UnityEditor.ProjectWindowCallback.EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            Object o = CreateScript(pathName, resourceFile);
            ProjectWindowUtil.ShowCreatedAsset(o);
        }
    }

    internal static UnityEngine.Object CreateScript(string pathName, string templatePath)
    {
        string className = Path.GetFileNameWithoutExtension(pathName).Replace(" ", string.Empty);
        string templateText = string.Empty;

        UTF8Encoding encoding = new UTF8Encoding(true, false);

        if (File.Exists(templatePath))
        {
            StreamReader reader = new StreamReader(templatePath);
            templateText = reader.ReadToEnd();
            reader.Close();

            templateText = templateText.Replace("#SCRIPTNAME#", className);
            templateText = templateText.Replace("#NOTRIM#", string.Empty);

            StreamWriter writer = new StreamWriter(Path.GetFullPath(pathName), false, encoding);
            writer.Write(templateText);
            writer.Close();

            AssetDatabase.ImportAsset(pathName);
            return AssetDatabase.LoadAssetAtPath(pathName, typeof(Object));
        }
        else
        {
            Debug.LogError(string.Format("The template file was not found: {0}", templatePath));
            return null;
        }
    }
}