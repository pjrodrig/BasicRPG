public class DefenseUpM : StatModifier {

    Stats stats = new Stats(0, 0, 10, 0, 0);
    string name = "Defense Up M";

    public override string GetName() {
        return this.name;
    }

    public override Stats GetStatModifier() {
        return stats;
    }

}