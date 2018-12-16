using UnityEngine;
using System;
using System.Reflection;

[Serializable]
public class Player : ISerializationCallbackReceiver {
    public int id;
    public string username;
    public string name;
    [NonSerialized]
    public Clazz clazz;
    public string classType;
    public Appearance appearance;
    public bool isInitialized;
    public PlayerData playerData;
    // public PlayerData PlayerData {get;set;} TODO: optimize by storing player data separately
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
            classType = clazz.GetType().ToString();
        }
    }

    public void OnAfterDeserialize() {
        if(classType != null && classType != "") {
            clazz = (Clazz) Activator.CreateInstance(Assembly.GetExecutingAssembly().GetType(classType));
        }
    }

}
