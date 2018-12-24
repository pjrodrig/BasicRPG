public class AttackUpM : StatModifier {

    Stats stats = new Stats(10, 0, 0, 0, 0);
    string name = "Attack Up M";

    public override string GetName() {
        return this.name;
    }

    public override Stats GetStatModifier() {
        return stats;
    }

}