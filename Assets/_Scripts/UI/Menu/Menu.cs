using UnityEngine;

public class Menu : MonoBehaviour {

    App app;
    bool active;
    
    public GameObject thisObj;
    public Auth auth;
    public GameSelect gameSelect;
    public GameOptions gameOptions;

    public void Init(App app) {
        this.app = app;
        auth.Init(this);
        gameSelect.Init(app, this);
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
            gameOptions.Deactivate();
            active = false;
        }
    }

    public void CompleteAuth(User user) {
        app.User = user;
        if (app.Game == null) {
            gameSelect.Activate();
        } else {
            app.CompleteGameSelect();
        }
    }

    public void CompleteGameSelect(Game game) {
        app.Game = game;
        app.CompleteGameSelect();
    }
}
