public class Edge {
    public readonly Space a, b;

    public Edge(Space a, Space b) {
        this.a = a;
        this.b = b;
        a.edges.Add(this);
        b.edges.Add(this);
    }

    public Space GetOther(Space space) {
        Space other;
        if(space == a) {
            other = b;
        } else {
            other = a;
        }
        return other;
    }

    public bool Equals(Space other) {
        return this.ToString() == other.ToString();
    }
}