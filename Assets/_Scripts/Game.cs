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

    private Space s10_5, s10_7,
        s11_7, s11_8,
        s12_2, s12_3, s12_5,
        s13_3, s13_8,
        s14_1, s14_2, s14_3, s14_4, s14_8, s14_10,
        s16_2, s16_4, s16_5, s16_7, s16_8, s16_10,
        s18_5, s18_6, s18_7;
    private List<Edge> edges;
    private bool rollingDice;
    private int currentRoll;

    private Space currentSpace;
    private List<Space> currentSpaceOptions;

	// Use this for initialization
	private void Start () {
        InitSpaces();
        InitEdges();
        currentSpace = s14_1;
        Vector3 startingPos = currentSpace.position;
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
        currentRoll = (int)Mathf.Floor(UnityEngine.Random.value * 6) + 1;
        diceDisplay.text = currentRoll + "";
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
        HighlightSpaces();
    }

    void HighlightSpaces() {
        string toLog = "Roll: " + currentRoll + "\n";
        List<List<Space>> paths = HighlightSpaces(currentSpace, currentRoll, currentSpace, new List<Space>(), new SortedDictionary<string, bool>());
        //I should pass currentRolL instead of store it globally
        foreach(List<Space> path in paths) {
            foreach(Space space in path) {
                toLog = toLog + space.ToString() + ", ";
            }
            Debug.Log(toLog);
            toLog = "";
        }
        Debug.Log("Count: " + paths.Count + "\n" + toLog);
    }

    //TODO check to see if an edge case I'm not catching is when you can come at the same space from different directions for the same amount
    List<List<Space>> HighlightSpaces(Space location, int steps, Space previous, List<Space> endpoints, SortedDictionary<string, bool> checkedSteps) {
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
            //TODO: reduce all of the toStrings in here to one
            foreach(Edge edge in location.edges) {
                otherSpace = edge.GetOther(location);
                //checked steps ensures that only one branch of previous -> tile at n steps will be explored
                if(otherSpace != previous && !checkedSteps.ContainsKey(previous.ToString() + "-" + otherSpace.ToString() + "-" + steps)) {
                    checkedSteps.Add(previous.ToString() + "-" + otherSpace.ToString() + "-" + steps, true);
                    foreach(List<Space> path in HighlightSpaces(otherSpace, steps - 1, location, endpoints, checkedSteps)) {
                        if(path.Count > 0) {
                            path.Insert(0, location);
                            paths.Add(path);
                        }
                    };
                }
            }
        }
        this.currentSpaceOptions = endpoints;
        return paths;
    }

    void InitSpaces() {
            this.s10_5 = new Space(spacesObject.transform.Find("s10_5").transform.position);
            this.s10_7 = new Space(spacesObject.transform.Find("s10_7").transform.position);
            this.s11_7 = new Space(spacesObject.transform.Find("s11_7").transform.position);
            this.s11_8 = new Space(spacesObject.transform.Find("s11_8").transform.position);
            this.s12_2 = new Space(spacesObject.transform.Find("s12_2").transform.position);
            this.s12_3 = new Space(spacesObject.transform.Find("s12_3").transform.position);
            this.s12_5 = new Space(spacesObject.transform.Find("s12_5").transform.position);
            this.s13_3 = new Space(spacesObject.transform.Find("s13_3").transform.position);
            this.s13_8 = new Space(spacesObject.transform.Find("s13_8").transform.position);
            this.s14_1 = new Space(spacesObject.transform.Find("s14_1").transform.position);
            this.s14_2 = new Space(spacesObject.transform.Find("s14_2").transform.position);
            this.s14_3 = new Space(spacesObject.transform.Find("s14_3").transform.position);
            this.s14_4 = new Space(spacesObject.transform.Find("s14_4").transform.position);
            this.s14_8 = new Space(spacesObject.transform.Find("s14_8").transform.position);
            this.s14_10 = new Space(spacesObject.transform.Find("s14_10").transform.position);
            this.s16_2 = new Space(spacesObject.transform.Find("s16_2").transform.position);
            this.s16_4 = new Space(spacesObject.transform.Find("s16_4").transform.position);
            this.s16_5 = new Space(spacesObject.transform.Find("s16_5").transform.position);
            this.s16_7 = new Space(spacesObject.transform.Find("s16_7").transform.position);
            this.s16_8 = new Space(spacesObject.transform.Find("s16_8").transform.position);
            this.s16_10 = new Space(spacesObject.transform.Find("s16_10").transform.position);
            this.s18_5 = new Space(spacesObject.transform.Find("s18_5").transform.position);
            this.s18_6 = new Space(spacesObject.transform.Find("s18_6").transform.position);
            this.s18_7 = new Space(spacesObject.transform.Find("s18_7").transform.position);
    }

    void InitEdges() {
        this.edges = new List<Edge>();
        this.edges.Add(new Edge(s10_5, s10_7));
        this.edges.Add(new Edge(s10_5, s12_5));
        this.edges.Add(new Edge(s10_7, s11_7));
        this.edges.Add(new Edge(s11_7, s11_8));
        this.edges.Add(new Edge(s11_8, s13_8));
        this.edges.Add(new Edge(s12_2, s12_3));
        this.edges.Add(new Edge(s12_2, s14_2));
        this.edges.Add(new Edge(s12_3, s12_5));
        this.edges.Add(new Edge(s12_3, s13_3));
        this.edges.Add(new Edge(s13_3, s14_3));
        this.edges.Add(new Edge(s13_8, s14_8));
        this.edges.Add(new Edge(s14_1, s14_2));
        this.edges.Add(new Edge(s14_2, s14_3));
        this.edges.Add(new Edge(s14_2, s16_2));
        this.edges.Add(new Edge(s14_3, s14_4));
        this.edges.Add(new Edge(s14_4, s16_4));
        this.edges.Add(new Edge(s14_8, s14_10));
        this.edges.Add(new Edge(s14_8, s16_8));
        this.edges.Add(new Edge(s14_10, s16_10));
        this.edges.Add(new Edge(s16_2, s16_4));
        this.edges.Add(new Edge(s16_4, s16_5));
        this.edges.Add(new Edge(s16_5, s16_7));
        this.edges.Add(new Edge(s16_5, s18_5));
        this.edges.Add(new Edge(s16_7, s16_8));
        this.edges.Add(new Edge(s16_7, s18_7));
        this.edges.Add(new Edge(s16_8, s16_10));
        this.edges.Add(new Edge(s18_5, s18_6));
        this.edges.Add(new Edge(s18_6, s18_7));
    }

    private class Space {
        public readonly Vector3 position;
        public readonly List<Edge> edges = new List<Edge>();

        public Space(Vector3 position) {
            this.position = position;
        }

        public string ToString() {
            return (position.x - 100F) + "_" + (position.z - 50F);
        }
    }

    private class Edge {
        public readonly Space a, b;

        public Edge(Space a, Space b) {
            this.a = a;
            this.b = b;
            a.edges.Add(this);
            b.edges.Add(this);
        }

        public Space GetOther(Space space) {
            Space other;
            if(space == a) {
                other = b;
            } else {
                other = a;
            }
            return other;
        }

        public bool Equals(Space other) {
            return this.ToString() == other.ToString();
        }
    }
}
