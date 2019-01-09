public class AttackUpS : StatModifier {

    public override string GetName() {
        return "Attack Up S";
    }

    public override string GetDescription() {
        return "Increases Attack by 5 for five turns.";
    }


    public override Stats GetStatModifier() {
        return new Stats(5, 0, 0, 0, 0);
    }

    public override int GetDuration() {
        return 5;
    }

}