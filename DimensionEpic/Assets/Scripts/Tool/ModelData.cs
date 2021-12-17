using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ModelData
{
    private int _Id;
    private int _HP;
    private int _Attack;
    private int _Defense;
    private int _Speed;
    private int _Level;
    private HeroJob _Job;
    private string _Name;
    private int _Exp;

    public int Id { get => _Id; set => _Id = value; }
    public int HP { get => _HP; set => _HP = value; }
    public int TotalHp { get => (int)((_HP + ItemHp) * (1 + PHp * 0.01f)); }
    public int Attack { get => _Attack; set => _Attack = value; }
    public int TotalAttack { get => (int)((_Attack + ItemAttack) * (1 + PAttack * 0.01f)); }
    public int Defense { get => _Defense; set => _Defense = value; }
    public int TotalDefense { get => (int)((_Defense + ItemDefense) * (1 + PDefense * 0.01f)); }
    public int Speed { get => _Speed; set => _Speed = value; }
    public int TotalSpeed { get => (int)((_Speed + ItemSpeed) * (1 + PSpeed * 0.01f)); }
    public int Level
    {
        get => _Level;
        set
        {
            int levelUp = value - _Level;
            _Level = value;
            _Attack += AttackPotential * 2 * levelUp + AttackUp * levelUp;
            _Defense += DefensePotential * 1 * levelUp + DefenseUp * levelUp;
            _HP += HpPotential * 3 * levelUp + HpUp * levelUp;
        }
    }
    public HeroJob Job { get => _Job; set => _Job = value; }
    public string Name { get => _Name; set => _Name = value; }
    //展示用模型
    public GameObject ShowModel;
    public int Exp
    {
        get => _Exp;
        set
        {
            _Exp = value;
            if (_Level < 50)
            {
                if (_Exp >= BaseData.m_Explist[_Level] && _Level <= 100)
                {
                    _Exp -= BaseData.m_Explist[_Level];
                    Level += 1;
                }
            }
        }
    }

    //升级增加的攻击力
    private int AttackUp;
    //升级增加的生命值
    private int HpUp;
    //升级增加的生命值
    private int DefenseUp;

    //攻击力潜能点 1:2
    private int AttackPotential;
    //防御力潜能点 1:1
    private int DefensePotential;
    //生命潜能点 1:3
    private int HpPotential;

    //装备增加的攻击力
    public int ItemAttack;
    //装备增加的速度
    public int ItemSpeed;
    //装备增加的血量
    public int ItemHp;
    //装备增加的防御
    public int ItemDefense;

    //增加的百分比攻击力(计算时乘以0.01)
    public int PAttack;
    //增加的百分比速度(计算时乘以0.01)
    public int PSpeed;
    //增加的百分比血量(计算时乘以0.01)
    public int PHp;
    //增加的百分比防御(计算时乘以0.01)
    public int PDefense;

    //暴击几率(默认为5%)(计算时乘以0.01)
    public int m_Crit;
    //暴击伤害(默认为150%)(计算时乘以0.01)
    public int m_CritDamage;

    //基础攻击技能
    public Ability m_CommonAbility;
    //被动技能列表
    public List<Ability> m_PassiveAbilityList;
    //终极技能
    public Ability m_FinalAbility;
    //装备字典
    public Dictionary<ItemPlace, Item> m_DicItem;
    //潜力值
    public int m_Potential;
    //模型key
    public string m_ModelKey;

<<<<<<< HEAD
    public ModelData() { }

    public ModelData(string name, HeroJob job, int level, bool isPlayer, string modelKey)
=======
    public class HeroDataJson
    {
        public int id;
        public int hp;
        public int attack;
        public int speed;
        public int level;
        public HeroJob job;
        public string name;
        public int exp;
        public int attackUp;
        public int hpUp;
        public string m_CommonAttack;
        public string m_GeneralAbility;
        public string m_FinalAbility;
        public Dictionary<string, int> m_DicItem;
        public int potential;
        public string modelKey;

        public HeroDataJson()
        {

        }

        /// <summary>
        /// Json初始化方法
        /// </summary>
        /// <param name="data"></param>
        public HeroDataJson(ModelData data)
        {
            id = data.Id;
            hp = data.HP;
            attack = data.Attack;
            speed = data.Speed;
            level = data.Level;
            job = data.Job;
            name = data.Name;
            exp = data.Exp;
            attackUp = data.AttackUp;
            hpUp = data.HpUp;
            itemAttack = data.ItemAttack;
            itemSpeed = data.ItemSpeed;
            itemHp = data.ItemHp;
            m_CommonAttack = data.m_CommonAttack.Name;
            m_GeneralAbility = data.m_GeneralAbility.Name;
            m_FinalAbility = data.m_FinalAbility.Name;
            potential = data.m_Potential;
            modelKey = data.m_ModelKey;
            foreach(ItemPlace key in data.m_DicItem.Keys)
            {
                data.m_DicItem.TryGetValue(key, out Item item);
                if (item != null)
                {
                    string itemPlace = key.ToString();
                    m_DicItem.Add(itemPlace, item.m_Id);
                }
            }
        }
    }

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="name">名字</param>
    /// <param name="job">职业</param>
    /// <param name="level">等级</param>
    /// <param name="isPlayer">是否玩家单位</param>
    /// <param name="isBar">是否酒馆单位</param>
    /// <param name="modelKey">模型名字</param>
    public ModelData(string name, HeroJob job, int level, bool isPlayer,string modelKey)
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
    {
        m_PassiveAbilityList = new List<Ability>();
        _Id = BaseData.Instanse.m_MainCrotroller.m_IdIndex++;
        m_DicItem = new Dictionary<ItemPlace, Item>();
        m_DicItem.Add(ItemPlace.Weapon, null);
        m_DicItem.Add(ItemPlace.Deputy, null);
        m_DicItem.Add(ItemPlace.Head, null);
        m_DicItem.Add(ItemPlace.Chest, null);
        m_DicItem.Add(ItemPlace.Foot, null);
        m_DicItem.Add(ItemPlace.Hand, null);
        m_DicItem.Add(ItemPlace.Necklace, null);
        m_DicItem.Add(ItemPlace.Ornament1, null);
        m_DicItem.Add(ItemPlace.Ornament2, null);

        m_Potential = 20;

        _Name = name;
        _Job = job;
        m_Crit = 5;
        m_CritDamage = 150;
        Item item;
        m_ModelKey = modelKey;

        if (job == HeroJob.Brave)
        {
            _HP = 100;
            _Attack = 20;
            _Speed = 100;
            HpUp = 25;
            AttackUp = 10;
            DefenseUp = 7;
            item = new Item(DataManager.Instanse.m_ItemDescContainer.GetDescByID(10001));
        }
        else if (job == HeroJob.Archer)
        {
            _HP = 80;
            _Attack = 20;
            _Speed = 110;
            HpUp = 15;
            AttackUp = 13;
            DefenseUp = 4;
            item = new Item(DataManager.Instanse.m_ItemDescContainer.GetDescByID(10002));
        }
        else if(job == HeroJob.Paladin)
        {
            _HP = 150;
            _Attack = 15;
            _Speed = 95;
            HpUp = 35;
            AttackUp = 5;
            DefenseUp = 10;
            item = new Item(DataManager.Instanse.m_ItemDescContainer.GetDescByID(10001));
        }
        else if (job == HeroJob.Master)
        {
            _HP = 70;
            _Attack = 20;
            _Speed = 100;
            HpUp = 10;
            AttackUp = 15;
            DefenseUp = 4;
            item = new Item(DataManager.Instanse.m_ItemDescContainer.GetDescByID(10003));
        }
        else
        {
            item = new Item(DataManager.Instanse.m_ItemDescContainer.GetDescByID(10001));
        }

        //if (_Level > 1)
        //{
        //    _Attack += AttackPotential * 1 * (_Level - 1) + AttackUp * (_Level - 1);
        //    _HP += HpPotential * 3 * (_Level - 1) + HpUp * (_Level - 1);
        //}
        Level = level;

        m_CommonAbility = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(10000));

        GameObject prefab = BaseData.Instanse.m_DicModelObj[m_ModelKey];
        ShowModel = UnityEngine.Object.Instantiate(prefab, BaseData.Instanse.m_ModelPool.transform);
        UnityEngine.UI.Image triggerImg = ShowModel.AddComponent<UnityEngine.UI.Image>();
        triggerImg.color = new Color(0, 0, 0, 0);

        BaseData.Instanse.m_CameraCrotroller.AddTriggerToModel(this, ShowModel);
        UpdateItem(item);

        if (isPlayer)
        {
<<<<<<< HEAD
            BaseData.Instanse.m_MainCrotroller.m_Player.itemList.Add(item);
            BaseData.Instanse.m_MainCrotroller.m_Player.AddHero(this);
=======
            int id = BaseData.Instanse.m_MainCrotroller.CreateModel(this, new Vector3(9999, 9999, 0));
            _ModelId = id;
            UpdateItem(item);
            BaseData.Instanse.GetModelById<BattleModel>(_ModelId).m_ModelHpSlider.gameObject.SetActive(false);
        }
    }

    public ModelData(HeroDataJson json)
    {
        _Name = json.name;
        _HP = json.hp;
        _Attack = json.attack;
        _Speed = json.speed;
        _Level = json.level;
        _Job = json.job;
        _Exp = json.exp;
        AttackUp = json.attackUp;
        HpUp = json.hpUp;
        m_CommonAttack = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByName(json.m_CommonAttack));
        m_GeneralAbility = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByName(json.m_GeneralAbility));
        m_FinalAbility = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByName(json.m_FinalAbility));
        m_Potential = json.potential;
        m_ModelKey = json.modelKey;

        foreach(string key in json.m_DicItem.Keys)
        {
            ItemPlace place = (ItemPlace)Enum.Parse(typeof(ItemPlace), key);
            int itemId = json.m_DicItem[key];
            UpdateItem()
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
        }
    }

    public ModelData(ModelJson data, bool isPlayer)
    {
        m_PassiveAbilityList = new List<Ability>();
        _Id = BaseData.Instanse.m_MainCrotroller.m_IdIndex++;
        m_DicItem = new Dictionary<ItemPlace, Item>();
        m_DicItem.Add(ItemPlace.Weapon, null);
        m_DicItem.Add(ItemPlace.Deputy, null);
        m_DicItem.Add(ItemPlace.Head, null);
        m_DicItem.Add(ItemPlace.Chest, null);
        m_DicItem.Add(ItemPlace.Foot, null);
        m_DicItem.Add(ItemPlace.Hand, null);
        m_DicItem.Add(ItemPlace.Necklace, null);
        m_DicItem.Add(ItemPlace.Ornament1, null);
        m_DicItem.Add(ItemPlace.Ornament2, null);

        m_Potential = 20;

        data.LoadJson(this);

        GameObject prefab = BaseData.Instanse.m_DicModelObj[m_ModelKey];
        ShowModel = UnityEngine.Object.Instantiate(prefab, BaseData.Instanse.m_ModelPool.transform);
        UnityEngine.UI.Image triggerImg = ShowModel.AddComponent<UnityEngine.UI.Image>();
        triggerImg.color = new Color(0, 0, 0, 0);

        BaseData.Instanse.m_CameraCrotroller.AddTriggerToModel(this, ShowModel);

        //foreach(int itemId in data.m_ItemList)
        //{
        //UpdateItem();
        //}

        if (isPlayer)
            BaseData.Instanse.m_MainCrotroller.m_Player.AddHero(this);
        else
            BaseData.Instanse.m_MainCrotroller.m_BarHeroList.Add(this);
        //UpdateItem(item);
    }

    public ModelData(EnemyDesc desc)
    {
        m_PassiveAbilityList = new List<Ability>();
        _Id = BaseData.Instanse.m_MainCrotroller.m_IdIndex++;
        _Name = desc.m_Name;
        _HP = desc.m_iHP;
        _Attack = desc.m_iAttack;
        _Defense = desc.m_iDefense;
        _Speed = desc.m_iSpeed;
        m_Crit = 0;
        m_CritDamage = 150;
        m_ModelKey = desc.m_Script;
        if (desc.m_AbilityArr[0] != null)
        {
            m_CommonAbility = desc.m_AbilityArr[0];
        }
        if (desc.m_AbilityArr[1] != null)
        {
            m_PassiveAbilityList.Add(desc.m_AbilityArr[1]);
        }
        if (desc.m_AbilityArr[2] != null)
        {
            m_FinalAbility = desc.m_AbilityArr[2];
        }
    }

    /// <summary>
    /// 更新装备
    /// </summary>
    /// <param name="item"></param>
    public void UpdateItem(Item item)
    {
        Item oldItem = m_DicItem[item.m_Type];
        if (oldItem != null)
        {
            oldItem.RemoveItemToHero();
        }
        m_DicItem[item.m_Type] = item;
        item.AddItemToHero(this);
    }

    public void RemoveItem(ItemPlace type)
    {
        m_DicItem[type].RemoveItemToHero();
        m_DicItem[type].m_Parent = 0;
        m_DicItem[type] = null;
    }

    public static string JobToString(HeroJob job)
    {
        switch (job)
        {
            case HeroJob.Brave:
                return "勇者";
            case HeroJob.Archer:
                return "弓箭手";
            case HeroJob.Paladin:
                return "圣骑士";
            case HeroJob.Master:
                return "魔法师";
        }
        return "未知职业";
    }

    public class ModelJson
    {
        //英雄属性
        public int Id;
        public int HP;
        public int Attack;
        public int Defense;
        public int Speed;
        public int Level;
        public HeroJob Job;
        public string Name;
        public int Exp;
        public string ModelKey;
        public int HpUp;
        public int AttackUp;
        public int Crit;
        public int CritDamage;

        //英雄装备
        //public List<int> m_ItemList;

        //英雄技能
        //public int m_CommonAbility;
        //public int m_GeneralAbility;
        //public int m_FinalAbility;

        public ModelJson() { }

        public void InitJson(ModelData data)
        {
            Id = data.Id;
            HP = data.HP;
            Attack = data.Attack;
            Defense = data.Defense;
            Speed = data.Speed;
            Level = data.Level;
            Job = data.Job;
            Name = data.Name;
            Exp = data.Exp;
            ModelKey = data.m_ModelKey;
            HpUp = data.HpUp;
            AttackUp = data.AttackUp;
            Crit = data.m_Crit;
            CritDamage = data.m_CritDamage;

            //foreach(Item item in data.m_DicItem.Values)
            //{
            //    if (item != null)
            //    {
            //        m_ItemList.Add(item.m_Id);
            //    }
            //}

            //if (data.m_CommonAbility != null)
            //    m_CommonAbility = data.m_CommonAbility.Id;
            //if (data.m_GeneralAbility != null)
            //    m_GeneralAbility = data.m_GeneralAbility.Id;
            //if (data.m_FinalAbility != null)
            //    m_FinalAbility = data.Id;
        }

        public void LoadJson(ModelData data)
        {
            data.Id = Id;
            data.HP = HP;
            data.Attack = Attack;
            data.Defense = Defense;
            data.Speed = Speed;
            data.Level = Level;
            data.Job = Job;
            data.Name = Name;
            data.Exp = Exp;
            data.m_ModelKey = ModelKey;
            data.HpUp = HpUp;
            data.AttackUp = AttackUp;
            data.m_Crit = Crit;
            data.m_CritDamage = CritDamage;

            //if (m_CommonAbility != 0)
            //    data.m_CommonAbility = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(m_CommonAbility));
            //if (m_GeneralAbility != 0)
            //    data.m_GeneralAbility = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(m_GeneralAbility));
            //if (m_FinalAbility != 0)
            //    data.m_FinalAbility = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(m_FinalAbility));
        }
    }
}

