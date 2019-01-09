public class AttackUpM : StatModifier {

    public override string GetName() {
        return "Attack Up M";
    }

    public override string GetDescription() {
        return "Increases Attack by 10 for five turns.";
    }


    public override Stats GetStatModifier() {
        return new Stats(10, 0, 0, 0, 0);
    }

    public override int GetDuration() {
        return 5;
    }

}