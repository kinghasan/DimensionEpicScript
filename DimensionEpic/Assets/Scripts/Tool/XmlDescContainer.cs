using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class DataManager
{
    private static DataManager m_Instanse = new DataManager();
    public static DataManager Instanse
    {
        get { return m_Instanse; }
    }

    public BuffDescContainer m_BuffDescContainer;
    public AbilityDescContainer m_AbilityDescContainer;
    public TaskDescContainer m_TaskDescContainer;
    public EnemyDescContainer m_EnemyDescContainer;
    public ExpeditionMapDescContainer m_ExpeditionMapDescContainer;
    public ItemDescContainer m_ItemDescContainer;
    public ItemFormulaContainer m_ItemFormulaContainer;
    public MaterialDescContainer m_MaterialDescContainer;
    public AttributeListDescContainer m_AttributeListDescContainer;

    public DataManager()
    {
        m_BuffDescContainer = new BuffDescContainer();
        m_AbilityDescContainer = new AbilityDescContainer();
        m_TaskDescContainer = new TaskDescContainer();
        m_EnemyDescContainer = new EnemyDescContainer();
        m_ItemFormulaContainer = new ItemFormulaContainer();
        m_ExpeditionMapDescContainer = new ExpeditionMapDescContainer();
        m_ItemDescContainer = new ItemDescContainer();
        m_MaterialDescContainer = new MaterialDescContainer();
        m_AttributeListDescContainer = new AttributeListDescContainer();
    }

    /// <summary>
    /// 解析所有XML内容
    /// </summary>
    public void Load()
    {
        m_AttributeListDescContainer.Load();
        m_TaskDescContainer.Load();
        m_ItemDescContainer.Load();
        m_MaterialDescContainer.Load();
        m_ItemFormulaContainer.Load();
        m_AbilityDescContainer.Load();
        m_BuffDescContainer.Load();
        m_EnemyDescContainer.Load();
        m_ExpeditionMapDescContainer.Load();
    }
}

public enum ModelType
{
    None,
    Battle,
    Home,
    Bullet,
    Buff,
    Ability,
}

/// <summary>
/// 总解析类
/// </summary>
public class BaseContainer
{
    public string m_Path;
    public Dictionary<int, BaseDesc> m_dicDesc = new Dictionary<int, BaseDesc>();

    public void Load()
    {
    }

    public BaseDesc GetDescByID(int id)
    {
        m_dicDesc.TryGetValue(id, out BaseDesc value);
        return value;
    }
}

public class BuffDescContainer : BaseContainer
{

    public BuffDescContainer()
    {
        m_Path = System.IO.Path.Combine(Application.streamingAssetsPath + Res_Def.m_Config + "BuffConfig.xml");
    }

    public new BuffDesc GetDescByID(int id)
    {
        m_dicDesc.TryGetValue(id, out BaseDesc value);
        return value as BuffDesc;
    }

    public BuffDesc GetDescByName(string name)
    {
        foreach (BaseDesc value in m_dicDesc.Values)
        {
            BuffDesc desc = value as BuffDesc;
            if (desc.m_Name == name)
                return desc;
        }
        return null;
    }

    public new void Load()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(m_Path);
        foreach (XmlElement element in doc.LastChild.ChildNodes)
        {
            BuffDesc desc = new BuffDesc(element);
            m_dicDesc.Add(desc.m_ID, desc);
        }
    }
}

public class AbilityDescContainer : BaseContainer
{

    public AbilityDescContainer()
    {
        m_Path = System.IO.Path.Combine(Application.streamingAssetsPath + Res_Def.m_Config + "AbilityConfig.xml");
    }

    public new AbilityDesc GetDescByID(int id)
    {
        m_dicDesc.TryGetValue(id, out BaseDesc value);
        return value as AbilityDesc;
    }

    public AbilityDesc GetDescByName(string name)
    {
        foreach (AbilityDesc value in m_dicDesc.Values)
        {
            AbilityDesc desc = value as AbilityDesc;
            if (desc.m_Script == name)
                return desc;
        }
        return null;
    }

    public new void Load()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(m_Path);
        foreach (XmlElement element in doc.LastChild.ChildNodes)
        {
            AbilityDesc desc = new AbilityDesc(element);
            m_dicDesc.Add(desc.m_ID, desc);
        }
    }
}

public class TaskDescContainer : BaseContainer
{

