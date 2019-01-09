public class DefenseUpS : StatModifier {

    public override string GetName() {
        return "Defense Up S";

    }

    public override string GetDescription() {
        return "Increases Defense by 5 for five turns.";
    }

    public override Stats GetStatModifier() {
        return new Stats(0, 0, 5, 0, 0);
    }

    public override int GetDuration() {
        return 5;
    }

}