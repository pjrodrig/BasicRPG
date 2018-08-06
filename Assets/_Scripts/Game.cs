using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

    private static float GRID_OFFSET_X = 100F;
    private static float GRID_OFFSET_Y = 3.2F;
    private static float GRID_OFFSET_Z = 50F;

    public GameObject playerCamera;
    public GameObject player;
    public GameObject turnMenu;
    public Text diceDisplay;
    public GameObject stopRollButton;
    public GameObject diceRollContainer;
    public GameObject spaces;

    private Space s10_5, s10_7,
        s11_7, s11_8,
        s12_2, s12_3,
        s13_3, s13_8,
        s14_1, s14_2, s14_3, s14_4, s14_8, s14_10,
        s16_2, s16_4, s16_5, s16_7, s16_8, s16_10,
        s18_5, s18_6, s18_7;
    private bool rollingDice;
    private int currentRole;

	// Use this for initialization
	private void Start () {
        InitSpaces();
        Vector3 startingPos = s14_1.position;
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
        currentRole = (int)Mathf.Floor(UnityEngine.Random.value * 6) + 1;
        diceDisplay.text = currentRole + "";
        StartCoroutine(PromptForMovement());
    }

    IEnumerator PromptForMovement() {
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
    }

    void InitSpaces() {}


    private class Space {
        public readonly Vector3 position;
        private Edge[] edges;

        Space(Vector3 position) {
            this.position = position;
        }

        void SetEdges(Edge[] edges){
            this.edges = edges;
        }

        Edge[] getEdges() {
            return this.edges;
        }
    }

    private class Edge {
        public readonly Space a, b;
        public readonly bool oneWay;

        Edge(Space a, Space b, bool oneWay) {
            this.a = a;
            this.b = b;
            this.oneWay = oneWay;
        }
    }
}
