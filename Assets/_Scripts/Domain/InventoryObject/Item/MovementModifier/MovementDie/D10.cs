public class D10 : MovementDie {
    
    public override string GetName() {
        return "D10";
    }

    public override string GetDescription() {
        return "Roll a D10 instead of a D6 for movement.";
    }

    public override int GetDie() {
        return 10;
    }
}