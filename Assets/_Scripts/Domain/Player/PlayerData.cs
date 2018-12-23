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
    public Stats currentStats;
    public Dictionary<string, int> classLevels;
    public object[][] classLevelsMatrix;
    public LinkedList<string> masteredClasses;
    public string map;
    public string space;
    public bool isInBattle;

    public PlayerData(int id) {
        this.id = id;
        equipment = new Equipment();
        inventory = new Inventory();
        // stats = new Stats();
        // currentStats = new Stats();
        stats = new Stats(5, 18, 15, 0, 170);
        currentStats = stats.copy();
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
        // TESTING
        // stats = new Stats(5, 18, 15, 0, 170);
        // currentStats = stats.copy();
        // TESTING
    }

    public Stats getBattleStats() {
        return new Stats(stats.atk, stats.mag, stats.spd, stats.def, stats.hp);
    }

    public Stats getCurrentStats() {
        return currentStats;
    }
}
