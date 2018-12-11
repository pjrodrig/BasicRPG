using UnityEngine;
using UnityEngine.UI;
using System;

public class GameListItem : MonoBehaviour {

    Game game;
    Action<Game> onClick;

    public GameObject thisObj;
    public GameObject playerListItemPrefab;
    public Text gameId;
    public Text week;
    public GameObject playerList;
    public Button thisButton;

    public void Init(Game game, Action<Game> onClick) {
        this.game = game;
        this.onClick = onClick;
        gameId.text = "Game: " + game.id;
        week.text = "Week: " + game.week;
        Vector3 previous = new Vector3(
            playerList.transform.position.x, 
            playerList.transform.position.y + 38, 
            playerList.transform.position.z
        );
        foreach(Player player in game.players) {
            GameObject newPlayerListItem = Instantiate(playerListItemPrefab) as GameObject;
            PlayerListItem playerListItem = newPlayerListItem.GetComponent<PlayerListItem>();
            playerListItem.Init(player);
            newPlayerListItem.transform.SetParent(playerList.transform);
            newPlayerListItem.transform.position = new Vector3(previous.x, previous.y - 18, previous.z);
            previous = newPlayerListItem.transform.position;
        }
        thisButton.onClick.AddListener(SelectGame);
    }

    void SelectGame() {
        onClick(game);
    }

    void OnDestroy() {
        thisButton.onClick.RemoveAllListeners();
    }

}
