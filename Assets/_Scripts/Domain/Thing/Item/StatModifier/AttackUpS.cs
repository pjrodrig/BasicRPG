public class AttackUpS : StatModifier {

    Stats stats = new Stats(5, 0, 0, 0, 0);
    string name = "Attack Up S";

    public override string GetName() {
        return this.name;
    }

    public override Stats GetStatModifier() {
        return stats;
    }

}