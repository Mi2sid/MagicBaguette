using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TormentSpell : Spell
{
    private void Awake()
    {
        Damage = 15.0f;
        Cooldown = 15.0f;
        ManaCost = 30.0f;
        Complexity = 3;
        Name = "Torment Spell";
        isOffense = true;

    }

    public override void ApplyEffectOnEnemy(PlayerController enemy, PlayerController player)
    {
        enemy.m_health.Dammage(Damage);
        enemy.ApplyTorment(15.0f);
    }

    public override void ApplyEffectOnPlayer(PlayerController player)
    {
        //player.m_health.Heal(10.0f);
    }
}
