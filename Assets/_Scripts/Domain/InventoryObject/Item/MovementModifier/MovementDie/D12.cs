public class D12 : MovementDie {

    public override string GetName() {
        return "D12";
    }

    public override string GetDescription() {
        return "Roll a D12 instead of a D6 for movement.";
    }

    public override int GetDie() {
        return 12;
    }
}