public class DefenseUpL : StatModifier {

    Stats stats = new Stats(0, 0, 20, 0, 0);

    public override Stats GetStatModifier() {
        return stats;
    }

}