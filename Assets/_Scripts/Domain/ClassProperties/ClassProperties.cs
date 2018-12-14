public static class ClassProperties {

    public enum ClassType {
        WARRIOR,
        MAGE,
        ASSASSIN
    }

    public static string GetTitle(ClassType classType) {
        switch(classType) {
            case ClassType.WARRIOR:
                return Warrior.title;
            case ClassType.MAGE:
                return Mage.title;
            case ClassType.ASSASSIN:
                return Assassin.title;
            default:
                return null;
        }
    }

    public static Stats GetStats(ClassType classType) {
        switch(classType) {
            case ClassType.WARRIOR:
                return Warrior.stats;
            case ClassType.MAGE:
                return Mage.stats;
            case ClassType.ASSASSIN:
                return Assassin.stats;
            default:
                return null;
        }
    }

    public static Stats GetMasteryStats(ClassType classType) {
        switch(classType) {
            case ClassType.WARRIOR:
                return Warrior.masteryStats;
            case ClassType.MAGE:
                return Mage.masteryStats;
            case ClassType.ASSASSIN:
                return Assassin.masteryStats;
            default:
                return null;
        }
    }
}