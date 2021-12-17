using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraCrotroller : MonoBehaviour
{
    private Dictionary<string, Button> m_DicButton;
    private Dictionary<string, Text> m_DicText;
    private Dictionary<string, GameObject> m_DicObj;
    private Dictionary<string, Image> m_DicImg;
    private Dictionary<string, Sprite> m_DicSprite;
    private Dictionary<string, Canvas> m_DicCanvas;
    public Dictionary<string, bool> m_DicPanelSwitch;
    //private Canvas m_UseCanvas;
    //是否在切换镜头中
    private bool m_Moveing;
    //是否在拖动英雄
    private bool m_IsMoveHero;
    //拖动的英雄物体
    private GameObject m_MoveingHero;
    //是否在拖动物品
    private bool m_IsMoveItem;
    //拖动的物品的起始位置
    private Vector3 m_ItemParentPos;
    //拖动的物品
    private GameObject m_MoveingItem;
    //背包当前分区
    private BagType m_BagType;
    //英雄信息当前分区
    private HeroInfoType m_HeroInfoType;
    //查看中的英雄
    private ModelData m_SelectHero;
<<<<<<< HEAD
    //远征地图展示栏位
    public Dictionary<int, GameObject> m_DicExdeditionMapTeam;
    //UI移动面板位置字典
    public Dictionary<GameObject, UIInteractive> m_DicUIMoveTarget;
    //铁匠铺查看图纸
    public Blacksmith m_BlacksmithItem;
    //铁匠铺使用材料列表
    public GameObject[] m_BlacksmithUseMaterial;
    //铁匠铺使用材料ID列表
    public int[] m_BlacksmithUseMaterialID;
    //铁匠铺选择材料ID列表
    public int[] m_BlacksmithUseMaterialChooseID;
    //装备页数
    public int m_ItemCatalogue;
    //材料页数
    public int m_MaterialCatalogue;
    //酒馆页数
    public int m_BarCatalogue;
=======
    //编队打开状态
    private bool m_IsTeamOpen;
    //锻造所打开状态
    private bool m_IsBlacksmithOpen;
    //酒吧打开状态
    private bool m_IsBarOpen;
    private CameraPos m_CameraPos;
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
    private GameObject m_Parent;
    private GameObject m_HeroIconPrefab;
    private Sprite m_Goblin;
    private List<GameObject> m_MessageList;
    //过场黑屏
    private Image m_BlackPanel;

    private void Awake()
    {
        m_Goblin = Resources.Load<Sprite>("Monster");

        m_DicButton = new Dictionary<string, Button>();
        m_DicText = new Dictionary<string, Text>();
        m_DicObj = new Dictionary<string, GameObject>();
        m_DicImg = new Dictionary<string, Image>();
        m_DicSprite = new Dictionary<string, Sprite>();
        m_DicCanvas = new Dictionary<string, Canvas>();
        m_DicExdeditionMapTeam = new Dictionary<int, GameObject>();
        m_MessageList = new List<GameObject>();
<<<<<<< HEAD
        m_DicPanelSwitch = new Dictionary<string, bool>();
        m_DicUIMoveTarget = new Dictionary<GameObject, UIInteractive>();
        m_BlacksmithUseMaterial = new GameObject[3];
        m_BlacksmithUseMaterialID = new int[3];
        m_BlacksmithUseMaterialChooseID = new int[3];

        SetButton("Airship/Setting", "setting");
        //SetButton("Canvas/Airship", "airship");
        SetButton("Airship/HeroPanel/Right/Item", "bagItem");
        SetButton("Airship/HeroPanel/Right/Material", "bagMaterial");
        SetButton("Airship/Expedition", "expedition");
        SetButton("Airship/Team", "team");
        SetButton("Airship/Hero", "hero");
        SetButton("Airship/Bar", "bar");
        SetButton("Airship/Blacksmith", "blacksmith");
        //SetButton("Airship/Industry", "industry");
        SetButton("Airship/IndustryPanel/Bar", "industryBar");
        SetButton("Airship/IndustryPanel/Blacksmith", "industryBlacksmith");
        SetButton("Airship/HeroPanel/Left/Attribute", "attributeButton");
        SetButton("Airship/HeroPanel/Left/Item", "itemButton");
        SetButton("Airship/IndustryPanel/Return", "industryReturn");
        SetButton("Airship/BarPanel/Buy", "barBuy");
        SetButton("Airship/BarPanel/Leave", "barLeave");
        SetButton("Airship/BlacksmithPanel/Left/Make", "blacksmithMake");
        SetButton("Airship/ExpeditionMap/Run", "run");
        SetButton("Airship/DropPanel/Take", "dropTake");

        SetObj((GameObject)Resources.Load("Prefab/UI/Message"), "messagePrefab");
        SetObj((GameObject)Resources.Load("Prefab/UI/BagIcon"), "bagIcon");
        SetObj((GameObject)Resources.Load("Prefab/UI/TaskIcon"), "taskIcon");
=======

        SetButton("Airship/Canvas/Setting", "setting");
        SetButton("Airship/Canvas/Save", "save");
        SetButton("Canvas/Airship", "airship");
        SetButton("Airship/Canvas/HeroPanel/Right/Item", "bagItem");
        SetButton("Airship/Canvas/HeroPanel/Right/Material", "bagMaterial");
        SetButton("Airship/Canvas/Expedition", "expedition");
        SetButton("Airship/Canvas/Team", "team");
        SetButton("Airship/Canvas/Hero", "hero");
        SetButton("Airship/Canvas/World", "world");
        SetButton("Airship/Canvas/Bar", "bar");
        SetButton("Airship/Canvas/Blacksmith", "blacksmith");
        SetButton("Airship/Canvas/HeroPanel/Left/Bottom/Attribute", "attributeButton");
        SetButton("Airship/Canvas/HeroPanel/Left/Bottom/Item", "itemButton");

        SetObj((GameObject)Resources.Load("Prefab/UI/Message"), "messagePrefab");
        SetObj((GameObject)Resources.Load("Prefab/UI/BagIcon"), "bagIcon");
        SetObj((GameObject)Resources.Load("Prefab/UI/BarHeroIcon"), "barHeroIcon");
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
        SetObj((GameObject)Resources.Load("Prefab/UI/MaterialIcon"), "materialIcon");
        SetObj((GameObject)Resources.Load("Prefab/UI/ItemFormulaIcon"), "itemFormulaIcon");
        SetObj((GameObject)Resources.Load("Prefab/UI/Vision"), "visionModel");
        SetObj((GameObject)Resources.Load("Prefab/UI/AbilityIcon"), "abilityIcon");
        SetObj("Vision", "vision");
<<<<<<< HEAD
        SetObj("Airship/TaskList/Background", "taskList");
        SetObj("Airship/BlackBackground", "blackBackground");
        SetObj("Airship/Round/BossRound", "bossRound");
        SetObj("Airship/Round/YearBackground", "yearBackground");
        SetObj("Airship/HeroPanel/Left/ItemPanel", "itemPanel");
        SetObj("Airship/HeroPanel/Left/Model/Head", "heroModelPos");
        SetObj("Airship/HeroPanel/Left/AttributePanel", "attributePanel");
        SetObj("Airship/HeroPanel/Right/ShowPanel", "bagShowPanel");
        SetObj("Airship/HeroPanel/Right/Catalogue/Right", "bagRight");
        SetObj("Airship/HeroPanel/Right/Catalogue/Left", "bagLeft");
        SetObj("Airship/ItemMessage", "itemMessage");
        SetObj("Airship/ItemMessage/Background/AbilityList", "itemAbilityList");
        SetObj("Airship/ItemFormulaMessage", "itemFormulaMessage");
        SetObj("Airship/MaterialMessage", "materialMessage");
        SetObj("Airship/BlacksmithPanel/Left/Item/Head", "blacksmithItemHead");
        SetObj("Airship/BlacksmithPanel/Left/UseMaterial/One/MaterialIcon", "blacksmithMaterialOne");
        SetObj("Airship/BlacksmithPanel/Left/UseMaterial/Two/MaterialIcon", "blacksmithMaterialTwo");
        SetObj("Airship/BlacksmithPanel/Left/UseMaterial/Three/MaterialIcon", "blacksmithMaterialThree");
        SetObj("Airship/BlacksmithPanel/Left/MaterialList", "blacksmithMaterialList");
        SetObj("Airship/BlacksmithPanel/Right/ItemFormulaList", "itemFormulaList");
        SetObj("Airship/HeroList", "heroList");
        SetObj("Airship/HeroPanel", "heroPanel");
        SetObj("Airship/TeamPanel", "teamPanel");
        SetObj("Airship/IndustryPanel", "industryPanel");
        SetObj("Airship/BarPanel", "barPanel");
        SetObj("Airship/BarPanel/Model/Head", "barHeroModelPos");
        SetObj("Airship/BarPanel/HeroList", "barHeroList");
        SetObj("Airship/BlacksmithPanel", "blacksmithPanel");
        SetObj("Airship/ExpeditionMap", "expeditionMap");
        SetObj("Airship/ExpeditionMap/Head", "expeditionMapHead");
        SetObj("Airship/ExpeditionMap/Head/Up", "expeditionMapLevelUp");
        SetObj("Airship/ExpeditionMap/Head/Down", "expeditionMapLevelDown");
        SetObj("Airship/ExpeditionMap/Left", "expeditionMapLeft");
        SetObj("Airship/ExpeditionMap/Right", "expeditionMapRight");
        SetObj("Airship/ExpeditionMap/DropList", "expeditionMapDropList");
        SetObj("Airship/ExpeditionBattle", "expeditionBattle");
        SetObj("Airship/DropPanel", "dropPanel");
        SetObj("Airship/DropPanel/DropList", "dropList");
        SetObj("Airship/ErrorMessage", "errorMessage");

        SetText("Airship/Round/BossRound/Text", "bossRound");
        SetText("Airship/Round/Year", "yearText");
        SetText("Airship/ItemMessage/Background/Attribute/Text", "itemAttributeText");
        SetText("Airship/ItemMessage/Background/Top/Text", "itemHead");
        SetText("Airship/ItemFormulaMessage/Text", "itemFormulaText");
        SetText("Airship/ItemFormulaMessage/Background/Text", "itemFormulaHead");
        SetText("Airship/MaterialMessage/Text", "materialText");
        SetText("Airship/MaterialMessage/Background/Text", "materialHead");
        SetText("Airship/HeroPanel/Left/Model/Name", "modelName");
        SetText("Airship/HeroPanel/Left/Model/Level", "modelLevel");
        SetText("Airship/HeroPanel/Left/Model/Job", "modelJob");
        SetText("Airship/HeroPanel/Left/AttributePanel/Text", "heroAttribute");
        SetText("Airship/HeroPanel/Right/Catalogue/Text", "bagCatalogue");
        SetText("Airship/BarPanel/Model/Name", "barModelName");
        SetText("Airship/BarPanel/Model/Level", "barModelLevel");
        SetText("Airship/BarPanel/Model/Job", "barModelJob");
        SetText("Airship/BarPanel/AttributePanel/Text", "barAttribute");
        SetText("Airship/BarPanel/Catalogue/Text", "barCatalogue");
        SetText("Airship/ExpeditionMap/Name", "expeditionMapName");
        SetText("Airship/ExpeditionMap/Head/Text", "expeditionMapLevel");
        SetText("Airship/ExpeditionMap/Catalogue", "expeditionMapCatalogue");
        SetText("Airship/ErrorMessage", "errorMessage");

        SetImg("Airship/Background", "airshipBackground");
        SetImg("Airship/ItemMessage/Background/Top/Head", "itemImg");
        SetImg("Airship/ItemFormulaMessage/Background/Head", "itemFormulaImg");
        SetImg("Airship/MaterialMessage/Background/Head", "materialImg");
        SetImg("Airship/HeroPanel/Left", "heroPanelLeftImg");
        SetImg("Airship/HeroPanel/Right", "heroPanelRightImg");
        SetImg("Airship/HeroPanel/Left/ItemPanel/Weapon", "itemWeapon");
        SetImg("Airship/HeroPanel/Left/ItemPanel/Deputy", "itemDeputy");
        SetImg("Airship/HeroPanel/Left/ItemPanel/Head", "itemHead");
        SetImg("Airship/HeroPanel/Left/ItemPanel/Chest", "itemChest");
        SetImg("Airship/HeroPanel/Left/ItemPanel/Foot", "itemFoot");
        SetImg("Airship/HeroPanel/Left/ItemPanel/Hand", "itemHand");
        SetImg("Airship/HeroPanel/Left/ItemPanel/Necklace", "itemNecklace");
        SetImg("Airship/HeroPanel/Left/ItemPanel/Ornament1", "itemOrnament1");
        SetImg("Airship/HeroPanel/Left/ItemPanel/Ornament2", "itemOrnament2");

        //添加铁匠铺材料位置
        m_BlacksmithUseMaterial[0] = GetObj("blacksmithMaterialOne");
        m_BlacksmithUseMaterial[1] = GetObj("blacksmithMaterialTwo");
        m_BlacksmithUseMaterial[2] = GetObj("blacksmithMaterialThree");

        //添加面板控制开关
        m_DicPanelSwitch.Add("heroPanel", false);
        m_DicPanelSwitch.Add("teamPanel", false);
        m_DicPanelSwitch.Add("industryPanel", false);
        m_DicPanelSwitch.Add("barPanel", false);
        m_DicPanelSwitch.Add("blacksmithPanel", false);
        m_DicPanelSwitch.Add("expeditionMap", false);
        m_DicPanelSwitch.Add("expeditionBattle", false);
        m_DicPanelSwitch.Add("dropPanel", false);
        m_DicPanelSwitch.Add("errorMessage", false);

        //添加UI移动位置
        UIInteractive bossRoundUI = new UIInteractive(new List<Vector2>() { new Vector2(2.5f, 0), new Vector2(82.5f, 0) });
        m_DicUIMoveTarget.Add(GetObj("bossRound").gameObject, bossRoundUI);

        Sprite[] sprites = Resources.LoadAll<Sprite>("UI/HeroPanel");
        int index = 0;
        foreach (Sprite sprite in sprites)
        {
            SetSprite(sprites[index], "heroPanelLeft" + index++);
        }

        sprites = Resources.LoadAll<Sprite>("UI/BagPanel");
        index = 0;
        foreach (Sprite sprite in sprites)
        {
            SetSprite(sprites[index], "heroPanelRight" + index++);
        }

        sprites = Resources.LoadAll<Sprite>("UI/HeroHead");
        index = 0;
        foreach (Sprite sprite in sprites)
        {
            SetSprite(sprites[index++], "heroHead" + sprite.name);
        }

        foreach(ExpeditionMapDesc desc in DataManager.Instanse.m_ExpeditionMapDescContainer.m_dicDesc.Values)
        {
            if (desc.m_Show)
            {
                sprites = Resources.LoadAll<Sprite>("UI/Head_" + desc.m_Script);
                index = 0;
                foreach (Sprite sprite in sprites)
                {
                    SetSprite(sprites[index++], "enemyHead" + sprite.name);
                }
            }
        }

=======
        SetObj("Airship/Canvas/HeroPanel/Left/Head/Model", "heroModelPos");
        SetObj("Airship/Canvas/HeroPanel/Left/Bottom/AttributePanel", "attributePanel");
        SetObj("Airship/Canvas/HeroPanel/Left/Bottom/ItemPanel", "itemPanel");
        SetObj("Airship/Canvas/HeroPanel/Left/Bottom/ItemPanel/Weapon", "itemWeapon");
        SetObj("Airship/Canvas/HeroPanel/Left/Bottom/ItemPanel/Deputy", "itemDeputy");
        SetObj("Airship/Canvas/HeroPanel/Left/Bottom/ItemPanel/Head", "itemHead");
        SetObj("Airship/Canvas/HeroPanel/Left/Bottom/ItemPanel/Chest", "itemChest");
        SetObj("Airship/Canvas/HeroPanel/Left/Bottom/ItemPanel/Foot", "itemFoot");
        SetObj("Airship/Canvas/HeroPanel/Left/Bottom/ItemPanel/Hand", "itemHand");
        SetObj("Airship/Canvas/HeroPanel/Left/Bottom/ItemPanel/Necklace", "itemNecklace");
        SetObj("Airship/Canvas/HeroPanel/Left/Bottom/ItemPanel/Ornament1", "itemOrnament1");
        SetObj("Airship/Canvas/HeroPanel/Left/Bottom/ItemPanel/Ornament2", "itemOrnament2");
        SetObj("Airship/Canvas/HeroPanel/Right/ShowPanel", "bagShowPanel");
        SetObj("Airship/Canvas/HeroPanel/Right/IconMessage", "iconMessage");
        SetObj("Airship/Canvas/BlacksmithPanel/Left/Item/Head", "blacksmithItemHead");
        SetObj("Airship/Canvas/BlacksmithPanel/Left/MaterialList", "blacksmithMaterialList");
        SetObj("Airship/Canvas/BlacksmithPanel/Right/ItemFormulaList", "itemFormulaList");
        SetObj("Airship/Canvas/BarPanel/Left/Head/Model", "barHeroModelPos");
        SetObj("Airship/Canvas/BarPanel/Right/HeroListPanel", "barHeroListPanel");

        SetObj("Airship/Canvas/BlackBackground", "blackBackground");
        SetObj("Airship/Canvas/HeroPanel", "heroPanel");
        SetObj("Airship/Canvas/HeroList", "heroList");
        SetObj("Airship/Canvas/TeamPanel", "teamPanel");
        SetObj("Airship/Canvas/BlacksmithPanel", "blacksmithPanel");
        SetObj("Airship/Canvas/BarPanel", "bar");

        SetText("Airship/Canvas/HeroPanel/Right/IconMessage/Text", "iconText");
        SetText("Airship/Canvas/HeroPanel/Right/IconMessage/Background/Text", "iconHead");
        SetText("Airship/Canvas/HeroPanel/Left/Bottom/AttributePanel/Hp", "attributeHp");
        SetText("Airship/Canvas/HeroPanel/Left/Bottom/AttributePanel/Attack", "attributeAttack");
        SetText("Airship/Canvas/HeroPanel/Left/Bottom/AttributePanel/Defense", "attributeDefense");
        SetText("Airship/Canvas/HeroPanel/Left/Bottom/AttributePanel/Speed", "attributeSpeed");
        SetText("Airship/Canvas/BarPanel/Left/AttributePanel/Hp", "barHeroAttributeHp");
        SetText("Airship/Canvas/BarPanel/Left/AttributePanel/Attack", "barHeroAttributeAttack");
        SetText("Airship/Canvas/BarPanel/Left/AttributePanel/Defense", "barHeroAttributeDefense");
        SetText("Airship/Canvas/BarPanel/Left/AttributePanel/Speed", "barHeroAttributeSpeed");

        SetImg("Airship/Canvas/HeroPanel/Right/IconMessage/Background/Head", "iconSprite");

        m_DicCameraPos.Add(CameraPos.Airship, new Vector2(0, 0));
        m_DicCameraPos.Add(CameraPos.Expedition, new Vector2(0, -845));

        m_CameraPos = CameraPos.Airship;
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
        m_Parent = Camera.main.gameObject; 
        m_HeroIconPrefab = (GameObject)Resources.Load("Prefab/UI/HeroIcon");
        m_BlackPanel = GameObject.Find("Canvas/BlackPanel").GetComponent<Image>();

        GetObj("blackBackground").SetActive(false);
        GetObj("itemMessage").transform.localPosition = new Vector3(9999, 9999, 0);
        //GetButton("airship").gameObject.SetActive(false);

        GetText("yearText").text = "第" + BaseData.Instanse.m_MainCrotroller.m_RoundYear + "年";

        //添加远征地图队伍展示栏位
        for (int i = 1; i <= 9; i++)
        {
            GameObject team = GameObject.Find("Airship/ExpeditionMap/Team/Left_" + i);
            m_DicExdeditionMapTeam.Add(i, team);
        }

        InitUI();
    }

    private void InitUI()
    {
        //-------------------------背包-------------------------
        //背包内装备区按钮
        Button bagItem = GetButton("bagItem");
        bagItem.onClick.AddListener(delegate ()
        {
            if (m_BagType != BagType.Item)
            {
                CanvasUpdate(13);
                m_BagType = BagType.Item;
                GetImg("heroPanelRightImg").sprite = GetSprite("heroPanelRight0");
                UpdateBagShow();
            }
        });

        //背包内材料区按钮
        Button bagMaterial = GetButton("bagMaterial");
        bagMaterial.onClick.AddListener(delegate ()
        {
            if (m_BagType != BagType.Material)
            {
                CanvasUpdate(14);
                m_BagType = BagType.Material;
                GetImg("heroPanelRightImg").sprite = GetSprite("heroPanelRight1");
                UpdateBagShow();
            }
        });

        //背包翻页功能(左)
        EventTrigger bagLeftTrigger = GetObj("bagLeft").AddComponent<EventTrigger>();
        EventTrigger.Entry bagLeft = new EventTrigger.Entry();
        bagLeft.eventID = EventTriggerType.PointerClick;
        bagLeft.callback.AddListener(delegate (BaseEventData e)
        {
<<<<<<< HEAD
            if(m_BagType == BagType.Item)
            {
                if (m_ItemCatalogue > 1)
                {
                    m_ItemCatalogue--;
                    UpdateBagShow();
                }
            }
            else if(m_BagType == BagType.Material)
            {
                if (m_MaterialCatalogue > 1)
                {
                    m_MaterialCatalogue--;
                    UpdateBagShow();
                }
            }
=======
            GetObj("blackBackground").SetActive(false);
            m_IsHeroOpen = false;
            m_IsTeamOpen = false;
            m_IsBlacksmithOpen = false;
            m_IsBarOpen = false;
            //GetObj("bagPanel").GetComponent<Animator>().SetTrigger("Hidden");
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
        });
        bagLeftTrigger.triggers.Add(bagLeft);

        //背包翻页功能(右)
        EventTrigger bagRightTrigger = GetObj("bagRight").AddComponent<EventTrigger>();
        EventTrigger.Entry bagRight = new EventTrigger.Entry();
        bagRight.eventID = EventTriggerType.PointerClick;
        bagRight.callback.AddListener(delegate (BaseEventData e)
        {
            if (m_BagType == BagType.Item)
            {
                int catalogueCount = BaseData.Instanse.m_MainCrotroller.m_Player.itemList.Count / 30;
                if (m_ItemCatalogue < catalogueCount + 1)
                {
                    m_ItemCatalogue++;
                    UpdateBagShow();
                }
            }
            else if (m_BagType == BagType.Material)
            {
                int catalogueCount = BaseData.Instanse.m_MainCrotroller.m_Player.dicMaterial.Count / 30;
                if (m_MaterialCatalogue < catalogueCount + 1)
                {
                    m_MaterialCatalogue++;
                    UpdateBagShow();
                }
            }
        });
        bagRightTrigger.triggers.Add(bagRight);

        //-------------------------背包-------------------------

        //-------------------------英雄信息-------------------------
        //英雄信息内属性按钮
        Button attributeButton = GetButton("attributeButton");
        attributeButton.onClick.AddListener(delegate ()
        {
            CanvasUpdate(11);
        });
        //英雄信息内装备按钮
        Button itemButton = GetButton("itemButton");
        itemButton.onClick.AddListener(delegate ()
        {
            CanvasUpdate(12);
        });
        //-------------------------英雄信息-------------------------

        //-------------------------酒馆信息-------------------------
        //雇佣英雄
        Button butButton = GetButton("barBuy");
        butButton.onClick.AddListener(delegate ()
        {
            if (m_SelectHero != null)
            {
                BaseData.Instanse.m_MainCrotroller.BuyHero(m_SelectHero);
                UpdateBar();
                UpdateHeroList();
            }
        });

        Button leaveButton = GetButton("barLeave");
        leaveButton.onClick.AddListener(delegate ()
        {
            if (m_SelectHero != null)
            {
                BaseData.Instanse.m_MainCrotroller.LeaveHero(m_SelectHero);
                UpdateBar();
                UpdateHeroList();
            }
        });
        //-------------------------酒馆信息-------------------------

        //-------------------------界面按钮-------------------------
        //英雄按钮
        Button hero = GetButton("hero");
        hero.onClick.AddListener(delegate ()
        {
            CanvasUpdate(1);
        });
        //执行任务按钮
        Button run = GetButton("run");
        run.onClick.AddListener(delegate ()
        {
            //判断队伍是否至少有一个英雄
            if (BaseData.Instanse.m_MainCrotroller.m_DicTeamModelGrid.m_ModelList.Count > 0)
            {
                //执行任务，将界面跳转到战斗界面
                BaseData.Instanse.m_MainCrotroller.YearEnd();
                GetText("yearText").text = "第" + BaseData.Instanse.m_MainCrotroller.m_RoundYear + "年";
                CanvasUpdate(6);
            }
            else
            {
                SendMessage("请至少上阵一位英雄", 3f);
            }
        });
        //拿取掉落按钮
        Button take = GetButton("dropTake");
        take.onClick.AddListener(delegate ()
        {
            ClosePanel();
        });
        //远征按钮
        Button expedition = GetButton("expedition");
        expedition.onClick.AddListener(delegate ()
        {
            CanvasUpdate(2);
        });
        //飞艇按钮
        //Button airship = GetButton("airship");
        //airship.onClick.AddListener(delegate ()
        //{
        //});
        //队伍按钮
        Button team = GetButton("team");
        team.onClick.AddListener(delegate ()
        {
            CanvasUpdate(3);
        });
        //酒吧按钮
        Button bar = GetButton("bar");
        bar.onClick.AddListener(delegate ()
        {
            CanvasUpdate(51);
        });
        //铁匠铺按钮
        Button blacksmith = GetButton("blacksmith");
        blacksmith.onClick.AddListener(delegate ()
        {
            CanvasUpdate(52);
        });
        //工业按钮
        //Button industry = GetButton("industry");
        //industry.onClick.AddListener(delegate ()
        //{
        //    CanvasUpdate(5);
        //});
        //工业界面酒馆按钮
        Button industryBar = GetButton("industryBar");
        industryBar.onClick.AddListener(delegate ()
        {
            CanvasUpdate(51);
        });
        //工业界面铁匠按钮
        Button industryBlacksmith = GetButton("industryBlacksmith");
        industryBlacksmith.onClick.AddListener(delegate ()
        {
            CanvasUpdate(52);
            //GetObj("blackBackground").SetActive(true);
        });
        //铁匠铺打造按钮
        Button BlacksmithMake = GetButton("blacksmithMake");
        BlacksmithMake.onClick.AddListener(delegate ()
        {
            if (m_BlacksmithItem != null)
            {
                //判断材料是否齐全
                bool canMake = true;
                Dictionary<MaterialDesc, int> formulaMaterials = m_BlacksmithItem.m_ItemFormula.m_DicMaterial;
                int index = 0;
                foreach (MaterialDesc material in formulaMaterials.Keys)
                {
                    int materialCount = 0;
                    if (material.m_ID < 10)
                    {
                        if (m_BlacksmithUseMaterialChooseID[index] == 0)
                        {
                            canMake = false;
                            break;
                        }
                        MaterialDesc formulaMaterial = DataManager.Instanse.m_MaterialDescContainer.GetDescByID(m_BlacksmithUseMaterialChooseID[index]);
                        materialCount = BaseData.Instanse.m_MainCrotroller.m_Player.GetMaterial(formulaMaterial);
                    }
                    else
                        materialCount = BaseData.Instanse.m_MainCrotroller.m_Player.GetMaterial(material);

                    if (materialCount < formulaMaterials[material])
                    {
                        canMake = false;
                        break;
                    }
                    index++;
                }
                //开始制作
                if (canMake)
                {
                    index = 0;
                    foreach (MaterialDesc material in formulaMaterials.Keys)
                    {
                        if (material.m_ID < 10)
                        {
                            MaterialDesc formulaMaterial = DataManager.Instanse.m_MaterialDescContainer.GetDescByID(m_BlacksmithUseMaterialChooseID[index]);
                            BaseData.Instanse.m_MainCrotroller.m_Player.AddMaterial(formulaMaterial, -formulaMaterials[material]);
                        }
                        else
                            BaseData.Instanse.m_MainCrotroller.m_Player.AddMaterial(material, -formulaMaterials[material]);

                        index++;
                    }
                    Item item = new Item(m_BlacksmithItem.m_Item);
                    BaseData.Instanse.m_MainCrotroller.m_Player.itemList.Add(item);
                    UpdateBlacksmith();
                    UpdateBlacksmithMake(m_BlacksmithItem.m_ItemFormula, m_BlacksmithItem.m_Item);
                }
                else
                {
                    SendMessage("材料不足", 3f);
                }
            }
        });
        //酒吧
        Button bar = GetButton("bar");
        bar.onClick.AddListener(delegate ()
        {
            GetObj("blackBackground").SetActive(true);
            m_IsBarOpen = true;
            UpdateBar();
        });
        Button save = GetButton("save");
        save.onClick.AddListener(delegate ()
        {
            SLCrotroller.Instance.SaveDataToJson(new SLCrotroller.SaveData());
        });
        //-------------------------界面按钮-------------------------

        //为黑色背景添加点击关闭功能
        GameObject blackPanel = GetObj("blackBackground");
        EventTrigger trigger = blackPanel.AddComponent<EventTrigger>();
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerClick;
        exit.callback.AddListener(delegate (BaseEventData e)
        {
            GetObj("blackBackground").SetActive(false);
            ClosePanel();
            //m_IsHeroOpen = false;
            //m_IsTeamOpen = false;
            //m_IsIndustryOpen = false;
            //m_IsBlacksmithOpen = false;
            //GetObj("bagPanel").GetComponent<Animator>().SetTrigger("Hidden");
        });
        trigger.triggers.Add(exit);

        //为所有返回按钮添加点击关闭功能
        Button industryReturn = GetButton("industryReturn");
        industryReturn.onClick.AddListener(delegate ()
        {
            ClosePanel();
        });

        InitUIMove();
        InitBlacksmithTrigger();
        InitExpeditionMap();
    }

    /// <summary>
    /// 镜头控制 1、打开英雄界面 2、打开远征界面 3、打开队伍界面  5、打开工业界面 6、打开战斗场景
    /// 11、英雄-属性 12、英雄-装备 13、英雄-装备 14、英雄-材料 15、英雄-消耗品
    /// 51、工业-酒吧 52、工业-铁匠
    /// 100、返回到飞艇界面并清除其他界面
    /// </summary>
    public void CanvasUpdate(int index)
    {
        switch (index)
        {
            case 1:
                Image background = GetImg("airshipBackground");
                GetObj("blackBackground").SetActive(true);
                m_DicPanelSwitch["heroPanel"] = true;
                m_BagType = BagType.Item;
                m_HeroInfoType = HeroInfoType.Attribute;
                m_SelectHero = BaseData.Instanse.m_MainCrotroller.m_Player.heroList[0];
                GetImg("heroPanelLeftImg").sprite = GetSprite("heroPanelLeft0");
                GetImg("heroPanelRightImg").sprite = GetSprite("heroPanelRight0");
                UpdateHeroPanel(m_SelectHero);
                m_ItemCatalogue = 1;
                m_MaterialCatalogue = 1;
                UpdateBagShow();
                break;
            case 2:
                GetObj("blackBackground").SetActive(true);
                GetText("expeditionMapCatalogue").text =  "1/" + DataManager.Instanse.m_ExpeditionMapDescContainer.m_dicDesc.Count;
                if (ExpeditionCrotroller.Instanse.m_IsRun)
                    m_DicPanelSwitch["expeditionBattle"] = true;
                else
                    m_DicPanelSwitch["expeditionMap"] = true;

                UpdateExpeditionMap();

                if (BaseData.Instanse.m_MainCrotroller.m_NoobState == NoobState.ExpeditionIntroduce)
                    BaseData.Instanse.m_MainCrotroller.ExpeditionIntroduce();
                break;
            case 3:
                GetObj("blackBackground").SetActive(true);
                UpdateTeamPanel();
                m_DicPanelSwitch["teamPanel"] = true;
                break;
            case 5:
                m_DicPanelSwitch["industryPanel"] = true;
                break;
            case 6:
                ClosePanel();
                ExpeditionCrotroller.Instanse.ExpeditionInit();
                m_DicPanelSwitch["expeditionBattle"] = true;
                break;
            case 11:
                m_HeroInfoType = HeroInfoType.Attribute;
                GetImg("heroPanelLeftImg").sprite = GetSprite("heroPanelLeft0");
                UpdateHeroPanel(m_SelectHero);
                break;
            case 12:
                m_HeroInfoType = HeroInfoType.Item;
                GetImg("heroPanelLeftImg").sprite = GetSprite("heroPanelLeft1");
                UpdateHeroPanel(m_SelectHero);
                break;
            case 51:
                ClosePanel();
                GetObj("blackBackground").SetActive(true);
                m_DicPanelSwitch["barPanel"] = true;
                m_BarCatalogue = 1;
                UpdateBar();
                break;
            case 52:
                ClosePanel();
                GetObj("blackBackground").SetActive(true);
                m_DicPanelSwitch["blacksmithPanel"] = true;
                m_MaterialCatalogue = 1;
                UpdateBlacksmith();
                break;
            case 100:
                ClosePanel();
                break;
        }
    }

    //判断黑屏进度
    private bool isBlack;
    private void Update()
    {

        //拖动英雄
        if (m_IsMoveHero)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            m_SelectHero.ShowModel.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }

        //拖动物品
        if (m_IsMoveItem)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            m_MoveingItem.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }

        //面板控制
        foreach(string key in m_DicPanelSwitch.Keys)
        {
            if (m_DicPanelSwitch[key])
            {
                Vector3 scale = GetObj(key).transform.localScale;
                scale.x += 0.05f;
                if (scale.x >= 1f)
                    scale.x = 1f;
                scale.y += 0.05f;
                if (scale.y >= 1f)
                    scale.y = 1f;
                GetObj(key).transform.localScale = scale;
            }
            else
            {
                Vector3 scale = GetObj(key).transform.localScale;
                scale.x -= 0.05f;
                if (scale.x <= 0f)
                    scale.x = 0f;
                scale.y -= 0.05f;
                if (scale.y <= 0f)
                    scale.y = 0f;
                GetObj(key).transform.localScale = scale;
            }
        }

        //移动UI面板
        foreach(GameObject UIObj in m_DicUIMoveTarget.Keys)
        {
            UIInteractive interactive = m_DicUIMoveTarget[UIObj];
            Vector2 target = interactive.m_TargetList[interactive.m_MovePosIndex];
            Vector2 p0 = UIObj.transform.localPosition;
            if (interactive.m_IsMoving)
            {
                if ((p0 - target).sqrMagnitude < 50)
                {
                    UIObj.transform.localPosition = new Vector2(target.x, target.y);
                    interactive.m_IsMoving = false;
                }
                else
                {
                    Vector2 p01 = (p0 - target).normalized * 10 * Time.timeScale;
                    Vector2 p02 = p0 - p01;
                    Vector2 p03 = new Vector2(p02.x, p02.y);
                    UIObj.transform.localPosition = p03;
                }
            }
        }

        //上移消息功能
        foreach (GameObject message in m_MessageList)
        {
            if (message != null)
            {
                Vector2 messagePos = message.transform.position;
                messagePos.y += 1f;
                message.transform.position = messagePos;
            }
        }
    }

    /// <summary>
    /// 更新任务面板
    /// </summary>
    public void UpdateTaskPanel()
    {
        GameObject taskList = GetObj("taskList");
        DeleteChild(taskList);

        foreach(TaskDesc desc in BaseData.Instanse.m_MainCrotroller.m_TaskList)
        {
            GameObject taskIcon = Instantiate(GetObj("taskIcon"), taskList.transform);
            Text text = taskIcon.GetComponent<Text>();
            text.text = desc.m_Title;
            for(int i = 0; i < desc.m_TargetCount; i++)
            {
                text.text += "\n";
                int progress = TaskCrotroller.m_DicTaskParameters[desc.m_Script][i];
                int target = desc.m_ParametersList[i];
                text.text += "  " + desc.m_Content + "(" + progress + "/" + target + ")";
            }
        }
    }

    /// <summary>
    /// 初始化UI移动功能
    /// </summary>
    public void InitUIMove()
    {
        //为年历添加移动功能
        GameObject yearBackground = GetObj("yearBackground");
        GameObject bossRound = GetObj("bossRound");
        EventTrigger yaerBackgroundTrigger = yearBackground.AddComponent<EventTrigger>();
        EventTrigger.Entry yaerBackgroundEnter = new EventTrigger.Entry();
        yaerBackgroundEnter.eventID = EventTriggerType.PointerEnter;
        yaerBackgroundEnter.callback.AddListener(delegate (BaseEventData e)
        {
            UIInteractive interactive = m_DicUIMoveTarget[bossRound];
            interactive.m_IsMoving = true;
            interactive.m_MovePosIndex = 1;
            GetText("bossRound").text = (BaseData.Instanse.m_MainCrotroller.m_BossRound - BaseData.Instanse.m_MainCrotroller.m_RoundYear).ToString();
        });
        EventTrigger.Entry yaerBackgroundExit = new EventTrigger.Entry();
        yaerBackgroundExit.eventID = EventTriggerType.PointerExit;
        yaerBackgroundExit.callback.AddListener(delegate (BaseEventData e)
        {
            UIInteractive interactive = m_DicUIMoveTarget[bossRound];
            interactive.m_IsMoving = true;
            interactive.m_MovePosIndex = 0;
        });
        yaerBackgroundTrigger.triggers.Add(yaerBackgroundEnter);
        yaerBackgroundTrigger.triggers.Add(yaerBackgroundExit);
    }

    /// <summary>
    /// 初始化远征地图
    /// </summary>
    public void InitExpeditionMap()
    {
        //查看上一个地图
        EventTrigger leftTrigger = GetObj("expeditionMapLeft").AddComponent<EventTrigger>();
        EventTrigger.Entry leftClick = new EventTrigger.Entry();
        leftClick.eventID = EventTriggerType.PointerClick;
        leftClick.callback.AddListener(delegate (BaseEventData a)
        {
            if(BaseData.Instanse.m_MainCrotroller.m_RoundYear == BaseData.Instanse.m_MainCrotroller.m_BossRound)
            {

            }
            else
            {
                int mapId = ExpeditionCrotroller.Instanse.m_ShowMap.m_ID;
                mapId -= 100;
                if (DataManager.Instanse.m_ExpeditionMapDescContainer.GetDescByID(mapId) != null)
                {
                    ExpeditionCrotroller.Instanse.m_ShowMap = DataManager.Instanse.m_ExpeditionMapDescContainer.GetDescByID(mapId);
                    UpdateExpeditionMap();
                }
            }
        });

        //查看下一个地图
        EventTrigger rightTrigger = GetObj("expeditionMapRight").AddComponent<EventTrigger>();
        EventTrigger.Entry rightClick = new EventTrigger.Entry();
        rightClick.eventID = EventTriggerType.PointerClick;
        rightClick.callback.AddListener(delegate (BaseEventData a)
        {
            if (BaseData.Instanse.m_MainCrotroller.m_RoundYear == BaseData.Instanse.m_MainCrotroller.m_BossRound)
            {

            }
            else
            {
                int mapId = ExpeditionCrotroller.Instanse.m_ShowMap.m_ID;
                mapId += 100;
                if (DataManager.Instanse.m_ExpeditionMapDescContainer.GetDescByID(mapId) != null)
                {
                    ExpeditionCrotroller.Instanse.m_ShowMap = DataManager.Instanse.m_ExpeditionMapDescContainer.GetDescByID(mapId);
                    UpdateExpeditionMap();
                }
            }
        });

        //提示难度
        EventTrigger levelUpTrigger = GetObj("expeditionMapLevelUp").AddComponent<EventTrigger>();
        EventTrigger.Entry levelUp = new EventTrigger.Entry();
        levelUp.eventID = EventTriggerType.PointerClick;
        levelUp.callback.AddListener(delegate (BaseEventData a)
        {
            if (BaseData.Instanse.m_MainCrotroller.m_RoundYear == BaseData.Instanse.m_MainCrotroller.m_BossRound)
            {

            }
            else
            {
                int mapId = ExpeditionCrotroller.Instanse.m_DicMapLevel[ExpeditionCrotroller.Instanse.m_ShowMap.m_ID];
                mapId++;
                if (DataManager.Instanse.m_ExpeditionMapDescContainer.GetDescByID(mapId) != null)
                {
                    ExpeditionCrotroller.Instanse.m_DicMapLevel[ExpeditionCrotroller.Instanse.m_ShowMap.m_ID] = mapId;
                    UpdateExpeditionMap();
                }
            }
        });

        //降低难度
        EventTrigger levelDownTrigger = GetObj("expeditionMapLevelDown").AddComponent<EventTrigger>();
        EventTrigger.Entry levelDown = new EventTrigger.Entry();
        levelDown.eventID = EventTriggerType.PointerClick;
        levelDown.callback.AddListener(delegate (BaseEventData a)
        {
            if (BaseData.Instanse.m_MainCrotroller.m_RoundYear == BaseData.Instanse.m_MainCrotroller.m_BossRound)
            {

            }
            else
            {
                int mapId = ExpeditionCrotroller.Instanse.m_DicMapLevel[ExpeditionCrotroller.Instanse.m_ShowMap.m_ID];
                mapId--;
                if (DataManager.Instanse.m_ExpeditionMapDescContainer.GetDescByID(mapId) != null)
                {
                    ExpeditionCrotroller.Instanse.m_DicMapLevel[ExpeditionCrotroller.Instanse.m_ShowMap.m_ID] = mapId;
                    UpdateExpeditionMap();
                }
            }
        });

        leftTrigger.triggers.Add(leftClick);
        rightTrigger.triggers.Add(rightClick);
        levelUpTrigger.triggers.Add(levelUp);
        levelDownTrigger.triggers.Add(levelDown);
    }

    /// <summary>
    /// 更新远征地图
    /// </summary>
    public void UpdateExpeditionMap()
    {
        PlayerModel player = BaseData.Instanse.m_MainCrotroller.m_Player;
        GameObject dropList = GetObj("expeditionMapDropList");
        DeleteChild(dropList);

        if (BaseData.Instanse.m_MainCrotroller.m_RoundYear == BaseData.Instanse.m_MainCrotroller.m_BossRound)
        {
            ExpeditionMapDesc map = DataManager.Instanse.m_ExpeditionMapDescContainer.GetDescByID(20101);
            ExpeditionCrotroller.Instanse.m_ShowMap = map;
            ExpeditionCrotroller.Instanse.m_ChooseMap = map;
            GetText("expeditionMapLevel").text = "等级" + ExpeditionCrotroller.Instanse.m_ChooseMap.m_Level;
            GetText("expeditionMapCatalogue").text = "1/1";
            GetObj("expeditionMapHead").GetComponent<Animator>().SetTrigger(ExpeditionCrotroller.Instanse.m_ChooseMap.m_Script);
            GetText("expeditionMapName").text = ExpeditionCrotroller.Instanse.m_ShowMap.m_Name;
        }
        else
        {
            int mapId = ExpeditionCrotroller.Instanse.m_ShowMap.m_ID;
            int levelId = ExpeditionCrotroller.Instanse.m_DicMapLevel[mapId];
            ExpeditionMapDesc levelMap = DataManager.Instanse.m_ExpeditionMapDescContainer.GetDescByID(levelId);
            ExpeditionCrotroller.Instanse.m_ChooseMap = levelMap;
            GetText("expeditionMapLevel").text = "等级" + levelMap.m_Level;
            GetText("expeditionMapCatalogue").text = (int)(mapId % 1000 * 0.01) + "/" + ExpeditionCrotroller.Instanse.m_DicMapLevel.Count;

            //添加装备掉落列表
            foreach (ItemDesc desc in levelMap.m_DicItem.Keys)
            {
                GameObject dropIcon = Instantiate(GetObj("materialIcon"), dropList.transform);
                Image image = dropIcon.transform.Find("Image").GetComponent<Image>();
                image.rectTransform.sizeDelta = new Vector2(12, 12);
                image.sprite = BaseData.Instanse.m_DicItemSprite[desc.m_Res];
                AddTriggerToItem(desc, dropIcon);
                Text Text = dropIcon.transform.Find("Text").GetComponent<Text>();
                Text.text = "";
            }
            //添加材料掉落列表
            foreach (MaterialDesc desc in levelMap.m_DicMaterial.Keys)
            {
                GameObject dropIcon = Instantiate(GetObj("materialIcon"), dropList.transform);
                Image image = dropIcon.transform.Find("Image").GetComponent<Image>();
                image.rectTransform.sizeDelta = new Vector2(12, 12);
                image.sprite = BaseData.Instanse.m_DicMaterialSprite[desc.m_Script];
                AddTriggerToMaterial(desc, dropIcon);
                Text Text = dropIcon.transform.Find("Text").GetComponent<Text>();
                Text.text = "";
            }
            //添加图纸掉落列表
            //foreach (ItemFormulaDesc desc in levelMap.m_ItemFormulaList)
            //{
            //    GameObject dropIcon = Instantiate(GetObj("materialIcon"), dropList.transform);
            //    Image image = dropIcon.transform.Find("Image").GetComponent<Image>();
            //    image.rectTransform.sizeDelta = new Vector2(12, 12);
            //    string path = DataManager.Instanse.m_ItemDescContainer.GetDescByID(desc.m_ItemId).m_Res;
            //    image.sprite = BaseData.Instanse.m_DicItemSprite[path];
            //    AddTriggerToItemFormula(desc, dropIcon);
            //    Text Text = dropIcon.transform.Find("Text").GetComponent<Text>();
            //    if (BaseData.Instanse.m_MainCrotroller.m_Player.itemFormulaList.Contains(desc))
            //    {
            //        dropIcon.transform.Find("BlackPanel").localScale = new Vector3(1, 1, 1);
            //        Text.color = Color.green;
            //        Text.text = "已拥有";
            //    }
            //    else
            //        Text.text = "";
            //}

            GetObj("expeditionMapHead").GetComponent<Animator>().SetTrigger(ExpeditionCrotroller.Instanse.m_ChooseMap.m_Script);
            GetText("expeditionMapName").text = ExpeditionCrotroller.Instanse.m_ShowMap.m_Name;
        }

        //显示编队英雄
        foreach (ModelGrid grid in BaseData.Instanse.m_MainCrotroller.m_DicTeamModelGrid.m_dicModelGrid.Values)
        {
            if (grid.modelData != null)
            {
                grid.modelData.ShowModel.transform.SetParent(m_DicExdeditionMapTeam[grid.gridIndex].transform);
                grid.modelData.ShowModel.transform.localPosition = new Vector3(0, 0, 0);
                grid.modelData.ShowModel.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    /// <summary>
    /// 更新结算面板
    /// </summary>
    public void UpdateDropPanel()
    {
        ClosePanel();
        m_DicPanelSwitch["dropPanel"] = true;
        GameObject dropList = GetObj("dropList");
        DeleteChild(dropList);

        //添加装备掉落
        foreach (Item item in ExpeditionCrotroller.Instanse.m_RoundItemList)
        {
            GameObject icon = Instantiate(GetObj("materialIcon"), dropList.transform);
            Image iconImage = icon.transform.Find("Image").GetComponent<Image>();
            iconImage.sprite = BaseData.Instanse.m_DicItemSprite[item.m_Res];
            icon.transform.Find("Quality").GetComponent<Image>().color = BaseData.Instanse.GetColorByQuality(item.m_Quality);
            Text iconText = icon.transform.Find("Text").GetComponent<Text>();
            iconText.text = "";
        }

        //添加材料掉落
        foreach (MaterialDesc material in ExpeditionCrotroller.Instanse.m_DicRoundMaterial.Keys)
        {
            GameObject icon = Instantiate(GetObj("materialIcon"), dropList.transform);
            Image iconImage = icon.transform.Find("Image").GetComponent<Image>();
            iconImage.sprite = BaseData.Instanse.m_DicMaterialSprite[material.m_Script];
            Text iconText = icon.transform.Find("Text").GetComponent<Text>();
            icon.transform.Find("Quality").GetComponent<Image>().color = BaseData.Instanse.GetColorByQuality(material.m_Quality);
            iconText.color = Color.green;
            iconText.text = ExpeditionCrotroller.Instanse.m_DicRoundMaterial[material] + "";
        }

        ExpeditionCrotroller.Instanse.m_RoundItemList.Clear();
        ExpeditionCrotroller.Instanse.m_DicRoundMaterial.Clear();
    }

    /// <summary>
    /// 更新酒吧内容
    /// </summary>
    public void UpdateBar()
    {
        m_SelectHero = null;
        GameObject heroList = GetObj("barHeroList");
        DeleteChild(heroList);

        //清除旧人物预览
        GameObject heroHead = GetObj("barHeroModelPos");
        for (int i = heroHead.transform.childCount - 1; i >= 0; i--)
        {
            GameObject child = heroHead.transform.GetChild(i).gameObject;
            child.transform.SetParent(BaseData.Instanse.m_ModelPool.transform);
            child.transform.localPosition = new Vector3(0, 0, 0);
            child.transform.localScale = new Vector3(1, 1, 1);
        }

        //清理文本框
        GetText("barModelName").text = "";
        GetText("barModelLevel").text = "";
        GetText("barModelJob").text = "";
        GetText("barAttribute").text = "";

        foreach (ModelData data in BaseData.Instanse.m_MainCrotroller.m_BarHeroList)
        {
            GameObject heroIcon = Instantiate(m_HeroIconPrefab, heroList.transform);
            Sprite head = GetSprite("heroHead" + data.Job.ToString());
            heroIcon.transform.Find("Head").GetComponent<Image>().sprite = head;
            heroIcon.transform.Find("Name").GetComponent<Text>().text = data.Name;
            heroIcon.transform.Find("Level").GetComponent<Text>().text = "Lv" + data.Level;
            heroIcon.transform.Find("Job").GetComponent<Text>().text = ModelData.JobToString(data.Job);

            EventTrigger trigger = heroIcon.AddComponent<EventTrigger>();
            //点击方法
            EventTrigger.Entry click = new EventTrigger.Entry();
            click.eventID = EventTriggerType.PointerClick;
            click.callback.AddListener(delegate (BaseEventData e)
            {
                if (m_DicPanelSwitch["barPanel"])
                {
                    //清除旧人物预览
                    for (int i = heroHead.transform.childCount - 1; i >= 0; i--)
                    {
                        GameObject child = heroHead.transform.GetChild(i).gameObject;
                        child.transform.SetParent(BaseData.Instanse.m_ModelPool.transform);
                        child.transform.localPosition = new Vector3(0, 0, 0);
                        child.transform.localScale = new Vector3(1, 1, 1);
                    }

                    //更新人物预览
                    GetText("barModelName").text = data.Name;
                    GetText("barModelLevel").text = "Lv" + data.Level.ToString();
                    GetText("barModelJob").text = ModelData.JobToString(data.Job);
                    data.ShowModel.transform.SetParent(GetObj("barHeroModelPos").transform);
                    data.ShowModel.transform.localPosition = new Vector3(0, 0, 0);
                    data.ShowModel.transform.localScale = new Vector3(1, 1, 1);

                    //更新属性
                    GetText("barAttribute").text = UpdateHeroPanelText(data);

                    m_SelectHero = data;
                }
            });

            trigger.triggers.Add(click);
        }
    }

    /// <summary>
    /// 初始化铁匠铺trigger
    /// </summary>
    public void InitBlacksmithTrigger()
    {
        GameObject blacksmithUseMaterial = GetObj("blacksmithMaterialOne");
        EventTrigger blacksmithTrigger = blacksmithUseMaterial.AddComponent<EventTrigger>();
        EventTrigger.Entry oneClick = new EventTrigger.Entry();
        oneClick.eventID = EventTriggerType.PointerClick;
        oneClick.callback.AddListener(delegate (BaseEventData e)
        {
            if (m_BlacksmithUseMaterialID[0] != 0 && m_BlacksmithUseMaterialID[0] < 10)
            {
                m_BlacksmithUseMaterial[0].transform.Find("Image").GetComponent<Image>().sprite = BaseData.Instanse.m_Question;
                m_BlacksmithUseMaterialChooseID[0] = 0;
                MaterialDesc formulaMaterial = BaseData.Instanse.GetDictionaryByIndex<MaterialDesc, int>(m_BlacksmithItem.m_ItemFormula.m_DicMaterial, 0);
                m_BlacksmithUseMaterial[0].transform.Find("Text").GetComponent<Text>().color = Color.red;
                m_BlacksmithUseMaterial[0].transform.Find("Text").GetComponent<Text>().text =
                    "0/" + m_BlacksmithItem.m_ItemFormula.m_DicMaterial[formulaMaterial];
            }
        });
        EventTrigger.Entry TwoClick = new EventTrigger.Entry();
        TwoClick.eventID = EventTriggerType.PointerClick;
        TwoClick.callback.AddListener(delegate (BaseEventData e)
        {
            if (m_BlacksmithUseMaterialID[1] != 0 && m_BlacksmithUseMaterialID[1] < 10)
            {
                m_BlacksmithUseMaterial[1].transform.Find("Image").GetComponent<Image>().sprite = BaseData.Instanse.m_Question;
                m_BlacksmithUseMaterialChooseID[1] = 0;
                MaterialDesc formulaMaterial = BaseData.Instanse.GetDictionaryByIndex<MaterialDesc, int>(m_BlacksmithItem.m_ItemFormula.m_DicMaterial, 1);
                m_BlacksmithUseMaterial[1].transform.Find("Text").GetComponent<Text>().color = Color.red;
                m_BlacksmithUseMaterial[1].transform.Find("Text").GetComponent<Text>().text =
                    "0/" + m_BlacksmithItem.m_ItemFormula.m_DicMaterial[formulaMaterial];
            }
        });
        EventTrigger.Entry ThreeClick = new EventTrigger.Entry();
        ThreeClick.eventID = EventTriggerType.PointerClick;
        ThreeClick.callback.AddListener(delegate (BaseEventData e)
        {
            if (m_BlacksmithUseMaterialID[2] != 0 && m_BlacksmithUseMaterialID[2] < 10)
            {
                m_BlacksmithUseMaterial[2].transform.Find("Image").GetComponent<Image>().sprite = BaseData.Instanse.m_Question;
                m_BlacksmithUseMaterialChooseID[2] = 0;
                MaterialDesc formulaMaterial = BaseData.Instanse.GetDictionaryByIndex<MaterialDesc, int>(m_BlacksmithItem.m_ItemFormula.m_DicMaterial, 2);
                m_BlacksmithUseMaterial[2].transform.Find("Text").GetComponent<Text>().color = Color.red;
                m_BlacksmithUseMaterial[2].transform.Find("Text").GetComponent<Text>().text =
                    "0/" + m_BlacksmithItem.m_ItemFormula.m_DicMaterial[formulaMaterial];
            }
        });
        blacksmithTrigger.triggers.Add(oneClick);
        blacksmithTrigger.triggers.Add(TwoClick);
        blacksmithTrigger.triggers.Add(ThreeClick);

        m_BlacksmithUseMaterial[0].transform.localScale = new Vector2(0, 0);
        m_BlacksmithUseMaterial[1].transform.localScale = new Vector2(0, 0);
        m_BlacksmithUseMaterial[2].transform.localScale = new Vector2(0, 0);
    }

    /// <summary>
    /// 更新铁匠铺内容
    /// </summary>
    public void UpdateBlacksmith()
    {
        GameObject formulaList = GetObj("itemFormulaList");
        DeleteChild(formulaList);
        GameObject materialList = GetObj("blacksmithMaterialList");
        DeleteChild(materialList);

        int index = 1;
        foreach (ItemFormulaDesc desc in BaseData.Instanse.m_MainCrotroller.m_Player.itemFormulaList)
        {
            GameObject formula = Instantiate(GetObj("bagIcon"), formulaList.transform);
            Image image = formula.transform.Find("Image").GetComponent<Image>();
            image.sprite = BaseData.Instanse.m_DicItemSprite[DataManager.Instanse.m_ItemDescContainer.GetDescByID(desc.m_ItemId).m_Res];
            ItemDesc item = DataManager.Instanse.m_ItemDescContainer.GetDescByID(desc.m_ItemId);
            formula.transform.Find("Quality").GetComponent<Image>().color = BaseData.Instanse.GetColorByQuality(item.m_Quality);

            AddTriggerToItemFormula(desc, item, formula);
        }

        index = 1;
        foreach (MaterialDesc material in BaseData.Instanse.m_MainCrotroller.m_Player.dicMaterial.Keys)
        {
            if (index > (m_MaterialCatalogue - 1) * 9 && index <= m_MaterialCatalogue * 9)
            {
                if(BaseData.Instanse.m_MainCrotroller.m_Player.dicMaterial[material] > 0)
                {
                    GameObject obj = Instantiate(GetObj("materialIcon"), GetObj("blacksmithMaterialList").transform);
                    Image image = obj.transform.Find("Image").GetComponent<Image>();
                    image.sprite = BaseData.Instanse.m_DicMaterialSprite[material.m_Script];
                    AddTriggerToMaterial(material, obj);
                    Text materialText = obj.transform.Find("Text").GetComponent<Text>();
                    obj.transform.Find("Quality").GetComponent<Image>().color = BaseData.Instanse.GetColorByQuality(material.m_Quality);
                    materialText.color = Color.green;
                    materialText.text = BaseData.Instanse.m_MainCrotroller.m_Player.dicMaterial[material] + "";

                    EventTrigger materialTrigger = obj.AddComponent<EventTrigger>();
                    EventTrigger.Entry click = new EventTrigger.Entry();
                    click.eventID = EventTriggerType.PointerClick;
                    click.callback.AddListener(delegate (BaseEventData e)
                    {
                        index = -1;
                        if (m_BlacksmithUseMaterialChooseID[0] == 0 && m_BlacksmithUseMaterialID[0] < 10 && m_BlacksmithUseMaterialID[0] > 0)
                            index = 0;
                        else if (m_BlacksmithUseMaterialChooseID[1] == 0 && m_BlacksmithUseMaterialID[1] < 10 && m_BlacksmithUseMaterialID[1] > 0)
                            index = 1;
                        else if (m_BlacksmithUseMaterialChooseID[2] == 0 && m_BlacksmithUseMaterialID[2] < 10 && m_BlacksmithUseMaterialID[2] > 0)
                            index = 2;

                        if (index != -1 && material.m_Quality == 
                        DataManager.Instanse.m_MaterialDescContainer.GetDescByID(m_BlacksmithUseMaterialID[index]).m_Quality)
                        {
                            Image formulaImage = m_BlacksmithUseMaterial[index].transform.Find("Image").GetComponent<Image>();
                            formulaImage.sprite = BaseData.Instanse.m_DicMaterialSprite[material.m_Script];
                            m_BlacksmithUseMaterialChooseID[index] = material.m_ID;
                            MaterialDesc formulaMaterial = BaseData.Instanse.GetDictionaryByIndex<MaterialDesc, int>(m_BlacksmithItem.m_ItemFormula.m_DicMaterial, index);
                            Text formulaText = m_BlacksmithUseMaterial[index].transform.Find("Text").GetComponent<Text>();
                            if (BaseData.Instanse.m_MainCrotroller.m_Player.GetMaterial(material) >= m_BlacksmithItem.m_ItemFormula.m_DicMaterial[formulaMaterial])
                                formulaText.color = Color.green;
                            else
                                formulaText.color = Color.red;

                            formulaText.text =
                                BaseData.Instanse.m_MainCrotroller.m_Player.GetMaterial(material) + "/" + m_BlacksmithItem.m_ItemFormula.m_DicMaterial[formulaMaterial];
                        }
                    });
                    materialTrigger.triggers.Add(click);
                }
            }
            index++;
        }
    }

    /// <summary>
    /// 判断对应位置的材料品质是否相同
    /// </summary>
    public bool JudgeQuality(MaterialDesc material, int index)
    {
        MaterialDesc forMulaMaterial = 
            BaseData.Instanse.GetDictionaryByIndex(m_BlacksmithItem.m_ItemFormula.m_DicMaterial,index);
        if (material.m_Quality == forMulaMaterial.m_Quality)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 更新铁匠铺选择的装备
    /// </summary>
    public void UpdateBlacksmithMake(ItemFormulaDesc formula, ItemDesc desc)
    {
        m_BlacksmithUseMaterial[0].transform.localScale = new Vector2(0, 0);
        m_BlacksmithUseMaterial[1].transform.localScale = new Vector2(0, 0);
        m_BlacksmithUseMaterial[2].transform.localScale = new Vector2(0, 0);

        m_BlacksmithUseMaterialChooseID[0] = 0;
        m_BlacksmithUseMaterialChooseID[1] = 0;
        m_BlacksmithUseMaterialChooseID[2] = 0;

        m_BlacksmithItem = new Blacksmith(desc, formula);
        //修改装备头像
        GetObj("blacksmithItemHead").GetComponent<Image>().sprite = BaseData.Instanse.m_DicItemSprite[desc.m_Res];

        //添加材料
        int index = 0;
        foreach (MaterialDesc material in formula.m_DicMaterial.Keys)
        {
            if (index < 3)
            {
                m_BlacksmithUseMaterial[index].transform.localScale = new Vector2(1, 1);
                m_BlacksmithUseMaterialID[index] = material.m_ID;
                m_BlacksmithUseMaterial[index].transform.Find("Quality").GetComponent<Image>().color = BaseData.Instanse.GetColorByQuality(material.m_Quality);
                Image materialImage = m_BlacksmithUseMaterial[index].transform.Find("Image").GetComponent<Image>();
                Text materialText = m_BlacksmithUseMaterial[index].transform.Find("Text").GetComponent<Text>();
                if (material.m_ID < 10)
                {
                    materialImage.sprite = BaseData.Instanse.m_Question;
                    materialText.color = Color.red;
                    materialText.text = "0/" + formula.m_DicMaterial[material];
                }
                else
                {
                    materialImage.sprite = BaseData.Instanse.m_DicMaterialSprite[material.m_Script];
                    if (BaseData.Instanse.m_MainCrotroller.m_Player.GetMaterial(material) < formula.m_DicMaterial[material])
                        materialText.color = Color.red;
                    else
                        materialText.color = Color.green;
                    materialText.text = BaseData.Instanse.m_MainCrotroller.m_Player.GetMaterial(material) + "/" + formula.m_DicMaterial[material];
                }
                index++;
            }
        }
    }

<<<<<<< HEAD
    /// <summary>
    /// 更新背包内容
    /// </summary>
    public void UpdateBagShow()
    {
        GameObject showPanel = GetObj("bagShowPanel");
        DeleteChild(showPanel);
        int index = 1;
        if (m_BagType== BagType.Item)
=======
        //酒吧打开关闭功能
        if (m_IsBarOpen)
        {
            Vector3 scale = GetObj("bar").transform.localScale;
            scale.x += 0.05f;
            if (scale.x >= 1f)
                scale.x = 1f;
            scale.y += 0.05f;
            if (scale.y >= 1f)
                scale.y = 1f;
            GetObj("bar").transform.localScale = scale;
        }
        else
        {
            Vector3 scale = GetObj("bar").transform.localScale;
            scale.x -= 0.05f;
            if (scale.x <= 0f)
                scale.x = 0f;
            scale.y -= 0.05f;
            if (scale.y <= 0f)
                scale.y = 0f;
            GetObj("bar").transform.localScale = scale;
        }

        //上移消息功能
        foreach (GameObject message in m_MessageList)
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
        {
            int catalogueMax = BaseData.Instanse.m_MainCrotroller.m_Player.itemList.Count / 30;
            GetText("bagCatalogue").text = m_ItemCatalogue + "/" + (catalogueMax + 1);

            foreach(Item item in BaseData.Instanse.m_MainCrotroller.m_Player.itemList)
            {
                if (index > (m_ItemCatalogue - 1) * 30 && index <= m_ItemCatalogue * 30)
                {
                    if (item.m_Vision == null)
                    {
                        GameObject vision = Instantiate(GetObj("visionModel"), GetObj("vision").transform);
                        vision.transform.localPosition = new Vector3(9999, 9999, 0);
                        SpriteRenderer visionImage = vision.GetComponent<SpriteRenderer>();
                        visionImage.sprite = BaseData.Instanse.m_DicItemSprite[item.m_Res];
                        item.m_Vision = vision;
                    }

                    GameObject obj = Instantiate(GetObj("bagIcon"), showPanel.transform);
                    Image image = obj.transform.Find("Image").GetComponent<Image>();
                    image.sprite = BaseData.Instanse.m_DicItemSprite[item.m_Res];
                    obj.transform.Find("Quality").GetComponent<Image>().color = BaseData.Instanse.GetColorByQuality(item.m_Quality);
                    if (item.m_Parent != 0)
                    {
                        Text Text = obj.transform.Find("Text").GetComponent<Text>();
                        obj.transform.Find("BlackPanel").localScale = new Vector3(1, 1, 1);
                        Text.color = Color.green;
                        Text.text = "已装备";
                    }
                    AddTriggerToItem(item, obj);
                }
                index++;
            }
        }
        else if (m_BagType == BagType.Material)
        {
            int catalogueMax = BaseData.Instanse.m_MainCrotroller.m_Player.dicMaterial.Count / 30;
            GetText("bagCatalogue").text = m_MaterialCatalogue + "/" + (catalogueMax + 1);

            foreach (MaterialDesc material in BaseData.Instanse.m_MainCrotroller.m_Player.dicMaterial.Keys)
            {
                if (index > (m_MaterialCatalogue - 1) * 30 && index <= m_MaterialCatalogue * 30)
                {
                    if (BaseData.Instanse.m_MainCrotroller.m_Player.dicMaterial[material] > 0)
                    {
                        GameObject obj = Instantiate(GetObj("bagIcon"), showPanel.transform);
                        Image image = obj.transform.Find("Image").GetComponent<Image>();
                        image.sprite = BaseData.Instanse.m_DicMaterialSprite[material.m_Script];
                        AddTriggerToMaterial(material, obj);
                        Text materialText = obj.transform.Find("Text").GetComponent<Text>();
                        obj.transform.Find("Quality").GetComponent<Image>().color = BaseData.Instanse.GetColorByQuality(material.m_Quality);
                        materialText.color = Color.green;
                        materialText.text = BaseData.Instanse.m_MainCrotroller.m_Player.dicMaterial[material] + "";
                    }
                }
                index++;
            }
        }
    }

    /// <summary>
    /// 更新队伍面板
    /// </summary>
    public void UpdateTeamPanel()
    {
        //显示已存在的英雄
        foreach (ModelGrid grid in BaseData.Instanse.m_MainCrotroller.m_DicTeamModelGrid.m_dicModelGrid.Values)
        {
            if (grid.modelData != null)
            {
                grid.modelData.ShowModel.transform.SetParent(grid.gridObj.transform);
                grid.modelData.ShowModel.transform.localPosition = new Vector3(0, 0, 0);
                grid.modelData.ShowModel.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        UpdateHeroList();
    }

    /// <summary>
    /// 更新英雄列表
    /// </summary>
    public void UpdateHeroList()
    {
        PlayerModel player = BaseData.Instanse.m_MainCrotroller.m_Player;
        DeleteChild(GetObj("heroList"));

        //添加英雄列表
        foreach (ModelData data in player.heroList)
        {
            if (BaseData.Instanse.m_MainCrotroller.m_DicTeamModelGrid.m_ModelList.Contains(data.Id))
            {

            }
            else
            {
            }

            GameObject heroIcon = Instantiate(m_HeroIconPrefab, GetObj("heroList").transform);
            Sprite head = GetSprite("heroHead" + data.Job.ToString());
            heroIcon.transform.Find("Head").GetComponent<Image>().sprite = head;
            heroIcon.transform.Find("Name").GetComponent<Text>().text = data.Name;
            heroIcon.transform.Find("Level").GetComponent<Text>().text = "Lv" + data.Level;
            heroIcon.transform.Find("Job").GetComponent<Text>().text = ModelData.JobToString(data.Job);

            AddTriggerToModel(data, heroIcon);
        }
    }

    /// <summary>
    /// 更新英雄面板
    /// </summary>
    public void UpdateHeroPanel(ModelData model)
    {
        if (model != null)
        {
            //移动旧人物
            for (int i = GetObj("heroModelPos").transform.childCount - 1; i >= 0; i--)
            {
                GameObject child = GetObj("heroModelPos").transform.GetChild(i).gameObject;
                child.transform.SetParent(BaseData.Instanse.m_ModelPool.transform);
                child.transform.localPosition = new Vector3(0, 0, 0);
                child.transform.localScale = new Vector3(1, 1, 1);
            }

            //移动新人物
            m_SelectHero = model;
            m_SelectHero.ShowModel.transform.SetParent(GetObj("heroModelPos").transform);
            m_SelectHero.ShowModel.transform.localPosition = new Vector3(0, 0, 0);
            m_SelectHero.ShowModel.transform.localScale = new Vector3(1, 1, 1);

            //更新人物预览
            GetText("modelName").text = model.Name;
            GetText("modelLevel").text = "Lv" + model.Level.ToString();
            GetText("modelJob").text = ModelData.JobToString(model.Job);

            //更新装备
            if (m_HeroInfoType == HeroInfoType.Item)
            {
                GetObj("attributePanel").SetActive(false);
                GetObj("itemPanel").SetActive(true);
                foreach (ItemPlace place in model.m_DicItem.Keys)
                {
                    Item item;
                    model.m_DicItem.TryGetValue(place, out item);
                    if (item != null)
                    {
                        GetImg("item" + item.m_Type.ToString()).sprite = BaseData.Instanse.m_DicItemSprite[item.m_Res];
                        AddTriggerToItem(item, GetImg("item" + item.m_Type.ToString()).gameObject);
                    }
                    else
                    {
                        GetImg("item" + place.ToString()).sprite = BaseData.Instanse.m_WhiteMask;
                    }
                }
            }

            //更新属性
            if (m_HeroInfoType== HeroInfoType.Attribute)
            {
                GetObj("itemPanel").SetActive(false);
                GetObj("attributePanel").SetActive(true);

                GetText("heroAttribute").text = UpdateHeroPanelText(model);
            }
        }
    }

    /// <summary>
    /// 为ItemIcon添加UI展示
    /// </summary>
    /// <param name="item"></param>
    /// <param name="obj"></param>
    public void AddTriggerToItem(Item item, GameObject obj)
    {
        EventTrigger triggers = obj.AddComponent<EventTrigger>();

        //进入方法
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(delegate (BaseEventData e)
        {
            GameObject abilityList = GetObj("itemAbilityList");
            DeleteChild(abilityList);

            //设置Icon位置，并处理是否超出屏幕
            GetImg("itemImg").sprite = BaseData.Instanse.m_DicItemSprite[item.m_Res];
            //技能修改
            Ability ability = item.m_Ability;
            if (ability != null)
            {
                GameObject abilityIcon = Instantiate(GetObj("abilityIcon"), abilityList.transform);
                string message = DataManager.Instanse.m_AbilityDescContainer.GetDescByID(ability.Id).m_Message;
                abilityIcon.transform.GetComponentInChildren<Text>().text = message;
                if (BaseData.Instanse.m_DicAbilitySprite.ContainsKey(ability.ScriteName))
                    abilityIcon.transform.GetComponent<Image>().sprite = BaseData.Instanse.m_DicAbilitySprite[ability.ScriteName];
            }

            GetText("itemHead").text = item.m_Name + "\n等级 " + item.m_Level +"\n" + item.m_Message;
            GetText("itemAttributeText").text = "";
            foreach (string[] value in item.m_AttributeList)
            {
                GetText("itemAttributeText").text += BaseData.AttributeToChinese(value[0]) + " + " + value[1] + "\n";
            }
            foreach (string[] value in item.m_ExtraAttributeList)
            {
                GetText("itemAttributeText").text += BaseData.AttributeToChinese(value[0]) + " + " + value[1] + "\n";
            }

            Vector3 pos = obj.transform.position;
            pos.x += obj.GetComponent<RectTransform>().rect.width + 5;
            pos.y += obj.GetComponent<RectTransform>().rect.height;
            float length = GetUIRect(GetObj("itemMessage").transform.Find("Background").GetComponent<RectTransform>(), pos);
            pos += new Vector3(0, length, 0);
            GetObj("itemMessage").transform.position = pos;
            UpdateLayout(GetObj("itemMessage").transform.Find("Background"));
        });

        //点击方法
        EventTrigger.Entry click = new EventTrigger.Entry();
        click.eventID = EventTriggerType.PointerClick;
        click.callback.AddListener(delegate (BaseEventData e)
        {
            //判断右键
            if (Input.GetMouseButtonUp(1))
            {
                if (item.m_JobList.Contains(m_SelectHero.Job))
                {
                    GetImg("item" + item.m_Type.ToString()).sprite = BaseData.Instanse.m_DicItemSprite[item.m_Res];
                    AddTriggerToItem(item, GetImg("item" + item.m_Type.ToString()).gameObject);
                    //如果该英雄有穿戴装备则卸下
                    if (m_SelectHero.m_DicItem[item.m_Type] != null)
                        m_SelectHero.RemoveItem(item.m_Type);
                    //如果该装备有英雄穿戴则卸下
                    if (item.m_Parent != 0)
                        BaseData.Instanse.m_MainCrotroller.m_Player.GetHero(item.m_Parent).RemoveItem(item.m_Type);
                    m_SelectHero.UpdateItem(item);
                    CanvasUpdate(12);
                    UpdateBagShow();
                }
                else
                {
                    SendMessage("职业不符合", 3f);
                }
            }
        });
        //退出方法
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener(delegate (BaseEventData e)
        {
            GetObj("itemMessage").transform.localPosition = new Vector3(9999, 9999, 0);
        });
        ////按下方法
        //EventTrigger.Entry down = new EventTrigger.Entry();
        //down.eventID = EventTriggerType.PointerDown;
        //down.callback.AddListener(delegate (BaseEventData e)
        //{
        //    if (m_DicPanelSwitch["heroPanel"])
        //    {
        //        m_IsMoveItem = true;
        //        m_MoveingItem = item.m_Vision;
        //        m_ItemParentPos = item.m_Vision.transform.localPosition;
        //    }
        //});
        ////松开方法
        //EventTrigger.Entry up = new EventTrigger.Entry();
        //up.eventID = EventTriggerType.PointerUp;
        //up.callback.AddListener(delegate (BaseEventData e)
        //{
        //    if (m_DicPanelSwitch["heroPanel"])
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit hit;
        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            //检测tag为Item的物品栏
        //            if (hit.transform.tag == "Item")
        //            {
        //                //检测是否为对应装备类型
        //                if (hit.collider.gameObject.name == item.m_Type.ToString())
        //                {
        //                    if (m_SelectHero.m_DicItem[item.m_Type] != item)
        //                    {
        //                        hit.collider.gameObject.GetComponent<Image>().sprite = BaseData.Instanse.m_DicItemSprite[item.m_Res];
        //                        AddTriggerToItem(item, GetImg("item" + item.m_Type.ToString()).gameObject);
        //                        //如果该装备有英雄穿戴则卸下
        //                        if (item.m_Parent != 0)
        //                            BaseData.Instanse.m_MainCrotroller.m_Player.GetHero(item.m_Parent).RemoveItem(item.m_Type);
        //                        m_SelectHero.UpdateItem(item);
        //                    }
        //                }
        //            }
        //        }
        //        item.m_Vision.transform.localPosition = new Vector3(9999, 9999, 0);
        //        m_IsMoveItem = false;
        //        m_MoveingItem = null;
        //    }
        //});


        triggers.triggers.Add(click);
        triggers.triggers.Add(enter);
        triggers.triggers.Add(exit);
        //triggers.triggers.Add(down);
        //triggers.triggers.Add(up);
    }

    /// <summary>
    /// 为掉落ItemIcon添加UI展示
    /// </summary>
    /// <param name="desc"></param>
    /// <param name="obj"></param>
    public void AddTriggerToItem(ItemDesc desc, GameObject obj)
    {
        EventTrigger triggers = obj.AddComponent<EventTrigger>();

        //进入方法
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(delegate (BaseEventData e)
        {
            GameObject abilityList = GetObj("itemAbilityList");
            DeleteChild(abilityList);
            //设置Icon位置，并处理是否超出屏幕
            GetImg("itemImg").sprite = BaseData.Instanse.m_DicItemSprite[desc.m_Res];
            //技能修改
            foreach (int abilityId in desc.m_AbilityList)
            {
                AbilityDesc ability = DataManager.Instanse.m_AbilityDescContainer.GetDescByID(abilityId);
                GameObject abilityIcon = Instantiate(GetObj("abilityIcon"), abilityList.transform);
                string message = ability.m_Message;
                abilityIcon.transform.GetComponentInChildren<Text>().text = message;
                if (BaseData.Instanse.m_DicAbilitySprite.ContainsKey(ability.m_Script))
                    abilityIcon.transform.GetComponent<Image>().sprite = BaseData.Instanse.m_DicAbilitySprite[ability.m_Script];
            }

            GetText("itemHead").text = desc.m_Name + "\n\n" + desc.m_Message;
            GetText("itemAttributeText").text = "";
            foreach (string[] value in desc.m_AttributeList)
            {
                GetText("itemAttributeText").text += BaseData.AttributeToChinese(value[0]) + " + " + value[1] + "\n";
            }
            GetText("itemAttributeText").text += "随机多项属性";

            Vector3 pos = obj.transform.position;
            pos.x += obj.GetComponent<RectTransform>().rect.width + 5;
            pos.y += obj.GetComponent<RectTransform>().rect.height;
            float length = GetUIRect(GetObj("itemMessage").transform.Find("Background").GetComponent<RectTransform>(), pos);
            pos += new Vector3(0, length, 0);
            GetObj("itemMessage").transform.position = pos;
            UpdateLayout(GetObj("itemMessage").transform.Find("Background"));
        });

        //退出方法
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener(delegate (BaseEventData e)
        {
            GetObj("itemMessage").transform.localPosition = new Vector3(9999, 9999, 0);
        });


        triggers.triggers.Add(enter);
        triggers.triggers.Add(exit);
    }

    /// <summary>
    /// 为图纸添加UI展示
    /// </summary>
    public void AddTriggerToItemFormula(ItemFormulaDesc formula,ItemDesc desc, GameObject obj)
    {
        EventTrigger triggers = obj.AddComponent<EventTrigger>();

        //进入方法
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(delegate (BaseEventData e)
        {
            GameObject abilityList = GetObj("itemAbilityList");
            DeleteChild(abilityList);
            //设置Icon位置，并处理是否超出屏幕
            GetImg("itemImg").sprite = BaseData.Instanse.m_DicItemSprite[desc.m_Res];
            //技能修改
            foreach (int abilityId in desc.m_AbilityList)
            {
                AbilityDesc ability = DataManager.Instanse.m_AbilityDescContainer.GetDescByID(abilityId);
                GameObject abilityIcon = Instantiate(GetObj("abilityIcon"), abilityList.transform);
                string message = ability.m_Message;
                abilityIcon.transform.GetComponentInChildren<Text>().text = message;
                if (BaseData.Instanse.m_DicAbilitySprite.ContainsKey(ability.m_Script))
                    abilityIcon.transform.GetComponent<Image>().sprite = BaseData.Instanse.m_DicAbilitySprite[ability.m_Script];
            }

            GetText("itemHead").text = desc.m_Name + "\n\n" + desc.m_Message;
            GetText("itemAttributeText").text = "";
            foreach (string[] value in desc.m_AttributeList)
            {
                GetText("itemAttributeText").text += BaseData.AttributeToChinese(value[0]) + " + " + value[1] + "\n";
            }
            GetText("itemAttributeText").text += "随机多项属性";

            Vector3 pos = obj.transform.position;
            pos.x += obj.GetComponent<RectTransform>().rect.width + 5;
            pos.y += obj.GetComponent<RectTransform>().rect.height;
            float length = GetUIRect(GetObj("itemMessage").transform.Find("Background").GetComponent<RectTransform>(), pos);
            pos += new Vector3(0, length, 0);
            GetObj("itemMessage").transform.position = pos;
            UpdateLayout(GetObj("itemMessage").transform.Find("Background"));
        });

        //退出方法
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener(delegate (BaseEventData e)
        {
            GetObj("itemMessage").transform.localPosition = new Vector3(9999, 9999, 0);
        });

        //点击方法
        EventTrigger.Entry click = new EventTrigger.Entry();
        click.eventID = EventTriggerType.PointerClick;
        click.callback.AddListener(delegate (BaseEventData e)
        {
            UpdateBlacksmithMake(formula, desc);
        });


        triggers.triggers.Add(enter);
        triggers.triggers.Add(exit);
        triggers.triggers.Add(click);
    }

    /// <summary>
<<<<<<< HEAD
    /// 为material添加UI展示
=======
    /// 更新酒吧内容
    /// </summary>
    public void UpdateBar()
    {
        GameObject barHeroListPanel = GetObj("barHeroListPanel");

        for (int i = barHeroListPanel.transform.childCount - 1; i >= 0; i--)
        {
            GameObject child = barHeroListPanel.transform.GetChild(i).gameObject;
            DestroyImmediate(child);
        }

        foreach (ModelData data in BaseData.Instanse.m_MainCrotroller.m_Player.barModelList)
        {
            GameObject barHeroIcon = Instantiate(GetObj("barHeroIcon"), barHeroListPanel.transform);
            GameObject head = barHeroIcon.transform.Find("Head").gameObject;
            Image headImage = head.GetComponent<Image>();

            //添加交互
            EventTrigger triggers = barHeroIcon.AddComponent<EventTrigger>();
            //鼠标进入方法
            EventTrigger.Entry click = new EventTrigger.Entry();
            click.eventID = EventTriggerType.PointerClick;
            click.callback.AddListener(delegate (BaseEventData e)
            {
                UpdateModelPos();
                BattleModel model = BaseData.Instanse.GetModelById<BattleModel>(data.ModelId);
                model.transform.SetParent(GetObj("barHeroModelPos").transform);
                model.transform.localPosition = new Vector3(0, 0, 0);
            });

            triggers.triggers.Add(click);
        }
    }

    /// <summary>
    /// 清空模型坐标内容
    /// </summary>
    private void UpdateModelPos()
    {
        GameObject modelPos = GetObj("barHeroModelPos");

        for (int i = modelPos.transform.childCount - 1; i >= 0; i--)
        {
            GameObject child = modelPos.transform.GetChild(i).gameObject;
            child.transform.SetParent(null);
        }
    }

    /// <summary>
    /// 更新背包内容
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
    /// </summary>
    /// <param name="material"></param>
    /// <param name="obj"></param>
    public void AddTriggerToMaterial(MaterialDesc material,GameObject obj)
    {
        EventTrigger triggers = obj.AddComponent<EventTrigger>();

        //进入方法
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(delegate (BaseEventData e)
        {
            //设置Icon位置，并处理是否超出屏幕

            GetImg("materialImg").sprite = BaseData.Instanse.m_DicMaterialSprite[material.m_Script];
            GetText("materialHead").text = material.m_Name;
            GetText("materialText").text = material.m_Message;
            Vector3 pos = obj.transform.position;
            pos.x += obj.GetComponent<RectTransform>().rect.width + 5;
            pos.y += obj.GetComponent<RectTransform>().rect.height;
            float length = GetUIRect(GetObj("materialMessage").GetComponent<RectTransform>(), pos);
            pos += new Vector3(0, length, 0);
            GetObj("materialMessage").transform.position = pos;
        });
        //退出方法
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener(delegate (BaseEventData e)
        {
            GetObj("materialMessage").transform.localPosition = new Vector3(9999, 9999, 0);
        });

        triggers.triggers.Add(enter);
        triggers.triggers.Add(exit);
    }

    /// <summary>
    /// 为图纸添加UI展示
    /// </summary>
    /// <param name="itemFormula"></param>
    /// <param name="obj"></param>
    public void AddTriggerToItemFormula(ItemFormulaDesc itemFormula, GameObject obj)
    {
        EventTrigger triggers = obj.AddComponent<EventTrigger>();

        //进入方法
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(delegate (BaseEventData e)
        {
            //设置Icon位置，并处理是否超出屏幕
            string path = DataManager.Instanse.m_ItemDescContainer.GetDescByID(itemFormula.m_ItemId).m_Res;
            GetImg("itemFormulaImg").sprite = BaseData.Instanse.m_DicItemSprite[path];
            GetText("itemFormulaHead").text = itemFormula.m_Name;
            GetText("itemFormulaText").text = itemFormula.m_Message;
            Vector3 pos = obj.transform.position;
            pos.x += obj.GetComponent<RectTransform>().rect.width + 5;
            pos.y += obj.GetComponent<RectTransform>().rect.height;
            float length = GetUIRect(GetObj("itemFormulaMessage").GetComponent<RectTransform>(), pos);
            pos += new Vector3(0, length, 0);
            GetObj("itemFormulaMessage").transform.position = pos;
        });
        //退出方法
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener(delegate (BaseEventData e)
        {
            GetObj("itemFormulaMessage").transform.localPosition = new Vector3(9999, 9999, 0);
        });

        triggers.triggers.Add(enter);
        triggers.triggers.Add(exit);
    }

    /// <summary>
    /// 获取UI面板超出屏幕的部分
    /// </summary>
    /// <param name="targetObj"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public float GetUIRect(RectTransform targetObj,Vector2 pos)
    {
        float y = pos.y / Screen.height;
        float objY = targetObj.rect.height / 135f;

        float length = y - objY;
        if (length < -1)
            return -(length + 1) * Screen.height;

        return 0;
    }

    /// <summary>
    /// 为模型添加交互功能
    /// </summary>
    public void AddTriggerToModel(ModelData data,GameObject obj)
    {
        EventTrigger trigger = obj.AddComponent<EventTrigger>();

        //按下方法
        EventTrigger.Entry down = new EventTrigger.Entry();
        down.eventID = EventTriggerType.PointerDown;
        down.callback.AddListener(delegate (BaseEventData e)
        {
            //当队伍面板开启时判断
            if (m_DicPanelSwitch["teamPanel"])
            {
                m_IsMoveHero = true;
                m_SelectHero = data;
                m_MoveingHero = data.ShowModel;
                data.ShowModel.transform.SetParent(BaseData.Instanse.m_ModelPool.transform);
                data.ShowModel.transform.localScale = new Vector3(1, 1, 1);
            }
        });

        //松开方法
        EventTrigger.Entry up = new EventTrigger.Entry();
        up.eventID = EventTriggerType.PointerUp;
        up.callback.AddListener(delegate (BaseEventData e)
        {
            //当队伍面板开启时判断
            if (m_DicPanelSwitch["teamPanel"])
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                ModelGridCollection collection = BaseData.Instanse.m_MainCrotroller.m_DicTeamModelGrid;
                if (Physics.Raycast(ray, out hit))
                {
                    //判断是队伍网格则进入
                    if (hit.transform.tag == "HeroPlace")
                    {
                        int number = int.Parse(hit.collider.name.Split('_')[1]);
                        ModelGrid grid = collection.m_dicModelGrid[number];
                        //查询是否队伍网格中已存在该英雄
                        foreach (ModelGrid oldGrid in collection.m_dicModelGrid.Values)
                        {
                            if (oldGrid.modelData == data)
                            {
                                //oldGrid.modelObj.transform.position = new Vector3(9999, 9999, 0);
                                collection.RemoveModel(oldGrid.gridIndex);

                                //如果该网格中有其他英雄，则进行交换位置
                                if (grid.modelData != null)
                                {
                                    grid.modelData.ShowModel.transform.SetParent(oldGrid.gridObj.transform);
                                    grid.modelData.ShowModel.transform.localPosition = new Vector3(0, 0, 0);
                                    oldGrid.battleModelId = grid.modelData.Id;
                                    oldGrid.modelData = grid.modelData;
                                    collection.RemoveModel(grid.gridIndex);
                                    collection.AddModel(oldGrid.gridIndex, oldGrid);
                                }
                                break;
                            }
                        }
                        if (grid.modelData != null)
                        {
                            grid.modelData.ShowModel.transform.SetParent(BaseData.Instanse.m_ModelPool.transform);
                            grid.modelData.ShowModel.transform.localPosition = new Vector3(0, 0, 0);
                            collection.RemoveModel(grid.gridIndex);
                            //移动英雄模型至指定位置
                            data.ShowModel.transform.SetParent(grid.gridObj.transform);
                            data.ShowModel.transform.localPosition = new Vector3(0, 0, 0);
                            //修改队伍网格数据
                            grid.battleModelId = data.Id;
                            grid.modelData = data;
                            collection.AddModel(number, grid);
                        }
                        else if (collection.m_ModelList.Count < BaseData.Instanse.m_MainCrotroller.m_TeamMaxCount)
                        {
                            //移动英雄模型至指定位置
                            data.ShowModel.transform.SetParent(grid.gridObj.transform);
                            data.ShowModel.transform.localPosition = new Vector3(0, 0, 0);
                            //修改队伍网格数据
                            grid.battleModelId = data.Id;
                            grid.modelData = data;
                            collection.AddModel(number, grid);
                        }
                        else
                        {
                            SendMessage("队伍已满", 3f);
                            data.ShowModel.transform.position = new Vector3(9999, 9999, 0);
                        }
                    }
                    else
                    {
                        //移动英雄换位置失败
                        bool isFind = false;
                        //判断当前队伍中是否存在该英雄，有的话则归位
                        foreach (ModelGrid oldGrid in collection.m_dicModelGrid.Values)
                        {
                            if (oldGrid.modelData == data)
                            {
                                data.ShowModel.transform.SetParent(oldGrid.gridObj.transform);
                                data.ShowModel.transform.localPosition = new Vector3(0, 0, 0);
                                isFind = true;
                                break;
                            }
                        }
                        if (isFind == false)
                            data.ShowModel.transform.position = new Vector3(9999, 9999, 0);
                    }
                }
                else
                {
                    //移动英雄换位置失败
                    int index = 0;
                    //判断当前队伍中是否存在该英雄，有的话则移出
                    foreach (ModelGrid oldGrid in collection.m_dicModelGrid.Values)
                    {
                        if (oldGrid.modelData == data)
                        {
                            index = oldGrid.gridIndex;
                            break;
                        }
                    }

                    if (index != 0)
                        collection.RemoveModel(index);

                    data.ShowModel.transform.position = new Vector3(9999, 9999, 0);
                }
                m_IsMoveHero = false;
                m_MoveingHero = null;
            }
        });

        //点击方法
        EventTrigger.Entry click = new EventTrigger.Entry();
        click.eventID = EventTriggerType.PointerClick;
        click.callback.AddListener(delegate (BaseEventData e)
        {
            if (m_DicPanelSwitch["heroPanel"])
            {
                UpdateHeroPanel(data);
            }
        });

        trigger.triggers.Add(up);
        trigger.triggers.Add(down);
        trigger.triggers.Add(click);
    }

    /// <summary>
    /// 发送提示
    /// </summary>
    /// <param name="message"></param>
    public void SendMessage(string message, float time)
    {
        m_DicPanelSwitch["errorMessage"] = true;
        GetText("errorMessage").text = message;
        //AudioSource.PlayClipAtPoint(BaseData.Instanse.m_ErrorMessage, Camera.main.transform.position);
        TimeCrotroller.Instance.AddTimerTask(time,"Message", delegate ()
        {
            m_DicPanelSwitch["errorMessage"] = false;
        });
    }

    /// <summary>
    /// 发送头顶信息
    /// </summary>
    /// <param name="message"></param>
    public void SendMessage(string message, float time, Transform parent)
    {
        GameObject messageObj = Instantiate(GetObj("messagePrefab"), parent);
        Text text = messageObj.GetComponentInChildren<Text>();
        text.text = message;
        messageObj.GetComponent<Canvas>().sortingLayerName = "Message";
        messageObj.GetComponent<Canvas>().sortingOrder = 10;
        m_MessageList.Add(messageObj);
        Destroy(messageObj, time);
    }

    /// <summary>
    /// 发送头顶消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="time"></param>
    /// <param name="parent"></param>
    /// <param name="color"></param>
    public void SendMessage(string message, float time, Transform parent, Color color)
    {
        GameObject messageObj = Instantiate(GetObj("messagePrefab"), parent);
        Text text = messageObj.GetComponentInChildren<Text>();
        text.color = color;
        text.text = message;
        messageObj.GetComponent<Canvas>().sortingLayerName = "Message";
        messageObj.GetComponent<Canvas>().sortingOrder = 10;
        m_MessageList.Add(messageObj);
        Destroy(messageObj, time);
    }

    /// <summary>
    /// 存储button
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    public void SetButton(string path, string name)
    {
        Button button = GameObject.Find(path).GetComponent<Button>();
        m_DicButton.Add(name, button);
    }

    /// <summary>
    /// 获取button
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Button GetButton(string name)
    {
        Button button;
        m_DicButton.TryGetValue(name, out button);
        return button;
    }

    /// <summary>
    /// 存储Text
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    public void SetText(string path, string name)
    {
        Text text = GameObject.Find(path).GetComponent<Text>();
        m_DicText.Add(name, text);
    }

    /// <summary>
    /// 获取Text
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Text GetText(string name)
    {
        Text text;
        m_DicText.TryGetValue(name, out text);
        return text;
    }

    /// <summary>
    /// 存储Obj
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    public void SetObj(string path, string name)
    {
        GameObject obj = GameObject.Find(path);
        m_DicObj.Add(name, obj);
    }

    /// <summary>
    /// 存储Obj
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="name"></param>
    public void SetObj(GameObject obj, string name)
    {
        m_DicObj.Add(name, obj);
    }

    /// <summary>
    /// 获取GameObject
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetObj(string name)
    {
        GameObject obj;
        m_DicObj.TryGetValue(name, out obj);
        return obj;
    }

    /// <summary>
    /// 存储Image
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    public void SetImg(string path, string name)
    {
        Image obj = GameObject.Find(path).GetComponent<Image>();
        m_DicImg.Add(name, obj);
    }

    /// <summary>
    /// 存储Image
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    public void SetImg(Image image, string name)
    {
        m_DicImg.Add(name, image);
    }

    /// <summary>
    /// 获取Image
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Image GetImg(string name)
    {
        Image img;
        m_DicImg.TryGetValue(name, out img);
        return img;
    }

    /// <summary>
    /// 存储Sprite
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    public void SetSprite(Sprite sprite, string name)
    {
        m_DicSprite.Add(name, sprite);
    }

    /// <summary>
    /// 获取Sprite
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Sprite GetSprite(string name)
    {
        Sprite sprite;
        m_DicSprite.TryGetValue(name, out sprite);
        return sprite;
    }

    /// <summary>
    /// 更新英雄面板文本
    /// </summary>
    public string UpdateHeroPanelText(ModelData data)
    {
        string attributeText = "";
        attributeText += "生命:" + data.TotalHp + "\n";
        attributeText += "攻击:" + data.TotalAttack + "\n";
        attributeText += "防御:" + data.TotalDefense + "\n";
        attributeText += "速度:" + data.TotalSpeed + "\n";
        attributeText += "暴击:" + data.m_Crit + "%\n";
        attributeText += "爆伤:" + data.m_CritDamage + "%\n";
        return attributeText;
    }

    /// <summary>
    /// 删除子物体
    /// </summary>
    public void DeleteChild(GameObject obj)
    {
        for (int i = obj.transform.childCount - 1; i >= 0; i--)
        {
            GameObject child = obj.transform.GetChild(i).gameObject;
            DestroyImmediate(child);
        }
    }

    /// <summary>
    /// 刷新带有Layout组件的UI布局
    /// </summary>
    public void UpdateLayout(Transform obj)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(obj.GetComponent<RectTransform>());
    }

    private List<string> switchList;

    /// <summary>
    /// 关闭所有窗口方法
    /// </summary>
    public void ClosePanel()
    {
        foreach(ModelData hero in BaseData.Instanse.m_MainCrotroller.m_Player.heroList)
        {
            hero.ShowModel.transform.localPosition = new Vector3(0, 0, 0);
        }

        if (switchList == null)
        {
            switchList = new List<string>();
            foreach (string key in m_DicPanelSwitch.Keys)
            {
                switchList.Add(key);
            }
        }

        for(int i = 0; i < switchList.Count; i++)
        {
            m_DicPanelSwitch[switchList[i]] = false;
        }
        GetObj("blackBackground").gameObject.SetActive(false);
        //m_IsHeroOpen = false;
        //m_IsTeamOpen = false;
        //m_IsIndustryOpen = false;
        //m_IsBlacksmithOpen = false;
    }

    /// <summary>
    /// 切换窗口(0、飞艇 1、战斗场景)
    /// </summary>
    /// <param name="canvasIndex"></param>
    //public void UpdateCanvas(int canvasIndex)
    //{
    //    m_UseCanvas.sortingOrder = 0;
    //    switch (canvasIndex)
    //    {
    //        case 0:
    //            GetCanvas("airshipCanvas").sortingOrder = 1;
    //            break;
    //        case 1:
    //            GetCanvas("expeditionCanvas").sortingOrder = 1;
    //            break;
    //    }
    //}

    /// <summary>
    /// 背包分区
    /// </summary>
    public enum BagType
    {
        Item,
        Material,
    }

    public class UIInteractive
    {
        public bool m_IsMoving;
        public int m_MovePosIndex;
        public List<Vector2> m_TargetList;

        public UIInteractive(List<Vector2> targetList)
        {
            m_MovePosIndex = 0;
            m_TargetList = targetList;
        }
    }

    /// <summary>
    /// 英雄信息分区
    /// </summary>
    public enum HeroInfoType
    {
        Item,
        Attribute,
    }
}

public class Blacksmith
{
    public ItemDesc m_Item;
    public ItemFormulaDesc m_ItemFormula;

    public Blacksmith(ItemDesc item,ItemFormulaDesc itemFormula)
    {
        m_Item = item;
        m_ItemFormula = itemFormula;
    }
}
