using System;

[Serializable]
public class User {

    public Int16 id;
    public string name;

    public User(string name) {
        this.name = name;
    }

    public override string ToString() {
        return "{id: " + this.id + ", name: " + this.name + "}";
    }
}
