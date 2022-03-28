using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExportUIScript : EditorWindow
{
    [MenuItem("Quick/Export UI Script")]
    public static void ExportUI()
    {
        ExportUIScript window = EditorWindow.GetWindow<ExportUIScript>();
        window.titleContent.text = "生成UI视图代码";
        window.Show();
    }


    public void OnGUI()
    {
        GUILayout.Label("请选择UI视图根节点：");
        if(GUILayout.Button("生成代码"))
        {
            if(Selection.activeGameObject != null)
            {
                Debug.Log("开始生成代码...");
                CreateUIScriptUtil.CreateUIScript(Selection.activeGameObject);
                Debug.Log("已完成");

                AssetDatabase.Refresh();
            }
        }

        if(Selection.activeGameObject != null)
        {
            GUILayout.Label("当前选中节点: " + Selection.activeGameObject.name);
        }
        else
        {
            GUILayout.Label("【ERROR】 没有选中的UI节点，无法生成代码！");
        }
    }

    public void OnSelectionChange()
    {
        this.Repaint();
    }
}
