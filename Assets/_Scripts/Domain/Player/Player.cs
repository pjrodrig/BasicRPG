using UnityEngine;
using System;

[Serializable]
public class Player : ISerializationCallbackReceiver {
    public int id;
    public string username;
    public string name;
    [NonSerialized]
    public Clazz clazz;
    public Type classType;
    public Appearance appearance;
    public bool isInitialized;
    public PlayerData PlayerData {get;set;}
    public PlayerModel PlayerModel {get;set;}

    public Player(int id, string username) {
        this.id = id;
        this.username = username;
        this.appearance = new Appearance();
    }

    public override string ToString() {
        return 
        "{ userId: " + id + 
        ", username: " + username + 
        ", name: " + name + " }";
    }

    public void OnBeforeSerialize() {
        if(clazz != null) {
            classType = clazz.GetType();
        }
    }

    public void OnAfterDeserialize() {
        if(classType != null) {
            clazz = (Clazz) Activator.CreateInstance(classType);
        }
    }

}
