using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainSpell : Spell
{
    private void Awake()
    {
        Damage = 10.0f;
        Cooldown = 20.0f;
        ManaCost = 5.0f;
        Complexity = 4;
        Name = "Drain Spell";
        isOffense = true;
    }

    public override void ApplyEffectOnEnemy(PlayerController enemy, PlayerController player)
    {
        float absorb = 20.0f;
        if(enemy.m_manaManager.m_mana < absorb)
            absorb = enemy.m_manaManager.m_mana;
        enemy.m_manaManager.m_mana -= absorb;
        player.m_manaManager.m_mana = Mathf.Min(player.m_manaManager.m_mana + absorb, player.m_manaManager.m_maxMana);
    }

    public override void ApplyEffectOnPlayer(PlayerController player)
    {
        //player.m_health.Heal(10.0f);
    }
}
