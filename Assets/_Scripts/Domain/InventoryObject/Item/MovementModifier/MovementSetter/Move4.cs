public class Move4 : MovementSetter {

    public override string GetName() {
        return "Move 4";
    }

    public override string GetDescription() {
        return "Automatically move 4 spaces instead of rolling to move.";
    }

    public override int GetValue() {
        return 4;
    }
}