using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffData
{

    //名字
    private string m_Name;
    public string name
    {
        get { return m_Name; }
    }

    /// <summary>
    /// Buff图标
    /// </summary>
    public Sprite m_BuffIcon;

    private BuffDesc.BuffAttribute m_Attribute;
    private float m_Value;


    //剩余时间（0以下为永久）
    private float m_Life;
    public float life
    {
        get { return m_Life; }
        set { m_Life = value; }
    }

    //是否隐藏
    public bool IsHidden;

    //唯一词缀(当多个buff持有相同名字的该词缀时，以最高值为准)
    //key词缀名字
    //value词缀数值
    public Dictionary<string,float> m_DicOnlyAttribute;
    public bool HasOnlyAttribute(string key)
    {
        if (m_DicOnlyAttribute.ContainsKey(key))
            return true;
        else
            return false;
    }
    //唯一词缀数值
    //public int m_OnlyPower;

    //图标物体(如果隐藏则为null）
    public GameObject m_ShowObj;

    //层数
    private int m_Count;
    public int count
    {
        get { return m_Count; }
        set { m_Count = value; }
    }

    //是否唯一
    public bool m_IsOnly;
    //是否死亡
    public bool m_IsDead;
    //持有者
    private int m_Self;
    public int self
    {
        get { return m_Self; }
        set { m_Self = value; }
    }
    //施加者
    private int m_Caster;
    public int Caster
    {
        get { return m_Caster; }
        set { m_Caster = value; }
    }

    public delegate void Init();
    public event Init InitDelegate;
    /// <summary>
    /// 立即生成方法
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public void BuffInit()
    {
        if (InitDelegate != null)
            InitDelegate.Invoke();
    }

    public delegate void Destory();
    public event Destory DestoryDelegate;
    /// <summary>
    /// 销毁方法方法
    /// </summary>
    /// <returns></returns>
    public void BuffDestory()
    {
        if (DestoryDelegate != null)
            DestoryDelegate.Invoke();
    }

    public delegate float Attack(float value);
    public event Attack AttackDelegate;
    /// <summary>
    /// 计算基础攻击力
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float GetAttack(float value)
    {
        if (AttackDelegate != null)
            value = AttackDelegate.Invoke(value);
        return value;
    }

    public delegate float ExtraAttack(float value);
    public event ExtraAttack ExtraAttackDelegate;
    /// <summary>
    /// 计算额外攻击力
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float GetExtraAttack(float value)
    {
        if (ExtraAttackDelegate != null)
            value = ExtraAttackDelegate.Invoke(value);
        return value;
    }

    public delegate float MaxHealth(float value);
    public event MaxHealth MaxHealthDelegate;
    /// <summary>
    /// 计算最大生命值
    /// </summary>
    /// <param name="speed"></param>
    /// <returns></returns>
    public float GetMaxHealth(float value)
    {
        if (MaxHealthDelegate != null)
            value = MaxHealthDelegate.Invoke(value);
        return value;
    }

    public BuffData()
    {

    }

    public BuffData(string iconName)
    {
        m_Name = iconName;
        if (BaseData.Instanse.m_DicBuffSprite.ContainsKey(iconName))
            m_BuffIcon = BaseData.Instanse.m_DicBuffSprite[iconName];
        else if (BaseData.Instanse.m_DicAbilitySprite.ContainsKey(iconName))
            m_BuffIcon = BaseData.Instanse.m_DicAbilitySprite[iconName];
        else
            m_BuffIcon = null;
    }

    public BuffData(string iconName,string onlyName,string onlyPower)
    {
        m_Name = iconName;
        if (BaseData.Instanse.m_DicBuffSprite.ContainsKey(iconName))
            m_BuffIcon = BaseData.Instanse.m_DicBuffSprite[iconName];
        else if (BaseData.Instanse.m_DicAbilitySprite.ContainsKey(iconName))
            m_BuffIcon = BaseData.Instanse.m_DicAbilitySprite[iconName];
        else
            m_BuffIcon = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iconName">名字</param>
    /// <param name="life">存活时间(-1为永久)</param>
    /// <param name="isHidden">是否隐藏</param>
    /// <param name="isOnly">是否唯一</param>
    public BuffData(string iconName, int life, bool isHidden, bool isOnly)
    {
        m_Name = iconName;
        if (BaseData.Instanse.m_DicBuffSprite.ContainsKey(iconName))
            m_BuffIcon = BaseData.Instanse.m_DicBuffSprite[iconName];
        else if (BaseData.Instanse.m_DicAbilitySprite.ContainsKey(iconName))
            m_BuffIcon = BaseData.Instanse.m_DicAbilitySprite[iconName];
        else
            m_BuffIcon = null;

        m_Life = life;
        IsHidden = isHidden;
        m_IsOnly = isOnly;
    }

    public delegate void OnAttack(BattleModel target);
    public event OnAttack OnAttackDelegate;
    /// <summary>
    /// 攻击前方法
    /// </summary>
    public void OnAttackBuff(BattleModel target)
    {
        if (OnAttackDelegate != null)
            OnAttackDelegate.Invoke(target);
    }

    public delegate void OnAttacked(BattleModel target);
    public event OnAttacked OnAttackedDelegate;
    /// <summary>
    /// 攻击后方法
    /// </summary>
    public void OnAttackedBuff(BattleModel target)
    {
        if (OnAttackedDelegate != null)
            OnAttackedDelegate.Invoke(target);
    }

    public delegate void OnBeHit(BattleModel target);
    public event OnBeHit OnBeHitDelegate;
    /// <summary>
    /// 受攻击前方法
    /// </summary>
    public void OnBeHitBuff(BattleModel target)
    {
        if (OnBeHitDelegate != null)
            OnBeHitDelegate.Invoke(target);
    }

    public delegate void OnBeHited(BattleModel target);
    public event OnBeHit OnBeHitedDelegate;
    /// <summary>
    /// 受攻击后方法
    /// </summary>
    public void OnBeHitedBuff(BattleModel target)
    {
        if (OnBeHitedDelegate != null)
            OnBeHitedDelegate.Invoke(target);
    }

    public delegate void RoundStart();
    public event RoundStart RoundStartDelegate;
    /// <summary>
    /// 回合执行方法
    /// </summary>
    public void UpdateStartBuff()
    {
        if (RoundStartDelegate != null)
            RoundStartDelegate.Invoke();
    }

    public delegate void RoundEnd();
    public event RoundEnd RoundEndDelegate;
    /// <summary>
    /// 回合结束方法
    /// </summary>
    public void UpdateEndBuff()
    {
        if (m_Life > 0)
        {
            if (RoundEndDelegate != null)
                RoundEndDelegate.Invoke();
            m_Life--;
            if (m_Life <= 0)
            {
                BuffDestory();
                m_IsDead = true;
            }
        }
    }

    /// <summary>
    /// 简易Buff功能添加
    /// 眩晕:Dizzy
    /// 流血:Blood
    /// 燃烧:Fire
    /// 中毒:Poison
    /// 流血基础伤害为添加者的30％
    /// 燃烧基础伤害为添加者的25％
    /// </summary>
    /// <param name="buffName"></param>
    public void SimpleBuff(string buffName, float[] pars)
    {
        switch (buffName)
        {
            case "Dizzy":
                m_BuffIcon = BaseData.Instanse.m_DicBuffSprite["Dizzy"];
                InitDelegate += delegate ()
                 {
                     BaseData.Instanse.GetModelById<BattleModel>(m_Self).m_DicState[m_State.Dizzy] = true;
                 };
                DestoryDelegate += delegate ()
                {
                    BaseData.Instanse.GetModelById<BattleModel>(m_Self).m_DicState[m_State.Dizzy] = false;
                };
                break;
            case "Blood":
                m_BuffIcon = BaseData.Instanse.m_DicBuffSprite["Blood"];
                RoundStartDelegate += delegate ()
                {
                    BattleModel caster = BaseData.Instanse.GetModelById<BattleModel>(m_Caster);
                    BattleModel self = BaseData.Instanse.GetModelById<BattleModel>(m_Self);
                    EventCrotroller.Instance.Damaged(caster, self, (int)(caster.TotalAttack * 0.3f));
                };
                break;
            case "Fire":
                m_BuffIcon = BaseData.Instanse.m_DicBuffSprite["Fire"];
                RoundStartDelegate += delegate ()
                {
                    BattleModel caster = BaseData.Instanse.GetModelById<BattleModel>(m_Caster);
                    BattleModel self = BaseData.Instanse.GetModelById<BattleModel>(m_Self);
                    EventCrotroller.Instance.Damaged(caster, self, (int)(caster.TotalAttack * 0.3f));
                };
                break;
            case "Poison":
                m_BuffIcon = BaseData.Instanse.m_DicBuffSprite["Poison"];
                RoundStartDelegate += delegate ()
                {
                    BattleModel caster = BaseData.Instanse.GetModelById<BattleModel>(m_Caster);
                    BattleModel self = BaseData.Instanse.GetModelById<BattleModel>(m_Self);
                    EventCrotroller.Instance.Damaged(caster, self, (int)(caster.TotalAttack * 0.3f));
                };
                break;
            case "AtkUp":
                m_DicOnlyAttribute.Add("AtkUp", pars[0]);
                m_BuffIcon = BaseData.Instanse.m_DicBuffSprite["AtkUp"];
                InitDelegate += delegate ()
                {
                    BaseData.Instanse.GetModelById<BattleModel>(m_Self).PAttack += (int)(pars[0] * 100);
                };
                DestoryDelegate += delegate ()
                {
                    BaseData.Instanse.GetModelById<BattleModel>(m_Self).PAttack -= (int)(pars[0] * 100);
                };
                break;
            case "DefDown":
                m_DicOnlyAttribute.Add("DefDown", pars[0]);
                m_BuffIcon = BaseData.Instanse.m_DicBuffSprite["DefDown"];
                InitDelegate += delegate ()
                {
                    BaseData.Instanse.GetModelById<BattleModel>(m_Self).PDefense -= (int)(pars[0] * 100);
                };
                DestoryDelegate += delegate ()
                {
                    BaseData.Instanse.GetModelById<BattleModel>(m_Self).PDefense += (int)(pars[0] * 100);
                };
                break;
            case "SpdDown":
                m_DicOnlyAttribute.Add("SpdDown", pars[0]);
                m_BuffIcon = BaseData.Instanse.m_DicBuffSprite["SpdDown"];
                InitDelegate += delegate ()
                {
                    BaseData.Instanse.GetModelById<BattleModel>(m_Self).PSpeed -= (int)(pars[0] * 100);
                };
                DestoryDelegate += delegate ()
                {
                    BaseData.Instanse.GetModelById<BattleModel>(m_Self).PSpeed += (int)(pars[0] * 100);
                };
                break;
        }
    }

    public bool IsDead()
    {
        return m_IsDead;
    }

    public void Clone(BuffData buff)
    {
        m_Count = buff.count;
        m_Value = buff.m_Value;
        m_Life = buff.life;
        m_IsDead = buff.m_IsDead;
    }

    public enum m_State
    {
        Dizzy,
    }
}
