using UnityEngine;
using UnityEngine.UI;

public class GameOverlay : MonoBehaviour {

    public MenuUI menu;
    public Button openMenu;
    public Text announcement;

    void Start() {
        openMenu.onClick.AddListener(delegate () {
            menu.Activate();
        });
    }
}