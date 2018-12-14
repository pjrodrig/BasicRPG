using UnityEngine;

public class InventoryUI : MonoBehaviour {

    TurnOptionsUI turnOptions;
    bool active;

    public GameObject thisObject;

    public void Init(TurnOptionsUI turnOptions) {
        this.turnOptions = turnOptions;
    }

    public void Activate(Inventory inventory, bool itemUsed, bool scrollUsed) {
        if(!active) {
            thisObject.SetActive(true);
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