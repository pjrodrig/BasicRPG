using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class App : MonoBehaviour {

    public MenuUI menu;
    public GameTurnUI gameTurn;
    public CameraModel gameCamera;
    public TestMap testMap;
    public GameObject playerPrefab;
    public User User {get;set;}
    public Game Game {get;set;}
    public Player ActivePlayer;

    void Start() {
        // START test stuff
        User = new User("asdf");
        User.id = 5;
        // END test stuff
        InitClasses();
        InitGame();
    }

    void InitClasses() {
        menu.Init(this);
        gameTurn.Init(this);
        testMap.Init();
    }

    void InitGame() {
        gameCamera.Locked = true;
        menu.Activate();
    }

    public void LoadGame() {
        InitPlayers();
        ContinueGame();
    }

    void InitPlayers() {
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
        }
    }

    void ContinueGame() {
        foreach(Player player in Game.players) {
            if(player.id == User.id) { //Temporary for testing
                ActivePlayer = player;
                break;
            }
        }
        gameCamera.FocusPosition(ActivePlayer.PlayerModel.thisObject.transform.position);
        // ActivePlayer = Game.players[Game.activePlayer];
        // if(ActivePlayer.userId == User.id) {
            gameTurn.Activate();
        // } else {
        //     //Either start polling, or wait for a push notification
        // }
    }

    public void EndTurn() {
        
    }
}
