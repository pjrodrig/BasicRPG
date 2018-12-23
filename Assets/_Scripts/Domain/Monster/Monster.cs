using System;

[Serializable]
public class Monster : BattleParticipant {

    public Stats stats;
    public Stats currentStats;

    public Monster() {
        stats = new Stats(20, 10, 10, 0, 150);
        currentStats = stats.copy();
    }

    public override Stats GetStats() {
        return stats;
    }

    public override Stats GetCurrentStats() {
        return currentStats;
    }
}