public enum HeroJob
{
    /// <summary>
    /// 勇者，全部平衡
    /// </summary>
    Brave,
    /// <summary>
    /// 弓箭手，攻击稍高，速度较快，防御和血量偏低
    /// </summary>
    Archer,
    /// <summary>
    /// 圣骑士，防御高，血量高，攻击低，速度稍低
    /// </summary>
    Paladin,
    /// <summary>
    /// 法师，防御低，血量低，攻击高
    /// </summary>
    Master,
}

public enum ItemPlace
{
    Weapon,//武器
    Deputy,//副手
    Head,//头部
    Chest,//胸部
    Foot,//脚部
    Hand,//护手
    Necklace,//项链
    Ornament1,//饰品栏1
    Ornament2,//饰品栏2
}

public class Item
{
    public int m_Id;
    //预制ID
    public int m_DescId;
    //穿戴该装备的英雄
    public int m_Parent;
    public int m_Id;
    public string m_Name;
    public string m_Res;
    public int m_Quality;
    public int m_ParentModel;
    public ItemPlace m_Type;
    public int m_Level;
    public Ability m_Ability;
    public string m_Message;
    public List<string[]> m_AttributeList;
    public List<string[]> m_ExtraAttributeList;
    //幻象物体
    public GameObject m_Vision;
    public List<HeroJob> m_JobList { get => DataManager.Instanse.m_ItemDescContainer.GetDescByID(m_DescId).m_JobList; }

