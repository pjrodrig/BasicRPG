using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MoveUI : MonoBehaviour {

    readonly float ROLL_TRANSITION_TIME = 0.5F;

    TurnOptionsUI turnOptions;
    Vector3 initialRollPosition;
    bool active;
    bool rolling;
    bool choosingSpace;
    SortedDictionary<string, List<Space>> pathOptions;

    public GameObject thisObject;
    public CameraModel gameCamera;
    public GameObject rollBox;
    public Text roll;
    public GameObject rollCornerPos;
    public GameObject buttons;
    public Button stop;
    public Button back;
    public SpaceVisuals spaceVisuals;

    public void Init(TurnOptionsUI turnOptions) {
        this.turnOptions = turnOptions;
    }

    public void Activate(Player player) {
        if(!active) {
            roll.transform.position = initialRollPosition;
            choosingSpace = false;
            ActivateButtons(player);
            thisObject.SetActive(true);
            active = true;
            StartCoroutine(StartRoll());
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObject.SetActive(false);
            rolling = false;
            active = false;
        }
    }

    void ActivateButtons(Player player) {
        stop.onClick.AddListener(delegate () {
            StopRoll(player);
        });
        back.onClick.AddListener(delegate () {
            Deactivate();
            turnOptions.Activate();
        });
        buttons.SetActive(true);
    }

    void DeactivateButtons() {
        stop.onClick.RemoveAllListeners();
        back.onClick.RemoveAllListeners();
        buttons.SetActive(false);
    }

    IEnumerator StartRoll() {
        float last = 0;
        float randomNumber;
        rolling = true;
        while(rolling) {
            while(rolling && (randomNumber = Mathf.Floor(UnityEngine.Random.value * 6) + 1) != last) {
                last = randomNumber;
                this.roll.text = randomNumber + "";
                yield return new WaitForSeconds(0.05F);
            }
        }
    }

    void StopRoll(Player player) {
        rolling = false;
        int finalRoll = (int)Mathf.Floor(UnityEngine.Random.value * 6) + 1;
        roll.text = roll + "";
        DeactivateButtons();
        StartCoroutine(Transition(finalRoll, player));
    }

    IEnumerator Transition(int finalRoll, Player player) {
        yield return new WaitForSeconds(1F);
        Vector3 startPos = this.rollBox.transform.position;
        Vector3 endPos = rollCornerPos.transform.position;
        yield return LocationUtil.SlerpVector(this.rollBox.transform, endPos, ROLL_TRANSITION_TIME);
        DisplayChoices(finalRoll, player);
    }

    public void DisplayChoices(int roll, Player player) {
        List<List<Space>> paths = MapUtil.FindPaths(roll, player);
        pathOptions = new SortedDictionary<string, List<Space>>();
        List<Vector3> toHighlight = new List<Vector3>();
        Vector3 pos;
        foreach(List<Space> path in paths) {
            pos = path[path.Count - 1].position;
            toHighlight.Add(pos);
            pathOptions.Add(pos.x + "_" + pos.z, path);
        }
        spaceVisuals.HighlightSpaces(toHighlight);
        choosingSpace = true;
    }

    void SpaceClickCheck() {
        if (Input.GetMouseButtonDown(0)){
            Ray ray = this.gameCamera.thisObj.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                List<Space> path;
                Vector3 hitPos = hit.transform.position;
                pathOptions.TryGetValue(hitPos.x + "_" + hitPos.z, out path);
                choosingSpace = false;
                spaceVisuals.EndHighlight();
                // playerUI.ResetDiceDisplay();
                // currentPlayer.TraversePath(path);
            }
        }
    }
}