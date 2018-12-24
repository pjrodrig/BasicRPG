public static class ItemPool {
    public static string[] GetItemPool(int level) {
        switch(level) {
            case 1:
                return new string[]{
                    "AttackUpS", 
                    "DefenseUpS", 
                    "HpUpS", 
                    "MagicUpS", 
                    "SpeedUpS"
                };
            default:
                return null;
        }
    }
}