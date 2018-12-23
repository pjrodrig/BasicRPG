using UnityEngine;

public class InventoryUI : MonoBehaviour {

    TurnOptionsUI turnOptions;
    bool active;

    public GameObject thisObj;

    public void Init(TurnOptionsUI turnOptions) {
        this.turnOptions = turnOptions;
    }

    public void Activate(Inventory inventory, bool itemUsed, bool scrollUsed) {
        if(!active) {
            thisObj.SetActive(true);
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