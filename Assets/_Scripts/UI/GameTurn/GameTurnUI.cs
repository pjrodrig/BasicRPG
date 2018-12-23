using UnityEngine;
using UnityEngine.UI;

public class GameTurnUI : MonoBehaviour {

    App app;
    bool active;
    bool itemUsed;
    bool scrollUsed;

    public GameObject thisObj;
    public TurnOptionsUI turnOptions;

    public void Init(App app, GameTurn2UI gameTurn2) {
        this.app = app;
        turnOptions.Init(app, this, gameTurn2);
    }

    public void Activate() {
        if(!active) {
            thisObj.SetActive(true);
            turnOptions.Activate();
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false);
            active = false;
        }
    }

}