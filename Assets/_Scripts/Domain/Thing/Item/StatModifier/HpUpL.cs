public class HpUpL : StatModifier {

    Stats stats = new Stats(0, 0, 0, 0, 200);
    string name = "Hp Up L";

    public override string GetName() {
        return this.name;
    }

    public override Stats GetStatModifier() {
        return stats;
    }

}