    public class ItemJson
    {
        public int id;
        public int parentModel;
        public List<string[]> m_ExtraAttributeList;

        public ItemJson()
        {

        }

        public ItemJson(Item item)
        {
            id = item.m_Id;
            parentModel = item.m_ParentModel;
            m_ExtraAttributeList = item.m_ExtraAttributeList;
            
        }
    }

    public Item()
    {
        m_AttributeList = new List<string[]>();
        m_ExtraAttributeList = new List<string[]>();
    }

    public Item(ItemDesc desc)
    {
        m_Id = BaseData.Instanse.m_MainCrotroller.m_IdIndex++;
        m_DescId = desc.m_ID;
        m_AttributeList = new List<string[]>();
        m_ExtraAttributeList = new List<string[]>();
        m_Name = desc.m_Name;
        m_Res = desc.m_Script;
        m_Quality = GetQuality(desc.m_Quality);
        m_Type = (ItemPlace)(desc.m_Type - 1);
        m_Level = GetLevel();
        if (desc.m_AbilityList.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, desc.m_AbilityList.Count);
            m_Ability = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(desc.m_AbilityList[index]));
        } 
        m_Message = desc.m_Message;
<<<<<<< HEAD
        //m_AttributeList = desc.m_AttributeList;
=======
        m_AttributeList = desc.m_AttributeList;
        m_ParentModel = desc.m_ID;
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915

        //随机词缀
        //普通为1词缀
        //稀有为2词缀
        //史诗为3词缀
        //传说为4词缀
        //可以自选一个特殊词缀附魔为第5词缀

        if (desc.m_AttributeList.Count > 0)
        {
<<<<<<< HEAD
            for(int i = 0; i < desc.m_AttributeList.Count; i++)
            {
                m_AttributeList.Add(desc.m_AttributeList[i]);
            }
        }
        else
            AddCommonAttribute();

        for (int i = 0; i < m_Quality; i++)
        {
            m_ExtraAttributeList.Add(DataManager.Instanse.m_AttributeListDescContainer.GetRandomAttribute(this));
        }
    }

    public Item(ItemJson json)
