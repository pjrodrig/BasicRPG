public class AttackUpL : StatModifier {

    Stats stats = new Stats(20, 0, 0, 0, 0);
    string name = "Attack Up L";

    public override string GetName() {
        return this.name;
    }

    public override Stats GetStatModifier() {
        return stats;
    }

}