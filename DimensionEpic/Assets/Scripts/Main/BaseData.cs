using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseData
{
    //声音字典
    public Dictionary<string, AudioClip> m_DicAudio = new Dictionary<string, AudioClip>();
    //Item图标字典
    public Dictionary<string, Sprite> m_DicItemSprite = new Dictionary<string, Sprite>();
    //Material图标字典
    public Dictionary<string, Sprite> m_DicMaterialSprite = new Dictionary<string, Sprite>();
    //buff图标字典
    public Dictionary<string, Sprite> m_DicBuffSprite = new Dictionary<string, Sprite>();
    //技能图标字典
    public Dictionary<string, Sprite> m_DicAbilitySprite = new Dictionary<string, Sprite>();
    //map地图字典
    public Dictionary<string, Sprite> m_DicMapSprite = new Dictionary<string, Sprite>();
    //model模型字典
    public Dictionary<string, GameObject> m_DicModelObj = new Dictionary<string, GameObject>();
    //模型池(用于生产和存放各类模型的路径)
    public GameObject m_ModelPool;
    //战斗模型血条预制
    public GameObject m_HPPrefab;
    //透明图片
    public Sprite m_WhiteMask;
    //问号图片
    public Sprite m_Question;
    //默认技能
    public Ability m_DefaultAbility;

    private static BaseData m_Instanse;

    public static BaseData Instanse
    {
        get
        {
            if (m_Instanse == null)
            {
                m_Instanse = new BaseData();
            }
            return m_Instanse;
        }
    }

    /// <summary>
    /// 通过下标获取，从0开始，T为Key
    /// </summary>
    /// <returns></returns>
    public T GetDictionaryByIndex<T,V>(Dictionary<T,V> dic, int index)
    {
        int dicIndex = 0;
        foreach(T key in dic.Keys)
        {
            if (dicIndex == index)
                return key;
        }
        return default(T);
    }

    public static List<Vector2> m_ModelPos = new List<Vector2>()
    {
        new Vector2(65,-290),
    };

    //对应等级的升级经验
    public static List<int> m_Explist = new List<int>()
    {
        0, //0
        50, //1
        50, //2
        50, //3
        50, //4
        100, //5
        100, //6
        100, //7
        100, //8
        100, //9
        300, //10
        300, //11
        300, //12
        300, //13
        300, //14
        400, //15
        400, //16
        400, //17
        400, //18
        400, //19
        800, //20
        800, //21
        800, //22
        800, //23
        800, //24
        1000, //25
        1000, //26
        1000, //27
        1000, //28
        1000, //29
        1500, //30
        1500, //31
        1500, //32
        1500, //33
        1500, //34
        2000, //35
        2000, //36
        2000, //37
        2000, //38
        2000, //39
        3500, //40
        3500, //41
        3500, //42
        3500, //43
        3500, //44
        5000, //45
        5000, //46
        5000, //47
        5000, //48
        5000, //49
    };

    public enum State
    {
        none,
        stun,
    }

    /// <summary>
    /// 总控制类
    /// </summary>
    public MainCrotroller m_MainCrotroller;
    /// <summary>
    /// 摄像机类
    /// </summary>
    public CameraCrotroller m_CameraCrotroller;
    /// <summary>
    /// 音效类
    /// </summary>
    public AudioCrotroller m_AudioCrotroller;
    /// <summary>
    /// 技能类，用来调用方法
    /// </summary>
    public Type m_AbilityType;
    /// <summary>
    /// 任务类，用来调用方法
    /// </summary>
    public Type m_TaskType;

    /// <summary>
    /// 获取model方法
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public T GetModelById<T>(int id)
    {
        BaseModel model = m_MainCrotroller.GetModelById(id);
        return (T)(object)model;
    }

    /// <summary>
    /// 根据品质获取边框颜色
    /// </summary>
    /// <param name="quality"></param>
    /// <returns></returns>
    public Color GetColorByQuality(int quality)
    {
        switch (quality)
        {
            case 1:
                return Color.white;
            case 2:
                return Color.green;
            case 3:
                return Color.blue;
            case 4:
                return Color.red;
        }
        return Color.white;
    }

    /// <summary>
    /// 发送提示
    /// </summary>
    /// <param name="message"></param>
    public void SendMessage(string message, float time)
    {
        m_CameraCrotroller.SendMessage(message, time);
    }

    /// <summary>
    /// 发送头顶提示
    /// </summary>
    /// <param name="message"></param>
    public void SendMessage(string message, float time, Transform parent)
    {
        m_CameraCrotroller.SendMessage(message, time, parent);
    }

    /// <summary>
    /// 属性汉化
    /// </summary>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static string AttributeToChinese(string attribute)
    {
        if (attribute == "atk")
            return "攻击力";
        else if (attribute == "Patk")
            return "百分比攻击";
        else if (attribute == "spd")
            return "速度";
        else if (attribute == "Pspd")
            return "百分比速度";
        else if (attribute == "hp")
            return "生命";
        else if (attribute == "Php")
            return "百分比生命";
        else if (attribute == "Pdef")
            return "百分比防御";
        else if (attribute == "def")
            return "防御";
        else if (attribute == "crit")
            return "暴击";
        else if (attribute == "critDmg")
            return "爆伤";
        else
            return "???";
    }
}

