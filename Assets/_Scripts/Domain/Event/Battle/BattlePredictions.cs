using System;

public class BattlePredictions {

    public int attackDefend;
    public int attackFail;
    public int magicMagicDefend;
    public int magicFail;
    public int empoweredAttackDefend;
    public int empoweredAttackMagicDefend;
    public int empoweredAttackCounter;

    public BattlePredictions(Stats attacker, Stats defender) {
        attackDefend = attacker.atk * 3 - defender.def * 2;
        attackFail = attacker.atk * 3 - defender.def;
        magicMagicDefend = attacker.mag * 3 - defender.mag * 2;
        magicFail = attacker.mag * 3 - defender.mag;
        empoweredAttackDefend = Math.Max(attacker.atk * 5 - defender.def * 2, 0) + Math.Max(attacker.mag * 4 - defender.mag, 0);
        empoweredAttackMagicDefend = Math.Max(attacker.atk * 5 - defender.def, 0) + Math.Max(attacker.mag * 4 - defender.mag * 2, 0);
        empoweredAttackCounter = Math.Max(defender.atk * 5 - attacker.def, defender.mag * 4 - attacker.mag);
    }

    public int GetDamage(Battle.Action attack, Battle.Action defense) {
        switch(attack) {
            case Battle.Action.ATTACK:
                return defense == Battle.Action.DEFEND ? attackDefend : attackFail;
            case Battle.Action.MAGIC:
                return defense == Battle.Action.MAGIC_DEFEND ? magicMagicDefend : magicFail;
            default: // empowered attack
                switch(defense) {
                    case Battle.Action.DEFEND:
                        return empoweredAttackDefend;
                    case Battle.Action.MAGIC_DEFEND:
                        return empoweredAttackMagicDefend;
                    default: // counter
                        return empoweredAttackCounter;
                }
        }
    }
}