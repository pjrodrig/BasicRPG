using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Text.RegularExpressions;

public class GameSelect : MonoBehaviour {

    App app;
    Menu menu;
    bool active = false;
    Game[] games;
    
    public GameObject thisObj;
    public Button createGame;
    public GameCreation gameCreation;

    public void Init(App app, Menu menu) {
        this.app = app;
        this.menu = menu;
        this.gameCreation.Init(app, this);
    }

    public void Activate() {
        if(!active) {
            thisObj.SetActive(true);
            createGame.onClick.AddListener(CreateGame);
            FetchGames();
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false);
            createGame.onClick.RemoveAllListeners();
            active = false;
        }
    }

    private void FetchGames() {
        // games = new Game[5];

        // Player[] players = new Player[4];
        // players[0] = new Player(0, "Paul", 100);
        // players[1] = new Player(0, "Max", 100);
        // players[2] = new Player(0, "Bryce", 100);
        // players[3] = new Player(0, "Corey", 100);

        // games[0] = new Game(1, 0, 0, 0, 0, players, 1);
        // games[1] = new Game(2, 0, 0, 0, 0, players, 1);
        // games[2] = new Game(3, 0, 0, 0, 0, players, 1);
        // games[3] = new Game(4, 0, 0, 0, 0, players, 1);
        // games[4] = new Game(5, 0, 0, 0, 0, players, 1);

        // foreach (var game in games) {
            
        // }
    }

    void CreateGame() {
        Deactivate();
        gameCreation.Activate();
    }

    public void CompleteGameCreation(Game game) {
        menu.CompleteGameSelect(game);
    }

}
