using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData : ISerializationCallbackReceiver {
    public int id;
    public int gold;
    public Equipment equipment;
    public Inventory inventory;
    public Stats stats;
    public Dictionary<string, int> classLevels;
    public object[][] classLevelsMatrix;
    public LinkedList<string> masteredClasses;
    public string map;
    public string space;

    public PlayerData(int id) {
        this.id = id;
        equipment = new Equipment();
        inventory = new Inventory();
        stats = new Stats();
        classLevels = new Dictionary<string, int>();
        masteredClasses = new LinkedList<string>();
    }

    public void OnBeforeSerialize() {
        if(classLevels != null) {
            classLevelsMatrix = new object[classLevels.Keys.Count][];
            int i = 0;
            foreach(KeyValuePair<string, int> classLevel in classLevels) {
                classLevelsMatrix[i++] = new object[]{classLevel.Key, classLevel.Value};
            }
        }
    }

    public void OnAfterDeserialize() {
        classLevels = new Dictionary<string, int>();
        if(classLevelsMatrix != null) {
            foreach(object[] classLevel in classLevelsMatrix) {
                classLevels.Add((string) classLevel[0], (int) classLevel[1]);
            }
        }
    }
}
