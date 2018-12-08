using System;

[Serializable]
public class User {
    public int id;
    public string name;

    public string ToString() {
        return "{id: " + this.id + ", name: " + this.name + "}";
    }
}
