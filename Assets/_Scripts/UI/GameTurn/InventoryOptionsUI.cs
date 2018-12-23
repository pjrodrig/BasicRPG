using UnityEngine;

public class InventoryOptionsUI : MonoBehaviour {

    Inventory inventory;
    bool active;

    public GameObject thisObj;

    public void Init(Inventory inventory) {
        this.inventory = inventory;
    }

    public void Activate(Player player) {
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