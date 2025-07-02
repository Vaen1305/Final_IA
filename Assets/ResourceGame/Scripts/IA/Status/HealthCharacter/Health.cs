using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public enum TypeAgent { Ciervo, Lobo, Jabali}

public class Health : MonoBehaviour
{
    [Header("Health Config")]
    public int maxHealth = 100;
    public int currentHealth;
    public bool IsDead { get { return currentHealth == 0; } }
    
    [Header("Agent Config")]
    public TypeAgent typeAgent;
    public Transform AimOffset;
    public TypeAgent[] typeAgentAllies = new TypeAgent[0]; // Array de aliados
    
    // Propiedades de compatibilidad
    public int health { get { return currentHealth; } set { currentHealth = value; } }
    public int healthMax { get { return maxHealth; } set { maxHealth = value; } }

    [Header("Unity Events")]
    public UnityEvent OnHealthChanges;
    public UnityEvent OnDead;

    protected AICharacterControl _AICharacterControl;

    public virtual void LoadComponent()
    {
        _AICharacterControl = GetComponent<AICharacterControl>();
        currentHealth = maxHealth;
    }

    public virtual void DoDamage(int dmg, Health hit)
    {
        if (IsDead)
        {
            return;
        }

        currentHealth -= dmg;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        OnHealthChanges?.Invoke();

        if (IsDead)
        {
            OnDead?.Invoke();
        }
    }

    public virtual void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanges?.Invoke();
    }
}
