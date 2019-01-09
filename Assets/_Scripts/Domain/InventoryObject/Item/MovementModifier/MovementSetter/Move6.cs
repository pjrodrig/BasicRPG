public class Move6 : MovementSetter {

    public override string GetName() {
        return "Move 6";
    }

    public override string GetDescription() {
        return "Automatically move 6 spaces instead of rolling to move.";
    }

    public override int GetValue() {
        return 6;
    }
}