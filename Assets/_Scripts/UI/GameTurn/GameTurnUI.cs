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

    public void GetSpaceEvent(Space space) {
        switch(space.type) {
            case Space.Type.EVENT:
                StartEvent();
                break;
            case Space.Type.ITEM_SHOP:
                break;
            case Space.Type.MAGIC_SHOP:
                break;
            case Space.Type.WEAPON_SHOP:
                break;
        }
    }

    void StartEvent() {
        
    }
}