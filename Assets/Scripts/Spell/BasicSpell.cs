using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpell : Spell
{
    private void Awake()
    {
        Damage = 30.0f;
        Cooldown = 10.0f;
        ManaCost = 20.0f;
        Complexity = 3;
        Name = "Damage Spell";
        isOffense = true;
    }

    public override void ApplyEffectOnEnemy(PlayerController enemy, PlayerController player)
    {
        
        enemy.m_health.Dammage(Damage);
    }

    public override void ApplyEffectOnPlayer(PlayerController player)
    {
        //player.m_health.Heal(10.0f);
    }
}
