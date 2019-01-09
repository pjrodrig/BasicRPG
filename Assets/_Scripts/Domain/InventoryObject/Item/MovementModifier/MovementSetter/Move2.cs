public class Move2 : MovementSetter {

    public override string GetName() {
        return "Move 2";
    }

    public override string GetDescription() {
        return "Automatically move 2 spaces instead of rolling to move.";
    }

    public override int GetValue() {
        return 2;
    }
}