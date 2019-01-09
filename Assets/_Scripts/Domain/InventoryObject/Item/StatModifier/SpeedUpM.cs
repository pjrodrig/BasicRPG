public class SpeedUpM : StatModifier {

    public override string GetName() {
        return "Speed Up M";
    }

    public override string GetDescription() {
        return "Increases Speed by 10 for five turns.";
    }

    public override Stats GetStatModifier() {
        return new Stats(0, 0, 0, 10, 0);
    }

    public override int GetDuration() {
        return 5;
    }

}