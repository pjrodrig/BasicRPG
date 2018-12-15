using UnityEngine;
using UnityEngine.UI;

public class GameTurnUI : MonoBehaviour {

    App app;
    bool active;
    bool itemUsed;
    bool scrollUsed;

    public GameObject thisObject;
    public TurnOptionsUI turnOptions;

    public void Init(App app) {
        this.app = app;
        turnOptions.Init(app, this);
    }

    public void Activate() {
        if(!active) {
            thisObject.SetActive(true);
            turnOptions.Activate();
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObject.SetActive(false);
            active = false;
        }
    }
}