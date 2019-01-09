public class HpUpS : StatModifier {

    public override string GetName() {
        return "Hp Up S";
    }

    public override string GetDescription() {
        return "Increases Health by 20 for five turns.";
    }

    public override Stats GetStatModifier() {
        return new Stats(0, 0, 0, 0, 20);
    }

    public override int GetDuration() {
        return 5;
    }

}