using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MoveUI : MonoBehaviour {

    readonly float ROLL_TRANSITION_TIME = 0.5F;

    App app;
    GameTurnUI gameTurn;
    TurnOptionsUI turnOptions;
    Vector3 initialRollPosition;
    bool active;
    bool rolling;
    bool choosingSpace;
    SortedDictionary<string, List<Space>> pathOptions;
    Vector3 mouseDownPos;

    public GameObject thisObject;
    public CameraModel gameCamera;
    public GameObject rollBox;
    public Text roll;
    public GameObject rollCornerPos;
    public GameObject buttons;
    public Button stop;
    public Button back;
    public SpaceVisuals spaceVisuals;


    public void Init(App app, GameTurnUI gameTurn, TurnOptionsUI turnOptions) {
        this.app = app;
        this.gameTurn = gameTurn;
        this.turnOptions = turnOptions;
    }

    public void Update() {
        if(choosingSpace) {
            SpaceClickCheck();
        }
    }

    public void Activate() {
        if(!active) {
            if(initialRollPosition == Vector3.zero) {
                initialRollPosition = roll.transform.position;
            } else {
                roll.transform.position = initialRollPosition;
            }
            choosingSpace = false;
            ActivateButtons();
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

    void ActivateButtons() {
        stop.onClick.AddListener(delegate () {
            StopRoll();
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

    void StopRoll() {
        rolling = false;
        int finalRoll = (int)Mathf.Floor(UnityEngine.Random.value * 6) + 1;
        roll.text = finalRoll + "";
        DeactivateButtons();
        StartCoroutine(Transition(finalRoll));
    }

    IEnumerator Transition(int finalRoll) {
        yield return new WaitForSeconds(1F);
        Vector3 startPos = this.rollBox.transform.position;
        Vector3 endPos = rollCornerPos.transform.position;
        yield return LocationUtil.SlerpVector(this.rollBox.transform, endPos, ROLL_TRANSITION_TIME);
        DisplayChoices(finalRoll);
    }

    public void DisplayChoices(int roll) {
        List<List<Space>> paths = MapUtil.FindPaths(roll, app.ActivePlayer);
        pathOptions = new SortedDictionary<string, List<Space>>();
        List<Vector3> toHighlight = new List<Vector3>();
        Vector3 pos;
        foreach(List<Space> path in paths) {
            pos = path[path.Count - 1].position;
            toHighlight.Add(pos);
            pathOptions.Add(pos.x + "_" + pos.z, path);
        }
        spaceVisuals.HighlightSpaces(toHighlight);
        gameCamera.ZoomOutToMap();
        gameCamera.Locked = false;
        choosingSpace = true;
    }

    void SpaceClickCheck() {
        if (Input.GetMouseButtonUp(0) && mouseDownPos == Input.mousePosition){
            Ray ray = this.gameCamera.thisObj.ScreenPointToRay(mouseDownPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                List<Space> path;
                Vector3 hitPos = hit.transform.position;
                pathOptions.TryGetValue(hitPos.x + "_" + hitPos.z, out path);
                choosingSpace = false;
                spaceVisuals.EndHighlight();
                mouseDownPos = Vector3.zero;
                app.ActivePlayer.PlayerModel.TraversePath(path, gameCamera, gameTurn.GetSpaceEvent);
                Deactivate();
            }
        } else if(Input.GetMouseButtonDown(0)) {
            mouseDownPos = Input.mousePosition;
        }
    }
}