=======
            //武器：百分比攻击，固定攻击，暴击率，暴击伤害，百分比防御，防御，生命
            case ItemPlace.Weapon:
                for(int i = 0; i < m_Quality; i++)
                {
                    m_ExtraAttributeList.Add(DataManager.Instanse.m_AttributeListDescContainer.GetRandomAttribute(this));
                }
                break;
            //副手：百分比攻击，固定攻击，暴击率，暴击伤害，百分比防御，防御，生命，速度
            case ItemPlace.Deputy:
                for (int i = 0; i < m_Quality; i++)
                {
                    m_ExtraAttributeList.Add(DataManager.Instanse.m_AttributeListDescContainer.GetRandomAttribute(this));
                }
                break;
        }
    }

    /// <summary>
    /// json初始化方法
    /// </summary>
    /// <param name="json"></param>
    public Item(ItemJson json)
    {
        ItemDesc desc = DataManager.Instanse.m_ItemDescContainer.GetDescByID(json.parentModel);
        m_AttributeList = new List<string[]>();
        m_ExtraAttributeList = new List<string[]>();
        m_Name = desc.m_Name;
        m_Res = desc.m_Script;
        m_Quality = desc.m_Quality;
        m_Type = (ItemPlace)(desc.m_Type - 1);
        m_Level = desc.m_Level;
        if (m_Type == ItemPlace.Weapon)
            m_Ability = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(desc.m_Ability));
        m_Message = desc.m_Message;
        m_AttributeList = desc.m_AttributeList;
        m_ParentModel = desc.m_ID;

        foreach(string[] extraAttribute in json.m_ExtraAttributeList)
        {
            m_ExtraAttributeList.Add(extraAttribute);
        }
    }

    public List<string> GetAttributesToString()
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
    {
        m_AttributeList = new List<string[]>();
        m_ExtraAttributeList = new List<string[]>();

        m_Id = json.m_Id;
        m_DescId = json.m_DescId;
        m_Parent = json.m_Parent;
        m_Level = json.m_Level;
        m_Quality = json.m_Quality;
        m_ExtraAttributeList = json.m_ExtraAttributeList;
        if (json.m_Ability != 0)
            m_Ability = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(json.m_Ability));

        ItemDesc desc = DataManager.Instanse.m_ItemDescContainer.GetDescByID(m_DescId);
        m_Name = desc.m_Name;
        m_Res = desc.m_Script;
        m_Type = (ItemPlace)(desc.m_Type - 1);
        m_Message = desc.m_Message;

        if (desc.m_AttributeList.Count > 0)
        {
            for (int i = 0; i < desc.m_AttributeList.Count; i++)
            {
                m_AttributeList.Add(desc.m_AttributeList[i]);
            }
        }
        else
            AddCommonAttribute();

        if (m_Parent != 0)
        {
            ModelData hero = BaseData.Instanse.m_MainCrotroller.m_Player.GetHero(m_Parent);
            if (hero != null)
                BaseData.Instanse.m_MainCrotroller.m_Player.GetHero(m_Parent).UpdateItem(this);
            else
                BaseData.Instanse.m_MainCrotroller.GetBarHero(m_Parent).UpdateItem(this);
        }

        if (json.m_IsPlayer)
            BaseData.Instanse.m_MainCrotroller.m_Player.itemList.Add(this);
    }

    /// <summary>
    /// 获取当前装备等级
    /// </summary>
    public int GetLevel()
    {
        return BaseData.Instanse.m_MainCrotroller.PlayerLevel;
        //int level = 1;
        //int returnLevel = 0;
        //foreach(ModelData data in BaseData.Instanse.m_MainCrotroller.m_Player.heroList)
        //{
        //    if (data.Level > level && data.Level / 10 > returnLevel)
        //    {
        //        returnLevel = data.Level / 10;
        //        level = returnLevel * 10;
        //    }
        //}
        //if (returnLevel == 0)
        //    return 1;
        //else
        //    return returnLevel * 10;
    }

    /// <summary>
    /// 获取品质
    /// </summary>
    public int GetQuality(int minQuality)
    {
        int Quality = minQuality;
        int randomIndex = UnityEngine.Random.Range(1, 101);
        if (randomIndex >= 95)
            Quality = 4;
        else if (randomIndex >= 80)
        {
            if (3 >= minQuality)
                Quality = 3;
        }
        else if (randomIndex >= 50)
        {
            if (2 >= minQuality)
                Quality = 2;
        }
        else
        {
            if (1 >= minQuality)
                Quality = 1;
        }
        return Quality;
    }

    public void AddCommonAttribute()
    {
        List<AttributeListDesc> list = DataManager.Instanse.m_AttributeListDescContainer.m_DicAttributeList[m_Type];
        AttributeListDesc attributeDesc = list[0];
        int value = 0;
        //添加基础属性
        if (m_Level >= 50)
            value = attributeDesc.m_Max6;
        else if (m_Level >= 40)
            value = attributeDesc.m_Max5;
        else if (m_Level >= 30)
            value = attributeDesc.m_Max4;
        else if (m_Level >= 20)
            value = attributeDesc.m_Max3;
        else if (m_Level >= 10)
            value = attributeDesc.m_Max2;
        else if (m_Level >= 1)
            value = attributeDesc.m_Max1;
        m_AttributeList.Add(new string[] { attributeDesc.m_Attribute, value.ToString() });
    }

    /// <summary>
    /// 给英雄添加装备属性
    /// </summary>
    /// <param name="hero"></param>
    public void AddItemToHero(ModelData hero)
    {
        m_Parent = hero.Id;

        foreach(string[] attribute in m_AttributeList)
        {
            HandleAttribute(attribute[0], attribute[1], 1, hero);
        }
        foreach (string[] attribute in m_ExtraAttributeList)
        {
            HandleAttribute(attribute[0], attribute[1], 1, hero);
        }

        if (m_Type == ItemPlace.Weapon)
        {
            hero.m_CommonAbility = m_Ability;
        }
        else
        {
            if (m_Ability != null)
                hero.m_PassiveAbilityList.Add(m_Ability);
        }
    }

    /// <summary>
    /// 从英雄身上移除装备属性
    /// </summary>
    public void RemoveItemToHero()
    {
        ModelData hero = BaseData.Instanse.m_MainCrotroller.m_Player.GetHero(m_Parent);
        hero.m_CommonAbility = null;

        foreach (string[] attribute in m_AttributeList)
        {
            HandleAttribute(attribute[0], attribute[1], -1, hero);
        }
        foreach (string[] attribute in m_ExtraAttributeList)
        {
            HandleAttribute(attribute[0], attribute[1], -1, hero);
        }

        if(m_Type!= ItemPlace.Weapon)
        {
            if (m_Ability != null)
                hero.m_PassiveAbilityList.Remove(m_Ability);
        }
    }

    /// <summary>
    /// 处理各项属性词缀
    /// </summary>
    /// <param name="attributeName">属性名</param>
    /// <param name="value">属性值</param>
    /// <param name="PN">正负</param>
    /// <param name="hero">处理目标</param>
    public void HandleAttribute(string attributeName,string value,int PN,ModelData hero)
    {
        switch (attributeName)
        {
            case "atk":
                hero.ItemAttack += int.Parse(value) * PN;
                break;
            case "Patk":
                hero.PAttack += int.Parse(value) * PN;
                break;
            case "hp":
                hero.ItemHp += int.Parse(value) * PN;
                break;
            case "Php":
                hero.PHp += int.Parse(value) * PN;
                break;
            case "Pdef":
                hero.PDefense += int.Parse(value) * PN;
                break;
            case "def":
                hero.Defense += int.Parse(value) * PN;
                break;
            case "spd":
                hero.ItemSpeed += int.Parse(value) * PN;
                break;
            case "Pspd":
                hero.PSpeed += int.Parse(value) * PN;
                break;
            case "crit":
                hero.m_Crit += int.Parse(value) * PN;
                break;
            case "critDmg":
                hero.m_CritDamage += int.Parse(value) * PN;
                break;
        }
    }

    public class ItemJson
    {
        public int m_Id;
        public int m_DescId;
        public int m_Parent;
        public int m_Level;
        public int m_Quality;
        public int m_Ability;
        public bool m_IsPlayer;
        public List<string[]> m_ExtraAttributeList;

        public ItemJson()
        {
            m_ExtraAttributeList = new List<string[]>();
        }

        public void InitJson(Item item)
        {
            m_Id = item.m_Id;
            m_DescId = item.m_DescId;
            m_Parent = item.m_Parent;
            m_ExtraAttributeList = item.m_ExtraAttributeList;
            m_Level = item.m_Level;
            m_Quality = item.m_Quality;
            if (item.m_Ability != null)
                m_Ability = item.m_Ability.Id;
            if (BaseData.Instanse.m_MainCrotroller.m_Player.GetItem(m_Id) != null)
                m_IsPlayer = true;
        }
    }
}

