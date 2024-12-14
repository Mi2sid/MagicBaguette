using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectSpell : Spell
{
    private void Awake()
    {
        Damage = 0.0f;
        Cooldown = 10.0f;
        ManaCost = 0.0f;
        Complexity = 2;
        Name = "Protect Spell";
        isOffense = false;

    }

    public override void ApplyEffectOnEnemy(PlayerController enemy, PlayerController player)
    {
        //enemy.m_health.Dammage(Damage);
    }

    public override void ApplyEffectOnPlayer(PlayerController player)
    {
        //player.m_health.Heal(10.0f);
        player.isProtected = true;
    }
}
