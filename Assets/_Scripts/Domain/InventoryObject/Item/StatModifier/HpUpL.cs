public class HpUpL : StatModifier {

    public override string GetName() {
        return "Hp Up L";
    }

    public override string GetDescription() {
        return "Increases Health by 200 for five turns.";
    }

    public override Stats GetStatModifier() {
        return new Stats(0, 0, 0, 0, 200);
    }

    public override int GetDuration() {
        return 5;
    }

}