    public TaskDescContainer()
    {
        m_Path = System.IO.Path.Combine(Application.streamingAssetsPath + Res_Def.m_Config + "TaskConfig.xml");
    }

    public new TaskDesc GetDescByID(int id)
    {
        m_dicDesc.TryGetValue(id, out BaseDesc value);
        return value as TaskDesc;
    }

    public TaskDesc GetDescByName(string name)
    {
        foreach (TaskDesc value in m_dicDesc.Values)
        {
            TaskDesc desc = value as TaskDesc;
            if (desc.m_Script == name)
                return desc;
        }
        return null;
    }

    public new void Load()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(m_Path);
        foreach (XmlElement element in doc.LastChild.ChildNodes)
        {
            TaskDesc desc = new TaskDesc(element);
            m_dicDesc.Add(desc.m_ID, desc);
        }
    }
}

public class EnemyDescContainer : BaseContainer
{
    public EnemyDescContainer()
    {
        m_Path = System.IO.Path.Combine(Application.streamingAssetsPath + Res_Def.m_Config + "EnemyConfig.xml");
    }

    public new EnemyDesc GetDescByID(int id)
    {
        m_dicDesc.TryGetValue(id, out BaseDesc value);
        return value as EnemyDesc;
    }

    public EnemyDesc GetDescByName(string name)
    {
        foreach (EnemyDesc value in m_dicDesc.Values)
        {
            EnemyDesc desc = value as EnemyDesc;
            if (desc.m_Name == name)
                return desc;
        }
        return null;
    }

    public new void Load()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(m_Path);
        foreach (XmlElement element in doc.LastChild.ChildNodes)
        {
            EnemyDesc desc = new EnemyDesc(element);
            m_dicDesc.Add(desc.m_ID, desc);
        }
    }
}

public class ExpeditionMapDescContainer : BaseContainer
{

    public ExpeditionMapDescContainer()
    {
        m_Path = System.IO.Path.Combine(Application.streamingAssetsPath + Res_Def.m_Config + "ExpeditionMapConfig.xml");
    }

    public new ExpeditionMapDesc GetDescByID(int id)
    {
        m_dicDesc.TryGetValue(id, out BaseDesc value);
        return value as ExpeditionMapDesc;
    }

    public ExpeditionMapDesc GetDescByName(string name)
    {
        foreach (ExpeditionMapDesc value in m_dicDesc.Values)
        {
            ExpeditionMapDesc desc = value as ExpeditionMapDesc;
            if (desc.m_Name == name)
                return desc;
        }
        return null;
    }

    public new void Load()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(m_Path);
        foreach (XmlElement element in doc.LastChild.ChildNodes)
        {
            ExpeditionMapDesc desc = new ExpeditionMapDesc(element);
            m_dicDesc.Add(desc.m_ID, desc);
        }
    }
}

public class ItemDescContainer : BaseContainer
{

    public ItemDescContainer()
    {
        m_Path = System.IO.Path.Combine(Application.streamingAssetsPath + Res_Def.m_Config + "ItemConfig.xml");
    }

    public new ItemDesc GetDescByID(int id)
    {
        m_dicDesc.TryGetValue(id, out BaseDesc value);
        return value as ItemDesc;
    }

    public ItemDesc GetDescByName(string name)
    {
        foreach (ItemDesc value in m_dicDesc.Values)
        {
            ItemDesc desc = value as ItemDesc;
            if (desc.m_Name == name)
                return desc;
        }
        return null;
    }

    public new void Load()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(m_Path);
        foreach (XmlElement element in doc.LastChild.ChildNodes)
        {
            ItemDesc desc = new ItemDesc(element);
            m_dicDesc.Add(desc.m_ID, desc);
        }
    }
}

public class MaterialDescContainer : BaseContainer
{

    public MaterialDescContainer()
    {
        m_Path = System.IO.Path.Combine(Application.streamingAssetsPath + Res_Def.m_Config + "MaterialConfig.xml");
    }

    public new MaterialDesc GetDescByID(int id)
    {
        m_dicDesc.TryGetValue(id, out BaseDesc value);
        return value as MaterialDesc;
    }

    public MaterialDesc GetDescByName(string name)
    {
        foreach (MaterialDesc value in m_dicDesc.Values)
        {
            MaterialDesc desc = value as MaterialDesc;
            if (desc.m_Name == name)
                return desc;
        }
        return null;
    }

