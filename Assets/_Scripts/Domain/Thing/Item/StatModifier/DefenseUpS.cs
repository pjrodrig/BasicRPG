public class DefenseUpS : StatModifier {

    Stats stats = new Stats(0, 0, 5, 0, 0);
    string name = "Defense Up S";

    public override string GetName() {
        return this.name;
    }

    public override Stats GetStatModifier() {
        return stats;
    }

}