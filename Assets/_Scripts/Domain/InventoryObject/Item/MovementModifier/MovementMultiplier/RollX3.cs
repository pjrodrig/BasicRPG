public class RollX3 : MovementMultiplier {

    public override string GetName() {
        return "Roll x3";
    }

    public override string GetDescription() {
        return "Movement roll is multiplied by 3.";
    }

    public override int GetMultiplier() {
        return 3;
    }

}