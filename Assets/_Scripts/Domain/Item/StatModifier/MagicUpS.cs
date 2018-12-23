public class MagicUpS : StatModifier {

    Stats stats = new Stats(0, 5, 0, 0, 0);

    public override Stats GetStatModifier() {
        return stats;
    }

}