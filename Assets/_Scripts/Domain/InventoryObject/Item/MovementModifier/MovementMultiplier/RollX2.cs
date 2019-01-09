public class RollX2 : MovementMultiplier {

    public override string GetName() {
        return "Roll x2";
    }

    public override string GetDescription() {
        return "Movement roll is multiplied by 2.";
    }

    public override int GetMultiplier() {
        return 2;
    }

}