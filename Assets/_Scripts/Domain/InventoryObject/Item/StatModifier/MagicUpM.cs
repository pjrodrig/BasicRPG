public class MagicUpM : StatModifier {

    public override string GetName() {
        return "Magic Up M";
    }

    public override string GetDescription() {
        return "Increases Magic by 10 for five turns.";
    }

    public override Stats GetStatModifier() {
        return new Stats(0, 10, 0, 0, 0);
    }

    public override int GetDuration() {
        return 5;
    }

}