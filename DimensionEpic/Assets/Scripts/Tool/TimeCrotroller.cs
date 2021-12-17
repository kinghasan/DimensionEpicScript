using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCrotroller
{
    public delegate void OnTimeStart();
    public delegate int OnTimeEnd();
    private Dictionary<string,TimerTask> m_timerTask;
    private List<string> m_DeleteList;
    private Dictionary<string,TimerTask> m_AddDic;

    public class TimerTask
    {
        public string TaskName;
        public float RealTime;
        public float Time;
        public bool IsLoop;
        public int LoopCount;
        public OnTimeStart CallBack;
        public OnTimeEnd EndBack;
    }

    private TimeCrotroller()
    {
        m_timerTask = new Dictionary<string, TimerTask>();
        m_DeleteList = new List<string>();
        m_AddDic = new Dictionary<string, TimerTask>();
    }

    private static TimeCrotroller m_Instance;

    public static TimeCrotroller Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new TimeCrotroller();
            }
            return m_Instance;
        }
    }

    public void UpdateTimer()
    {
        foreach (TimerTask task in m_timerTask.Values)
        {
            if (task == null)
            {
                continue;
            }
            if (task.RealTime < Time.time)
            {
                if (null != task.CallBack)
                {
                    if(task.IsLoop == true)
                    {
                        task.CallBack?.Invoke();
                        task.LoopCount--;
                        if (task.LoopCount == 0)
                        {
                            task.LoopCount = task.EndBack.Invoke();
                            if(task.LoopCount > 0)
                                task.RealTime = Time.time + task.Time;
                            else
                                m_DeleteList.Add(task.TaskName);
                        }
                        else
                            task.RealTime = Time.time + task.Time;
                    }
                    else
                    {
                        task.CallBack?.Invoke();
                        m_DeleteList.Add(task.TaskName);
                    }
                }
            }
        }

        //新增定时
        foreach(string key in m_AddDic.Keys)
        {
            if (m_timerTask.ContainsKey(key))
            {
                m_timerTask.Remove(key);
                if (m_DeleteList.Contains(key))
                    m_DeleteList.Remove(key);
            }
            m_timerTask.Add(key, m_AddDic[key]);
        }
        m_AddDic.Clear();
        
        //删除定时
        foreach(string taskName in m_DeleteList)
        {
            m_timerTask.Remove(taskName);
        }
        m_DeleteList.Clear();
    }

    public TimerTask AddTimerTask(float time, string taskName, OnTimeStart callback)
    {
        if (null == callback)
        {
            return null;
        }
        TimerTask task = new TimerTask();
        task.Time = time;
        task.RealTime = Time.time + time;
        task.CallBack = callback;
        task.TaskName = taskName;
        m_AddDic.Add(taskName,task);
        return task;
    }

    public TimerTask AddTimerTask(float time, string taskName, OnTimeStart callback,int loopCount)
    {
        if (null == callback)
        {
            return null;
        }
        TimerTask task = new TimerTask();
        task.Time = time;
        task.RealTime = Time.time + time;
        task.CallBack = callback;
        task.TaskName = taskName;
        task.IsLoop = true;
        task.LoopCount = loopCount;
        m_AddDic.Add(taskName, task);
        return task;
    }

    /// <summary>
    /// 定时循环器
    /// </summary>
    /// <param name="time">多久循环一次</param>
    /// <param name="taskName">定时器名字</param>
    /// <param name="callback">执行方法 (返回值是剩余执行次数)</param>
    /// <param name="loopCount">判断次数</param>
    /// <param name="end">结束方法</param>
    /// <returns></returns>
    public TimerTask AddTimerTask(float time, string taskName, OnTimeStart callback, int loopCount,OnTimeEnd end)
    {
        if (null == callback)
        {
            return null;
        }
        TimerTask task = new TimerTask();
        task.Time = time;
        task.RealTime = Time.time + time;
        task.CallBack = callback;
        task.EndBack = end;
        task.TaskName = taskName;
        task.IsLoop = true;
        task.LoopCount = loopCount;
        m_AddDic.Add(taskName, task);
        return task;
    }

    public void RemoveTimerTask(string taskName)
    {
        if(!string.IsNullOrEmpty(taskName))
            m_DeleteList.Add(taskName);
    }
}
