using UnityEngine;

public class InventoryOptionsUI : MonoBehaviour {

    Inventory inventory;
    bool active;

    public GameObject thisObject;

    public void Init(Inventory inventory) {
        this.inventory = inventory;
    }

    public void Activate(Player player) {
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