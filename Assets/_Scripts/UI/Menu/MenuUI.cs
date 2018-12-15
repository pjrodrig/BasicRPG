using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;

public class MenuUI : MonoBehaviour {

    App app;
    bool active;
    
    public GameObject thisObj;
    public CanvasScaler canvasScaler;
    public AuthUI auth;
    public GameSelectUI gameSelect;
    public PlayerSetupUI playerSetup;
    public GameObject gameNotStarted;
    public Text waitingForPlayers;
    public Button returnToGameSelect;
    public GameOptionsUI gameOptions;

    public void Init(App app) {
        this.app = app;
        auth.Init(this);
        gameSelect.Init(app, this);
        playerSetup.Init(this);
        gameOptions.Init(this);
    }
    
    public void Activate() {
        if(!active) {
            thisObj.SetActive(true);
            if(app.User == null) {
                auth.Activate();
            } else if (app.Game == null) {
                gameSelect.Activate();
            } else {
                gameOptions.Activate();
            }
            active = true;
        }
    }

    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false);
            auth.Deactivate();
            gameSelect.Deactivate();
            playerSetup.Deactivate();
            gameOptions.Deactivate();
            active = false;
        }
    }

    public void CompleteAuth(User user) {
        app.User = user;
        gameSelect.Activate();
    }

    public void CompleteGameSelect(Game game) {
        app.Game = game;
        if (game.isStarted) {
            Deactivate();
            app.LoadGame();
        } else {
            Player userPlayer = null;
            StringBuilder sb = new StringBuilder();
            foreach(Player player in game.players) {
                if(!player.isInitialized) {
                    if(player.id == app.User.id) {
                        userPlayer = player;
                        break;
                    } else {
                        sb.Append(player.username);
                        sb.Append("\n");
                    }
                }
            }
            if(userPlayer != null) {
                playerSetup.Activate(game, userPlayer);
            } else if(sb.Length > 0) {
                waitingForPlayers.text = sb.ToString();
                gameNotStarted.SetActive(true);
                returnToGameSelect.onClick.AddListener(ReturnToGameSelect);
            } else {
                StartCoroutine(Rest.Put(API.game, null, game, new Action<Game>(delegate (Game updatedGame) {
                    Deactivate();
                    app.LoadGame();
                }), new Action<RestError>(delegate (RestError err) {
                    Debug.Log(err.message);
                })));
            }
        }
    }

    private void ReturnToGameSelect() {
        gameNotStarted.SetActive(false);
        waitingForPlayers.text = "";
        returnToGameSelect.onClick.RemoveAllListeners();
        gameSelect.Activate();
    }

    public float GetScale() {
        return canvasScaler.referenceResolution.y / Screen.height;
    }
}
