public class AttackUpL : StatModifier {

    Stats stats = new Stats(20, 0, 0, 0, 0);

    public override Stats GetStatModifier() {
        return stats;
    }

}