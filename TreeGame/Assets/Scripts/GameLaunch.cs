using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLaunch : MonoBehaviour
{
    private void Awake()
    {
        this.InitFramework();
        this.CheckUpdate();
        this.InitBusiness();
    }

    private void CheckUpdate()
    {

    }

    private void InitFramework()
    {
        this.gameObject.AddComponent<DebugManager>();
        this.gameObject.AddComponent<ResourceManager>();
        this.gameObject.AddComponent<ScheduleManager>().Init();
        this.gameObject.AddComponent<UIManager>();
    }

    private void InitBusiness()
    {
        this.gameObject.AddComponent<BusinessMain>();
        BusinessMain.Instance.InitGame();
    }
}
