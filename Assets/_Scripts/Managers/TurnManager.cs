using UnityEngine;
using System.Collections.Generic;

public class TurnManager {

    private readonly Game game;
    private readonly List<Player> players;
    private readonly GameCamera camera;
    private readonly PlayerUI playerUI;
    private readonly Visuals visuals;

    private bool cameraIsMoveable = false;
    private bool rollingDice = false;
    private bool choosingSpace = false;
    private Player currentPlayer = null;

    private SortedDictionary<string, List<Space>> pathOptions;

    public TurnManager(Game game, List<Player> players, GameCamera camera, PlayerUI playerUI, Visuals visuals) {
        this.game = game;
        this.players = players;
        this.camera = camera;
        this.playerUI = playerUI;
        this.visuals = visuals;
    }

    public void Update() {
        if(cameraIsMoveable) {
            CameraMovementCheck();
        }
        if(choosingSpace) {
            SpaceClickCheck();
        }
    }

    public void TakeTurns() {
        // foreach(Player player in this.players) {
        //     TakeTurn(player);
        // }
        TakeTurn(this.players[0]);
    }

    void TakeTurn(Player player) {
        this.currentPlayer = player;
        this.camera.FocusPosition(player.go.transform.position);
    }

    void CameraMovementCheck() {
        float speed = 20F;
        if(Input.GetKey("w")) {
            camera.go.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }
        if(Input.GetKey("a")) {
            camera.go.transform.position += new Vector3(-speed * Time.deltaTime,0,0);
        }
        if(Input.GetKey("s")) {
            camera.go.transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        }
        if(Input.GetKey("d")) {
            camera.go.transform.position += new Vector3(speed * Time.deltaTime,0,0);
        }
    }

    public void ChooseMovement(int roll) {
        List<List<Space>> paths = FindPaths(roll);
        this.pathOptions = new SortedDictionary<string, List<Space>>();
        List<Vector3> toHighlight = new List<Vector3>();
        Vector3 pos;
        foreach(List<Space> path in paths) {
            pos = path[path.Count - 1].position;
            toHighlight.Add(pos);
            pathOptions.Add(pos.x + "_" + pos.z, path);
        }
        this.visuals.HighlightSpaces(toHighlight);
        this.choosingSpace = true;
    }

    List<List<Space>> FindPaths(int roll) {
        Space currentSpace = this.currentPlayer.GetSpace();
        return FindPaths(currentSpace, roll, currentSpace,
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

    void SpaceClickCheck() {
        if (Input.GetMouseButtonDown(0)){
            Ray ray = this.camera.go.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                List<Space> path;
                Vector3 hitPos = hit.transform.position;
                this.pathOptions.TryGetValue(hitPos.x + "_" + hitPos.z, out path);
                this.choosingSpace = false;
                this.visuals.EndHighlight();
                this.playerUI.ResetDiceDisplay();
                this.currentPlayer.TraversePath(path);
            }
        }
    }
}
