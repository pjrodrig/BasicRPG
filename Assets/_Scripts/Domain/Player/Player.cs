public class Player {
    public int userId;
    public string name;
    public int gold;
    public Job job;
    public Inventory inventory;
    public Equipment equipment;
    public Stats stats;

    public Player(int userId, string name, int gold) {
        this.userId = userId;
        this.name = name;
        this.gold = gold;
    }
}