public class ModelGridCollection
{
    public int m_AliveModelCount;
    public List<int> m_ModelList;
    //网格字典以几号位当做key
    public Dictionary<int, ModelGrid> m_dicModelGrid;
    //光环列表
    public List<BuffData> m_HaloList;

    public ModelGridCollection()
    {
        m_dicModelGrid = new Dictionary<int, ModelGrid>();
        m_ModelList = new List<int>();
        m_HaloList = new List<BuffData>();
    }

    /// <summary>
    /// 添加模型
    /// </summary>
    /// <param name="index"></param>
    /// <param name="grid"></param>
    public void AddModel(int index, ModelGrid grid)
    {
        m_dicModelGrid[index] = grid;
        m_AliveModelCount++;
        m_ModelList.Add(grid.battleModelId);
    }

    public void RemoveModel(int index)
    {
        m_ModelList.Remove(m_dicModelGrid[index].battleModelId);
        ModelGrid grid = m_dicModelGrid[index];
        grid.battleModelId = 0;
        grid.modelData = null;
        m_AliveModelCount--;
    }

    /// <summary>
    /// 获取模型处于几号位
    /// </summary>
    /// <param name="id">想要获取模型的ID</param>
    /// <returns></returns>
    public int GetIndex(int id)
    {
        foreach (ModelGrid model in m_dicModelGrid.Values)
        {
            if (model.battleModelId == id)
                return model.gridIndex;
        }
        return -1;
    }

