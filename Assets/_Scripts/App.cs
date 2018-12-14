using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class App : MonoBehaviour {

    public MenuUI menu;
    public GameTurnUI gameTurn;
    public User User {get;set;}
    public Game Game {get;set;}
    public GameMap GameMap {get;set;}
    public Player activePlayer;

    void Start() {
        InitClasses();
        InitGame();
    }

    void InitClasses() {
        menu.Init(this);
    }

    void InitGame() {
        menu.Activate();
    }

    public void LoadGame() {
        InitPlayers();
        ContinueGame();
    }

    void InitPlayers() {
        foreach(Player player in Game.players) {
            player.PlayerModel = new PlayerModel();
        }
    }

    void ContinueGame() {
        activePlayer = Game.players[Game.activePlayer];
        if(activePlayer.userId == User.id) {
            gameTurn.Activate(activePlayer);
        } else {
            //Either start polling, or wait for a push notification
        }
    }
}
