public class SpeedUpS : StatModifier {

    public override string GetName() {
        return "Speed Up S";
    }

    public override string GetDescription() {
        return "Increases Speed by 5 for five turns.";
    }

    public override Stats GetStatModifier() {
        return new Stats(0, 0, 0, 5, 0);
    }

    public override int GetDuration() {
        return 5;
    }

}