public class HpUpL : StatModifier {

    Stats stats = new Stats(0, 0, 0, 0, 200);

    public override Stats GetStatModifier() {
        return stats;
    }

}