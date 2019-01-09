public class Move3 : MovementSetter {

    public override string GetName() {
        return "Move 3";
    }

    public override string GetDescription() {
        return "Automatically move 3 spaces instead of rolling to move.";
    }

    public override int GetValue() {
        return 2;
    }
}