using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[System.Serializable]
public class MainCrotroller : MonoBehaviour
{
    //玩家类
    public PlayerModel m_Player;
    //玩家等级
    private int m_PlayerLevel;
    //游戏状态
    private GameState m_GameState;
    //对象字典(key为物体ID）
    private Dictionary<int, BaseModel> m_ModelDic;
    //年份(回合数)
    public int m_RoundYear;
    //魔王城来临回合
    public int m_BossRound;
    //小队最大人数
    public int m_TeamMaxCount;
    //ID生成指针
    public int m_IdIndex;
    //任务列表
    public List<TaskDesc> m_TaskList;
    //新手教程阶段
    public NoobState m_NoobState;
    //战斗场景
    public GameObject m_BattleObj;
    //主城场景
    public GameObject m_HomeObj;
    //队伍界面站位
    public ModelGridCollection m_DicTeamModelGrid;
    //酒馆中的英雄
    public List<ModelData> m_BarHeroList;
    //酒馆英雄刷新数
    public int m_BarHeroCount;
    //酒馆可操作次数
    public int m_BarUpdateCount;
    //酒馆已操作次数
    public int m_BarUpdateUseCount;

    public int PlayerLevel {
        get => m_PlayerLevel;
        set
        {
            m_PlayerLevel = value;
            LevelUp();
        }
    }

    void Awake()
    {
        m_IdIndex = 1;
        m_RoundYear = 1;
        m_BarHeroCount = 3;
        m_BarUpdateCount = 1;
        m_TeamMaxCount = 5;
        DontDestroyOnLoad(transform.gameObject);
        m_GameState = GameState.Menu;
        DataManager.Instanse.Load();
        BaseData.Instanse.m_MainCrotroller = this;
        m_TaskList = new List<TaskDesc>();
        BaseData.Instanse.m_AbilityType = Type.GetType("AbilityCrotroller");
        BaseData.Instanse.m_TaskType = Type.GetType("TaskCrotroller");
        BaseData.Instanse.m_HPPrefab = Resources.Load<GameObject>("Prefab/Battle/HP");
        BaseData.Instanse.m_WhiteMask = Resources.Load<Sprite>("UI/WhiteMask");
        BaseData.Instanse.m_DefaultAbility = new Ability(DataManager.Instanse.m_AbilityDescContainer.GetDescByID(10000));
        BaseData.Instanse.m_Question = Resources.Load<Sprite>("UI/Question");

        //添加音效
        string path = Res_Def.m_UISound;
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>(path);
        foreach (AudioClip audioClip in audioClips)
        {
            BaseData.Instanse.m_DicAudio.Add(audioClip.name, audioClip);
        }

        //添加物品图标
        path = Res_Def.m_ItemSprite;
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        foreach(Sprite sprite in sprites)
        {
            BaseData.Instanse.m_DicItemSprite.Add(sprite.name, sprite);
        }

        //添加材料图标
        path = Res_Def.m_MaterialSprite;
        sprites = Resources.LoadAll<Sprite>(path);
        foreach (Sprite sprite in sprites)
        {
            BaseData.Instanse.m_DicMaterialSprite.Add(sprite.name, sprite);
        }

        //添加Buff图标
        path = Res_Def.m_BuffSprite;
        sprites = Resources.LoadAll<Sprite>(path);
        foreach (Sprite sprite in sprites)
        {
            BaseData.Instanse.m_DicBuffSprite.Add(sprite.name, sprite);
        }

        //添加技能图标
        path = Res_Def.m_AbilitySprite;
        sprites = Resources.LoadAll<Sprite>(path);
        foreach (Sprite sprite in sprites)
        {
            BaseData.Instanse.m_DicAbilitySprite.Add(sprite.name, sprite);
        }

        //添加地图图片
        //path = Res_Def.m_MapSprite;
        //sprites = Resources.LoadAll<Sprite>(path);
        //foreach (Sprite sprite in sprites)
        //{
        //    BaseData.Instanse.m_DicMapSprite.Add(sprite.name, sprite);
        //}

        //添加关卡进度图片
        path = Res_Def.m_Progress;
        sprites = Resources.LoadAll<Sprite>(path);
        ExpeditionCrotroller.m_ProgressArr = new Sprite[sprites.Length];
        for(int i = 0; i < sprites.Length; i++)
        {
            ExpeditionCrotroller.m_ProgressArr[i] = sprites[i];
        }

        //添加模型
        path = Res_Def.m_BattleModel;
        GameObject[] objArr = Resources.LoadAll<GameObject>(path);
        foreach(GameObject obj in objArr)
        {
            BaseData.Instanse.m_DicModelObj.Add(obj.name, obj);
        }

        m_BarHeroList = new List<ModelData>();
        m_ModelDic = new Dictionary<int, BaseModel>();
        m_DicTeamModelGrid = new ModelGridCollection();
        m_Player = transform.gameObject.AddComponent<PlayerModel>();
    }

