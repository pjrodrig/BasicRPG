public class MagicUpM : StatModifier {

    Stats stats = new Stats(0, 10, 0, 0, 0);

    public override Stats GetStatModifier() {
        return stats;
    }

}