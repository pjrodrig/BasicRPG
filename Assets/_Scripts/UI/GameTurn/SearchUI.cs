using UnityEngine;

public class SearchUI : MonoBehaviour {

    TurnOptionsUI turnOptions;
    bool active;

    public GameObject thisObject;
    public CameraModel gameCamera;

    public void Init(TurnOptionsUI turnOptions) {
        this.turnOptions = turnOptions;
    }

    public void Activate() {
        if(!active) {
            thisObject.SetActive(true);
            gameCamera.ZoomOutToMap();
            gameCamera.Locked = false;
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