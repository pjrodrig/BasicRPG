public class MagicUpS : StatModifier {

    Stats stats = new Stats(0, 5, 0, 0, 0);
    string name = "Magic Up S";

    public override string GetName() {
        return this.name;
    }

    public override Stats GetStatModifier() {
        return stats;
    }

}