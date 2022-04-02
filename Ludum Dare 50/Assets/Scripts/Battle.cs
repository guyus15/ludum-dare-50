using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    private float _modifiedAttackerDamage;
    private int _attackerDamage;
    private float _attackerHP;
    private int _defenderDamage;
    private float _defenderHP;
    private int _tileDefendibility;
    // Update is called once per frame
    int updateBattle() //Runs the battle for a turn
    {
        //Defenders turn first
        _modifiedAttackerDamage = applyTileModifier(_tileDefendibility, _attackerDamage); //Apply the attack damage penelty to the attacker.
        _attackerHP = takeTurn(_defenderDamage, _attackerHP);
        if (_attackerHP <= 0)
        {
            return 0;
        }
        _defenderHP = takeTurn(_modifiedAttackerDamage, _defenderHP);
        if (_defenderHP <= 0)
        {
            return 1;
        }

        return 0;
    }

    float takeTurn(float attackerDamage, float defenderHP) //The 'attacker' is the one doing the damage here not the same as the one attacking the tile
    {
        return defenderHP - attackerDamage;
    }

    float applyTileModifier(int tileDefendibility, int attackerDamage) //Applies tile effects to attackers damage stat
    {
        return attackerDamage/tileDefendibility;
    }

    void initialiseBattle(int attackerDamageInput, float attackerHPInput, int defenderDamageInput, float defenderHPInput, int tileDefendibilityInput) //Initialse all the variables
    {
        _attackerDamage = attackerDamageInput;
        _attackerHP = attackerHPInput;
        _defenderDamage = defenderDamageInput;
        _defenderHP = defenderHPInput;
        _tileDefendibility = tileDefendibilityInput;
    }

}
