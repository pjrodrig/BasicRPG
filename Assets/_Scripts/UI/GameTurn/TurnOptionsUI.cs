using UnityEngine;
using UnityEngine.UI;

public class TurnOptionsUI : MonoBehaviour {

    GameTurnUI gameTurn;
    bool active;
    bool itemUsed;
    bool scrollUsed;

    public GameObject thisObject;
    public MoveUI move;
    public SearchUI search;
    public InventoryUI inventory;
    public PlayerDataUI playerData;
    public Button moveButton;
    public Button searchButton;
    public Button inventoryButton;
    public Button playerDataButton;

    public void Init(GameTurnUI gameTurn) {
        this.gameTurn = gameTurn;
        move.Init(this);
        search.Init(this);
        inventory.Init(this);
        playerData.Init(this);
    }

    public void Activate(Player player) {
        if(!active) {
            moveButton.onClick.AddListener(delegate () {
            Deactivate();
            move.Activate(player);
        });
        searchButton.onClick.AddListener(delegate () {
            Deactivate();
            search.Activate();
        });
        inventoryButton.onClick.AddListener(delegate () {
            Deactivate();
            inventory.Activate(player.inventory, itemUsed, scrollUsed);
        });
        playerDataButton.onClick.AddListener(delegate () {
            Deactivate();
            playerData.Activate(player);
        });
            thisObject.SetActive(true);
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            moveButton.onClick.RemoveAllListeners();
            searchButton.onClick.RemoveAllListeners();
            inventoryButton.onClick.RemoveAllListeners();
            playerDataButton.onClick.RemoveAllListeners();
            thisObject.SetActive(false);
            active = false;
        }
    }
}