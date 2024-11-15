using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpell : Spell
{
    private void Awake()
    {
        Damage = 20.0f;
        Cooldown = 3.0f;
        ManaCost = 20.0f;
    }

    public override void ApplyEffectOnEnemy(PlayerController enemy)
    {
        
        enemy.m_health.Dammage(Damage);
    }

    public override void ApplyEffectOnPlayer(PlayerController player)
    {
        player.m_health.Heal(10.0f);
    }
}
