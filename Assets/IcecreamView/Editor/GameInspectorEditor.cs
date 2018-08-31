using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IcecreamView
{
    [CustomEditor(typeof(GameViewAbstract), true), CanEditMultipleObjects]
    public class GameViewAbstractEditor : Editor
    {
        public static Texture2D GetTexture2D(Color32 color32) {
            Texture2D texturesss = new Texture2D(4, 4);
            Color32[] colors = texturesss.GetPixels32();
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = color32;
            }
            texturesss.SetPixels32(colors);
            texturesss.Apply();
            return texturesss;
        }

        public override void OnInspectorGUI()
        {
            GUIStyle gUIStyle = new GUIStyle();
            
            gUIStyle.fontSize = 20;
            gUIStyle.normal.textColor = Color.black;
            gUIStyle.normal.background = GameViewAbstractEditor.GetTexture2D(new Color32(0, 205, 255, 100));
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            gUIStyle.margin = new RectOffset(0, 0, 10, 0);

            GUILayout.Space(10);
            GUILayout.Box(" Game View ", gUIStyle);

            gUIStyle = new GUIStyle();
            gUIStyle.fontSize = 16;
            gUIStyle.normal.textColor = Color.white;
            gUIStyle.normal.background = GameViewAbstractEditor.GetTexture2D(new Color32(77, 77, 77, 255));
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            GUILayout.Box(this.target.GetType().Name, gUIStyle);

            EditorGUILayout.HelpBox("游戏界面基础组件" , MessageType.None);
            GUILayout.Space(10);
            gUIStyle = null;
            base.DrawDefaultInspector();
            this.serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomEditor(typeof(GameViewModuleConnector), true) , CanEditMultipleObjects]
    public class GameViewModuleConnectorEditor : Editor {
        public override void OnInspectorGUI()
        {
            GUIStyle gUIStyle = new GUIStyle();

            gUIStyle.fontSize = 20;
            gUIStyle.normal.textColor = Color.black;
            gUIStyle.normal.background = GameViewAbstractEditor.GetTexture2D(new Color32(255, 0, 99, 100));
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            gUIStyle.margin = new RectOffset(0, 0, 10, 0);


            GUILayout.Space(10);
            GUILayout.Box(" Game View Connect ", gUIStyle);
            EditorGUILayout.HelpBox("模块管理器,用于关联并激活该对象上的所有< Game View Module >", MessageType.None);
            GUILayout.Space(10);
            gUIStyle = null;
            this.serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomEditor(typeof(GameViewAbstractModule), true), CanEditMultipleObjects]
    public class GameViewAbstractModuleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GUIStyle gUIStyle = new GUIStyle();

            gUIStyle.fontSize = 20;
            gUIStyle.normal.textColor = Color.black;
            gUIStyle.normal.background = GameViewAbstractEditor.GetTexture2D(new Color32(147, 255, 0, 100));
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            gUIStyle.margin = new RectOffset(0, 0, 10, 0);


            GUILayout.Space(10);
            GUILayout.Box(" Game View Module ", gUIStyle);

            gUIStyle = new GUIStyle();
            gUIStyle.fontSize = 16;
            gUIStyle.normal.textColor = Color.white;
            gUIStyle.normal.background = GameViewAbstractEditor.GetTexture2D(new Color32(77, 77, 77, 255));
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            GUILayout.Box(this.target.GetType().Name , gUIStyle);

            EditorGUILayout.HelpBox("界面功能模块组件,允许多模块自由组合", MessageType.None);
            GUILayout.Space(10);
            gUIStyle = null;
            base.DrawDefaultInspector();
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}