    public new void Load()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(m_Path);
        foreach (XmlElement element in doc.LastChild.ChildNodes)
        {
            MaterialDesc desc = new MaterialDesc(element);
            m_dicDesc.Add(desc.m_ID, desc);
        }
    }
}

public class ItemFormulaContainer : BaseContainer
{

    public ItemFormulaContainer()
    {
        m_Path = System.IO.Path.Combine(Application.streamingAssetsPath + Res_Def.m_Config + "ItemFormulaConfig.xml");
    }

    public new ItemFormulaDesc GetDescByID(int id)
    {
        m_dicDesc.TryGetValue(id, out BaseDesc value);
        return value as ItemFormulaDesc;
    }

    public ItemFormulaDesc GetDescByName(string name)
    {
        foreach (ItemFormulaDesc value in m_dicDesc.Values)
        {
            ItemFormulaDesc desc = value as ItemFormulaDesc;
            if (desc.m_Name == name)
                return desc;
        }
        return null;
    }

    public new void Load()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(m_Path);
        foreach (XmlElement element in doc.LastChild.ChildNodes)
        {
            ItemFormulaDesc desc = new ItemFormulaDesc(element);
            m_dicDesc.Add(desc.m_ID, desc);
        }
    }
}

public class AttributeListDescContainer
{
    public string m_Path;
    public Dictionary<ItemPlace, List<AttributeListDesc>> m_DicAttributeList = new Dictionary<ItemPlace, List<AttributeListDesc>>();

    public AttributeListDescContainer()
    {
        m_Path = System.IO.Path.Combine(Application.streamingAssetsPath + Res_Def.m_Config + "RandomAttributeConfig.xml");
    }

    public string[] GetRandomAttribute(Item item)
    {
        List<AttributeListDesc> list = m_DicAttributeList[item.m_Type];
        AttributeListDesc desc = list[UnityEngine.Random.Range(0, list.Count)];

        //分级：lv1:1-9   lv2:10-19   lv3:20-29   lv4:30-39   lv5:40-50
        int value = 0;
        if (item.m_Level >= 50)
            value = UnityEngine.Random.Range(desc.m_Min6, desc.m_Max6 + 1);
        else if (item.m_Level >= 40)
            value = UnityEngine.Random.Range(desc.m_Min5, desc.m_Max5 + 1);
        else if (item.m_Level >= 30)
            value = UnityEngine.Random.Range(desc.m_Min4, desc.m_Max4 + 1);
        else if (item.m_Level >= 20)
            value = UnityEngine.Random.Range(desc.m_Min3, desc.m_Max3 + 1);
        else if (item.m_Level >= 10)
            value = UnityEngine.Random.Range(desc.m_Min2, desc.m_Max2 + 1);
        else if (item.m_Level >= 1)
            value = UnityEngine.Random.Range(desc.m_Min1, desc.m_Max1 + 1);

        return new string[] { desc.m_Attribute, value.ToString() };
    }

    public void Load()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(m_Path);

        //初始化词缀列表
        m_DicAttributeList.Add(ItemPlace.Weapon, new List<AttributeListDesc>());
        m_DicAttributeList.Add(ItemPlace.Deputy, new List<AttributeListDesc>());
        m_DicAttributeList.Add(ItemPlace.Head, new List<AttributeListDesc>());
        m_DicAttributeList.Add(ItemPlace.Chest, new List<AttributeListDesc>());
        m_DicAttributeList.Add(ItemPlace.Foot, new List<AttributeListDesc>());
        m_DicAttributeList.Add(ItemPlace.Hand, new List<AttributeListDesc>());
        m_DicAttributeList.Add(ItemPlace.Necklace, new List<AttributeListDesc>());
        m_DicAttributeList.Add(ItemPlace.Ornament1, new List<AttributeListDesc>());
        m_DicAttributeList.Add(ItemPlace.Ornament2, new List<AttributeListDesc>());

        foreach (XmlElement element in doc.LastChild.ChildNodes)
        {
            AttributeListDesc desc = new AttributeListDesc(element);
            m_DicAttributeList[desc.m_Type].Add(desc);
        }
    }
}

public class BaseDesc
{
    public int m_ID;
    public string m_Name;
    public string m_Res;
    public string m_Script;
    public ModelType m_ModelType;

    protected BaseDesc()
    {

    }

