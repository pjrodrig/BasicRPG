public class D20 : MovementDie {

    public override string GetName() {
        return "D20";
    }

    public override string GetDescription() {
        return "Roll a D20 instead of a D6 for movement.";
    }

    public override int GetDie() {
        return 20;
    }
}