using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : BaseModel
{
<<<<<<< HEAD
=======
    public class PlayerJson
    {
        public int m_Gold;
        public int m_IdIndex;
        //配方列表
        public List<int> m_ItemFormulaList;
        //材料列表
        public List<MaterialJson> m_MaterialList;
        //装备列表
        public List<Item.ItemJson> m_ItemList;
        //英雄json
        public HeroJson m_HeroJson;

        public class MaterialJson
        {
            public int m_Id;
            public int m_Count;

            public MaterialJson()
            {

            }

            public MaterialJson(int id,int count)
            {
                m_Id = id;
                m_Count = count;
            }
        }

        public class HeroJson
        {
            public List<ModelData.HeroDataJson> m_HeroList;
            public List<ModelData.HeroDataJson> m_BarHeroList;

            public HeroJson()
            {

            }

            public HeroJson(PlayerModel model)
            {
                m_HeroList = new List<ModelData.HeroDataJson>();
                m_BarHeroList = new List<ModelData.HeroDataJson>();

                foreach(ModelData data in model.m_HeroList)
                {
                    m_HeroList.Add(new ModelData.HeroDataJson(data));
                }
                foreach(ModelData data in model.m_BarModelList)
                {
                    m_BarHeroList.Add(new ModelData.HeroDataJson(data));
                }
            }
        }

        public PlayerJson()
        {
        }

        public PlayerJson(PlayerModel model)
        {
            m_Gold = model.m_iGold;
            Debug.Log("gold:" + m_Gold);
            m_IdIndex = BaseData.Instanse.m_MainCrotroller.m_IdIndex;
            Debug.Log("HeroIndex:" + m_IdIndex);
            m_HeroJson = new HeroJson(model);
            Debug.Log("HeroCount:" + m_HeroJson.m_HeroList.Count);
            Debug.Log("BarHeroCount:" + m_HeroJson.m_BarHeroList.Count);

            m_MaterialList = new List<MaterialJson>();
            foreach(MaterialDesc material in model.m_DicMaterial.Keys)
            {
                m_MaterialList.Add(new MaterialJson(material.m_ID, model.m_DicMaterial[material]));
            }
            Debug.Log("MaterialList:" + m_MaterialList.Count);

            m_ItemFormulaList = new List<int>();
            foreach(ItemFormulaDesc itemFormula in model.m_ItemFormulaList)
            {
                m_ItemFormulaList.Add(itemFormula.m_ID);
            }
            Debug.Log("ItemFormula:" + m_ItemFormulaList.Count);

            m_ItemList = new List<Item.ItemJson>();
            foreach(Item item in model.m_ItemList)
            {
                m_ItemList.Add(new Item.ItemJson(item));
            }
            Debug.Log("ItemList:" + m_ItemList.Count);
        }
    }
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915

    //玩家金钱
    private int m_iGold;
    public int gold
    {
        set { m_iGold = value; }
        get { return m_iGold; }
    }

    //玩家持有的英雄列表
    private List<ModelData> m_HeroList;
    public List<ModelData> heroList
    {
        get
        {
            if (m_HeroList == null)
                m_HeroList = new List<ModelData>();
            return m_HeroList;
        }
    }

    //酒吧随机英雄列表
    private List<ModelData> m_BarModelList;
    public List<ModelData> barModelList
    {
        get
        {
            if (m_BarModelList == null)
                m_BarModelList = new List<ModelData>();
            return m_BarModelList;
        }
    }

    //玩家持有的装备列表
    private List<Item> m_ItemList;
    public List<Item> itemList
    {
        get
        {
            if (m_ItemList == null)
                m_ItemList = new List<Item>();
            return m_ItemList;
        }
    }

    //玩家持有的材料列表
    private Dictionary<MaterialDesc, int> m_DicMaterial = new Dictionary<MaterialDesc, int>();
    public Dictionary<MaterialDesc, int> dicMaterial
    {
        get
        {
            return m_DicMaterial;
        }
    }

    //玩家持有的配方列表
    private List<ItemFormulaDesc> m_ItemFormulaList;
    public List<ItemFormulaDesc> itemFormulaList
    {
        get
        {
            if (m_ItemFormulaList == null)
                m_ItemFormulaList = new List<ItemFormulaDesc>();
            return m_ItemFormulaList;
        }
    }

    public PlayerModel()
    {
<<<<<<< HEAD

=======
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
    }

    /// <summary>
    /// 添加材料方法（可用来修改数量）
    /// </summary>
    /// <param name="material"></param>
    /// <param name="count"></param>
    public void AddMaterial(MaterialDesc material,int count)
    {
        if (m_DicMaterial.ContainsKey(material))
        {
            m_DicMaterial[material] += count;
        }
        else
        {
            m_DicMaterial.Add(material, count);
        }
    }

    /// <summary>
    /// 获取材料数量
    /// </summary>
    /// <param name="material"></param>
    /// <returns></returns>
    public int GetMaterial(MaterialDesc material)
    {
        if (m_DicMaterial.ContainsKey(material))
        {
            return m_DicMaterial[material];
        }
        else
        {
            return 0;
        }
    }

    public void AddHero(ModelData hero)
    {
<<<<<<< HEAD
        Debug.Log("Add Hero" + hero.Id + ":" + hero.Name);
=======
        if (hero.Id == 0)
        {
            hero.Id = BaseData.Instanse.m_MainCrotroller.m_IdIndex++;
        }
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
        heroList.Add(hero);
        foreach(ItemPlace key in hero.m_DicItem.Keys)
        {
            hero.m_DicItem.TryGetValue(key, out Item item);
            if (item != null)
            {
                itemList.Add(item);
            }
        }
    }

    public void AddBarHero(ModelData hero)
    {
        if (hero.Id == 0)
        {
            hero.Id = BaseData.Instanse.m_MainCrotroller.m_IdIndex++;
        }
        barModelList.Add(hero);
    }

    public ModelData GetHero(int id)
    {
        foreach(ModelData hero in heroList)
        {
            if (hero.Id == id)
                return hero;
        }
        return null;
    }

    public Item GetItem(int id)
    {
        foreach (Item item in itemList)
        {
            if (item.m_Id == id)
                return item;
        }
        return null;
    }
}
