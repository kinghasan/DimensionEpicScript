using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleModel : BaseModel
{
    //基础最大生命值
    private int _MaxHP;
    //当前生命值
    private int _HP;
    //基础攻击力
    private int _Attack;
    //基础防御力
    private int _Defense;
    //护盾(最大值为生命值的1倍)
    private int _Shield;
    //速度
    private int _Speed;
    //进度
    private int _Progress;
    //治疗量
    private int _Health;
    //名字
    private string _HeroName;
    //等级
    private int _Level;
    //职业
    private HeroJob _Job;

    public int MaxHP { get => _MaxHP; set => _MaxHP = value; }
    public int TotalHP { get => (int)(MaxHP * (1 + PHp * 0.01f)); }
    public int HP { get => _HP; set => _HP = value; }
    public int Attack { get => _Attack; set => _Attack = value; }
    public int TotalAttack { get => (int)(Attack * (1 + PAttack * 0.01f)); }
    public int Defense { get => _Defense; set => _Defense = value; }
    public int TotalDefense { get => (int)(_Defense * (1 + PDefense * 0.01f)); }
    public int Speed { get => _Speed; set => _Speed = value; }
    public int TotalSpeed { get => (int)(_Speed * (1 + PSpeed * 0.01f)); }
    public int Progress { get => _Progress; set => _Progress = value; }
    public string HeroName { get => _HeroName; set => _HeroName = value; }
    public int Level { get => _Level; set => _Level = value; }
    public HeroJob Job { get => _Job; set => _Job = value; }
    public int Health { get => _Health; set => _Health = value; }
    public int Shield { get => _Shield; set => _Shield = value; }

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

    //Buff列表
    public List<BuffData> m_BuffList;
    //状态字典
    public Dictionary<BuffData.m_State, bool> m_DicState;
    //所属网格
    public ModelGrid m_Parent;
    //所属ModelData类
    public ModelData m_Data;
    //基础攻击技能
    public Ability m_CommonAttack;
    //终极技能
    public Ability m_FinalAbility;
    //攻击的敌人列表
    public List<BattleModel> m_EnemyList;
    //攻击前事件
    public event EventCrotroller.eventHandler OnAttack;
    //攻击后事件
    public event EventCrotroller.eventHandler Attacked;
    //动画组件
    public Animator m_Animator;
    //是否移动中
    public bool m_IsMoveing;
    //是否存活
    public bool m_IsAlive;
    //移动地点（屏幕中间，自身原本位置)
    private MovePos m_TargetPos;
    //是否玩家物体
    public bool m_IsPlayer;
    //血条
    public Slider m_ModelHpSlider;
    //护盾
    public Slider m_ModelShieldSlider;
    //血条文本
    private Text m_ModelHpText;
    //Buff显示列表
    public GameObject m_BuffPanel;
    //音效脚本
    public AudioPlay m_Audio;

    private enum MovePos
    {
        Center,
        Parent,
    }

    private void Awake()
    {
        m_EnemyList = new List<BattleModel>();
        m_BuffList = new List<BuffData>();
        m_DicState = new Dictionary<BuffData.m_State, bool>();
        m_IsAlive = true;
        m_Animator = gameObject.transform.Find("Body").GetComponent<Animator>();
        Attacked += delegate (EventBase e)
        {
            MoveToParent();
        };

        foreach (BuffData.m_State state in Enum.GetValues(typeof(BuffData.m_State)))
        {
            m_DicState.Add(state, false);
        }
    }

    private void Start()
    {
    }

    public void Run()
    {
        m_ModelHpSlider.value = (float)HP / TotalHP;
        m_ModelShieldSlider.value = (float)Shield / TotalHP;
        //m_ModelHpText.text = HP.ToString();

        //如果在演出中则进行移动
        if (m_IsMoveing)
        {
            //移动到屏幕中间攻击
            if(m_TargetPos== MovePos.Center)
            {
                Vector2 target = ExpeditionCrotroller.Instanse.m_BattleBackground.transform.position;
                Vector2 p0 = transform.position;
                if ((p0 - target).sqrMagnitude < 50)
                {
                    transform.position = new Vector2(target.x, target.y);
                    m_IsMoveing = false;
                    //m_Animator.SetTrigger("Attack");
                    //TimeCrotroller.Instance.AddTimerTask(0.1f, "Attack" + m_Id, delegate ()
                    //{
                    // }, 1, delegate ()
                    // {
                    //     if(m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    //     {
                    //         m_Animator.SetTrigger("Idle");
                    //         AttackedEvent attacked = new AttackedEvent();
                    //         attacked.m_Attacker = this;
                    //         attacked.m_TargetList = m_EnemyList;
                    //         Attacked.Invoke(attacked);
                    //         return 0;
                    //     }
                    //     else
                    //     {
                    //         return 1;
                    //     }
                    // });
                }
                else
                {
                    Vector2 p01 = (p0 - target).normalized * 10 * Time.timeScale;
                    Vector2 p02 = p0 - p01;
                    Vector2 p03 = new Vector2(p02.x, p02.y);
                    transform.position = p03;
                }
            }
            //移动到原本位置
            else if (m_TargetPos == MovePos.Parent)
            {
                Vector2 target = new Vector2(0, 0);
                Vector2 p0 = transform.localPosition;
                if ((p0 - target).sqrMagnitude < 50)
                {
                    transform.localPosition = new Vector2(target.x, target.y);
                    m_IsMoveing = false;
                    //m_Animator.SetTrigger("Idle");
                    //ExpeditionCrotroller.Instanse.state = ExpeditionState.Wait;
                    //Debug.Log(gameObject.transform.Find("Body").GetComponent<Animator>().GetCurrentAnimatorClipInfo(0));
                }
                else
                {
                    Vector2 p01 = (p0 - target).normalized * 10 * Time.timeScale;
                    Vector2 p02 = p0 - p01;
                    Vector2 p03 = new Vector3(p02.x, p02.y, 0);
                    transform.localPosition = p03;
                }
            }
        }
    }

    /// <summary>
    /// 初始化单位数据
    /// </summary>
    /// <param name="data"></param>
    public void InitModel(ModelData data)
    {
        m_ModelHpSlider = gameObject.transform.Find("HPos/Canvas/HP").GetComponent<Slider>();
        m_ModelShieldSlider = gameObject.transform.Find("HPos/Canvas/Shield").GetComponent<Slider>();
        m_ModelHpText = gameObject.transform.Find("HPos/Canvas/HP/Value").GetComponent<Text>();
        m_BuffPanel = gameObject.transform.Find("HPos/Canvas/BuffList").gameObject;
        _HeroName = data.Name;
        _Level = data.Level;
        _Job = data.Job;

        PHp = data.PHp;
        _MaxHP = data.HP + data.ItemHp;
        _HP = TotalHP;
        _Attack = data.Attack + data.ItemAttack;
        PAttack = data.PAttack;
        _Defense = data.Defense + data.ItemDefense;
        PDefense = data.PDefense;
        _Speed = data.Speed + data.ItemSpeed;
        PSpeed = data.PSpeed;
        m_Crit = data.m_Crit;
        m_CritDamage = data.m_CritDamage;

        m_Data = data;

        //添加基础攻击
        if (data.m_CommonAbility != null)
        {
            Ability ability = data.m_CommonAbility;
            object[] parameters = new object[] { this, ability.Level,ExpeditionCrotroller.Instanse.m_DicLeftModelGrid,
                ExpeditionCrotroller.Instanse.m_DicRightModelGrid};
            if (ability.IsPassive)
            {
                if (ability.CanCast(this))
                    ability.Cast(this);
            }
            m_CommonAttack = ability;
        }
        //添加被动技能
        foreach(Ability ability in data.m_PassiveAbilityList)
        {
            object[] parameters = new object[] { this, ability.Level,ExpeditionCrotroller.Instanse.m_DicLeftModelGrid,
                ExpeditionCrotroller.Instanse.m_DicRightModelGrid};
            if (ability.IsPassive)
            {
                if (ability.CanCast(this))
                    ability.Cast(this);
            }
        }
        //添加终极技能
        if (data.m_FinalAbility != null)
        {
            Ability ability = data.m_FinalAbility;
            if (ability.IsPassive)
            {
                if (ability.CanCast(this))
                    ability.Cast(this);
            }
            m_FinalAbility = ability;
        }
    }

    /// <summary>
    /// 执行回合方法
    /// </summary>
    public virtual void ExecutionRound()
    {
        //技能冷却
        if (m_FinalAbility != null && m_FinalAbility.CD > 0)
            m_FinalAbility.CD--;
        if (m_CommonAttack != null && m_CommonAttack.CD > 0)
            m_CommonAttack.CD--;

        foreach (BuffData buff in m_BuffList)
        {
            buff.UpdateStartBuff();
        }

        //如果活着则继续施放技能
        if (m_IsAlive)
        {
            //清空攻击列表
            m_EnemyList.Clear();

            //判断是否晕眩状态
            if (m_DicState[BuffData.m_State.Dizzy] == false)
            {
                //选择攻击方式（正常角色，大招>技能>普攻）
                //判断大招是否可施放
                if (m_FinalAbility != null && m_FinalAbility.IsPassive == false && m_FinalAbility.CanCast(this))
                    m_EnemyList = m_FinalAbility.Cast(this);
                //判断普攻是否可施放
                else if (m_CommonAttack != null && m_CommonAttack.IsPassive == false && m_CommonAttack.CanCast(this))
                    m_EnemyList = m_CommonAttack.Cast(this);
                else
                    m_EnemyList = BaseData.Instanse.m_DefaultAbility.Cast(this);

                //攻击前

                //攻击

                //攻击后
                foreach (BuffData buff in m_BuffList)
                {
                    buff.UpdateEndBuff();
                }
            }
            else
            {
                ExpeditionCrotroller.Instanse.state = ExpeditionState.Wait;
            }
        }
    }

    /// <summary>
    /// 受击方法
    /// </summary>
<<<<<<< HEAD
    public override void BeHit(BattleModel attacker, int damage, bool isAnimation)
=======
    /// <param name="attacker">攻击者</param>
    /// <param name="damage">伤害</param>
    /// <param name="isAnimator">是否播放动画</param>
    public override void BeHit(BattleModel attacker, int damage,bool isAnimator)
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
    {
        if (Shield > damage)
            Shield -= damage;
        else
        {
            damage -= Shield;
            Shield = 0;
            HP -= damage;
        }
        m_ModelHpSlider.value = (float)HP / TotalHP;
        m_ModelShieldSlider.value = (float)Shield / TotalHP;
        //m_ModelHpText.text = HP.ToString();

        if (HP <= 0)
        {
            EventCrotroller.Instance.Kill(attacker, this);
        }
        else
        {
<<<<<<< HEAD
            if (isAnimation)
=======
            if (isAnimator)
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
            {
                m_Animator.SetTrigger("BeHit");
                TimeCrotroller.Instance.AddTimerTask(0.1f, "Behit" + m_Id, delegate () { }, 1, delegate ()
                {
<<<<<<< HEAD
                    if (m_Animator != null)
                    {
                        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
                        {
                            m_Animator.ResetTrigger("BeHit");
                            m_Animator.SetTrigger("Idle");
                            ExpeditionCrotroller.Instanse.state = ExpeditionState.Wait;
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    return 0;
=======
                    if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                    {
                        m_Animator.SetTrigger("Idle");
                        ExpeditionCrotroller.Instanse.state = ExpeditionState.Wait;
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
                });
            }
        }
    }

    /// <summary>
    /// 死亡方法
    /// </summary>
    /// <param name="killer"></param>
    public override void Death(BattleModel killer)
    {
        m_Animator.ResetTrigger("Idle");
        m_Animator.SetTrigger("Death");
        TimeCrotroller.Instance.AddTimerTask(0.1f, "Death" + m_Id, delegate () { }, 1, delegate ()
        {
            //重置网格数据
            //m_Parent.modelData = null;
            //m_Parent.modelId = 0;
            //m_Parent.modelObj = null;

            //当动画播放到0.9f的时候死亡
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            {
                //减少所属队伍存活单位数量
                if (m_IsPlayer)
                    ExpeditionCrotroller.Instanse.m_DicLeftModelGrid.m_AliveModelCount--;
                else
                {
                    ExpeditionCrotroller.Instanse.m_DicRightModelGrid.m_AliveModelCount--;
                    ExpeditionCrotroller.Instanse.m_RoundExp += 100;
                    ExpeditionCrotroller.Instanse.m_RoundGold += 100;

                    //掉落物品
                    foreach(ItemDesc itemDesc in ExpeditionCrotroller.Instanse.m_ShowMap.m_DicItem.Keys)
                    {
                        float number = Random.Range(0f, 100f);
                        if (number <= ExpeditionCrotroller.Instanse.m_ShowMap.m_DicItem[itemDesc])
                        {
                            Item item = new Item(itemDesc);
                            BaseData.Instanse.m_MainCrotroller.m_Player.itemList.Add(item);
                            ExpeditionCrotroller.Instanse.m_RoundItemList.Add(item);
                        }
                    }
                    //掉落材料
                    foreach (MaterialDesc materialDesc in ExpeditionCrotroller.Instanse.m_ShowMap.m_DicMaterial.Keys)
                    {
                        float number = Random.Range(0f, 100f);
                        if (number <= ExpeditionCrotroller.Instanse.m_ShowMap.m_DicMaterial[materialDesc])
                        {
                            ExpeditionCrotroller.Instanse.AddDropMaterial(materialDesc, 1);
                        }
                    }
                }

                //将model设为死亡
                m_IsAlive = false;
                gameObject.SetActive(false);
                ExpeditionCrotroller.Instanse.state = ExpeditionState.Wait;
                //BaseData.Instanse.m_MainCrotroller.DestoryModel(m_Id, true);
                return 0;
            }
            else
            {
                return 1;
            }
        });
        ExpeditionCrotroller.Instanse.ModelDead(m_Parent);
    }

    /// <summary>
    /// 施法演出
    /// </summary>
    public void MoveToCenter()
    {
        m_TargetPos = MovePos.Center;
        //m_IsMoveing = true;
    }

    /// <summary>
    /// 施法结束
    /// </summary>
    public void MoveToParent()
    {
        m_TargetPos = MovePos.Parent;
        //m_IsMoveing = true;
    }

    /// <summary>
    /// 添加Buff
    /// </summary>
    /// <param name="buff"></param>
    public void AddBuff(BuffData buff)
    {
        buff.self = m_Id;
        buff.Caster = m_Id;
        bool isHave = false;

        //判断是否唯一，且是否已拥有
        if (buff.m_IsOnly)
        {
            foreach(BuffData buffC in m_BuffList)
            {
                if (buffC.name == buff.name)
                {
                    buffC.life = buff.life;
                    if (buffC.IsDead())
                        buffC.BuffInit();
                    buffC.m_IsDead = false;
                    isHave = true;
                }
            }
        }

        if (!isHave)
        {
            buff.BuffInit();
            if (!buff.IsHidden)
            {
                buff.m_ShowObj = new GameObject(buff.name);
                buff.m_ShowObj.AddComponent<RectTransform>();
                buff.m_ShowObj.transform.SetParent(m_BuffPanel.transform);
                buff.m_ShowObj.transform.localPosition = new Vector3(0, 0, 0);
                buff.m_ShowObj.transform.localScale = new Vector3(1, 1, 1);
                buff.m_ShowObj.AddComponent<SpriteRenderer>().sprite = buff.m_BuffIcon;
                buff.m_ShowObj.GetComponent<SpriteRenderer>().sortingLayerName = "Model";
            }

            m_BuffList.Add(buff);
        }
    }

    /// <summary>
    /// 添加他人施加的Buff
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="master"></param>
    public void AddBuff(BuffData buff, int master)
    {
        buff.self = m_Id;
        buff.Caster = master;
        bool isHave = false;

        //判断是否唯一，且是否已拥有
        if (buff.m_IsOnly)
        {
            foreach (BuffData buffC in m_BuffList)
            {
                if (buffC.name == buff.name)
                {
                    buffC.life = buff.life;
                    if (buffC.IsDead())
                        buffC.BuffInit();
                    buffC.m_IsDead = false;
                    isHave = true;
                }
            }
        }

        if (!isHave)
        {
            buff.BuffInit();
            if (!buff.IsHidden)
            {
                buff.m_ShowObj = new GameObject(buff.name);
                buff.m_ShowObj.AddComponent<RectTransform>();
                buff.m_ShowObj.transform.SetParent(m_BuffPanel.transform);
                buff.m_ShowObj.transform.localPosition = new Vector3(0, 0, 0);
                buff.m_ShowObj.transform.localScale = new Vector3(1, 1, 1);
                buff.m_ShowObj.AddComponent<SpriteRenderer>().sprite = buff.m_BuffIcon;
                buff.m_ShowObj.GetComponent<SpriteRenderer>().sortingLayerName = "Model";
            }

            m_BuffList.Add(buff);
        }
    }

    /// <summary>
    /// 获取Buff列表
    /// </summary>
    /// <returns></returns>
    public List<BuffData> GetBuffList()
    {
        return m_BuffList;
    }

    /// <summary>
    /// 是否存在指定Buff
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public BuffData HasBuff(string name)
    {
        foreach (BuffData buff in m_BuffList)
        {
            if (buff.name == name)
                return buff;
        }
        return null;
    }

    /// <summary>
    /// 更新Buff方法(一回合调用一次)
    /// </summary>
    public void UpdateMethod()
    {
        for (int i = 0; i < m_BuffList.Count; i++)
        {
            BuffData buff = m_BuffList[i];
            buff.UpdateStartBuff();
            if (buff.IsDead())
                m_BuffList.Remove(buff);
        }
    }
}
