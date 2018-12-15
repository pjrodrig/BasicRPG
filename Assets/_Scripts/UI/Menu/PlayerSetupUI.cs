using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using System.Text;

public class PlayerSetupUI : MonoBehaviour {

    bool active;
    MenuUI menu;
    Game game;
    Player player;

    public GameObject thisObj;
    public InputField nameInput;
    public Button redButton;
    public Button blueButton;
    public Button greenButton;
    public Button yellowButton;
    public Button purpleButton;
    public GameObject colorSelection;
    public Dropdown sexDropdown;
    public Button lightestButton;
    public Button lightButton;
    public Button mediumButton;
    public Button darkButton;
    public Button darkestButton;
    public GameObject skinSelection;
    public Dropdown classDropdown;
    public Button doneButton;


    public void Init(MenuUI menu) {
        this.menu = menu;
    }

    public void Activate(Game game, Player player) {
        if(!active) {
            thisObj.SetActive(true);
            ActivateInputs();
            this.game = game;
            this.player = player;
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false); 
            DeactivateInputs();
            game = null;
            player = null;
            active = false;
        }
    }

    void ActivateInputs() {
        doneButton.interactable = false;
        nameInput.text = "";
        ActivateColors();
        sexDropdown.value = 0;
        ActivateSkin();
        classDropdown.value = 0;
        doneButton.onClick.AddListener(Done);
    }

    void ActivateColors() {
        colorSelection.SetActive(false);
        redButton.onClick.AddListener(delegate (){PickColor(Appearance.Color.RED, redButton.transform.position);});
        blueButton.onClick.AddListener(delegate (){PickColor(Appearance.Color.BLUE, blueButton.transform.position);});
        greenButton.onClick.AddListener(delegate (){PickColor(Appearance.Color.GREEN, greenButton.transform.position);});
        yellowButton.onClick.AddListener(delegate (){PickColor(Appearance.Color.YELLOW, yellowButton.transform.position);});
        purpleButton.onClick.AddListener(delegate (){PickColor(Appearance.Color.PURPLE, purpleButton.transform.position);});
    }

    void ActivateSkin() {
        skinSelection.SetActive(false);
        lightestButton.onClick.AddListener(delegate (){PickSkinColor(Appearance.SkinColor.LIGHTEST, lightestButton.transform.position);});
        lightButton.onClick.AddListener(delegate (){PickSkinColor(Appearance.SkinColor.LIGHT, lightButton.transform.position);});
        mediumButton.onClick.AddListener(delegate (){PickSkinColor(Appearance.SkinColor.MEDIUM, mediumButton.transform.position);});
        darkButton.onClick.AddListener(delegate (){PickSkinColor(Appearance.SkinColor.DARK, darkButton.transform.position);});
        darkestButton.onClick.AddListener(delegate (){PickSkinColor(Appearance.SkinColor.DARKEST, darkestButton.transform.position);});
    }

    void PickColor(Appearance.Color color, Vector2 position) {
        player.appearance.color = color;
        colorSelection.transform.position = position;
        colorSelection.SetActive(true);
        CheckEnableDone();
    }

    void PickSkinColor(Appearance.SkinColor skinColor, Vector2 position) {
        player.appearance.skinColor = skinColor;
        skinSelection.transform.position = position;
        skinSelection.SetActive(true);
        CheckEnableDone();
    }

    void DeactivateInputs() {
        nameInput.text = "";
        DeactivateColors();
        DeactivateSkin();
        doneButton.onClick.RemoveAllListeners();
    }

    void DeactivateColors() {
        redButton.onClick.RemoveAllListeners();
        blueButton.onClick.RemoveAllListeners();
        greenButton.onClick.RemoveAllListeners();
        yellowButton.onClick.RemoveAllListeners();
        purpleButton.onClick.RemoveAllListeners();
    }

    void DeactivateSkin() {
        lightestButton.onClick.RemoveAllListeners();
        lightButton.onClick.RemoveAllListeners();
        mediumButton.onClick.RemoveAllListeners();
        darkButton.onClick.RemoveAllListeners();
        darkestButton.onClick.RemoveAllListeners();
    }

    void CheckEnableDone() {
        doneButton.interactable = nameInput.text.Length > 0 && 
            player.appearance.color != Appearance.Color.NONE && 
            player.appearance.skinColor != Appearance.SkinColor.NONE;
    }

    void Done() {
        player.name = nameInput.text;
        player.clazz = new Clazz[]{new Warrior(), new Mage(), new Assassin()}[classDropdown.value];
        player.appearance.sex = (Appearance.Sex) sexDropdown.value;
        player.isInitialized = true;
        StartCoroutine(Rest.Put(API.game, null, game, new Action<Game>(delegate (Game game) {
            Deactivate();
            menu.CompleteGameSelect(game);
        }), new Action<RestError>(delegate (RestError err) {
            Debug.Log(err.message);
        })));
    }

}
