using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCrotroller
{
    private static EventCrotroller m_Instance;

    public static EventCrotroller Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new EventCrotroller();
            }
            return m_Instance;
        }
    }

    /// <summary>
    /// 事件模板
    /// </summary>
    /// <param name="eventBase"></param>
    public delegate void eventHandler(EventBase eventBase);

    /// <summary>
    /// 出生事件(包括复活，重生，召唤)
    /// </summary>
    public event eventHandler OnCreated;
    public void Created(BattleModel caster,BattleModel target)
    {
        CreatedEvent e = new CreatedEvent();
        e.m_Caster = caster;
        e.m_Target = target;

        //添加光环
        if (target.m_IsPlayer)
        {
            foreach (BuffData halo in ExpeditionCrotroller.Instanse.m_DicLeftModelGrid.m_HaloList)
            {
                target.AddBuff(halo);
            }
        }
        else
        {
            foreach (BuffData halo in ExpeditionCrotroller.Instanse.m_DicRightModelGrid.m_HaloList)
            {
                target.AddBuff(halo);
            }
        }

        OnCreated?.Invoke(e);
    }

    /// <summary>
    /// 查看事件
    /// </summary>
    public event eventHandler OnGetHero;

    /// <summary>
    /// 获取英雄事件
    /// </summary>
    public void GetHeroed(ModelData hero)
    {
        OnGetHeroEvent e = new OnGetHeroEvent();
        e.m_Hero = hero;
        OnGetHero?.Invoke(e);
    }

    /// <summary>
    /// 伤害事件
    /// </summary>
<<<<<<< HEAD
    public event eventHandler OnDamaged;
    public void Damaged(BattleModel attacker, BattleModel target, float Fdamage)
=======
    public event eventHandler OnDamage;
    public void Damaged(BattleModel attacker, BattleModel target, int damage,bool isAnimator)
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
    {
        DamageEvent e = new DamageEvent();
        e.m_Attacker = attacker;
        e.m_Target = target;

        if (target != null && target.m_IsAlive)
        {
            //防御计算
            Fdamage -= Fdamage * (target.TotalDefense / (target.TotalDefense + 1000f));

            int damage = (int)Fdamage;
            //暴击判断
            if (attacker.m_Crit >= UnityEngine.Random.Range(0, 100))
            {
                float critDamage = damage * attacker.m_CritDamage * 0.01f;
                damage = (int)critDamage;
                BaseData.Instanse.m_CameraCrotroller.SendMessage("-" + damage, 2f, target.transform, Color.red);
            }
            else
            {
                BaseData.Instanse.m_CameraCrotroller.SendMessage("-" + damage, 2f, target.transform);
            }
            target.BeHit(attacker, damage, false);

            OnDamaged?.Invoke(e);
        }
    }

    /// <summary>
    /// 攻击事件
    /// </summary>
    public event eventHandler OnAttacked;
    public void Attacked(BattleModel attacker, BattleModel target, float Fdamage, bool isAnimation)
    {
        DamageEvent e = new DamageEvent();
        e.m_Attacker = attacker;
        e.m_Target = target;

        if (target != null && target.m_IsAlive)
        {
            //攻击前事件
            foreach(BuffData buff in attacker.m_BuffList)
            {
                buff.OnAttackBuff(target);
            }
            //受攻击前事件
            foreach (BuffData buff in target.m_BuffList)
            {
                buff.OnBeHitBuff(attacker);
            }

            //防御计算
            Fdamage -= Fdamage * (target.TotalDefense / (target.TotalDefense + 1000f));

            int damage = (int)Fdamage;
            //暴击判断
            if(attacker.m_Crit >= UnityEngine.Random.Range(0, 100))
            {
                float critDamage = damage * attacker.m_CritDamage * 0.01f;
                damage = (int)critDamage;
                BaseData.Instanse.m_CameraCrotroller.SendMessage("-" + damage, 2f, target.transform, Color.red);
            }
            else
            {
                BaseData.Instanse.m_CameraCrotroller.SendMessage("-" + damage, 2f, target.transform);
            }

            //攻击后事件
            foreach (BuffData buff in attacker.m_BuffList)
            {
                buff.OnAttackedBuff(target);
            }
            //受攻击后事件
            foreach (BuffData buff in target.m_BuffList)
            {
                buff.OnBeHitedBuff(attacker);
            }
            target.BeHit(attacker, damage, isAnimation);
            OnAttacked?.Invoke(e);
        }
    }

    /// <summary>
    /// 治疗事件
    /// </summary>
    public event eventHandler OnHealth;
    public void Healthed(BattleModel attacker, BattleModel target, float Fhealth, bool isAnimation)
    {
        HealthEvent e = new HealthEvent();
        e.m_Attacker = attacker;
        e.m_Target = target;

        Fhealth += Fhealth * attacker.Health * 0.01f;
        e.m_Health = Fhealth;
        int health = (int)Fhealth;
        if (target != null && target.m_IsAlive)
        {
<<<<<<< HEAD
            BaseData.Instanse.m_CameraCrotroller.SendMessage("+" + health, 2f, target.transform, Color.green);
            target.HP += health;
            if (target.HP > target.TotalHP)
            {
                e.m_OverHealth = target.HP - target.TotalHP;
                target.HP = target.TotalHP;
            }
=======
            target.BeHit(attacker, damage, isAnimator);
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915

            OnHealth?.Invoke(e);
        }
    }

    /// <summary>
    /// 击杀事件
    /// </summary>
    public event eventHandler OnKill;
    public void Kill(BattleModel killer, BattleModel target)
    {
        KillEvent e = new KillEvent();
        e.m_Killer = killer;
        e.m_Target = target;

        OnKill?.Invoke(e);

        target.Death(killer);
    }

}

