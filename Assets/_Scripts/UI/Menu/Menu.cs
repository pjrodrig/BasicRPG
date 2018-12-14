using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Menu : MonoBehaviour {

    App app;
    bool active;
    
    public GameObject thisObj;
    public CanvasScaler canvasScaler;
    public Auth auth;
    public GameSelect gameSelect;
    public PlayerSetup playerSetup;
    public GameObject gameNotStarted;
    public Text waitingForPlayers;
    public Button returnToGameSelect;
    public GameOptions gameOptions;

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
            app.CompleteGameSelect();
        } else { 
            Player userPlayer = null;
            StringBuilder sb = new StringBuilder();
            foreach(Player player in game.players) {
                if(!player.isInitialized) {
                    if(player.userId == app.User.id) {
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
                Deactivate();
                app.CompleteGameSelect();
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
