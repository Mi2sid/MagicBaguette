using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public float Damage { get; protected set; }
    public float Cooldown { get; protected set; }
    public float ManaCost { get; protected set; }

    private bool isOnCooldown = false;

    public bool IsOnCooldown => isOnCooldown;

    public void Use(PlayerController enemy, PlayerController player)
    {
        if (!isOnCooldown)
        {
            if(player.m_manaManager.m_mana > ManaCost){
                ApplyEffectOnEnemy(enemy);
                ApplyEffectOnPlayer(player);
                player.m_manaManager.m_mana -= ManaCost;

                StartCoroutine(HandleCooldown());
            } else {
                Debug.Log("Pas assez de mana...");
            }
        }
        else
        {
            Debug.Log("Compétence en cooldown !");
        }
    }

    private IEnumerator HandleCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(Cooldown);
        isOnCooldown = false;
    }

    public abstract void ApplyEffectOnEnemy(PlayerController enemy);
    public abstract void ApplyEffectOnPlayer(PlayerController player);
}
