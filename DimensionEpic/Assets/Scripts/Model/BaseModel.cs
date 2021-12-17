using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BaseModel : MonoBehaviour
{
    public int m_Id;

    public BaseModel()
    {
    }

<<<<<<< HEAD
    public virtual void BeHit(BattleModel attacker,int damage,bool IsAnimation)
=======
    public virtual void BeHit(BattleModel attacker,int damage,bool isAnimator)
>>>>>>> 609bbe983ea706e17ce475795ad149ffb8a26915
    {

    }

    public virtual void Death(BattleModel killer)
    {

    }

    public virtual bool OnChoose()
    {
        return false;
    }

    public virtual void OnMove()
    {

    }
}
