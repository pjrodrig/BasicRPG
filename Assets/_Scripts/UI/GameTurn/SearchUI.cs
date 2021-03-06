using UnityEngine;
using UnityEngine.UI;

public class SearchUI : MonoBehaviour {

    TurnOptionsUI turnOptions;
    bool active;

    public GameObject thisObj;
    public CameraModel gameCamera;
    public Button back;

    public void Init(TurnOptionsUI turnOptions) {
        this.turnOptions = turnOptions;
    }

    public void Activate(Vector3 playerPos) {
        if(!active) {
            thisObj.SetActive(true);
            gameCamera.ZoomOutToMap();
            gameCamera.Locked = false;
            back.onClick.AddListener(delegate () {
                Deactivate(playerPos);
                turnOptions.Activate();
            });
            active = true;
        }
    }
    
    public void Deactivate(Vector3 playerPos) {
        if(active) {
            thisObj.SetActive(false);
            back.onClick.RemoveAllListeners();
            gameCamera.FocusPosition(playerPos);
            gameCamera.Locked = true;
            active = false;
        }
    }
}