using UnityEngine;
using System.Collections.Generic;

public class Space {

    public readonly Vector3 position;
    public readonly List<Edge> edges = new List<Edge>();
    public enum SpaceType {EVENT, ITEM, SHOP};

    public Space(Vector3 position) {
        this.position = position;
        this.type = SpaceType.EVENT;
    }

    public override string ToString() {
        return (position.x - 100F) + "_" + (position.z - 50F);
    }

    public SpaceType GetType() {
        return this.type;
    }
}
