public class DefenseUpL : StatModifier {

    public override string GetName() {
        return "Defense Up L";
    }

    public override string GetDescription() {
        return "Increases Defense by 20 for five turns.";
    }


    public override Stats GetStatModifier() {
        return new Stats(0, 0, 20, 0, 0);
    }

    public override int GetDuration() {
        return 5;
    }

}