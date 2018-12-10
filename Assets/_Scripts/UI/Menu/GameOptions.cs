using UnityEngine;

public class GameOptions : MonoBehaviour {

    Menu menu;
    bool active = false;

    public GameObject thisObj;

    public void Init(Menu menu) {
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
