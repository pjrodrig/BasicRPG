public class DefenseUpL : StatModifier {

    Stats stats = new Stats(0, 0, 20, 0, 0);
    string name = "Defense Up L";

    public override string GetName() {
        return this.name;
    }

    public override Stats GetStatModifier() {
        return stats;
    }

}