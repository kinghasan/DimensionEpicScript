using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    private Button m_NewGame;
    private Button m_LoadGame;

    private void Awake()
    {
        m_NewGame = GameObject.Find("Canvas/Panel/New").GetComponent<Button>();
        m_LoadGame = GameObject.Find("Canvas/Panel/Load").GetComponent<Button>();

        m_NewGame.onClick.AddListener(delegate ()
        {
            BaseData.Instanse.m_MainCrotroller.WaitLoadBattle();
        });

        //如果没有存档则加载按钮不可点击
        SLCrotroller.SaveData data = SLCrotroller.Instance.GetSaveData();
        if (data == null)
            m_LoadGame.enabled = false;
        m_LoadGame.onClick.AddListener(delegate ()
        {
            BaseData.Instanse.m_MainCrotroller.WaitLoadBattle(data);
        });

        //StartCoroutine(ClickIntoGame());
    }

    //public IEnumerator ClickIntoGame()
    //{
    //    while (true)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Mouse0))
    //        {
    //            SLCrotroller.SaveData data = SLCrotroller.Instance.GetSaveData();
    //            if (data != null)
    //            {
    //                BaseData.Instanse.m_MainCrotroller.WaitLoadBattle(data);
    //                //BaseData.Instanse.m_MainCrotroller.WaitLoadBattle();
    //            }
    //            else
    //            {
    //                BaseData.Instanse.m_MainCrotroller.WaitLoadBattle();
    //            }
    //            break;
    //        }
    //        yield return new WaitForSeconds(0.01f);
    //    }
    //}
}
