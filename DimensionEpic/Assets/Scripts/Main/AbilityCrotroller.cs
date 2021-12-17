using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCrotroller
{
    /// <summary>
    /// 示例
    /// </summary>
    //public static List<BattleModel> Hit_Slime(BattleModel attacker, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    //{
    //    List<BattleModel> list = new List<BattleModel>();
    //    List<ModelGrid> gridList = enemyTeam.GetGrid(6, null);
    //    AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Hit_Slime");
    //    float power = desc.m_AttributeList[0];
    //    float damage = attacker.TotalAttack * power;
    //    Animator animator = attacker.m_Animator;

    //    SimpleAttack(damage, gridList, desc, attacker, animator);

    //    return list;
    //}

    /// <summary>
    /// 打击
    /// </summary>
    public static List<BattleModel> Hit(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(6, null);
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Hit");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        Animator animator = self.m_Animator;

        SimpleAttack(damage, gridList,self, animator);
        return list;
    }

    /// <summary>
    /// 挥砍
    /// </summary>
    public static List<BattleModel> Slash(BattleModel self, int level,ModelGridCollection team,ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(1, null);
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Slash");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        Animator animator = self.m_Animator;

        SimpleAttack(damage, gridList,self, animator);
        return list;
    }

    /// <summary>
    /// 射箭
    /// </summary>
    public static List<BattleModel> Arrow(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(6, null);
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Arrow");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        Animator animator = self.m_Animator;

        SimpleAttack(damage, gridList,self, animator);
        return list;
    }

    /// <summary>
    /// 火球
    /// </summary>
    public static List<BattleModel> FireBall(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(6, null);
        Animator animator = self.m_Animator;
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("FireBall");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        float number = desc.m_AttributeList[1];

        BuffData buff = new BuffData("Fire");
        buff.SimpleBuff("Fire", null);
        buff.life = desc.m_AttributeList[2];

        SimpleAttack(damage, gridList,self, animator, buff, number);

        return list;
    }

    /// <summary>
    /// 酸液
    /// </summary>
    public static List<BattleModel> AcidSolution(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(8, null);
        Animator animator = self.m_Animator;
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("AcidSolution");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        float number = 1;

        BuffData buff = new BuffData("AcidSolution", (int)desc.m_AttributeList[2], false, true);
        buff.SimpleBuff("DefDown", new float[] { desc.m_AttributeList[1] });

        SimpleAttack(damage, gridList, self, animator, buff, number);

        return list;
    }

    /// <summary>
    /// 缠绕
    /// </summary>
    public static List<BattleModel> Twining(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(6, null);
        Animator animator = self.m_Animator;
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Twining");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;

        BuffData buff = new BuffData("Twining", (int)desc.m_AttributeList[1], false, true);
        buff.SimpleBuff("Dizzy", null);

        SimpleAttack(damage, gridList, self, animator, buff, 1);

        return list;
    }

    /// <summary>
    /// 重击
    /// </summary>
    public static List<BattleModel> Bash(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        //attacker.MoveToCenter();
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(6, null);
        Animator animator = self.m_Animator;
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Bash");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        float number = desc.m_AttributeList[1];

        BuffData buff = new BuffData("Bash");
        buff.SimpleBuff("Dizzy", null);
        buff.life = desc.m_AttributeList[2];

        SimpleAttack(damage, gridList,self, animator, buff, number);
        return list;
    }

    /// <summary>
    /// 刺击
    /// </summary>
    public static List<BattleModel> Thorn(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        //attacker.MoveToCenter();
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(6, null);
        Animator animator = self.m_Animator;
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Thorn");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        float number = desc.m_AttributeList[1];

        BuffData buff = new BuffData("Thorn");
        buff.SimpleBuff("Blood", null);
        buff.life = desc.m_AttributeList[2];

        SimpleAttack(damage, gridList,self, animator, buff, number);
        return list;
    }

    /// <summary>
    /// 治疗
    /// </summary>
    public static List<BattleModel> Treat(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        //attacker.MoveToCenter();
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = team.GetGrid(11, new int[] { 1 });
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Treat");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        Animator animator = self.m_Animator;

        SimpleHealth(damage, gridList,self, animator);
        return list;
    }

    /// <summary>
    /// 治疗(精灵)
    /// </summary>
    public static List<BattleModel> Treat_Elf(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        //attacker.MoveToCenter();
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = team.GetGrid(11, new int[] { 1 });
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Treat_Elf");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        Animator animator = self.m_Animator;

        SimpleHealth(damage, gridList, self, animator);
        return list;
    }

    /// <summary>
    /// 撕咬
    /// </summary>
    public static List<BattleModel> Bite(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(6, null);
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Bite");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        Animator animator = self.m_Animator;

        SimpleAttack(damage, gridList, self, animator);

        return list;
    }

    /// <summary>
    /// 史莱姆撞击
    /// </summary>
    public static List<BattleModel> Hit_Slime(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(6, null);
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Hit_Slime");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        Animator animator = self.m_Animator;

        SimpleAttack(damage, gridList, self, animator);

        return list;
    }

    /// <summary>
    /// 木刺
    /// </summary>
    public static List<BattleModel> WoodThorn(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(5, new int[] { self.m_Parent.gridIndex });
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("WoodThorn");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        Animator animator = self.m_Animator;

        SimpleAttack(damage, gridList, self, animator);

        return list;
    }

    /// <summary>
    /// 飞弹
    /// </summary>
    public static List<BattleModel> Missile(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(6, null);
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Missile");
        float power = desc.m_AttributeList[0];
        float damage = self.TotalAttack * power;
        Animator animator = self.m_Animator;

        SimpleAttack(damage, gridList, self, animator);

        return list;
    }
    
    /// <summary>
    /// 血刃
    /// </summary>
    public static List<BattleModel> BloodDagger(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("BloodDagger");
        float number = desc.m_AttributeList[0];
        BuffData buff = new BuffData("BloodDagger_Debuff");
        buff.SimpleBuff("Blood", null);
        buff.life = desc.m_AttributeList[1];

        BuffData bloodDagger = new BuffData("BloodDagger", -1, true, true);
        bloodDagger.OnAttackedDelegate += delegate (BattleModel target)
        {
            if (target.m_IsAlive)
            {
                float random = Random.Range(0, 1f);
                if (random <= number)
                {
                    target.AddBuff(buff);
                }
            }
        };

        self.AddBuff(bloodDagger);

        return list;
    }

    /// <summary>
    /// 火刃
    /// </summary>
    public static List<BattleModel> FireDagger(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("FireDagger");
        float number = desc.m_AttributeList[0];
        BuffData buff = new BuffData("FireDagger_Debuff");
        buff.SimpleBuff("Fire", null);
        buff.life = desc.m_AttributeList[1];

        BuffData FireDagger = new BuffData("FireDagger", -1, true, true);
        FireDagger.OnAttackedDelegate += delegate (BattleModel target)
        {
            if (target.m_IsAlive)
            {
                float random = Random.Range(0, 1f);
                if (random <= number)
                {
                    target.AddBuff(buff);
                }
            }
        };
        FireDagger.life = -1;

        self.AddBuff(FireDagger);

        return list;
    }

    /// <summary>
    /// 毒刃
    /// </summary>
    public static List<BattleModel> PoisonDagger(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("PoisonDagger");
        float number = desc.m_AttributeList[0];
        BuffData buff = new BuffData("PoisonDagger_Debuff");
        buff.SimpleBuff("Poison", null);
        buff.life = desc.m_AttributeList[1];

        BuffData PoisonDagger = new BuffData("PoisonDagger", -1, true, true);
        PoisonDagger.OnAttackedDelegate += delegate (BattleModel target)
        {
            if (target.m_IsAlive)
            {
                float random = Random.Range(0, 1f);
                if (random <= number)
                {
                    target.AddBuff(buff);
                }
            }
        };
        PoisonDagger.life = -1;

        self.AddBuff(PoisonDagger);

        return list;
    }

    /// <summary>
    /// 利爪
    /// </summary>
    public static List<BattleModel> SharpClaw(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("SharpClaw");
        float number = desc.m_AttributeList[0];
        BuffData buff = new BuffData("SharpClaw");
        buff.InitDelegate += delegate ()
        {
            BaseData.Instanse.GetModelById<BattleModel>(buff.self).m_Crit += (int)(desc.m_AttributeList[0] * 100);
        };
        buff.life = -1;

        self.AddBuff(buff);

        return list;
    }

    /// <summary>
    /// 史莱姆粘液
    /// </summary>
    public static List<BattleModel> SlimeMucus(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("SlimeMucus");
        float slowNumber = desc.m_AttributeList[0];
        BuffData slimeMucus_Debuff = new BuffData("SlimeMucus", (int)desc.m_AttributeList[1], false, true);
        slimeMucus_Debuff.SimpleBuff("SpdDown", new float[] { slowNumber });

        BuffData slimeMucus = new BuffData("SlimeMucus", -1, false, true);
        slimeMucus.OnBeHitDelegate += delegate (BattleModel attacker)
        {
            attacker.AddBuff(slimeMucus_Debuff, self.m_Id);
        };

        self.AddBuff(slimeMucus);

        return list;
    }

    /// <summary>
    /// 尖刺树甲
    /// </summary>
    public static List<BattleModel> SpikedArmor(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("SpikedArmor");

        BuffData slimeMucus = new BuffData("SpikedArmor", -1, false, true);
        slimeMucus.OnBeHitDelegate += delegate (BattleModel attacker)
        {
            EventCrotroller.Instance.Damaged(self, attacker, (int)(desc.m_AttributeList[0] * self.TotalDefense));
        };

        self.AddBuff(slimeMucus);

        return list;
    }

    /// <summary>
    /// 治愈树甲
    /// </summary>
    public static List<BattleModel> HealthArmor(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("HealthArmor");

        BuffData healthArmor = new BuffData("HealthArmor", -1, false, true);
        healthArmor.RoundStartDelegate += delegate ()
        {
            EventCrotroller.Instance.Healthed(self, self, (int)(desc.m_AttributeList[0] * self.TotalHP), false);
        };

        self.AddBuff(healthArmor);

        return list;
    }

    /// <summary>
    /// 生命之力
    /// </summary>
    public static List<BattleModel> LifePower(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("LifePower");

        BuffData LifePower = new BuffData("HealthArmor", -1, false, true);
        LifePower.RoundStartDelegate += delegate ()
        {
            List<ModelGrid> gridList = team.GetGrid(8, null);
            foreach(ModelGrid grid in gridList)
            {
                if (grid.IsAlive())
                {
                    BattleModel target = BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId);
                    EventCrotroller.Instance.Healthed(self, target, desc.m_AttributeList[0] * self.TotalHP, false);
                }
            }
        };

        self.AddBuff(LifePower);

        return list;
    }

    /// <summary>
    /// 统帅之力
    /// </summary>
    public static List<BattleModel> CommanderPower(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("CommanderPower");
        BuffData CommanderPowerHalo = new BuffData("CommanderPower", -1, false, true);
        CommanderPowerHalo.SimpleBuff("AtkUp", new float[] { desc.m_AttributeList[0] });

        BuffData CommanderPower = new BuffData("CommanderPower", -1, true, true);
        CommanderPower.InitDelegate += delegate ()
        {
            team.m_HaloList.Add(CommanderPowerHalo);
        };
        CommanderPower.DestoryDelegate += delegate ()
        {
            team.m_HaloList.Remove(CommanderPowerHalo);
        };

        self.AddBuff(CommanderPower);

        return list;
    }

    /// <summary>
    /// 狼魂
    /// </summary>
    public static List<BattleModel> WolfSoul(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("WolfSoul");

        BuffData WolfSoul = new BuffData("WolfSoul", -1, true, true);
        WolfSoul.InitDelegate += delegate ()
        {
            BattleModel model = BaseData.Instanse.GetModelById<BattleModel>(WolfSoul.self);
            model.m_Crit += (int)(desc.m_AttributeList[0] * 100);
        };
        WolfSoul.DestoryDelegate += delegate ()
        {
            BattleModel model = BaseData.Instanse.GetModelById<BattleModel>(WolfSoul.self);
            model.m_Crit -= (int)(desc.m_AttributeList[0] * 100);
        };

        self.AddBuff(WolfSoul);

        return list;
    }

    /// <summary>
    /// 精灵祝福
    /// </summary>
    public static List<BattleModel> SpiritBuff(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("SpiritBuff");

        BuffData SpiritBuff = new BuffData("SpiritBuff", -1, true, true);
        SpiritBuff.InitDelegate += delegate ()
        {
            BattleModel model = BaseData.Instanse.GetModelById<BattleModel>(SpiritBuff.self);
            model.Health += (int)(desc.m_AttributeList[0] * 100);
            EventCrotroller.Instance.OnHealth += delegate (EventBase e)
            {
                HealthEvent healthE = (HealthEvent)e;
                healthE.m_Target.Shield += (int)healthE.m_OverHealth;
            };
        };
        SpiritBuff.DestoryDelegate += delegate ()
        {
            BattleModel model = BaseData.Instanse.GetModelById<BattleModel>(SpiritBuff.self);
            model.Health -= (int)(desc.m_AttributeList[0] * 100);
        };

        self.AddBuff(SpiritBuff);

        return list;
    }

    /// <summary>
    /// 召唤史莱姆
    /// </summary>
    public static List<BattleModel> CallSlime(BattleModel self, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        List<BattleModel> list = new List<BattleModel>();
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("CallSlime");
        EnemyDesc enemyDesc;

        if (ExpeditionCrotroller.Instanse.m_ChooseMap.m_Level >= 40)
        {
            enemyDesc = DataManager.Instanse.m_EnemyDescContainer.GetDescByID(10115);
        }
        else if (ExpeditionCrotroller.Instanse.m_ChooseMap.m_Level >= 30)
        {
            enemyDesc = DataManager.Instanse.m_EnemyDescContainer.GetDescByID(10114);
        }
        else if (ExpeditionCrotroller.Instanse.m_ChooseMap.m_Level >= 20)
        {
            enemyDesc = DataManager.Instanse.m_EnemyDescContainer.GetDescByID(10113);
        }
        else if (ExpeditionCrotroller.Instanse.m_ChooseMap.m_Level >= 10)
        {
            enemyDesc = DataManager.Instanse.m_EnemyDescContainer.GetDescByID(10112);
        }
        else
        {
            enemyDesc = DataManager.Instanse.m_EnemyDescContainer.GetDescByID(10111);
        }

        ExpeditionCrotroller.Instanse.CreateEnemy(team, enemyDesc);
        ExpeditionCrotroller.Instanse.state = ExpeditionState.Wait;

        return list;
    }

    //--------------------------------------------------------------------------------------------------------------------
    //简易方法
    //--------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 带动画的攻击
    /// </summary>
    public static void SimpleAttack(float damage, List<ModelGrid> gridList, BattleModel self, Animator animator)
    {
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Attack");
        TimeCrotroller.Instance.AddTimerTask(0.1f, "Attack" + self.m_Id, delegate () { }, 1, delegate ()
        {
            if (self != null)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    foreach (ModelGrid grid in gridList)
                    {
<<<<<<< HEAD
                        BattleModel target = BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId);
                        EventCrotroller.Instance.Attacked(self, target, damage, true);
                        target.m_Audio.Play(BaseData.Instanse.m_DicAudio["Attack_Common"]);
=======
                        BattleModel target = BaseData.Instanse.GetModelById<BattleModel>(grid.modelId);
                        EventCrotroller.Instance.Damaged(attacker, target, (int)(attacker.Attack * 1), true);
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
                    }
                    animator.ResetTrigger("Attack");
                    animator.SetTrigger("Idle");
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        });
    }

    /// <summary>
    /// 带动画的攻击，并有几率添加Buff
    /// </summary>
    public static void SimpleAttack(float damage, List<ModelGrid> gridList, BattleModel self, Animator animator, BuffData buff, float number)
    {
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Attack");
        TimeCrotroller.Instance.AddTimerTask(0.1f, "Attack" + self.m_Id, delegate () { }, 1, delegate ()
        {
            if (self != null)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    foreach (ModelGrid grid in gridList)
                    {
<<<<<<< HEAD
                        BattleModel target = BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId);
                        EventCrotroller.Instance.Attacked(self, target, damage, true);
                        target.m_Audio.Play(BaseData.Instanse.m_DicAudio["Attack_Common"]);
                        float random = Random.Range(0, 1f);
                        if (random <= number)
                        {
                            target.AddBuff(buff);
                        }
=======
                        BattleModel target = BaseData.Instanse.GetModelById<BattleModel>(grid.modelId);
                        EventCrotroller.Instance.Damaged(attacker, target, (int)(attacker.Attack * 0.8),true);
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
                    }
                    animator.ResetTrigger("Attack");
                    animator.SetTrigger("Idle");
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        });
    }

    /// <summary>
    /// 不带动画的攻击，并有几率添加Buff
    /// </summary>
    public static void SimpleAttack(float damage, List<ModelGrid> gridList, BattleModel self, BuffData buff, float number)
    {
        foreach (ModelGrid grid in gridList)
        {
            BattleModel target = BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId);
            EventCrotroller.Instance.Attacked(self, target, damage, true);
            target.m_Audio.Play(BaseData.Instanse.m_DicAudio["Attack_Common"]);
            float random = Random.Range(0, 1f);
            if (random <= number)
            {
                target.AddBuff(buff);
            }
        }
    }

    /// <summary>
    /// 不带动画的攻击
    /// </summary>
    public static void SimpleAttack(float damage, List<ModelGrid> gridList, BattleModel self)
    {
        foreach (ModelGrid grid in gridList)
        {
            BattleModel target = BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId);
            EventCrotroller.Instance.Attacked(self, target, damage, true);
            target.m_Audio.Play(BaseData.Instanse.m_DicAudio["Attack_Common"]);
        }
    }

    /// <summary>
    /// 带动画的治疗
    /// </summary>
    public static void SimpleHealth(float damage, List<ModelGrid> gridList, BattleModel self, Animator animator)
    {
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Attack");
        TimeCrotroller.Instance.AddTimerTask(0.1f, "Attack" + self.m_Id, delegate () { }, 1, delegate ()
        {
            if (self != null)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    foreach (ModelGrid grid in gridList)
                    {
<<<<<<< HEAD
                        BattleModel target = BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId);
                        EventCrotroller.Instance.Healthed(self, target, damage, true);
=======
                        BattleModel target = BaseData.Instanse.GetModelById<BattleModel>(grid.modelId);
                        EventCrotroller.Instance.Damaged(attacker, target, (int)(attacker.Attack * 1.5),true);
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
                    }
                    animator.ResetTrigger("Attack");
                    animator.SetTrigger("Idle");
                    ExpeditionCrotroller.Instanse.state = ExpeditionState.Wait;
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        });
    }

    /// <summary>
    /// 带动画的治疗，并有几率添加Buff
    /// </summary>
    public static void SimpleHealth(float damage, List<ModelGrid> gridList, BattleModel self, Animator animator, BuffData buff, float number)
    {
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Attack");
        TimeCrotroller.Instance.AddTimerTask(0.1f, "Attack" + self.m_Id, delegate () { }, 1, delegate ()
        {
            if (self != null)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    foreach (ModelGrid grid in gridList)
                    {
<<<<<<< HEAD
                        BattleModel target = BaseData.Instanse.GetModelById<BattleModel>(grid.battleModelId);
                        EventCrotroller.Instance.Healthed(self, target, damage, true);
=======
                        BattleModel target = BaseData.Instanse.GetModelById<BattleModel>(grid.modelId);
                        EventCrotroller.Instance.Damaged(attacker, target, (int)(attacker.Attack * 1.3),true);
                        float number = desc.m_AttributeList[0];
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
                        float random = Random.Range(0, 1f);
                        if (random <= number)
                        {
<<<<<<< HEAD
=======
                            BuffData buff = new BuffData("FireBall_Debuff");
                            buff.life = desc.m_AttributeList[2];
                            buff.RoundStartDelegate += delegate ()
                            {
                                EventCrotroller.Instance.Damaged(attacker, target, (int)(attacker.Attack * desc.m_AttributeList[1]),false);
                            };
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
                            target.AddBuff(buff);
                        }
                    }
                    animator.ResetTrigger("Attack");
                    animator.SetTrigger("Idle");
                    ExpeditionCrotroller.Instanse.state = ExpeditionState.Wait;
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        });
    }

    /// <summary>
    /// 爪击
    /// </summary>
    public static List<BattleModel> Claw(BattleModel attacker, int level, ModelGridCollection team, ModelGridCollection enemyTeam)
    {
        //attacker.MoveToCenter();
        List<BattleModel> list = new List<BattleModel>();
        List<ModelGrid> gridList = enemyTeam.GetGrid(6, null);
        AbilityDesc desc = DataManager.Instanse.m_AbilityDescContainer.GetDescByName("Claw");
        Animator animator = attacker.m_Animator;
        animator.SetTrigger("Attack");
        TimeCrotroller.Instance.AddTimerTask(0.1f, "Attack" + attacker.m_Id, delegate () { }, 1, delegate ()
        {
            if (attacker != null)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    foreach (ModelGrid grid in gridList)
                    {
                        BattleModel target = BaseData.Instanse.GetModelById<BattleModel>(grid.modelId);
                        EventCrotroller.Instance.Damaged(attacker, target, (int)(attacker.Attack * 1),true);

                        //添加流血debuff
                        BuffData buff = new BuffData("Claw_Debuff");
                        buff.life = desc.m_AttributeList[1];
                        buff.RoundStartDelegate += delegate ()
                        {
                            EventCrotroller.Instance.Damaged(attacker, target, (int)(attacker.Attack * desc.m_AttributeList[0]),false);
                        };
                        target.AddBuff(buff);
                    }
                    animator.SetTrigger("Idle");
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        });
        return list;
    }
}
