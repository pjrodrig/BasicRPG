using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    private int NUM_PLAYERS = 3;
    private readonly Quaternion FORWARD = Quaternion.LookRotation(Vector3.forward);

    public GameCamera playerCamera;
    public PlayerUI playerUI;



    public GameObject turnMenu;
    public Text diceDisplay;
    public GameObject stopRollButton;
    public GameObject spacesObject;
    public GameObject spaceHighlight;
    public GameObject spacePointer;
    public GameObject playerModel;

    private List<Player> players;
    private TurnManager turnManager;
    private EventManager eventManager;

	void Start() {
        var map = new TestMap(spacesObject);
        InitPlayers(map.GetStart().position);
        this.turnManager = new TurnManager(this, players, playerCamera);
        this.eventManager = new  EventManager(players);
	}

    void InitPlayers(Vector3 initialPosition) {
        players = new List<Player>();
        Player player;
        for(int i = 0; i < NUM_PLAYERS; i++) {
            player = Instantiate(this.playerModel, initialPosition, FORWARD)
                .GetComponent(typeof(Player)) as Player;
            player.Initialize();
            players.Add(player);
        }
    }

    void Update() {
        turnManager.Update();
    }

    // public void RollDice() {
    //     turnMenu.SetActive(false);
    //     stopRollButton.SetActive(true);
    //     diceRollContainer.SetActive(true);
    //     rollingDice = true;
    //     StartCoroutine(RandomDiceNumber());
    //     StartCoroutine(FocusMap());
    // }
    //
    // IEnumerator RandomDiceNumber() {
    //     float last = 0;
    //     float randomNumber;
    //     while(rollingDice) {
    //         while(rollingDice && (randomNumber = Mathf.Floor(UnityEngine.Random.value * 6) + 1) != last) {
    //             last = randomNumber;
    //             diceDisplay.text = randomNumber + "";
    //             yield return new WaitForSeconds(0.05F);
    //         }
    //     }
    // }
    //
    // public void StopRollingDice() {
    //     rollingDice = false;
    //     stopRollButton.SetActive(false);
    //     int currentRoll = (int)Mathf.Floor(UnityEngine.Random.value * 6) + 1;
    //     diceDisplay.text = currentRoll + "";
    //     StartCoroutine(PromptForMovement(currentRoll));
    // }
    //
    // IEnumerator PromptForMovement(int currentRoll) {
    //     yield return new WaitForSeconds(1F);
    //     Vector3 startPos = diceDisplay.transform.position;
    //     Vector3 endPos = new Vector3(420, 200, 0) + startPos;
    //     float startTime = Time.time;
    //     float fracComplete = 0;
    //     while(fracComplete < 1) {
    //         fracComplete = (Time.time - startTime) / 0.5F;
    //         diceDisplay.transform.position = Vector3.Slerp(startPos, endPos, fracComplete);
    //         yield return new WaitForSeconds(0.00001F);
    //     }
    //     HighlightSpaces(FindPaths(currentRoll));
    // }
    //
    // List<List<Space>> FindPaths(int currentRoll) {
    //     return FindPaths(this.currentSpace, currentRoll, currentSpace,
    //         new List<Space>(), new SortedDictionary<string, bool>());
    // }
    //
    // //TODO: break up this method
    // List<List<Space>> FindPaths(Space location, int steps, Space previous, List<Space>
    //     endpoints, SortedDictionary<string, bool> checkedSteps) {
    //     List<List<Space>> paths = new List<List<Space>>();
    //     if(steps == 0) {
    //         if(endpoints.IndexOf(location) == -1) {
    //             endpoints.Add(location);
    //             List<Space> newList = new List<Space>();
    //             newList.Add(location);
    //             paths.Add(newList);
    //         }
    //     } else {
    //         Space otherSpace;
    //         string key;
    //         //TODO: reduce all of the toStrings in here to one
    //         foreach(Edge edge in location.edges) {
    //             otherSpace = edge.GetOther(location);
    //             key = previous.ToString() + "-" + otherSpace.ToString() + "-" + steps;
    //             //checked steps ensures that only one branch of previous -> otherSpace at n steps will be explored
    //             if(otherSpace != previous && !checkedSteps.ContainsKey(key)) {
    //                 checkedSteps.Add(key, true);
    //                 foreach(List<Space> path in FindPaths(otherSpace, steps - 1, location, endpoints, checkedSteps)) {
    //                     if(path.Count > 0) {
    //                         path.Insert(0, location);
    //                         paths.Add(path);
    //                     }
    //                 };
    //             }
    //         }
    //     }
    //     return paths;
    // }
    //
    // void HighlightSpaces(List<List<Space>> paths) {
    //     this.choosingSpace = true;
    //     Quaternion rotation = new Quaternion(0, 0, 0, 0);
    //     Vector3 highlightOffset = new Vector3(0, -0.01F, 0);
    //     Vector3 pointerOffset = new Vector3(0, 7F, 0);
    //     Vector3 pos;
    //     this.pathOptions = new SortedDictionary<string, List<Space>>();
    //     foreach(List<Space> path in paths) {
    //         pos = path[path.Count - 1].position;
    //         pathOptions.Add(pos.x + "_" + pos.z, path);
    //         StartCoroutine(SpaceHighlightPulse(Instantiate(this.spaceHighlight, pos + highlightOffset, rotation)));
    //         StartCoroutine(SpacePointerBob(Instantiate(this.spacePointer, pos + pointerOffset, rotation)));
    //     }
    // }
    //
    // IEnumerator SpaceHighlightPulse(GameObject spaceHighlight) {
    //     spaceHighlight.SetActive(true);
    //     bool grow = true;
    //     int count = 0;
    //     while(choosingSpace) {
    //         count++;
    //         if(grow) {
    //             spaceHighlight.transform.localScale += new Vector3(0.02F, 0, 0.02F);
    //         } else {
    //             spaceHighlight.transform.localScale += new Vector3(-0.02F, 0, -0.02F);
    //         }
    //         if (count == 50) {
    //             count = 0;
    //             grow = !grow;
    //         }
    //         yield return new WaitForSeconds(0.01F);
    //     }
    //     Destroy(spaceHighlight);
    // }
    //
    // IEnumerator SpacePointerBob(GameObject spacePointer) {
    //     spacePointer.SetActive(true);
    //     Quaternion rotation = new Quaternion(0, 0, 0, 0);
    //     bool rise = true;
    //     int count = 0;
    //     while(choosingSpace) {
    //         count++;
    //         if(rise) {
    //             spacePointer.transform.SetPositionAndRotation(spacePointer.transform.position + new Vector3(0, 0.025F, 0), rotation);
    //         } else {
    //             spacePointer.transform.SetPositionAndRotation(spacePointer.transform.position + new Vector3(0, -0.025F, 0), rotation);
    //         }
    //         if(count == 50) {
    //             count = 0;
    //             rise = !rise;
    //         }
    //         yield return new WaitForSeconds(0.01F);
    //     }
    //     Destroy(spacePointer);
    // }
    //
    // //TODO: break up this method
    // IEnumerator MovePlayer(List<Space> path) {
    //     FocusPlayer();
    //     Vector3 offset = new Vector3(0, 0.2F, 0);
    //     Vector3 startPos = Vector3.zero;
    //     Vector3 endPos;
    //     float startTime;
    //     float fracComplete;
    //     float speed;
    //     foreach(Space space in path) {
    //         if(startPos != Vector3.zero) {
    //             endPos = space.position + offset;
    //             Vector3 dirFromAtoB = (endPos - player.transform.position).normalized;
    //             float dotProd = Vector3.Dot(dirFromAtoB, player.transform.forward);
    //             if(dotProd <= 0.9) {
    //                 StartCoroutine(FaceTarget(player.transform, endPos));
    //                 yield return new WaitForSeconds(0.2F);
    //             }
    //             speed = Vector3.Distance(startPos, endPos) / 20F;
    //             startTime = Time.time;
    //             fracComplete = 0;
    //             while(fracComplete < 1) {
    //                 fracComplete = (Time.time - startTime) / speed;
    //                 player.transform.position = Vector3.Lerp(startPos, endPos, fracComplete);
    //                 FocusPlayer();
    //                 yield return new WaitForSeconds(0.00001F);
    //             }
    //         }
    //         startPos = space.position + offset;
    //     }
    //     StartCoroutine(FaceTarget(player.transform, player.transform.position + new Vector3(0, 0, -1)));
    //     this.currentSpace = path[path.Count - 1];
    //     turnMenu.SetActive(true);
    // }
}
