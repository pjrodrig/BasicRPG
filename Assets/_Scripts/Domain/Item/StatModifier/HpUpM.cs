public class HpUpM : StatModifier {

    Stats stats = new Stats(0, 0, 0, 0, 100);

    public override Stats GetStatModifier() {
        return stats;
    }

}