    public BaseDesc(ModelType type)
    {
        m_ModelType = type;
    }

    public BaseDesc(XmlElement element)
    {
        m_ID = int.Parse(element.GetAttribute("id"));
        m_Name = element.GetAttribute("name");
        m_Script = element.GetAttribute("res");
        m_Res = element.GetAttribute("res");
    }
}

public class BuffDesc : BaseDesc
{
    public BuffAttribute m_Attribute;
    public float m_Value;
    public float m_Time;

    public BuffDesc(XmlElement element) : base(element)
    {
        m_Attribute = (BuffAttribute)Enum.Parse(typeof(BuffAttribute), element.GetAttribute("attribute"));
        m_Value = float.Parse(element.GetAttribute("value"));
        m_Time = float.Parse(element.GetAttribute("time"));
    }

    public enum BuffAttribute
    {
        atk,
        atkSpeed,
        atkRound,
        range,
        rangeDmg,
        speed,
        hp,
    }
}

public class TaskDesc : BaseDesc
{
    public string m_Title;
    public string m_Content;
    public string m_Remarks;
    public List<int> m_ParametersList;
    public int m_TargetCount;
    public int m_LastTask;

    public TaskDesc(XmlElement element) : base(element)
    {
        m_ParametersList = new List<int>();
        m_Title = element.GetAttribute("name");
        m_Content = element.GetAttribute("content");
        m_Remarks = element.GetAttribute("remarks");
        string lastTaskStr = element.GetAttribute("lastTask");
        if (!string.IsNullOrEmpty(lastTaskStr))
        {
            m_LastTask = int.Parse(element.GetAttribute("lastTask"));
        }
        m_TargetCount = int.Parse(element.GetAttribute("targetCount"));

        string[] ParametersList = element.GetAttribute("parameters").Split('|');
        foreach (string parameter in ParametersList)
        {
            if (string.IsNullOrEmpty(parameter))
            {

            }
            else
            {
                m_ParametersList.Add(int.Parse(parameter));
            }
        }
    }
}

public class AbilityDesc : BaseDesc
{
    public bool m_IsPassive;
    public Ability.AbilityCondition m_Condition;
    public string m_Message;
    public List<float> m_AttributeList;
    public int m_InitCD;
    public int m_CD;

    public AbilityDesc(XmlElement element) : base(element)
    {
        m_AttributeList = new List<float>();
        string condition = element.GetAttribute("condition");
        if (!string.IsNullOrEmpty(condition))
            m_Condition = (Ability.AbilityCondition)Enum.Parse(typeof(Ability.AbilityCondition), element.GetAttribute("condition"));
        else
            m_Condition = Ability.AbilityCondition.none;
        int isPassive = int.Parse(element.GetAttribute("isPassive"));
        if (isPassive == 1)
            m_IsPassive = true;
        m_Message = element.GetAttribute("message");
        string[] attributeList = element.GetAttribute("attribute").Split('|');
        foreach(string attribute in attributeList)
        {
            if (string.IsNullOrEmpty(attribute))
            {

            }
            else
            {
                m_AttributeList.Add(float.Parse(attribute));
            }
        }
        m_InitCD = int.Parse(element.GetAttribute("initCD"));
        m_CD = int.Parse(element.GetAttribute("cd"));
    }
}

public class HeroDesc : BaseDesc
{
    public int m_iHP;
    public int m_iAttack;
    public int m_iSpeed;
    public HeroJob m_Job;

    public HeroDesc(XmlElement element) : base(element)
    {
        m_iHP = int.Parse(element.GetAttribute("health"));
        m_iAttack = int.Parse(element.GetAttribute("attack"));
        m_iSpeed = int.Parse(element.GetAttribute("speed"));
        string[] abilityList = element.GetAttribute("ability").Split('|');
        if (abilityList[0] != "0")
        {
            Ability commonAttack = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(int.Parse(abilityList[0])));
        }
        if (abilityList[1] != "0")
        {
            Ability generalAbility = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(int.Parse(abilityList[1])));
        }
        if (abilityList[2] != "0")
        {
            Ability finalAbility = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(int.Parse(abilityList[2])));
        }
    }
}

public class EnemyDesc : BaseDesc
{
    public int m_iHP;
    public int m_iAttack;
    public int m_iDefense;
    public int m_iSpeed;
    public Ability[] m_AbilityArr;

