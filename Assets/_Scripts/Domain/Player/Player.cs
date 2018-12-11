using System;

[Serializable]
public class Player {
    public int userId;
    public string username;
    public string name;
    public int gold;
    public Clazz clazz;
    public Appearance appearance;
    public Inventory inventory;
    public Equipment equipment;
    public Stats stats;
    public Stats perLevelStats;

    public Player(int userId, string username) {
        this.userId = userId;
        this.username = username;
    }

    public override string ToString() {
        return 
        "{ userId: " + userId + 
        ", username: " + username + 
        ", name: " + name + " }";
    }
}
