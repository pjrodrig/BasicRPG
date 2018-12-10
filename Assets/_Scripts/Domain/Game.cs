public class Game {

    public int gameId;
    public int turn;
    public int week;
    public int playerTurn;
    public int activePlayer;
    public Player[] players;
    public int mapId;

    public Game(int gameId, int turn, int week, int playerTurn, int activePlayer, Player[] players, int mapId) {
        this.gameId = gameId;
        this.turn = turn;
        this.week = week;
        this.playerTurn = playerTurn;
        this.activePlayer = activePlayer;
        this.players = players;
        this.mapId = mapId;
    }

}