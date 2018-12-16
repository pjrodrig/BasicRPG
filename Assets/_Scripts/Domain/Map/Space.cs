using UnityEngine;
using System.Collections.Generic;

public class Space {

    public enum Type {
        EVENT,
        WEAPON_SHOP,
        MAGIC_SHOP,
        ITEM_SHOP
    }
    public readonly string id;
    public readonly Vector3 position;
    public readonly List<Edge> edges = new List<Edge>();
    public readonly Type type;

    public Space(string id, Vector3 position) {
        this.id = id;
        this.position = position;
        this.type = Type.EVENT;
    }

    public override string ToString() {
        return (position.x - 100F) + "_" + (position.z - 50F);
    }
}