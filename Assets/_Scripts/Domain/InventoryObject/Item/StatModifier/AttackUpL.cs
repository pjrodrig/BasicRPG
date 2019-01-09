public class AttackUpL : StatModifier {

    public override string GetName() {
        return "Attack Up L";
    }

    public override string GetDescription() {
        return "Increases Attack by 20 for five turns.";
    }

    public override Stats GetStatModifier() {
        return new Stats(20, 0, 0, 0, 0);
    }

    public override int GetDuration() {
        return 5;
    }

}