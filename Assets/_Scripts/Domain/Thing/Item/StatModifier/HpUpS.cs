public class HpUpS : StatModifier {

    Stats stats = new Stats(0, 0, 0, 0, 20);
    string name = "Hp Up S";

    public override string GetName() {
        return this.name;
    }

    public override Stats GetStatModifier() {
        return stats;
    }

}