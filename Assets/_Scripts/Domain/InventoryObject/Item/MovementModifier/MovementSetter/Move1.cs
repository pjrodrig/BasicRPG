public class Move1 : MovementSetter {

    public override string GetName() {
        return "Move 1";
    }

    public override string GetDescription() {
        return "Automatically move 1 space instead of rolling to move.";
    }

    public override int GetValue() {
        return 1;
    }
}