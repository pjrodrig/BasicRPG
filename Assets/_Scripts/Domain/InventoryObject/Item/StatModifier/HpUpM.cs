public class HpUpM : StatModifier {

    public override string GetName() {
        return "Hp Up M";
    }

    public override string GetDescription() {
        return "Increases Health by 100 for five turns.";
    }

    public override Stats GetStatModifier() {
        return new Stats(0, 0, 0, 0, 100);
    }

    public override int GetDuration() {
        return 5;
    }

}