    public EnemyDesc(XmlElement element) : base(element)
    {
        m_AbilityArr = new Ability[3];
        m_iHP = int.Parse(element.GetAttribute("health"));
        m_iAttack = int.Parse(element.GetAttribute("attack"));
        m_iDefense = int.Parse(element.GetAttribute("defense"));
        m_iSpeed = int.Parse(element.GetAttribute("speed"));
        string[] abilityList = element.GetAttribute("ability").Split('|');
        if (abilityList[0] != "0")
        {
            Ability commonAttack = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(int.Parse(abilityList[0])));
            m_AbilityArr[0] = commonAttack;
        }
        if (abilityList[1] != "0")
        {
            Ability generalAbility = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(int.Parse(abilityList[1])));
            m_AbilityArr[1] = generalAbility;
        }
        if (abilityList[2] != "0")
        {
            Ability finalAbility = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(int.Parse(abilityList[2])));
            m_AbilityArr[2] = finalAbility;
        }
    }
}

public class ExpeditionMapDesc : BaseDesc
{
    public bool m_Show;
    public int m_Level;
    public Dictionary<EnemyDesc, float> m_DicEnemy;
    public int m_Round;
    //key为掉落的装备，value为掉率
    public Dictionary<ItemDesc,float> m_DicItem;
    //key为掉落的材料，value为掉率
    public Dictionary<MaterialDesc, float> m_DicMaterial;
    //Boss关卡怪物站位,key为几号位，value为怪物ID
    public Dictionary<int, int> m_DicBossRound;
    //掉落图纸奖励
    public List<ItemFormulaDesc> m_ItemFormulaList;

    public ExpeditionMapDesc(XmlElement element) : base(element)
    {
        m_DicEnemy = new Dictionary<EnemyDesc, float>();
        m_DicItem = new Dictionary<ItemDesc, float>();
        m_DicMaterial = new Dictionary<MaterialDesc, float>();
        m_DicBossRound = new Dictionary<int, int>();
        m_ItemFormulaList = new List<ItemFormulaDesc>();
        string[] levelRange = element.GetAttribute("levelRange").Split('-');
        string show = element.GetAttribute("show");
        if (show == "1")
            m_Show = true;
        m_Level = int.Parse(element.GetAttribute("level"));
        m_Round = int.Parse(element.GetAttribute("round"));

        //敌人随机范围
        string str = element.GetAttribute("enemyRange");
        if (!string.IsNullOrEmpty(str))
        {
            string[] enemyRange = str.Split('|');
            foreach (string enemyInfo in enemyRange)
            {
                string[] enemyArr = enemyInfo.Split(':');
                m_DicEnemy.Add(DataManager.Instanse.m_EnemyDescContainer.GetDescByID(int.Parse(enemyArr[0])), float.Parse(enemyArr[1]));
            }
        }
        //装备掉落池
        str = element.GetAttribute("itemRange");
        if (!string.IsNullOrEmpty(str))
        {
            string[] itemRange = str.Split('|');
            foreach (string itemArr in itemRange)
            {
                string[] itemKV = itemArr.Split(':');
                ItemDesc item = DataManager.Instanse.m_ItemDescContainer.GetDescByID(int.Parse(itemKV[0]));
                m_DicItem.Add(item, float.Parse(itemKV[1]));
            }
        }
        //材料掉落池
        str = element.GetAttribute("materialRange");
        if (!string.IsNullOrEmpty(str))
        {
            string[] materialRange = str.Split('|');
            foreach (string materialArr in materialRange)
            {
                string[] materialKV = materialArr.Split(':');
                MaterialDesc material = DataManager.Instanse.m_MaterialDescContainer.GetDescByID(int.Parse(materialKV[0]));
                m_DicMaterial.Add(material, float.Parse(materialKV[1]));
            }
        }
        //Boss战
        str = element.GetAttribute("bossRound");
        if (!string.IsNullOrEmpty(str))
        {
            string[] bossRound = str.Split('|');
            foreach (string roundArr in bossRound)
            {
                string[] roundKV = roundArr.Split(':');
                m_DicBossRound.Add(int.Parse(roundKV[0]), int.Parse(roundKV[1]));
            }
        }
        //图纸掉落池
        str = element.GetAttribute("itemFormula");
        if (!string.IsNullOrEmpty(str))
        {
            string[] itemFormulaArr = str.Split('|');
            foreach (string itemFormula in itemFormulaArr)
            {
                m_ItemFormulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(int.Parse(itemFormula)));
            }
        }
    }
}

