using System;
using System.Collections.Generic;

[Serializable]
public class Battle : Event {

    public enum Action {
        ATTACK,
        EMPOWERED_ATTACK,
        MAGIC,
        ITEM,
        DEFEND,
        COUNTER,
        MAGIC_DEFEND,
        SURRENDER
    }

    Action homeAction;
    Action awayAction;
    
    [NonSerialized]
    public Player home;
    [NonSerialized]
    public BattleParticipant away;
    public bool attacking;
    public List<int> players; 
    public int phase;
    public Monster monster;
    public bool over;

    public Battle(Player home, BattleParticipant away) {
        this.home = home;
        this.away = away;
        this.phase = 0;
        over = false;
        home.playerData.isInBattle = true;
        players = new List<int>();
        players.Add(home.id);
        if(away is Player) {
            ((Player)away).playerData.isInBattle = true;
            players.Add(((Player)away).id);
        } else {
            monster = (Monster)away;
        }
    }

    public BattleParticipant GetAttacker() {
        return attacking ? home : away;
    }

    public BattleParticipant GetDefender() {
        return attacking ? away : home;
    }

    public void SetOffensiveAction(Action action) {
        if(attacking) {
            homeAction = action;
        } else {
            awayAction = action;
        }
    }

    public Action GetOffensiveAction() {
        return attacking ? homeAction : awayAction;
    }

    public void SetDefensiveAction(Action action) {
        if(attacking) {
            awayAction = action;
        } else {
            homeAction = action;
        }
    }

    public Action GetDefensiveAction() {
        return attacking ? awayAction : homeAction;
    }

    public void NextPhase() {
        phase++;
        // TESTING
        // if(phase == 3) {
        //     attacking = !attacking;
        //     phase = 1;
        // }
        // TESTING
        if(phase == 2) {
            attacking = !attacking;
        }
    }

    public void EndBattle(bool won) {
        if(away is Player) {
            if(players.Count > 2 || monster != null) {
                players.Remove(won ? ((Player)away).id : home.id);
                home = null;
                away = null;
            }
        } else {
            if(won) {
                if(players.Count > 1) {
                    monster = null;
                    home = null;
                    away = null;
                }
            } else {
                players.Remove(home.id);
            }
            
        }
    }
    
}