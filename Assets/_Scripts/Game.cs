using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    private int NUM_PLAYERS = 3;
    private readonly Quaternion TO_CAMERA = Quaternion.LookRotation(Vector3.back);

    public GameCamera playerCamera;
    public PlayerUI playerUI;
    public Visuals visuals;



    public GameObject turnMenu;
    public Text diceDisplay;
    public GameObject stopRollButton;
    public GameObject spacesObject;

    public GameObject playerModel;

    private List<Player> players;
    private TurnManager turnManager;
    private EventManager eventManager;

	void Start() {
        var map = new TestMap(spacesObject);
        InitPlayers(map.GetStart());
        this.turnManager = new TurnManager(this, this.players, this.playerCamera, this.playerUI, this.visuals);
        this.eventManager = new  EventManager(players);
        this.playerUI.Initialize(this.turnManager, this.playerCamera);
        StartRound();
	}

    void InitPlayers(Space space) {
        this.players = new List<Player>();
        Player player;
        for(int i = 0; i < NUM_PLAYERS; i++) {
            player = Instantiate(this.playerModel, space.position, TO_CAMERA)
                .GetComponent(typeof(Player)) as Player;
            player.Initialize(space, this.playerCamera);
            player.go.SetActive(true);//TODO: remove when activation is solved
            players.Add(player);
        }
    }

    void Update() {
        turnManager.Update();
    }

    void StartRound() {
        this.turnManager.TakeTurns();
    }

    public void EndRound() {
        StartRound();
    }
}
