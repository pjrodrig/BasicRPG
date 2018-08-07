using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    private float GRID_OFFSET_X = 100F;
    private float GRID_OFFSET_Y = 3.2F;
    private float GRID_OFFSET_Z = 50F;

    public GameObject playerCamera;
    public GameObject player;
    public GameObject turnMenu;
    public Text diceDisplay;
    public GameObject stopRollButton;
    public GameObject diceRollContainer;
    public GameObject spacesObject;

    private bool rollingDice;

    private TestMap map;
    private Space currentSpace;

	// Use this for initialization
	private void Start () {
        this.map = new TestMap(spacesObject);
        this.currentSpace = this.map.GetStart();
        Vector3 startingPos = this.currentSpace.position;
        player.transform.SetPositionAndRotation(
            new Vector3(startingPos.x, startingPos.y + 0.2F, startingPos.z),
            player.transform.rotation
        );
        FocusPlayer();
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

    SortedDictionary<string, GameObject> HighlightSpaces(List<List<Space>> paths) {
        SortedDictionary<string, GameObject> highlights = new SortedDictionary<string, GameObject>();
        foreach(List<Space> path in paths) {

        }
        return highlights;
    }
}
