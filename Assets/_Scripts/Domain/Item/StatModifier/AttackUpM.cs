public class AttackUpM : StatModifier {

    Stats stats = new Stats(10, 0, 0, 0, 0);

    public override Stats GetStatModifier() {
        return stats;
    }

}