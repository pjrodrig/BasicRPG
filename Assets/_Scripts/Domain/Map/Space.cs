using UnityEngine;
using System.Collections.Generic;

public class Space {

    public readonly string id;
    public readonly Vector3 position;
    public readonly List<Edge> edges = new List<Edge>();

    public Space(string id, Vector3 position) {
        this.id = id;
        this.position = position;
    }

    public override string ToString() {
        return (position.x - 100F) + "_" + (position.z - 50F);
    }
}