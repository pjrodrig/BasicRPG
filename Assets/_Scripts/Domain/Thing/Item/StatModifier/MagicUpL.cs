public class MagicUpL : StatModifier {

    Stats stats = new Stats(0, 20, 0, 0, 0);
    string name = "Magic Up L";

    public override string GetName() {
        return this.name;
    }

    public override Stats GetStatModifier() {
        return stats;
    }

}