/// <summary>
/// 总事件类
/// </summary>
public class EventBase
{
    protected EventType m_EventType;

    protected EventBase()
    {

    }

    public EventBase(EventType type)
    {
        m_EventType = type;
    }
}

/// <summary>
/// 出生事件类
/// </summary>
public class CreatedEvent : EventBase
{
    public BattleModel m_Caster;
    public BattleModel m_Target;

    public CreatedEvent()
    {
        m_EventType = EventType.Created;
    }
}

/// <summary>
/// 查看事件类
/// </summary>
public class OnGetHeroEvent : EventBase
{
    public ModelData m_Hero;

    public OnGetHeroEvent()
    {
        m_EventType = EventType.GetHero;
    }
}

/// <summary>
/// 伤害事件类
/// </summary>
public class DamageEvent : EventBase
{
    public BattleModel m_Attacker;
    public BattleModel m_Target;
    public float m_Damage;

    public DamageEvent()
    {
        m_EventType = EventType.Damage;
    }
}

/// <summary>
/// 治疗事件类
/// </summary>
public class HealthEvent : EventBase
{
    public BattleModel m_Attacker;
    public BattleModel m_Target;
    public float m_Health;
    public float m_OverHealth;

    public HealthEvent()
    {
        m_EventType = EventType.Headth;
    }
}

/// <summary>
/// 攻击前事件类
/// </summary>
public class OnAttackEvent : EventBase
{
    public BattleModel m_Attacker;
    public BattleModel m_Target;

    public OnAttackEvent()
    {
        m_EventType = EventType.OnAttack;
    }
}

/// <summary>
/// 攻击成功事件类
/// </summary>
public class AttackedEvent : EventBase
{
    public BattleModel m_Attacker;
    public List<BattleModel> m_TargetList;

    public AttackedEvent()
    {
        m_EventType = EventType.Attacked;
    }
}

/// <summary>
/// 击杀事件
/// </summary>
public class KillEvent : EventBase
{
    public BattleModel m_Killer;
    public BattleModel m_Target;

    public KillEvent()
    {
        m_EventType = EventType.Kill;
    }
}

/// <summary>
/// 升级事件
/// </summary>
public class UpgradeEvent : EventBase
{
    public BaseModel m_Target;

    public UpgradeEvent()
    {
        m_EventType = EventType.Upgrade;
    }
}

/// <summary>
/// 事件类型
/// </summary>
public enum EventType
{
    Created,
    GetHero,
    Choosed,
    Damage,
    Headth,
    OnAttack,
    Attacked,
    Kill,
    Upgrade,
}
