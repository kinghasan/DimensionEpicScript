using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TaskCrotroller
{
    public static Dictionary<string, List<int>> m_DicTaskParameters = new Dictionary<string, List<int>>();

    /// <summary>
    /// 新手引导1
    /// </summary>
    public static void Noob_1()
    {
        TaskDesc desc = DataManager.Instanse.m_TaskDescContainer.GetDescByName("Noob_1");
        m_DicTaskParameters.Add(desc.m_Script, new List<int>() { 0 });
        EventCrotroller.Instance.OnGetHero += delegate (EventBase eventBase)
        {
            m_DicTaskParameters[desc.m_Script][0] += 1;
            if (m_DicTaskParameters[desc.m_Script][0] == desc.m_ParametersList[0])
            {
                LastTask(desc);
            }
        };
    }

    /// <summary>
    /// 新手引导2
    /// </summary>
    public static void Noob_2()
    {
        TaskDesc desc = DataManager.Instanse.m_TaskDescContainer.GetDescByName("Noob_2");
        m_DicTaskParameters.Add(desc.m_Script, new List<int>() { 0 });
        TimeCrotroller.Instance.AddTimerTask(0.5f, desc.m_Script, delegate() { }, 1, delegate ()
        {
            if (BaseData.Instanse.m_MainCrotroller.m_DicTeamModelGrid.m_ModelList.Count > 0)
            {
                LastTask(desc);
                return 0;
            }
            else
            {
                return 1;
            }
        });
    }

    /// <summary>
    /// 新手引导3
    /// </summary>
    public static void Noob_3()
    {
        TaskDesc desc = DataManager.Instanse.m_TaskDescContainer.GetDescByName("Noob_3");
        m_DicTaskParameters.Add(desc.m_Script, new List<int>() { 0 });
        EventCrotroller.Instance.OnKill += delegate(EventBase eventBase)
        {
            KillEvent killEvent = (KillEvent)eventBase;
            if (!killEvent.m_Target.m_IsPlayer)
            {
                m_DicTaskParameters[desc.m_Script][0] += 1;
                if (m_DicTaskParameters[desc.m_Script][0] == desc.m_ParametersList[0])
                {
                    LastTask(desc);
                }
            }
        };
    }

    public static void StartTask(TaskDesc desc)
    {
        object[] parameters = new object[] { };
        MethodInfo method = BaseData.Instanse.m_TaskType.GetMethod(desc.m_Script);
        Debug.Log(desc.m_Name + "开始");
        BaseData.Instanse.m_MainCrotroller.m_TaskList.Add(desc);
        method.Invoke(null, parameters);
        BaseData.Instanse.m_CameraCrotroller.UpdateTaskPanel();
    }

    public static void LastTask(TaskDesc desc)
    {
        Debug.Log(desc.m_Name + "完成");
        BaseData.Instanse.m_MainCrotroller.m_TaskList.Remove(desc);
        if (desc.m_LastTask != 0)
        {
            TaskDesc task = DataManager.Instanse.m_TaskDescContainer.GetDescByID(desc.m_LastTask);
            object[] parameters = new object[] {};
            MethodInfo method = BaseData.Instanse.m_TaskType.GetMethod(task.m_Script);
            Debug.Log(task.m_Name + "开始");
            BaseData.Instanse.m_MainCrotroller.m_TaskList.Add(task);
            method.Invoke(null, parameters);
        }
        BaseData.Instanse.m_CameraCrotroller.UpdateTaskPanel();
    }
}