public class Ability
{
    //ID
    private int _Id;
    //技能名
    private string _Name;
    //函数名
    private string _ScriteName;
    //是否被动
    private bool _IsPassive;
    //0=可使用
    private int _CD;
    //使用后CD时间
    private int _UseCD;
    //技能等级
    private int _Level;
    //使用条件
    public AbilityCondition m_Condition;

    public int Id { get => _Id; set => _Id = value; }
    public string Name { get => _Name; set => _Name = value; }
    public string ScriteName { get => _ScriteName; set => _ScriteName = value; }
    public bool IsPassive { get => _IsPassive; set => _IsPassive = value; }
    public int CD { get => _CD; set => _CD = value; }
    public int UseCD { get => _UseCD; set => _UseCD = value; }
    public int Level { get => _Level; set => _Level = value; }

    public Ability() { }

    public Ability(AbilityDesc desc)
    {
        _Id = desc.m_ID;
        _Name = desc.m_Name;
        _ScriteName = desc.m_Script;
        _IsPassive = desc.m_IsPassive;
        _CD = desc.m_InitCD;
        _UseCD = desc.m_CD;
        m_Condition = desc.m_Condition;
    }

    /// <summary>
    /// 施放技能
    /// </summary>
    /// <param name="caster">施法者</param>
    /// <returns></returns>
    public virtual List<BattleModel> Cast(BattleModel caster)
    {
        Type type = BaseData.Instanse.m_AbilityType;
        ModelGridCollection team;
        ModelGridCollection enemyTeam;
        ModelGridCollection left = ExpeditionCrotroller.Instanse.m_DicLeftModelGrid;
        ModelGridCollection right = ExpeditionCrotroller.Instanse.m_DicRightModelGrid;
        if (caster.m_IsPlayer) 
        {
            team = left;
            enemyTeam = right;
        }
        else
        {
            team = right;
            enemyTeam = left;
        }

        object[] parameters = new object[] { caster, _Level, team, enemyTeam };
        MethodInfo method = type.GetMethod(_ScriteName);
        _CD = _UseCD;
        return method.Invoke(null, parameters) as List<BattleModel> ;
    }

    public bool CanCast(BattleModel caster)
    {
        if (_CD == 0)
        {
            if (JudgeCondition(caster))
                return true;
        }
        return false;
    }

    /// <summary>
    /// 判断施放条件
    /// </summary>
    public bool JudgeCondition(BattleModel caster)
    {
        switch (m_Condition)
        {
            case AbilityCondition.none:
                return true;
            case AbilityCondition.health:
                if (caster.m_IsPlayer)
                {
                    foreach (int modelId in ExpeditionCrotroller.Instanse.m_DicLeftModelGrid.m_ModelList)
                    {
                        BattleModel model = BaseData.Instanse.GetModelById<BattleModel>(modelId);
                        if (model.HP != model.TotalHP)
                        {
                            return true;
                        }
                    }
                }
                break;
        }
        return false;
    }

    public enum AbilityCondition
    {
        none,
        health,
    }
}