    /// <summary>
    /// 购买英雄方法
    /// </summary>
    public void BuyHero(ModelData data)
    {
        m_BarHeroList.Remove(data);
        m_Player.AddHero(data);
        m_Player.itemList.Add(data.m_DicItem[ItemPlace.Weapon]);
        m_BarUpdateUseCount++;
        EventCrotroller.Instance.GetHeroed(data);
        if(m_BarUpdateUseCount == m_BarUpdateCount)
        {
            m_BarHeroList.Clear();
            BaseData.Instanse.m_CameraCrotroller.UpdateBar();
        }
    }

    /// <summary>
    /// 遣散英雄方法
    /// </summary>
    public void LeaveHero(ModelData data)
    {
        m_BarHeroList.Remove(data);
    }

    public void WaitLoadBattle()
    {
        AsyncOperation sceneState = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GameMenu");
        TimeCrotroller.Instance.AddTimerTask(0.1f, "WaitLoad", delegate ()
        {
        }, 1,delegate()
        {
            if (sceneState.isDone)
            {
                InitBattleScene();
                InitCommon();
                return 0;
            }
            return 1;
        });
    }

    public void WaitLoadBattle(SLCrotroller.SaveData data)
    {
        AsyncOperation sceneState = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GameMenu");
        TimeCrotroller.Instance.AddTimerTask(0.1f, "WaitLoad", delegate ()
        {
        }, 1, delegate ()
        {
            if (sceneState.isDone)
            {
                InitBattleScene(data);
                return 0;
            }
            return 1;
        });
    }

    private IEnumerator PauseSwitchScene(Text text, AsyncOperation sceneState)
    {
        bool IsGoOn = false;
        while (!IsGoOn)
        {
            if (sceneState.progress >= 0.9f)
            {
                text.text = "按任意键继续";
                if (Input.anyKeyDown)
                {
                    IsGoOn = true;
                    sceneState.allowSceneActivation = true;
                    yield break;
                }
            }
            yield return null;
        }
    }

