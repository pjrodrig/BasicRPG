public class D8 : MovementDie {

    public override string GetName() {
        return "D8";
    }

    public override string GetDescription() {
        return "Roll a D8 instead of a D6 for movement.";
    }

    public override int GetDie() {
        return 8;
    }
}