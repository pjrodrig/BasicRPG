public class RollX4 : MovementMultiplier {

    public override string GetName() {
        return "Roll x4";
    }

    public override string GetDescription() {
        return "Movement roll is multiplied by 4.";
    }

    public override int GetMultiplier() {
        return 4;
    }

}