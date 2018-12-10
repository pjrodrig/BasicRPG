using System;

[Serializable]
public class Player {
    public int userId;
    public string name;
    public int gold;
    public Job job;
    public Inventory inventory;
    public Equipment equipment;
    public Stats stats;

    public Player(int userId) {
        this.userId = userId;
    }

    public override string ToString() {
        return 
        "{ userId: " + userId + 
        ", name: " + name + " }";
    }
}
