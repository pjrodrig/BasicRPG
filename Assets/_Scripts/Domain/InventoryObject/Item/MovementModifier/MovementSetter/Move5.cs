public class Move5 : MovementSetter {

    public override string GetName() {
        return "Move 5";
    }

    public override string GetDescription() {
        return "Automatically move 5 spaces instead of rolling to move.";
    }

    public override int GetValue() {
        return 5;
    }
}