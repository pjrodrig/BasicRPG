using System;

[Serializable]
public class Player {
    public int userId;
    public string username;
    public string name;
    public int gold;
    public ClassProperties.ClassType clazz;
    public Appearance appearance;
    public Inventory inventory;
    public Equipment equipment;
    public Stats stats;
    public bool isInitialized;
    public PlayerModel PlayerModel {get;set;}

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
