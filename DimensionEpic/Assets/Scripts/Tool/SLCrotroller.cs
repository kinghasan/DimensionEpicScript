using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SLCrotroller
{
    private static SLCrotroller m_Instance;
    public static SLCrotroller Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new SLCrotroller();
            return m_Instance;
        }
    }

    string m_SavePath = "/game.txt";
    public void SaveDataToJson(SaveData data)
    {
        string path = Application.persistentDataPath + m_SavePath;
        StreamWriter sw = File.CreateText(path);
        string json = JsonMapper.ToJson(data);
        sw.Close();
        File.WriteAllText(path, json);
    }

    public SaveData GetSaveData()
    {
        string path = Application.persistentDataPath + m_SavePath;
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            SaveData saveData = JsonMapper.ToObject<SaveData>(data);
            return saveData;
        }
        return null;
    }

    [Serializable]
    public class SaveData
    {
<<<<<<< HEAD
        public int m_Gold;
        public List<ModelData.ModelJson> m_HeroJsonList;
        public List<ModelData.ModelJson> m_BarJsonList;
        public List<Item.ItemJson> m_ItemJsonList;
        public List<int> m_ItemFormulaList;
        public int[] m_Team = new int[9];
        public Dictionary<string, int> m_DicMaterial;
        public int m_IdIndex;
        public int m_Year;
        public int m_BossRound;
        public int m_PlayerLevel;

        public SaveData()
        {
            m_HeroJsonList = new List<ModelData.ModelJson>();
            m_BarJsonList = new List<ModelData.ModelJson>();
            m_ItemJsonList = new List<Item.ItemJson>();
            m_ItemFormulaList = new List<int>();
            m_DicMaterial = new Dictionary<string, int>();
=======
        public PlayerModel.PlayerJson m_PlayerJson;

        public SaveData()
        {
            m_PlayerJson = new PlayerModel.PlayerJson(BaseData.Instanse.m_MainCrotroller.m_Player);
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
        }
    }
}
