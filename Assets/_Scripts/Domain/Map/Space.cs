using UnityEngine;
using System.Collections.Generic;

public class Space {

    public enum Type {
        EVENT,
        WEAPON_SHOP,
        MAGIC_SHOP,
        ITEM_SHOP,
        ITEM,
        SCROLL,
        WEAPON,
        SHIELD
    }

    public readonly string id;
    public readonly Vector3 position;
    public readonly List<Edge> edges = new List<Edge>();
    public Type type;
    public int level;

    public Space(string id, Vector3 position, int level) {
        this.id = id;
        this.position = position;
        this.level = level;
        this.type = Type.EVENT;
    }

    public override string ToString() {
        return (position.x - 100F) + "_" + (position.z - 50F);
    }
}