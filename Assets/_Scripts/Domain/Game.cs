using System;
using System.Text;

[Serializable]
public class Game {

    public int id;
    public int turn;
    public int week;
    public int playerTurn;
    public int activePlayer;
    public Player[] players;
    public bool isStarted;

    public Game(Player[] players) {
        this.players = players;
    }

    public Game(int id, int turn, int week, int playerTurn, int activePlayer, Player[] players, int mapId) {
        this.id = id;
        this.turn = turn;
        this.week = week;
        this.playerTurn = playerTurn;
        this.activePlayer = activePlayer;
        this.players = players;
    }

    public override string ToString() {
        return 
        "{ id: " + id + 
        ", turn: " + turn + 
        ", week: " + week + 
        ", playerTurn: " + playerTurn + 
        ", activePlayer: " + activePlayer +
        ", players: " + GetPlayersString() + " }";
    }

    string GetPlayersString() {
        StringBuilder sb = new StringBuilder("[");
        if(players != null) {
            sb.Append(players[0].ToString());
            for(int i = 1; i < players.Length; i++) {
                sb.Append(", ");
                sb.Append(players[i].ToString());
            }
        }
        sb.Append("]");
        return sb.ToString();
    }

}