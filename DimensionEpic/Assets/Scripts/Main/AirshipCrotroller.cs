using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirshipCrotroller
{
    //飞艇场景物体
    public GameObject m_Parent;
    private Text m_GoldText;

    private void Start()
    {
        m_Parent = GameObject.Find("Airship");
        m_GoldText = m_Parent.gameObject.transform.Find("Canvas/UIPanel/Gold").GetComponent<Text>();
    }

    public void Run()
    {
        m_GoldText.text = "金钱:" + BaseData.Instanse.m_MainCrotroller.m_Player.gold;
    }
}
