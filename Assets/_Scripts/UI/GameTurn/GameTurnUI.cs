using UnityEngine;
using UnityEngine.UI;

public class GameTurnUI : MonoBehaviour {

    App app;
    bool active;
    Player player;
    bool itemUsed;
    bool scrollUsed;

    public GameObject thisObject;
    public TurnOptionsUI turnOptions;

    public void Init(App app) {
        this.app = app;
        turnOptions.Init(this);
    }

    public void Activate(Player player) {
        if(!active) {
            this.player = player;
            thisObject.SetActive(true);
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            this.player = null;
            thisObject.SetActive(false);
            active = false;
        }
    }
}