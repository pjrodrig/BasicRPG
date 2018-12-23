using UnityEngine;
using UnityEngine.UI;

public class GameOptionsUI : MonoBehaviour {

    App app;
    GameSelectUI gameSelect;
    AuthUI auth;
    MenuUI menu;
    bool active = false;

    public GameObject thisObj;
    public Button gameSelectButton;
    public Button logout;
    public Button backToGame;

    public void Init(App app, GameSelectUI gameSelect, AuthUI auth, MenuUI menu) {
        this.app = app;
        this.gameSelect = gameSelect;
        this.auth = auth;
        this.menu = menu;
    }

    public void Activate() {
        if(!active) {
            thisObj.SetActive(true);
            gameSelectButton.onClick.AddListener(GameSelect);
            logout.onClick.AddListener(Logout);
            backToGame.onClick.AddListener(BackToGame);
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false);
            gameSelectButton.onClick.RemoveAllListeners();
            logout.onClick.RemoveAllListeners();
            backToGame.onClick.RemoveAllListeners();
            active = false;
        }
    }

    void GameSelect() {
        app.ClearGame();
        Deactivate();
        gameSelect.Activate();
    }

    void BackToGame() {
        menu.Deactivate();
    }

    void Logout() {
        app.ClearUser();
        Deactivate();
        auth.Activate();
    }
}
