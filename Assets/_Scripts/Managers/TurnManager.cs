using UnityEngine;
using System.Collections.Generic;

public class TurnManager {

    private readonly Game game;
    private readonly List<Player> players;
    private readonly GameCamera camera;

    private bool rollingDice = false;
    private bool choosingSpace = false;

    private SortedDictionary<string, List<Space>> pathOptions;

    public TurnManager(Game game, List<Player> players, GameCamera camera) {
        this.game = game;
        this.players = players;
        this.camera = camera;
    }

    public void Update() {
        if(choosingSpace) {
            MovementCheck();
            SpaceClickCheck();
        }
    }

    public void TakeTurns() {
        foreach(Player player in players) {
            TakeTurn(player);
        }
    }

    void TakeTurn(Player player) {
        this.camera.FocusPosition(player.go.transform.position);
    }

    void MovementCheck() {
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

    void SpaceClickCheck() {
        if (Input.GetMouseButtonDown(0)){
            // Ray ray = camera.go.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit;
            // if (Physics.Raycast(ray, out hit)){
            //     List<Space> path;
            //     Vector3 hitPos = hit.transform.position;
            //     pathOptions.TryGetValue(hitPos.x + "_" + hitPos.z, out path);
            //     this.choosingSpace = false;
            //     diceRollContainer.SetActive(false);
            //     this.diceDisplay.transform.position = new Vector3(486F, 292F, 0);
            //     StartCoroutine(MovePlayer(path));
            // }
        }
    }

    void FocusPlayer(Player player) {
        Vector3 playerPos = player.go.transform.position;
        camera.go.transform.SetPositionAndRotation(
            new Vector3(playerPos.x, playerPos.y + 10F, playerPos.z - 12F),
            camera.go.transform.rotation
        );
    }
}
