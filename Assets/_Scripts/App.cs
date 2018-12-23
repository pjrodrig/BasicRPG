using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class App : MonoBehaviour {

    PlayerModel[] playerModels = null;
    Coroutine poll;

    public MenuUI menu;
    public GameTurnUI gameTurn;
    public GameTurn2UI gameTurn2;
    public CameraModel gameCamera;
    public GameOverlay gameOverlay;
    public TestMap testMap;
    public GameObject playerPrefab;
    public User User {get;set;}
    public Game Game {get;set;}
    public Player ActivePlayer;

    void Start() {
        // TESTING
        // User = new User("asdf");
        // User.id = 5;
        // TESTING
        InitClasses();
        InitGame();
    }

    void InitClasses() {
        menu.Init(this);
        gameTurn.Init(this, gameTurn2);
        gameTurn2.Init(this);
        testMap.Init();
    }

    void InitGame() {
        gameCamera.Locked = true;
        menu.Activate();
        // TESTING
        // ActivePlayer = new Player(5, "asdf");
        // ActivePlayer.playerData = new PlayerData(5);
        // gameTurn2.Activate();
        // TESTING
    }

    public void LoadGame() {
        if(playerModels != null) {
            UpdatePlayerModels();
        } else {
            InitPlayers();
        }
        ContinueGame();
    }

    void InitPlayers() {
        playerModels = new PlayerModel[Game.players.Length];
        int i = 0;
        foreach(Player player in Game.players) {
            GameObject newPlayer = Instantiate(playerPrefab) as GameObject;
            PlayerModel playerModel = newPlayer.GetComponent<PlayerModel>();
            string spaceId = player.playerData.space;
            Space space;
            if(spaceId != null && spaceId !="") {
                space = testMap.GetSpace(spaceId);
            } else {
                space = testMap.GetStart();
            }
            playerModel.Init(space);
            player.PlayerModel = playerModel;
            playerModels[i++] = playerModel;
        }
    }

    void UpdatePlayerModels() {
        int i = 0;
        foreach(Player player in Game.players) {
            player.PlayerModel = playerModels[i++];
        }
    }

    public void AdvanceTurn() {
        Game.playerTurn = Game.playerTurn < Game.players.Length - 1 ? Game.playerTurn + 1 : 0;
        Game.activePlayer = Game.playerTurn;
        if(Game.playerTurn == 0) {
            Game.turn++;
            if(Game.turn % 7 == 0) {
                Game.week++;
            }
        }
        StartCoroutine(Rest.Put(API.game, null, Game, new Action<Game>(delegate (Game updatedGame) {
            Game = updatedGame;
            LoadGame();
        }), new Action<RestError>(delegate (RestError err) {
            Debug.Log(err.message);
        })));
    }

    public void ContinueGame() {
        ActivePlayer = Game.players[Game.activePlayer];
        gameCamera.FocusPosition(ActivePlayer.PlayerModel.thisObj.transform.position);
        if(ActivePlayer.id == User.id) {
            gameOverlay.announcement.text = "";
            if(ActivePlayer.playerData.isInBattle) {
                gameTurn2.Activate();
            } else {
                gameTurn.Activate();
            }
        } else {
            gameOverlay.announcement.text = "Waiting on:\n" + ActivePlayer.name;
            StartCoroutine(PollGame());
        }
    }

    IEnumerator PollGame() {
        yield return new WaitForSeconds(10);
        poll = StartCoroutine(Rest.Get(API.gameLastUpdated, "gameId=" + Game.id, new Action<Game>(delegate (Game game) {
            if(game.lastUpdated != Game.lastUpdated) {
                RefreshGame();
            } else {
                StartCoroutine(PollGame());
            }
        }), new Action<RestError>(delegate (RestError err) {
            Debug.Log(err.message);
        })));
    }

    void RefreshGame() {
        StartCoroutine(Rest.Get(API.game, "gameId=" + Game.id, new Action<Game>(delegate (Game game) {
            ClearGame();
            Game = game;
            LoadGame();
        }), new Action<RestError>(delegate (RestError err) {
            Debug.Log(err.message);
        })));
    }

    public void ClearGame() {
        if(poll != null) {
            StopCoroutine(poll);
        }
        playerModels = null;
        foreach(Player player in Game.players) {
            if(player.PlayerModel != null) {
                Destroy(player.PlayerModel);
            }
        }
        Game = null;
    }

    public void ClearUser() {
        ClearGame();
        User = null;
    }
}