    /// <summary>
    /// 获取数组内的对应位置网格
    /// 1、最前一列 2、最后一列 3、最上一行 4、最下一行 5、对应一行 6、最前一位 7、指定 8、所有
    /// 11、生命值最低的X位，X为参数
    /// </summary>
    /// <param name="type">1、最前一列 2、最后一列 3、最上一行 4、最下一行 5、对应一行 6、最前一位 7、指定</param>
    /// <param name="paramList">附带参数，可为null</param>
    /// <returns></returns>
    public List<ModelGrid> GetGrid(int type,int[] paramList)
    {
        List<ModelGrid> gridList = new List<ModelGrid>();
        switch (type)
        {
            case 1://最前一列
                foreach (ModelGrid grid in m_dicModelGrid.Values)
                {
                    if (grid.battleModelId != 0 && BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId).m_IsAlive) 
                    {
                        if (grid.gridIndex <= 3)
                        {
                            if (m_dicModelGrid[1].battleModelId != 0)
                                gridList.Add(m_dicModelGrid[1]);
                            if (m_dicModelGrid[2].battleModelId != 0)
                                gridList.Add(m_dicModelGrid[2]);
                            if (m_dicModelGrid[3].battleModelId != 0)
                                gridList.Add(m_dicModelGrid[3]);
                        }
                        else if(grid.gridIndex <= 6)
                        {
                            if (m_dicModelGrid[4].battleModelId != 0)
                                gridList.Add(m_dicModelGrid[4]);
                            if (m_dicModelGrid[5].battleModelId != 0)
                                gridList.Add(m_dicModelGrid[5]);
                            if (m_dicModelGrid[6].battleModelId != 0)
                                gridList.Add(m_dicModelGrid[6]);
                        }
                        else if(grid.gridIndex <= 9)
                        {
                            if (m_dicModelGrid[7].battleModelId != 0)
                                gridList.Add(m_dicModelGrid[7]);
                            if (m_dicModelGrid[8].battleModelId != 0)
                                gridList.Add(m_dicModelGrid[8]);
                            if (m_dicModelGrid[9].battleModelId != 0)
                                gridList.Add(m_dicModelGrid[9]);
                        }
                        break;
                    }
                }
                break;
            case 5:
                int line = paramList[0] % 3;
                if (line == 0)
                    line = 3;
                //判断对应行是否有敌人
                for (int i = 0; i <= 2; i++)
                {
                    ModelGrid grid = m_dicModelGrid[line + i * 3];
                    if (grid.battleModelId != 0 && BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId).m_IsAlive)
                    {
                        if (m_dicModelGrid[line].battleModelId != 0)
                            gridList.Add(m_dicModelGrid[line]);
                        if (m_dicModelGrid[line + 3].battleModelId != 0)
                            gridList.Add(m_dicModelGrid[line + 3]);
                        if (m_dicModelGrid[line + 6].battleModelId != 0)
                            gridList.Add(m_dicModelGrid[line + 6]);
                        break;
                    }
                }
                //如果对应行没有，则判断另外2行
                if (gridList.Count == 0)
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        if (i == line)
                            continue;
                        for (int k = 0; k <= 2; k++)
                        {
                            ModelGrid grid = m_dicModelGrid[i + k * 3];
                            if (grid.battleModelId != 0 && BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId).m_IsAlive)
                            {
                                if (m_dicModelGrid[i].battleModelId != 0)
                                    gridList.Add(m_dicModelGrid[i]);
                                if (m_dicModelGrid[i + 3].battleModelId != 0)
                                    gridList.Add(m_dicModelGrid[i + 3]);
                                if (m_dicModelGrid[i + 6].battleModelId != 0)
                                    gridList.Add(m_dicModelGrid[i + 6]);
                                break;
                            }
                        }
                    }
                }
                break;
            case 6://最前一位
                foreach (ModelGrid grid in m_dicModelGrid.Values)
                {
                    if (grid.battleModelId != 0 && BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId).m_IsAlive)
                    {
                        gridList.Add(grid);
                        break;
                    }
                }
                break;
            case 8:
                foreach(ModelGrid grid in m_dicModelGrid.Values)
                {
                    if (grid.IsAlive())
                    {
                        gridList.Add(grid);
                    }
                }
                break;
            case 11://生命值最低的X位
                int count = 0;

                List<BattleModel> modelList = new List<BattleModel>();
                foreach (ModelGrid grid in m_dicModelGrid.Values)
                {
                    if (grid.battleModelId != 0)
                        modelList.Add(BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId));
                }
                modelList.Sort((x, y) => ((float)(x.HP / x.TotalHP)).CompareTo(y.HP / y.TotalHP));
                foreach(BattleModel model in modelList)
                {
                    gridList.Add(model.m_Parent);
                    count++;
                    if (count == paramList[0])
                        break;
                }
                break;
        }
        return gridList;
    }
}

public class ModelGrid
{
    //所属英雄属性
    public ModelData modelData;
    //队伍中的几号位
    public int gridIndex;
    //网格中的战斗单位ID
    public int battleModelId;
    //网格主体
    public GameObject gridObj;

    public bool IsAlive()
    {
        if (battleModelId != 0)
        {
            BattleModel battleModel = BaseData.Instanse.GetModelById<BattleModel>(battleModelId);
            if (battleModel != null)
                if (battleModel.m_IsAlive)
                    return true;
        }
        return false;
    }
}

