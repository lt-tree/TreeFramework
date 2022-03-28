using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateUIScriptUtil
{
    public static void CreateUIScript(GameObject gameObj)
    {
        string gameObjectName = gameObj.name;
        string className = gameObjectName + "_UIControl";
        StreamWriter sw = null;

        string filePath = Application.dataPath + "/Scripts/Business/Controller/" + className + ".cs";

        if (File.Exists(filePath))
        {
            return;
        }

        sw = new StreamWriter(filePath);
        sw.WriteLine("using UnityEngine;\nusing System.Collections;\nusing UnityEngine.UI;\nusing System.Collections.Generic;\n");
        sw.WriteLine("public class " + className + " : UIController {\n");

        sw.WriteLine("\t" + "public override void Awake() {");
        sw.WriteLine("\t\t" + "base.Awake();" + "\n");
        sw.WriteLine("\t" + "}" + "\n");

        sw.WriteLine("\t" + "void Start() {");
        sw.WriteLine("\t" + "}" + "\n");
        sw.WriteLine("}");

        sw.Flush();
        sw.Close();

        Debug.Log("Export File: " + filePath);

    }
}
