public class HpUpS : StatModifier {

    Stats stats = new Stats(0, 0, 0, 0, 20);

    public override Stats GetStatModifier() {
        return stats;
    }

}