public class ItemDesc : BaseDesc
{
    /// <summary>
    /// 最低品质
    /// </summary>
    public int m_Quality;
    public int m_Type;
    public List<int> m_AbilityList;
    public string m_Message;
    public List<string[]> m_AttributeList;
    public List<HeroJob> m_JobList;

    public ItemDesc(XmlElement element) : base(element)
    {
        m_AttributeList = new List<string[]>();
        m_AbilityList = new List<int>();
        m_JobList = new List<HeroJob>();
        m_Quality = int.Parse(element.GetAttribute("quality"));
        m_Type = int.Parse(element.GetAttribute("type"));
        m_Message = element.GetAttribute("message");
        m_Res = element.GetAttribute("res");
        string[] abilityList = element.GetAttribute("ability").Split('|');
        foreach(string ability in abilityList)
        {
            if (!string.IsNullOrEmpty(ability))
                m_AbilityList.Add(int.Parse(ability));
        }

        string[] attributes = element.GetAttribute("attribute").Split('|');
        if (!string.IsNullOrEmpty(attributes[0]))
        {
            foreach (string attribute in attributes)
            {
                m_AttributeList.Add(attribute.Split(':'));
            }
        }

        string[] jobArr = element.GetAttribute("job").Split('|');
        foreach(string job in jobArr)
        {
            m_JobList.Add((HeroJob)Enum.Parse(typeof(HeroJob), job));
        }
    }
}

public class MaterialDesc : BaseDesc
{
    public string m_Message;
    public int m_Quality;

    public MaterialDesc(XmlElement element) : base(element)
    {
        m_Message = element.GetAttribute("message");
        m_Quality = int.Parse(element.GetAttribute("quality"));
    }
}

public class ItemFormulaDesc : BaseDesc
{
    public string m_Message;
    public int m_ItemId;
    public Dictionary<MaterialDesc, int> m_DicMaterial;

    public ItemFormulaDesc(XmlElement element) : base(element)
    {
        m_DicMaterial = new Dictionary<MaterialDesc, int>();
        m_Message = element.GetAttribute("message");
        m_ItemId = int.Parse(element.GetAttribute("itemId"));
        string materialString = element.GetAttribute("material");
        string[] materialArr = materialString.Split('|');
        foreach(string material in materialArr)
        {
            string[] materialAttribute = material.Split(':');
            m_DicMaterial.Add(DataManager.Instanse.m_MaterialDescContainer.GetDescByID(int.Parse(materialAttribute[0])),
                int.Parse(materialAttribute[1]));
        }
    }
}

public class AttributeListDesc
{
    public ItemPlace m_Type;
    public string m_Attribute;
    public int m_Min1;
    public int m_Max1;
    public int m_Min2;
    public int m_Max2;
    public int m_Min3;
    public int m_Max3;
    public int m_Min4;
    public int m_Max4;
    public int m_Min5;
    public int m_Max5;
    public int m_Min6;
    public int m_Max6;

    public AttributeListDesc(XmlElement element)
    {
        m_Type = (ItemPlace)Enum.Parse(typeof(ItemPlace), element.GetAttribute("type"));
        m_Attribute = element.GetAttribute("attribute");
        string level1 = element.GetAttribute("level1");
        string level2 = element.GetAttribute("level2");
        string level3 = element.GetAttribute("level3");
        string level4 = element.GetAttribute("level4");
        string level5 = element.GetAttribute("level5");
        string level6 = element.GetAttribute("level6");

        string[] range = level1.Split('-');
        m_Min1 = int.Parse(range[0]);
        m_Max1 = int.Parse(range[1]);

        range = level2.Split('-');
        m_Min2 = int.Parse(range[0]);
        m_Max2 = int.Parse(range[1]);

        range = level3.Split('-');
        m_Min3 = int.Parse(range[0]);
        m_Max3 = int.Parse(range[1]);

        range = level4.Split('-');
        m_Min4 = int.Parse(range[0]);
        m_Max4 = int.Parse(range[1]);

        range = level5.Split('-');
        m_Min5 = int.Parse(range[0]);
        m_Max5 = int.Parse(range[1]);

        range = level6.Split('-');
        m_Min6 = int.Parse(range[0]);
        m_Max6 = int.Parse(range[1]);
    }
}