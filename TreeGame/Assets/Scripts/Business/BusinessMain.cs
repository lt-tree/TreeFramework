using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessMain : UnitySingleton<BusinessMain>
{
    public void InitGame()
    {
        this.EnterGame();
    }

    public void EnterGame()
    {
        UIManager.Instance.ShowUIView("UILogin");
    }
}
