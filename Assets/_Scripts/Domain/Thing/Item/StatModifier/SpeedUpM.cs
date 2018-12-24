public class SpeedUpM : StatModifier {

    Stats stats = new Stats(0, 0, 0, 10, 0);
    string name = "Speed Up M";

    public override string GetName() {
        return this.name;
    }

    public override Stats GetStatModifier() {
        return stats;
    }

}