public class SpeedUpL : StatModifier {

    public override string GetName() {
        return "Speed Up L";
    }

    public override string GetDescription() {
        return "Increases Speed by 20 for five turns.";
    }

    public override Stats GetStatModifier() {
        return new Stats(0, 0, 0, 20, 0);
    }

    public override int GetDuration() {
        return 5;
    }

}