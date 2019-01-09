using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MoveUI : MonoBehaviour {

    readonly float ROLL_TRANSITION_TIME = 0.5F;

    App app;
    GameTurnUI gameTurn;
    GameTurn2UI gameTurn2;
    TurnOptionsUI turnOptions;
    Vector3 initialRollPosition;
    bool active;
    bool rolling;
    bool choosingSpace;
    SortedDictionary<string, List<Space>> pathOptions;
    Vector3 mouseDownPos;
    MovementModifier mod;
    Action modCallback;

    public GameObject thisObj;
    public CameraModel gameCamera;
    public GameObject rollBox;
    public Text roll;
    public GameObject rollCornerPos;
    public GameObject buttons;
    public Button stop;
    public Button back;
    public SpaceVisuals spaceVisuals;
    public Text multiply;


    public void Init(App app, GameTurnUI gameTurn, GameTurn2UI gameTurn2, TurnOptionsUI turnOptions) {
        this.app = app;
        this.gameTurn = gameTurn;
        this.gameTurn2 = gameTurn2;
        this.turnOptions = turnOptions;
    }

    public void Update() {
        if(choosingSpace) {
            SpaceClickCheck();
        }
    }

    public void Activate(MovementModifier mod, Action modCallback) {
        if(!active) {
            thisObj.SetActive(true);
            this.mod = mod;
            this.modCallback = modCallback;
            if(initialRollPosition == Vector3.zero) {
                initialRollPosition = rollBox.transform.position;
            } else {
                rollBox.transform.position = initialRollPosition;
            }
            choosingSpace = false;
            ActivateButtons();
            StartCoroutine(StartRoll());
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false);
            mod = null;
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
        int max = 6;
        if (mod is MovementSetter) {
            modCallback();
            StartCoroutine(Transition(((MovementSetter)mod).GetValue()));
        } else {
            if(mod is MovementDie) {
                max = ((MovementDie) mod).GetDie();
            }
            while(rolling) {
                while(rolling && (randomNumber = Mathf.Floor(UnityEngine.Random.value * max) + 1) != last) {
                    last = randomNumber;
                    roll.text = randomNumber + "";
                    yield return new WaitForSeconds(0.05F);
                }
            }
        }
    }

    void StopRoll() {
        rolling = false;
        int finalRoll = (int)Mathf.Floor(UnityEngine.Random.value * 6) + 1;
        roll.text = finalRoll + "";
        DeactivateButtons();
        if(mod is MovementMultiplier) {
            StartCoroutine(Multiply(finalRoll, ((MovementMultiplier)mod).GetMultiplier()));
        } else {
            StartCoroutine(Transition(finalRoll));
        }
        if(mod != null) {
            modCallback();
        }
    }

    IEnumerator Multiply(int finalRoll, int multiple) {
        Color color;
        multiply.text = "x" + multiple;
        for(int i = 0; i <= 10; i++) {
            color = multiply.color;
            color.a = i/10F;
            multiply.color = color;
            yield return new WaitForSeconds(0.1F);
        }
        finalRoll = finalRoll * multiple;
        roll.text = finalRoll + "";
        for(int i = 10; i >= 0; i--) {
            color = multiply.color;
            color.a = i/10F;
            multiply.color = color;
            yield return new WaitForSeconds(0.1F);
        }
        StartCoroutine(Transition(finalRoll));
    }

    IEnumerator Transition(int finalRoll) {
        yield return new WaitForSeconds(1F);
        Vector3 startPos = rollBox.transform.position;
        Vector3 endPos = rollCornerPos.transform.position;
        yield return LocationUtil.SlerpVector(rollBox.transform, endPos, ROLL_TRANSITION_TIME);
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
            Ray ray = gameCamera.thisObj.ScreenPointToRay(mouseDownPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                List<Space> path;
                Vector3 hitPos = hit.transform.position;
                pathOptions.TryGetValue(hitPos.x + "_" + hitPos.z, out path);
                choosingSpace = false;
                spaceVisuals.EndHighlight();
                mouseDownPos = Vector3.zero;
                app.ActivePlayer.PlayerModel.TraversePath(path, gameCamera, gameTurn2.Activate);
                Deactivate();
                gameTurn.Deactivate();
            }
        } else if(Input.GetMouseButtonDown(0)) {
            mouseDownPos = Input.mousePosition;
        }
    }
}