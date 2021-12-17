using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExpeditionCrotroller
{
    private static ExpeditionCrotroller m_Instanse;
    public static ExpeditionCrotroller Instanse
    {
        get
        {
            return m_Instanse;
        }
        set { m_Instanse = value; }
    }
    //关卡进度图片集
    public static Sprite[] m_ProgressArr;
    //战斗场景背景
    public GameObject m_BattleBackground;
    //战斗场景进度列表
    public GameObject m_BattleProgressList;
    //进度条预制
    public GameObject m_ProgressPrefab;
    //远征场景动画
    public Animator m_BattleAnimator;
    //是否开始远征
    public bool m_IsRun;
    //远征状态
    private ExpeditionState m_State;
    public ExpeditionState state
    {
        get { return m_State; }
        set { m_State = value; }
    }
    //左侧战斗站位
    public ModelGridCollection m_DicLeftModelGrid;
    //右侧战斗站位
    public ModelGridCollection m_DicRightModelGrid;
    //单位列表
    public List<BattleModel> m_ExpeditionModelList;
    //展示地图
    public ExpeditionMapDesc m_ShowMap;
    //选中地图
    public ExpeditionMapDesc m_ChooseMap;
    //关卡进度(相当于第几小关)
    public int m_Progress;
    //关卡进度图片
    public Image m_ProgressShow;
    //本轮战斗积攒经验
    public int m_RoundExp;
    //本轮战斗积攒金币
    public int m_RoundGold;
    //本轮战斗积攒的装备
    public List<Item> m_RoundItemList;
    //本轮战斗积攒的材料
    public Dictionary<MaterialDesc, int> m_DicRoundMaterial;
    //关卡难度字典(key:展示地图id,实际难度地图id)
    public Dictionary<int, int> m_DicMapLevel;
    //战斗单位对应进度显示
    public Dictionary<BattleModel, GameObject> m_DicBattleProgress;

    public ExpeditionCrotroller()
    {
        m_ShowMap = DataManager.Instanse.m_ExpeditionMapDescContainer.GetDescByID(10101);
        m_DicLeftModelGrid = new ModelGridCollection();
        m_DicRightModelGrid = new ModelGridCollection();
        m_ExpeditionModelList = new List<BattleModel>();
        m_BattleBackground = GameObject.Find("Airship/ExpeditionBattle");
        m_ProgressShow = GameObject.Find("Airship/ExpeditionBattle/Progress").GetComponent<Image>();
        m_BattleProgressList = GameObject.Find("Airship/ExpeditionBattle/BattleProgress");
        m_ProgressPrefab = (GameObject)Resources.Load("Prefab/UI/ProgressIcon");
        m_RoundItemList = new List<Item>();
        m_DicRoundMaterial = new Dictionary<MaterialDesc, int>();
        m_DicMapLevel = new Dictionary<int, int>();
        m_DicBattleProgress = new Dictionary<BattleModel, GameObject>();

        for (int i = 1; i <= 9; i++)
        {
            ModelGrid grid = new ModelGrid();
            grid.gridObj = GameObject.Find("Airship/ExpeditionBattle/Left_" + i);
            grid.gridIndex = i;
            m_DicLeftModelGrid.m_dicModelGrid.Add(i, grid);
        }

        for (int i = 1; i <= 9; i++)
        {
            ModelGrid grid = new ModelGrid();
            grid.gridObj = GameObject.Find("Airship/ExpeditionBattle/Right_" + i);
            grid.gridIndex = i;
            m_DicRightModelGrid.m_dicModelGrid.Add(i, grid);
        }

        foreach(ExpeditionMapDesc desc in DataManager.Instanse.m_ExpeditionMapDescContainer.m_dicDesc.Values)
        {
            if (desc.m_Show)
                m_DicMapLevel.Add(desc.m_ID, desc.m_ID);
        }
    }

    /// <summary>
    /// 远征场景初始化
    /// </summary>
    public void ExpeditionInit()
    {
        m_BattleAnimator.SetTrigger(m_ChooseMap.m_Script);
        m_ProgressShow.sprite = m_ProgressArr[m_Progress];
        m_Progress++;
        int enemyCount = Random.Range(2, 5);

        m_ExpeditionModelList.Clear();
        m_DicLeftModelGrid.m_AliveModelCount = 0;
        m_DicLeftModelGrid.m_ModelList.Clear();
        m_DicRightModelGrid.m_AliveModelCount = 0;
        m_DicRightModelGrid.m_ModelList.Clear();

        if (m_Progress < m_ChooseMap.m_Round)
        {
            CreateRandomEnemy(m_DicRightModelGrid, enemyCount);
        }
        else
        {
            //生成Boss关卡
            foreach(int key in m_ChooseMap.m_DicBossRound.Keys)
            {
                ModelGrid grid = m_DicRightModelGrid.m_dicModelGrid[key];

                //生成敌人数据和战斗模型
                ModelData data = new ModelData(DataManager.Instanse.m_EnemyDescContainer.GetDescByID(m_ChooseMap.m_DicBossRound[key]));
                int id = BaseData.Instanse.m_MainCrotroller.CreateModel(data, grid.gridObj.transform);
                BattleModel enemy = BaseData.Instanse.GetModelById<BattleModel>(id);
                enemy.m_Parent = grid;

                grid.battleModelId = id;

                m_ExpeditionModelList.Add(enemy);
                GameObject progress = GameObject.Instantiate(m_ProgressPrefab, m_BattleProgressList.transform);
                progress.transform.Find("Background/Head").GetComponent<Image>().sprite =
                    BaseData.Instanse.m_CameraCrotroller.GetSprite("enemyHead" + enemy.m_Data.m_ModelKey);
                m_DicBattleProgress.Add(enemy, progress);
                m_DicRightModelGrid.m_AliveModelCount++;
                m_DicRightModelGrid.m_ModelList.Add(id);
            }
        }

        //通过玩家队伍生成英雄
        foreach (ModelGrid grid in BaseData.Instanse.m_MainCrotroller.m_DicTeamModelGrid.m_dicModelGrid.Values)
        {
            if (grid.modelData != null)
            {
                ModelGrid exGrid = m_DicLeftModelGrid.m_dicModelGrid[grid.gridIndex];
                Vector3 pos = exGrid.gridObj.transform.position;
                int id = BaseData.Instanse.m_MainCrotroller.CreateModel(grid.modelData, exGrid.gridObj.transform);
                BattleModel hero = BaseData.Instanse.GetModelById<BattleModel>(id);
                hero.m_Parent = exGrid;

                exGrid.battleModelId = id;
                exGrid.modelData = grid.modelData;

                m_ExpeditionModelList.Add(hero);
                GameObject progress = GameObject.Instantiate(m_ProgressPrefab, m_BattleProgressList.transform);
                progress.transform.Find("Background/Head").GetComponent<Image>().sprite = BaseData.Instanse.m_CameraCrotroller.GetSprite("heroHead" + hero.Job.ToString());
                m_DicBattleProgress.Add(hero, progress);
                m_DicLeftModelGrid.m_AliveModelCount++;
                m_DicLeftModelGrid.m_ModelList.Add(id);
            }
        }

        //添加光环
        foreach(BuffData halo in m_DicLeftModelGrid.m_HaloList)
        {
            foreach (int modelId in m_DicLeftModelGrid.m_ModelList)
            {
                BattleModel model = BaseData.Instanse.GetModelById<BattleModel>(modelId);
                model.AddBuff(halo);
            }
        }
        foreach (BuffData halo in m_DicRightModelGrid.m_HaloList)
        {
            foreach (int modelId in m_DicRightModelGrid.m_ModelList)
            {
                BattleModel model = BaseData.Instanse.GetModelById<BattleModel>(modelId);
                model.AddBuff(halo);
            }
        }

        m_State = ExpeditionState.Wait;
        m_IsRun = true;
    }

    /// <summary>
    /// 战斗结束方法
    /// </summary>
    public void BattleEnd()
    {
        m_State = ExpeditionState.Ready;

        //清除Model
        foreach (ModelGrid grid in m_DicLeftModelGrid.m_dicModelGrid.Values)
        {
            if (grid.battleModelId != 0)
            {
                BaseData.Instanse.m_MainCrotroller.DestoryModel(grid.battleModelId, true);
                grid.battleModelId = 0;
            }
        }
        foreach (ModelGrid grid in m_DicRightModelGrid.m_dicModelGrid.Values)
        {
            if (grid.battleModelId != 0)
            {
                BaseData.Instanse.m_MainCrotroller.DestoryModel(grid.battleModelId, true);
                grid.battleModelId = 0;
            }
        }
        BaseData.Instanse.m_CameraCrotroller.DeleteChild(m_BattleProgressList);
        m_DicBattleProgress.Clear();

        //战斗结算
        foreach (ModelGrid grid in m_DicLeftModelGrid.m_dicModelGrid.Values)
        {
            if (grid.modelData != null)
                grid.modelData.Exp += m_RoundExp;
        }
        BaseData.Instanse.m_MainCrotroller.m_Player.gold += m_RoundGold;
        m_RoundExp = 0;

        //判断玩家是否失败或者关卡完成
        if (m_DicLeftModelGrid.m_AliveModelCount <= 0 || m_Progress == 5)
        {
            if(m_DicLeftModelGrid.m_AliveModelCount <= 0)
            {
                //每6关给予传说材料
                if (BaseData.Instanse.m_MainCrotroller.m_RoundYear % 6 == 0)
                    AddDropMaterial(DataManager.Instanse.m_MaterialDescContainer.GetDescByID(10004), 1);
            }
            else if (m_Progress == 5)
            {
                if (BaseData.Instanse.m_MainCrotroller.m_RoundYear % 6 == 0)
                    AddDropMaterial(DataManager.Instanse.m_MaterialDescContainer.GetDescByID(10004), 1);
                //通关奖励
                foreach (ItemFormulaDesc itemFormula in m_ChooseMap.m_ItemFormulaList)
                {
                    if (!BaseData.Instanse.m_MainCrotroller.m_Player.itemFormulaList.Contains(itemFormula))
                        BaseData.Instanse.m_MainCrotroller.m_Player.itemFormulaList.Add(itemFormula);
                }

                //升级玩家等级
                if (m_ChooseMap.m_Level > BaseData.Instanse.m_MainCrotroller.PlayerLevel)
                    BaseData.Instanse.m_MainCrotroller.PlayerLevel = m_ChooseMap.m_Level;
            }

            //进行保存
            BaseData.Instanse.m_MainCrotroller.Save();
            //重置关卡进度
            m_Progress = 0;
            //修改状态
            m_IsRun = false;
            //打开结算面板
            BaseData.Instanse.m_CameraCrotroller.UpdateDropPanel();
        }
        else
        {
            ExpeditionInit();
        }
    }

    public void Run()
    {
        if(m_State == ExpeditionState.Wait)
        {
            if (m_DicLeftModelGrid.m_AliveModelCount <= 0 || m_DicRightModelGrid.m_AliveModelCount <= 0)
                BattleEnd();
            else
            {
                m_ExpeditionModelList.Sort((x, y) => -x.Progress.CompareTo(y.Progress));
                int index = 1;
                foreach (BattleModel battle in m_ExpeditionModelList)
                {
                    if (battle.m_IsAlive)
                    {
                        battle.Progress += battle.Speed;
                        m_DicBattleProgress[battle].transform.SetSiblingIndex(m_BattleProgressList.transform.childCount - index++);
                        float value = battle.Progress / 10000f;
                        m_DicBattleProgress[battle].GetComponent<Slider>().value = value;
                        //如果大于等于10000则行动一次
                        if (battle.Progress >= 10000)
                        {
                            //行动条清零
                            battle.Progress = 0;
                            m_State = ExpeditionState.Battle;
                            battle.ExecutionRound();
                            break;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 战斗单位死亡移除
    /// </summary>
    /// <param name="grid"></param>
    public void ModelDead(ModelGrid grid)
    {
        m_ExpeditionModelList.Remove(BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId));
    }

    /// <summary>
    /// 根据当前地图创建随机敌人
    /// </summary>
    /// <param name="direction">0为左侧，1有右侧</param>
    /// <param name="count">创建数量</param>
    /// <param name=""></param>
    public void CreateRandomEnemy(ModelGridCollection gridCollection, int count)
    {

        //添加可以随机生成的位置
        List<int> randomIndex = new List<int>();
        foreach(ModelGrid grid in gridCollection.m_dicModelGrid.Values)
        {
            if (grid.battleModelId != 0)
            {
                BattleModel model = BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId);
                if (model != null && !model.m_IsAlive)
                {
                    randomIndex.Add(grid.gridIndex);
                }
            }
            else
                randomIndex.Add(grid.gridIndex);
        }
        //随机生成敌人
        for (int i = 0; i < count; i++)
        {
            //随机
            int randomPos = Random.Range(0, randomIndex.Count);
            int index = randomIndex[randomPos];
            ModelGrid grid = gridCollection.m_dicModelGrid[index];

            float random = Random.Range(0, 101);
            float range = 0;
            EnemyDesc enemyDesc = null;
            foreach (EnemyDesc desc in m_ChooseMap.m_DicEnemy.Keys)
            {
                range += m_ChooseMap.m_DicEnemy[desc];
                if (random <= range)
                {
                    enemyDesc = desc;
                    break;
                }
            }

            //生成敌人数据和战斗模型
            ModelData data = new ModelData(enemyDesc);
            int id = BaseData.Instanse.m_MainCrotroller.CreateModel(data, grid.gridObj.transform);
            BattleModel enemy = BaseData.Instanse.GetModelById<BattleModel>(id);
            enemy.m_Parent = grid;

            grid.battleModelId = id;

            m_ExpeditionModelList.Add(enemy);
            GameObject progress = GameObject.Instantiate(m_ProgressPrefab, m_BattleProgressList.transform);
            progress.transform.Find("Background/Head").GetComponent<Image>().sprite =
                BaseData.Instanse.m_CameraCrotroller.GetSprite("enemyHead" + enemy.m_Data.m_ModelKey);
            m_DicBattleProgress.Add(enemy, progress);
            gridCollection.m_AliveModelCount++;
            gridCollection.m_ModelList.Add(data.Id);

            randomIndex.Remove(index);
        }
    }

    /// <summary>
    /// 战斗中召唤
    /// </summary>
    public void CreateEnemy(ModelGridCollection gridCollection, EnemyDesc desc)
    {
        //添加可以随机生成的位置
        List<int> randomIndex = new List<int>();
        foreach (ModelGrid grid in gridCollection.m_dicModelGrid.Values)
        {
            if (grid.battleModelId != 0)
            {
                BattleModel model = BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId);
                if (model != null && !model.m_IsAlive)
                {
                    randomIndex.Add(grid.gridIndex);
                }
            }
            else
                randomIndex.Add(grid.gridIndex);
        }

        //随机
        int randomPos = Random.Range(0, randomIndex.Count);
        int index = randomIndex[randomPos];
        ModelGrid modelGrid = gridCollection.m_dicModelGrid[index];

        //生成敌人数据和战斗模型
        ModelData data = new ModelData(desc);
        int id = BaseData.Instanse.m_MainCrotroller.CreateModel(data, modelGrid.gridObj.transform);
        BattleModel enemy = BaseData.Instanse.GetModelById<BattleModel>(id);
        enemy.m_Parent = modelGrid;

        modelGrid.battleModelId = id;

        m_ExpeditionModelList.Add(enemy);
        GameObject progress = GameObject.Instantiate(m_ProgressPrefab, m_BattleProgressList.transform);
        progress.transform.Find("Background/Head").GetComponent<Image>().sprite =
            BaseData.Instanse.m_CameraCrotroller.GetSprite("enemyHead" + enemy.m_Data.m_ModelKey);
        m_DicBattleProgress.Add(enemy, progress);
        gridCollection.m_AliveModelCount++;
        gridCollection.m_ModelList.Add(data.Id);
    }

    /// <summary>
    /// 添加掉落材料
    /// </summary>
    public void AddDropMaterial(MaterialDesc material, int count)
    {
        BaseData.Instanse.m_MainCrotroller.m_Player.AddMaterial(material, 1);
        if (m_DicRoundMaterial.ContainsKey(material))
        {
            m_DicRoundMaterial[material] += count;
        }
        else
        {
            m_DicRoundMaterial.Add(material, count);
        }
    }
}

public enum ExpeditionState
{
    Ready,
    /// <summary>
    /// 战斗状态
    /// </summary>
    Battle,
    /// <summary>
    /// 计算进度状态
    /// </summary>
    Wait,
    Dead,
}
