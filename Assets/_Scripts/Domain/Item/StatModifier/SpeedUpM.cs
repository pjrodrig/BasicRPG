public class SpeedUpM : StatModifier {

    Stats stats = new Stats(0, 0, 0, 10, 0);

    public override Stats GetStatModifier() {
        return stats;
    }

}