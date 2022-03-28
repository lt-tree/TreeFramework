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
        window.titleContent.text = "����UI��ͼ����";
        window.Show();
    }


    public void OnGUI()
    {
        GUILayout.Label("��ѡ��UI��ͼ���ڵ㣺");
        if(GUILayout.Button("���ɴ���"))
        {
            if(Selection.activeGameObject != null)
            {
                Debug.Log("��ʼ���ɴ���...");
                CreateUIScriptUtil.CreateUIScript(Selection.activeGameObject);
                Debug.Log("�����");

                AssetDatabase.Refresh();
            }
        }

        if(Selection.activeGameObject != null)
        {
            GUILayout.Label("��ǰѡ�нڵ�: " + Selection.activeGameObject.name);
        }
        else
        {
            GUILayout.Label("��ERROR�� û��ѡ�е�UI�ڵ㣬�޷����ɴ��룡");
        }
    }

    public void OnSelectionChange()
    {
        this.Repaint();
    }
}
