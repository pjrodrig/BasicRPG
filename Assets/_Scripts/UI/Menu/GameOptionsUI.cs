using UnityEngine;

public class GameOptionsUI : MonoBehaviour {

    MenuUI menu;
    bool active = false;

    public GameObject thisObj;

    public void Init(MenuUI menu) {
        this.menu = menu;
    }

    public void Activate() {
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
