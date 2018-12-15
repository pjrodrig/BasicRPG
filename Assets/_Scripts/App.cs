using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class App : MonoBehaviour {

    public MenuUI menu;
    public GameTurnUI gameTurn;
    public CameraModel gameCamera;
    public User User {get;set;}
    public Game Game {get;set;}
    public Player ActivePlayer;

    void Start() {
        gameCamera.Locked = true;
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
        // foreach(Player player in Game.players) {
        //     player.PlayerModel = new PlayerModel();
        // }
    }

    void ContinueGame() {
        foreach(Player player in Game.players) {
            if(player.id == User.id) {
                ActivePlayer = player;
                break;
            }
        }
        // ActivePlayer = Game.players[Game.activePlayer];
        // if(ActivePlayer.userId == User.id) {
            gameTurn.Activate();
        // } else {
        //     //Either start polling, or wait for a push notification
        // }
    }
}
