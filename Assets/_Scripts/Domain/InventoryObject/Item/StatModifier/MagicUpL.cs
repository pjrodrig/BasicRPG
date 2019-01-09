public class MagicUpL : StatModifier {

    public override string GetName() {
        return "Magic Up L";
    }

    public override string GetDescription() {
        return "Increases Magic by 20 for five turns.";
    }

    public override Stats GetStatModifier() {
        return new Stats(0, 20, 0, 0, 0);
    }

    public override int GetDuration() {
        return 5;
    }

}