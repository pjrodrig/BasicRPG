using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour {

    private readonly float DICE_ROLL_DISPLAY_TRANSITION_TIME = 0.5F;

    public GameObject go;

    public GameObject turnMenu;
    public Button turnMenu_move;
    public Button turnMenu_inventory;
    public Button turnMenu_look;
    public Button turnMenu_options;
    public GameObject diceRoll;
    public Text diceRoll_display;
    public GameObject diceRoll_buttons;
    public Button diceRoll_stop;
    public Button diceRoll_cancel;

    private TurnManager turnManager;
    private GameCamera playerCamera;

    private bool rollingDice = false;

    public void Initialize(TurnManager turnManager, GameCamera playerCamera) {
        this.turnManager = turnManager;
        this.playerCamera = playerCamera;
        this.turnMenu_move.onClick.AddListener(TurnMenu_Move);
        this.turnMenu_inventory.onClick.AddListener(TurnMenu_Inventory);
        this.turnMenu_look.onClick.AddListener(TurnMenu_Look);
        this.turnMenu_options.onClick.AddListener(TurnMenu_Options);
        this.diceRoll_stop.onClick.AddListener(DiceRoll_Stop);
        this.diceRoll_cancel.onClick.AddListener(DiceRoll_Cancel);
    }

    public void DisplayTurnMenu() {
        this.turnMenu.SetActive(true);
    }

    void TurnMenu_Move() {
        this.turnMenu.SetActive(false);
        this.diceRoll.SetActive(true);
        this.rollingDice = true;
        StartCoroutine(DisplayRandomNumbers());
        StartCoroutine(playerCamera.FocusMap());
    }

    void TurnMenu_Inventory() {
        this.turnMenu.SetActive(false);
        //TODO
    }

    void TurnMenu_Look() {
        this.turnMenu.SetActive(false);
        //TODO
    }

    void TurnMenu_Options() {
        this.turnMenu.SetActive(false);
        //TODO
    }

    void DiceRoll_Stop() {
        this.rollingDice = false;
        int roll = (int)Mathf.Floor(UnityEngine.Random.value * 6) + 1;
        this.diceRoll_display.text = roll + "";
        this.diceRoll_buttons.SetActive(false);
        StartCoroutine(TransformDiceDisplay(roll));
    }

    void DiceRoll_Cancel() {
        this.rollingDice = false;
        this.diceRoll_display.text = "";
        this.diceRoll.SetActive(false);
        this.turnMenu.SetActive(true);
    }

    IEnumerator DisplayRandomNumbers() {
        float last = 0;
        float randomNumber;
        while(this.rollingDice) {
            while(this.rollingDice && (randomNumber = Mathf.Floor(UnityEngine.Random.value * 6) + 1) != last) {
                last = randomNumber;
                this.diceRoll_display.text = randomNumber + "";
                yield return new WaitForSeconds(0.05F);
            }
        }
    }

    IEnumerator TransformDiceDisplay(int roll) {
        yield return new WaitForSeconds(1F);
        Vector3 startPos = this.diceRoll_display.transform.position;
        Vector3 endPos = new Vector3(420, 200, 0) + startPos; //TODO: fix this
        StartCoroutine(ReturnDiceRollValue(roll));
        yield return LocationUtil.SlerpVector(this.diceRoll_display.transform, endPos, DICE_ROLL_DISPLAY_TRANSITION_TIME);
    }

    IEnumerator ReturnDiceRollValue(int roll) {
        yield return new WaitForSeconds(DICE_ROLL_DISPLAY_TRANSITION_TIME);
        this.turnManager.ChooseMovement(roll);
    }

    public void ResetDiceDisplay() {
        this.diceRoll_buttons.SetActive(true);
        this.diceRoll_display.transform.position = new Vector3(486F, 292F, 0);
        this.diceRoll_display.text = "";
        this.diceRoll.SetActive(false);
    }

}
