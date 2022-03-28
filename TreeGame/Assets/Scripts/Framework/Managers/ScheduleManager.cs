using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class ScheduleNode
{
    public int schedulerId;                                 // 唯一ID
    public bool isRemoved;                                  // 是否已删除
    public int repeat;                                      // 触发次数
    public float delay;                                     // 第一次触发延迟时间
    public float duration;                                  // 触发间隔时间
    public float passedTime;                                // 已经过时间
    public object param;                                    // 自定义参数
    public ScheduleManager.ScheduleHandler callback;        // 回调
}


public class ScheduleManager : UnitySingleton<ScheduleManager>
{
    private int autoIncreaseId = 1;

    public delegate void ScheduleHandler(object param);

    private Dictionary<int, ScheduleNode> schedulerMap  = null;
    private List<ScheduleNode> removeSchedulerList = null;
    private List<ScheduleNode> newSchedulerList = null;

    public void Init()
    {
        this.schedulerMap = new Dictionary<int, ScheduleNode>();
        this.autoIncreaseId = 1;

        this.removeSchedulerList = new List<ScheduleNode>(); 
        this.newSchedulerList = new List<ScheduleNode>();
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        for(int i = 0; i < this.newSchedulerList.Count; i++)
        {
            this.schedulerMap.Add(this.newSchedulerList[i].schedulerId, this.newSchedulerList[i]);
        }
        this.newSchedulerList.Clear();

        foreach(ScheduleNode node in this.schedulerMap.Values)
        {
            if (node.isRemoved)
            {
                this.removeSchedulerList.Add(node);
                continue;
            }

            node.passedTime += dt;
            if(node.passedTime >= (node.delay + node.duration))
            {
                node.callback(node.param);
                --node.repeat;
                node.passedTime -= (node.delay + node.duration);
                node.delay = 0;

                if(node.repeat == 0)
                {
                    node.isRemoved = true;
                    this.removeSchedulerList.Add(node);
                }
            }
        }

        for (int i = 0; i < this.removeSchedulerList.Count; i++)
        {
            this.schedulerMap.Remove(this.removeSchedulerList[i].schedulerId);
        }
        this.removeSchedulerList.Clear();
    }

    public int ScheduleOnce(ScheduleHandler func, float delay)
    {
        return this.Schedule(func, 0, 1, delay);
    }

    public int ScheduleOnce(ScheduleHandler func, float delay, object param)
    {
        return this.Schedule(func, 0, 1, param, delay);
    }

    // repeat <= 0 means forevenr
    public int Schedule(ScheduleHandler func, float duration, int repeat, float delay = 0.0f)
    {
        return Schedule(func, duration, repeat, null, delay);
    }

    // repeat <= 0 means forevenr
    public int Schedule(ScheduleHandler func, float duration, int repeat, object param, float delay = 0.0f)
    {
        ScheduleNode node = new ScheduleNode();
        node.callback = func;
        node.param = param;
        node.repeat = repeat;
        node.duration = duration;
        node.delay = delay;
        node.passedTime = duration;
        node.isRemoved = false;

        node.schedulerId = this.autoIncreaseId;
        this.autoIncreaseId++;

        this.newSchedulerList.Add(node);
        return node.schedulerId;
    }

    public void Unschedule(int schedulerId)
    {
        if (!this.schedulerMap.ContainsKey(schedulerId))
        {
            return;
        }

        ScheduleNode node = this.schedulerMap[schedulerId];
        node.isRemoved = true;
    }
}
