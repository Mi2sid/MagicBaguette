using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public float Damage { get; protected set; }
    public float Cooldown { get; protected set; }
    public float ManaCost { get; protected set; }

    public bool isOnCooldown = false;

    public bool IsOnCooldown => isOnCooldown;

    public void Use(PlayerController player)
    {
        player.m_manaManager.m_mana -= ManaCost;
        StartCoroutine(HandleCooldown());
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
