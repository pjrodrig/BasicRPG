public class MagicUpS : StatModifier {

    string name = "Magic Up S";
    string description = "Increases Magic by 5 for five turns.";
    Stats stats = new Stats(0, 5, 0, 0, 0);
    int duration = 5;

    public override string GetName() {
        return "Magic Up S";
    }

    public override string GetDescription() {
        return "Increases Magic by 5 for five turns.";
    }

    public override Stats GetStatModifier() {
        return new Stats(0, 5, 0, 0, 0);
    }

    public override int GetDuration() {
        return 5;
    }

}