using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class UIController : MonoBehaviour
{
    public Dictionary<string, GameObject> view = new Dictionary<string, GameObject>();

    private void LoadAllObjects(GameObject obj, string path)
    {
        foreach(Transform tf in obj.transform)
        {
            string fPath = path + tf.gameObject.name;
            if(this.view.ContainsKey(fPath))
            {
                continue;
            }
            this.view.Add(fPath, tf.gameObject);
            LoadAllObjects(tf.gameObject, fPath + "/");
        }
    }

    public virtual void Awake()
    {
        this.LoadAllObjects(this.gameObject, "");
    }

    public void registButtonListener(string viewName, UnityAction onClick)
    {
        Button btn = this.view[viewName].GetComponent<Button>();
        if(btn == null)
        {
            Debugger.LogWarning("[UIController - registButtonListener]  Not Button Component!");
            return;
        }
        btn.onClick.AddListener(onClick);
    }

    public void registSliderListener(string viewName, UnityAction<float> onValueChange)
    {
        Slider slider = this.view[viewName].GetComponent<Slider>();
        if(slider == null)
        {
            Debugger.LogWarning("[UIContoller - registSliderListener]  Not Slider Component!");
            return;
        }
        slider.onValueChanged.AddListener(onValueChange);
    }
}



public class UIManager:UnitySingleton<UIManager>
{
    public GameObject canvas;

    public override void Awake()
    {
        base.Awake();
        this.canvas = GameObject.Find("Canvas");
        if(this.canvas == null)
        {
            Debugger.LogError("[UIManager - Awake]  Load Canvas Failed!");
        }
    }

    public UIController ShowUIView(string name)
    {
        string path = "GUI/prefab/" + name + ".prefab";
        GameObject uiPrefab = (GameObject)ResourceManager.Instance.GetAssetCache<GameObject>(path);
        GameObject uiView = GameObject.Instantiate(uiPrefab);
        uiView.name = uiPrefab.name;
        uiView.transform.SetParent(this.canvas.transform, false);

        int lastIndex = name.LastIndexOf("/");
        if(lastIndex > 0)
        {
            name = name.Substring(lastIndex + 1);
        }

        Type type = Type.GetType(name + "_UIControl");
        UIController uiCtrl = (UIController)uiView.AddComponent(type);
        return uiCtrl;
    }

    public void RemoveUIView(string name)
    {
        int lastIndex = name.LastIndexOf("/");
        if(lastIndex >= 0)
        {
            name = name.Substring(lastIndex + 1);
        }

        Transform view = this.canvas.transform.Find(name);
        if(view)
        {
            GameObject.DestroyImmediate(view.gameObject);
        }
    }

    public void RemoveAllViews()
    {
        List<Transform> children = new List<Transform>();
        foreach(Transform tf in this.canvas.transform)
        {
            children.Add(tf);
        }

        for(int i = 0; i < children.Count; i++)
        {
            GameObject.DestroyImmediate(children[0].gameObject);
        }
    }
}












