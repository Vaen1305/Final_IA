using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public enum TypeAgent { Soldier, Zombie, Civil,Guard, Resources, Accommodation, Ciervo, Lobo, Jabali}
public class Health : MonoBehaviour
{
    public int health=0;
    public int healthMax= 100;
    public Image healthBar;

    public TypeAgent typeAgent;
    public TypeAgent[] typeAgentAllies;

    public Transform AimOffset;

    
    public UnityEvent DeadEvent;
    public UnityEvent BonusEvent;
    public bool IsDead { get => health <= 0; }
    public Bonus _bonus;
    public bool Inmortal;
    public void UpdabeHEalthBar()
    {
        healthBar.fillAmount = ((float)health / (float)healthMax);
    }
    public virtual void LoadComponent()
    {
        health = healthMax;
        _bonus = GetComponent<Bonus>();
    }
    public virtual void DoDamage(int dmg, Health hit)
    {
        if (Inmortal) return;
        if (IsDead) return;
        health = Mathf.Clamp(health - dmg, 0, healthMax);

        UpdabeHEalthBar();
        if (IsDead)
        {
            hit._bonus.AddBonus(_bonus);
            _bonus.Point = 0;
            _bonus.UpdabeHEalthBar();
            hit.EventBonusDead();
            Dead();
        }
         
    }
    public virtual void Dead()
    {
        DeadEvent?.Invoke();
        Destroy(this.gameObject, 3);
    }
    public void EventBonusDead()
    {

        BonusEvent?.Invoke();
    }
}
