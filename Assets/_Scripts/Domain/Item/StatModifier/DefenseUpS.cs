public class DefenseUpS : StatModifier {

    Stats stats = new Stats(0, 0, 5, 0, 0);

    public override Stats GetStatModifier() {
        return stats;
    }

}