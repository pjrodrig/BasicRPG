public class MagicUpL : StatModifier {

    Stats stats = new Stats(0, 20, 0, 0, 0);

    public override Stats GetStatModifier() {
        return stats;
    }

}