using UnityEngine;
using UnityEngine.UI;

public class TurnOptionsUI : MonoBehaviour {

    App app;
    bool active;
    bool itemUsed;
    bool scrollUsed;

    public GameObject thisObj;
    public MoveUI move;
    public SearchUI search;
    public InventoryUI inventory;
    public PlayerDataUI playerData;
    public Button moveButton;
    public Button searchButton;
    public Button inventoryButton;
    public Button playerDataButton;

    public void Init(App app, GameTurnUI gameTurn, GameTurn2UI gameTurn2) {
        this.app = app;
        move.Init(app, gameTurn, gameTurn2, this);
        search.Init(this);
        inventory.Init(this);
        playerData.Init(this);
    }

    public void Activate() {
        if(!active) {
            moveButton.onClick.AddListener(delegate () {
                Deactivate();
                move.Activate();
            });
            searchButton.onClick.AddListener(delegate () {
                Deactivate();
                search.Activate(app.ActivePlayer.PlayerModel.thisObj.transform.position);
            });
            inventoryButton.onClick.AddListener(delegate () {
                Deactivate();
                inventory.Activate(app.ActivePlayer.playerData.inventory, itemUsed, scrollUsed);
            });
            playerDataButton.onClick.AddListener(delegate () {
                Deactivate();
                playerData.Activate(app.ActivePlayer);
            });
            thisObj.SetActive(true);
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            moveButton.onClick.RemoveAllListeners();
            searchButton.onClick.RemoveAllListeners();
            inventoryButton.onClick.RemoveAllListeners();
            playerDataButton.onClick.RemoveAllListeners();
            thisObj.SetActive(false);
            active = false;
        }
    }
}