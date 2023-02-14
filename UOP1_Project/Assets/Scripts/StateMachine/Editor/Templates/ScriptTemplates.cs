using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

internal class ScriptTemplates
{
    private static readonly string _path = "Assets/Scripts/StateMachine/Editor/Templates";

    [MenuItem("Assets/Create/State Machines/Action Script", false, 0)]
    public static void CreateActionScript()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                                                                ScriptableObject.CreateInstance<DoCreateStateMachineScriptAsset>(),
                                                                "NewActionSO.cs",
                                                                (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image,
                                                                $"{_path}/StateAction.txt");
    }

    [MenuItem("Assets/Create/State Machines/Condition Script", false, 0)]
    public static void CreateConditionScript()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                                                                ScriptableObject.CreateInstance<DoCreateStateMachineScriptAsset>(),
                                                                "NewConditionSO.cs",
                                                                (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image,
                                                                $"{_path}/StateCondition.txt");
    }

    private class DoCreateStateMachineScriptAsset : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            var text = File.ReadAllText(resourceFile);

            var fileName = Path.GetFileName(pathName);
            {
                var newName = fileName.Replace(" ", "");
                if (!newName.Contains("SO"))
                {
                    newName = newName.Insert(fileName.Length - 3, "SO");
                }

                pathName = pathName.Replace(fileName, newName);
                fileName = newName;
            }

            var fileNameWithoutExtension = fileName.Substring(0, fileName.Length - 3);
            text = text.Replace("#SCRIPTNAME#", fileNameWithoutExtension);

            var runtimeName = fileNameWithoutExtension.Replace("SO", "");
            text = text.Replace("#RUNTIMENAME#", runtimeName);

            for (var i = runtimeName.Length - 1; i > 0; i--)
            {
                if (char.IsUpper(runtimeName[i]) && char.IsLower(runtimeName[i - 1]))
                {
                    runtimeName = runtimeName.Insert(i, " ");
                }
            }

            text = text.Replace("#RUNTIMENAME_WITH_SPACES#", runtimeName);

            var fullPath = Path.GetFullPath(pathName);
            var encoding = new UTF8Encoding(true);
            File.WriteAllText(fullPath, text, encoding);
            AssetDatabase.ImportAsset(pathName);
            ProjectWindowUtil.ShowCreatedAsset(AssetDatabase.LoadAssetAtPath(pathName, typeof(Object)));
        }
    }
}