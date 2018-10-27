public class BattleSpaceEvent {

    private Combatant a;
    private Combatant b;
    private PlayerUI playerUI;

    public BattleSpaceEvent(Combatant a, Combatant b, PlayerUI playerUI) {
        this.a = a;
        this.b = b;
        this.playerUI = playerUI;
    }

    public void Battle(Action callback) {

    }
}