    /// <summary>
    /// 基础内容初始化
    /// </summary>
    public void InitCommon()
    {
        BaseData.Instanse.m_CameraCrotroller = Camera.main.gameObject.AddComponent<CameraCrotroller>();
        BaseData.Instanse.m_AudioCrotroller = Camera.main.gameObject.AddComponent<AudioCrotroller>();
        ExpeditionCrotroller.Instanse = new ExpeditionCrotroller();
<<<<<<< HEAD
        ExpeditionCrotroller.Instanse.m_BattleAnimator = GameObject.Find("Airship/ExpeditionBattle").GetComponent<Animator>();
        m_Player.gold += 10000;
        m_PlayerLevel = 1;

        BaseData.Instanse.m_ModelPool = GameObject.Find("Airship/ModelPool");

        //添加基础图纸
        AddCommonFormula();
=======
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915

        //读取存档
        SLCrotroller.SaveData saveData = SLCrotroller.Instance.GetSaveData();
        if (saveData != null)
        {
<<<<<<< HEAD
            ModelGrid grid = new ModelGrid();
            grid.gridObj = GameObject.Find("Airship/TeamPanel/Left_" + i);
            grid.gridIndex = i;
            m_DicTeamModelGrid.m_dicModelGrid.Add(i, grid);
=======
            PlayerModel.PlayerJson json = saveData.m_PlayerJson;
            m_Player.gold = json.m_Gold;
            m_IdIndex = json.m_IdIndex;
            foreach (ModelData.HeroDataJson data in json.m_HeroJson.m_HeroList)
            {
                m_Player.AddHero(new ModelData(data));
            }
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
        }
        else
        {

            m_Player.gold += 10000;

<<<<<<< HEAD
        UpdateBar();

        new ModelData("勇士", HeroJob.Brave, 5, true, "Test");
        new ModelData("法师", HeroJob.Master, 5, true, "Master");
        m_BossRound = 13;
        TaskCrotroller.StartTask(DataManager.Instanse.m_TaskDescContainer.GetDescByID(10001));

        //m_Player.itemFormulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10001));
        //m_Player.itemFormulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10002));
=======
            for (int i = 1; i <= 9; i++)
            {
                ModelGrid grid = new ModelGrid();
                grid.gridObj = GameObject.Find("Airship/Canvas/TeamPanel/Left_" + i);
                grid.gridIndex = i;
                m_DicTeamModelGrid.m_dicModelGrid.Add(i, grid);
            }

            ModelData data1 = new ModelData("狗头人勇士", HeroJob.Brave, 5, true, "Test");
            ModelData data2 = new ModelData("狗头人法师", HeroJob.Master, 5, true, "Test");
            BaseData.Instanse.m_MainCrotroller.m_Player.AddHero(data1);
            BaseData.Instanse.m_MainCrotroller.m_Player.AddHero(data2);

            m_Player.itemFormulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10001));
            m_Player.itemFormulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10002));
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915

            //foreach (ModelData modelData in m_Player.heroList)
            //{
            //    int id = CreateModel(modelData, new Vector3(9999, 9999, 0));
            //    modelData.ModelId = id;
            //}

            SetState(GameState.Game);

            BaseData.Instanse.m_CameraCrotroller.UpdateHeroList();

            ModelData data3 = new ModelData("酒馆测试", HeroJob.Archer, 10, true, "Test");
            BaseData.Instanse.m_MainCrotroller.m_Player.AddBarHero(data3);
        }
    }

    /// <summary>
    /// 远征教程方法
    /// </summary>
    public void ExpeditionIntroduce()
    {
        
    }

    /// <summary>
    /// 新档加载
    /// </summary>
    public void InitBattleScene()
    {
        m_NoobState = NoobState.ExpeditionIntroduce;
    }

    /// <summary>
    /// 加载存档
    /// </summary>
    /// <param name="data"></param>
    public void InitBattleScene(SLCrotroller.SaveData data)
    {
        BaseData.Instanse.m_CameraCrotroller = Camera.main.gameObject.AddComponent<CameraCrotroller>();
        BaseData.Instanse.m_AudioCrotroller = Camera.main.gameObject.AddComponent<AudioCrotroller>();
        ExpeditionCrotroller.Instanse = new ExpeditionCrotroller();
        ExpeditionCrotroller.Instanse.m_BattleAnimator = GameObject.Find("Airship/ExpeditionBattle").GetComponent<Animator>();
        m_Player.gold = data.m_Gold;
        m_IdIndex = data.m_IdIndex;
        m_RoundYear = data.m_Year;
        m_BossRound = data.m_BossRound;
        BaseData.Instanse.m_CameraCrotroller.GetText("yearText").text = "第" + m_RoundYear + "年";
        m_PlayerLevel = data.m_PlayerLevel;

        BaseData.Instanse.m_ModelPool = GameObject.Find("Airship/ModelPool");

        //加载英雄
        foreach(ModelData.ModelJson Json in data.m_HeroJsonList)
        {
            new ModelData(Json, true);
        }

        //加载酒馆英雄
        foreach (ModelData.ModelJson Json in data.m_BarJsonList)
        {
            new ModelData(Json, false);
        }

        //加载队伍阵型
        for (int i = 1; i <= 9; i++)
        {
            ModelGrid grid = new ModelGrid();
            grid.gridObj = GameObject.Find("Airship/TeamPanel/Left_" + i);
            grid.gridIndex = i;
            m_DicTeamModelGrid.m_dicModelGrid.Add(i, grid);
            int heroId = data.m_Team[i - 1];
            if (heroId != 0)
            {
                grid.modelData = m_Player.GetHero(heroId);
                m_DicTeamModelGrid.AddModel(i, grid);
            }
        }

        //加载装备
        foreach (Item.ItemJson Json in data.m_ItemJsonList)
        {
            new Item(Json);
        }

        //加载配方
        foreach(int id in data.m_ItemFormulaList)
        {
            m_Player.itemFormulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(id));
        }

        //加载材料
        foreach (string id in data.m_DicMaterial.Keys)
        {
            m_Player.AddMaterial(DataManager.Instanse.m_MaterialDescContainer.GetDescByID(int.Parse(id)), data.m_DicMaterial[id]);
        }

        //m_Player.itemFormulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10001));
        //m_Player.itemFormulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10002));

        //foreach (ModelData modelData in m_Player.heroList)
        //{
        //    int id = CreateModel(modelData, new Vector3(9999, 9999, 0));
        //    modelData.ModelId = id;
        //}

        SetState(GameState.Game);

        BaseData.Instanse.m_CameraCrotroller.UpdateHeroList();
    }

    void Update()
    {
        TimeCrotroller.Instance.UpdateTimer();
        if(ExpeditionCrotroller.Instanse !=null && ExpeditionCrotroller.Instanse.m_IsRun)
        {
            ExpeditionCrotroller.Instanse.Run();
            foreach (BattleModel model in ExpeditionCrotroller.Instanse.m_ExpeditionModelList)
            {
                if (model.m_IsAlive)
                    model.Run();
            }
        }
    }

    /// <summary>
    /// 结束本年
    /// </summary>
    public void YearEnd()
    {
        //增加一回合
        m_RoundYear++;
        UpdateBar();
    }

    /// <summary>
    /// 刷新酒馆
    /// </summary>
    public void UpdateBar()
    {
        //刷新酒馆
        m_BarHeroList.Clear();
        //刷新操作次数
        m_BarUpdateUseCount = 0;

        for (int i = 0; i < m_BarHeroCount; i++)
        {
            int heroIndex = UnityEngine.Random.Range(0, 3);
            int level = UnityEngine.Random.Range(m_PlayerLevel, m_PlayerLevel + 5);
            switch (heroIndex)
            {
                case 0:
                    m_BarHeroList.Add(new ModelData("勇者", HeroJob.Brave, level, false, "Test"));
                    break;
                case 1:
                    m_BarHeroList.Add(new ModelData("游侠", HeroJob.Archer, level, false, "Archer"));
                    break;
                case 2:
                    m_BarHeroList.Add(new ModelData("法师", HeroJob.Master, level, false, "Master"));
                    break;
            }
        }
    }

    /// <summary>
    /// 创建战斗模型方法
    /// </summary>
    /// <param name="createModel"></param>
    public int CreateModel(ModelData data, Transform parent)
    {
        GameObject prefab = BaseData.Instanse.m_DicModelObj[data.m_ModelKey];
        GameObject gameObject = Instantiate(prefab, parent);
        gameObject.name = "BattleModel";
        //添加血条
        AddHpSolder(gameObject);

        //Sprite goblin = Resources.Load<Sprite>("Monster");
        //SpriteRenderer render = gameObject.transform.Find("Body").GetComponent<SpriteRenderer>();
        //render.sprite = goblin;
        //render.size = new Vector2(100, 140);
        BattleModel battleModel = gameObject.AddComponent<BattleModel>();
        battleModel.m_Id = m_IdIndex;
        m_ModelDic.Add(m_IdIndex++, battleModel);
        if (m_Player.heroList.Contains(data))
            battleModel.m_IsPlayer = true;

        battleModel.m_Audio = gameObject.AddComponent<AudioPlay>();

        battleModel.InitModel(data);

        return battleModel.m_Id;
    }

    /// <summary>
    /// 创建战斗模型方法（指定ID），加载存档时使用
    /// </summary>
    /// <param name="createModel"></param>
    public int CreateModel(ModelData data, Transform parent, int id)
    {
        GameObject prefab = BaseData.Instanse.m_DicModelObj[data.m_ModelKey];
        GameObject gameObject = Instantiate(prefab, parent);
        gameObject.name = "BattleModel";

        //添加血条
        AddHpSolder(gameObject);

        BattleModel battleModel = gameObject.AddComponent<BattleModel>();
        battleModel.m_Id = m_IdIndex;
        m_ModelDic.Add(m_IdIndex++, battleModel);

        battleModel.InitModel(data);

        return battleModel.m_Id;
    }

    /// <summary>
    /// 删除方法
    /// </summary>
    /// <param name="id"></param>
    /// <param name="now"></param>
    public void DestoryModel(int id,bool now)
    {
        GameObject obj = GetModelById(id).gameObject;
        if (now)
            DestroyImmediate(obj);
        else
            Destroy(obj);

        m_ModelDic.Remove(id);
    }

    /// <summary>
    /// 获取游戏状态
    /// </summary>
    /// <returns></returns>
    public GameState GetState()
    {
        return m_GameState;
    }

    /// <summary>
    /// 设置游戏状态
    /// </summary>
    /// <param name="state"></param>
    public void SetState(GameState state)
    {
        m_GameState = state;
        if (state == GameState.Stop)
        {
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// 通过ID获取model对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BaseModel GetModelById(int id)
    {
        m_ModelDic.TryGetValue(id, out BaseModel value);
        return value;
    }

    /// <summary>
    /// 保存功能
    /// </summary>
    public void Save()
    {
        //清理列表
        List<int> deleteList = new List<int>();

        SLCrotroller.SaveData data = new SLCrotroller.SaveData();

        //保存队伍阵型
        foreach(ModelGrid grid in m_DicTeamModelGrid.m_dicModelGrid.Values)
        {
            if (grid.modelData != null)
            {
                data.m_Team[grid.gridIndex - 1] = grid.modelData.Id;
            }
        }

        //保存基础数据
        data.m_Gold = m_Player.gold;
        data.m_IdIndex = m_IdIndex;
        data.m_Year = m_RoundYear;
        data.m_BossRound = m_BossRound;
        data.m_PlayerLevel = m_PlayerLevel;

        //保存英雄Json
        foreach (ModelData hero in m_Player.heroList)
        {
            ModelData.ModelJson json = new ModelData.ModelJson();
            json.InitJson(hero);
            data.m_HeroJsonList.Add(json);
        }

        //保存酒馆英雄Json
        foreach (ModelData hero in m_BarHeroList)
        {
            ModelData.ModelJson json = new ModelData.ModelJson();
            json.InitJson(hero);
            data.m_BarJsonList.Add(json);

            Item item = hero.m_DicItem[ItemPlace.Weapon];
            Item.ItemJson itemJson = new Item.ItemJson();
            itemJson.InitJson(item);
            data.m_ItemJsonList.Add(itemJson);
        }

        //保存装备Json
        foreach (Item item in m_Player.itemList)
        {
            Item.ItemJson json = new Item.ItemJson();
            json.InitJson(item);
            data.m_ItemJsonList.Add(json);
        }
        
        //保存配方
        foreach(ItemFormulaDesc desc in m_Player.itemFormulaList)
        {
            data.m_ItemFormulaList.Add(desc.m_ID);
        }

        //保存材料
        foreach(MaterialDesc desc in m_Player.dicMaterial.Keys)
        {
            data.m_DicMaterial.Add(desc.m_ID.ToString(), m_Player.dicMaterial[desc]);
        }

        //清理model
        foreach (int id in deleteList)
        {
            m_ModelDic.Remove(id);
        }

        SLCrotroller.Instance.SaveDataToJson(data);
    }

    /// <summary>
    /// 添加血条预制
    /// </summary>
    public void AddHpSolder(GameObject obj)
    {
        GameObject HP = Instantiate(BaseData.Instanse.m_HPPrefab, obj.transform.Find("HPos").transform);
        HP.name = "Canvas";
        HP.GetComponent<Canvas>().sortingLayerName = "Message";
    }

    /// <summary>
    /// 获取酒馆英雄
    /// </summary>
    public ModelData GetBarHero(int id)
    {
        foreach (ModelData hero in m_BarHeroList)
        {
            if (hero.Id == id)
                return hero;
        }
        return null;
    }

    public void AddCommonFormula()
    {
        List<ItemFormulaDesc> formulaList = new List<ItemFormulaDesc>();
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10004));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10005));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10006));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10007));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10008));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10009));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10010));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10011));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10012));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10013));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10014));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10015));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10022));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10023));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10024));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10025));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10026));
        formulaList.Add(DataManager.Instanse.m_ItemFormulaContainer.GetDescByID(10027));
        foreach (ItemFormulaDesc itemFormula in formulaList)
        {
            BaseData.Instanse.m_MainCrotroller.m_Player.itemFormulaList.Add(itemFormula);
        }
    }

    /// <summary>
    /// 升级方法
    /// </summary>
    public void LevelUp()
    {
        
    }
}

public enum GameState
{
    Menu,
    Game,
    Stop,
}

public enum NoobState
{
    AirshipIntroduce,//飞艇介绍，交代背景
    ExpeditionIntroduce,//远征介绍，交代战斗方式
    Team,//队伍系统介绍
    WorldMap,//世界地图介绍
}
