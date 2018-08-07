using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    private float GRID_OFFSET_X = 100F;
    private float GRID_OFFSET_Y = 3.2F;
    private float GRID_OFFSET_Z = 50F;

    public Camera playerCamera;
    public GameObject player;
    public GameObject turnMenu;
    public Text diceDisplay;
    public GameObject stopRollButton;
    public GameObject diceRollContainer;
    public GameObject spacesObject;
    public GameObject spaceHighlight;
    public GameObject spacePointer;

    private bool rollingDice;
    private bool choosingSpace;
    private SortedDictionary<string, List<Space>> pathOptions;
    private TestMap map;
    private Space currentSpace;

	void Start() {
        this.map = new TestMap(spacesObject);
        this.currentSpace = this.map.GetStart();
        Vector3 startingPos = this.currentSpace.position;
        player.transform.SetPositionAndRotation(
            new Vector3(startingPos.x, startingPos.y + 0.2F, startingPos.z),
            player.transform.rotation
        );
        FocusPlayer();
	}

    void Update() {
        if(choosingSpace) {
            SpaceClickCheck();
        }
    }

    void FocusPlayer() {
        Vector3 playerPos =player.transform.position;
        playerCamera.transform.SetPositionAndRotation(
            new Vector3(playerPos.x, playerPos.y + 7F, playerPos.z - 10F),
            playerCamera.transform.rotation
        );
    }

    IEnumerator FocusMap() {
        Vector3 startPos = playerCamera.transform.position;
        Vector3 endPos = new Vector3(0, 10, -10) + startPos;
        float startTime = Time.time;
        float fracComplete = 0;
        while(fracComplete < 1) {
            fracComplete = (Time.time - startTime) / 0.5F;
            playerCamera.transform.position = Vector3.Slerp(startPos, endPos, fracComplete);
            yield return new WaitForSeconds(0.00001F);
        }
    }

    public void RollDice() {
        turnMenu.SetActive(false);
        diceRollContainer.SetActive(true);
        rollingDice = true;
        StartCoroutine(RandomDiceNumber());
        StartCoroutine(FocusMap());
    }

    IEnumerator RandomDiceNumber() {
        float last = 0;
        float randomNumber;
        while(rollingDice) {
            while(rollingDice && (randomNumber = Mathf.Floor(UnityEngine.Random.value * 6) + 1) != last) {
                last = randomNumber;
                diceDisplay.text = randomNumber + "";
                yield return new WaitForSeconds(0.05F);
            }
        }
    }

    public void StopRollingDice() {
        rollingDice = false;
        stopRollButton.SetActive(false);
        int currentRoll = (int)Mathf.Floor(UnityEngine.Random.value * 6) + 1;
        diceDisplay.text = currentRoll + "";
        StartCoroutine(PromptForMovement(currentRoll));
    }

    IEnumerator PromptForMovement(int currentRoll) {
        yield return new WaitForSeconds(1F);
        Vector3 startPos = diceDisplay.transform.position;
        Vector3 endPos = new Vector3(420, 200, 0) + startPos;
        float startTime = Time.time;
        float fracComplete = 0;
        while(fracComplete < 1) {
            fracComplete = (Time.time - startTime) / 0.5F;
            diceDisplay.transform.position = Vector3.Slerp(startPos, endPos, fracComplete);
            yield return new WaitForSeconds(0.00001F);
        }
        HighlightSpaces(FindPaths(currentRoll));
    }

    List<List<Space>> FindPaths(int currentRoll) {
        return FindPaths(this.currentSpace, currentRoll, currentSpace,
            new List<Space>(), new SortedDictionary<string, bool>());
    }

    //TODO: break up this method
    List<List<Space>> FindPaths(Space location, int steps, Space previous, List<Space>
        endpoints, SortedDictionary<string, bool> checkedSteps) {
        List<List<Space>> paths = new List<List<Space>>();
        if(steps == 0) {
            if(endpoints.IndexOf(location) == -1) {
                endpoints.Add(location);
                List<Space> newList = new List<Space>();
                newList.Add(location);
                paths.Add(newList);
            }
        } else {
            Space otherSpace;
            string key;
            //TODO: reduce all of the toStrings in here to one
            foreach(Edge edge in location.edges) {
                otherSpace = edge.GetOther(location);
                key = previous.ToString() + "-" + otherSpace.ToString() + "-" + steps;
                //checked steps ensures that only one branch of previous -> otherSpace at n steps will be explored
                if(otherSpace != previous && !checkedSteps.ContainsKey(key)) {
                    checkedSteps.Add(key, true);
                    foreach(List<Space> path in FindPaths(otherSpace, steps - 1, location, endpoints, checkedSteps)) {
                        if(path.Count > 0) {
                            path.Insert(0, location);
                            paths.Add(path);
                        }
                    };
                }
            }
        }
        return paths;
    }

    void HighlightSpaces(List<List<Space>> paths) {
        this.choosingSpace = true;
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        Vector3 highlightOffset = new Vector3(0, -0.01F, 0);
        Vector3 pointerOffset = new Vector3(0, 7F, 0);
        Vector3 pos;
        this.pathOptions = new SortedDictionary<string, List<Space>>();
        foreach(List<Space> path in paths) {
            pos = path[path.Count - 1].position;
            pathOptions.Add(pos.x + "_" + pos.y, path);
            StartCoroutine(SpaceHighlightPulse(Instantiate(this.spaceHighlight, pos + highlightOffset, rotation)));
            StartCoroutine(SpacePointerBob(Instantiate(this.spacePointer, pos + pointerOffset, rotation)));
        }
    }

    IEnumerator SpaceHighlightPulse(GameObject spaceHighlight) {
        spaceHighlight.SetActive(true);
        bool grow = true;
        int count = 0;
        while(choosingSpace) {
            count++;
            if(grow) {
                spaceHighlight.transform.localScale += new Vector3(0.02F, 0, 0.02F);
            } else {
                spaceHighlight.transform.localScale += new Vector3(-0.02F, 0, -0.02F);
            }
            if (count == 50) {
                count = 0;
                grow = !grow;
            }
            yield return new WaitForSeconds(0.01F);
        }
        Destroy(spaceHighlight);
    }

    IEnumerator SpacePointerBob(GameObject spacePointer) {
        spacePointer.SetActive(true);
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        bool rise = true;
        int count = 0;
        while(choosingSpace) {
            count++;
            if(rise) {
                spacePointer.transform.SetPositionAndRotation(spacePointer.transform.position + new Vector3(0, 0.025F, 0), rotation);
            } else {
                spacePointer.transform.SetPositionAndRotation(spacePointer.transform.position + new Vector3(0, -0.025F, 0), rotation);
            }
            if(count == 50) {
                count = 0;
                rise = !rise;
            }
            yield return new WaitForSeconds(0.01F);
        }
        Destroy(spacePointer);
    }

    void SpaceClickCheck() {
        if (Input.GetMouseButtonDown(0)){
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                List<Space> path;
                Vector3 hitPos = hit.transform.position;
                pathOptions.TryGetValue(hitPos.x + "_" + hitPos.z, out path);
                this.choosingSpace = false;
            }
        }
    }

    void MovePlayer(List<Space> path) {
        //TODO: implement player movement
    }
}
