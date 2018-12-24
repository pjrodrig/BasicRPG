public class HpUpM : StatModifier {

    Stats stats = new Stats(0, 0, 0, 0, 100);
    string name = "Hp Up M";

    public override string GetName() {
        return this.name;
    }

    public override Stats GetStatModifier() {
        return stats;
    }

}