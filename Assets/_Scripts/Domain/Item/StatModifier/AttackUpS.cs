public class AttackUpS : StatModifier {

    Stats stats = new Stats(5, 0, 0, 0, 0);

    public override Stats GetStatModifier() {
        return stats;
    }

}