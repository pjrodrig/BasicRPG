using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Text.RegularExpressions;

public class GameSelect : MonoBehaviour {

    App app;
    Menu menu;
    bool active = false;
    Game[] games;
    
    public GameObject thisObj;
    public GameObject gameList;
    public Button createGame;
    public GameCreation gameCreation;
    public GameObject gameListItemPrefab;
    public GameObject[] gameListItems;

    public void Init(App app, Menu menu) {
        this.app = app;
        this.menu = menu;
        this.gameCreation.Init(app, this);
    }

    public void Activate() {
        if(!active) {
            thisObj.SetActive(true);
            createGame.onClick.AddListener(CreateGame);
            FetchGames();
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false);
            games = null;
            if(gameListItems != null) {
                foreach(GameObject gameListItem in gameListItems) {
                    Destroy(gameListItem);
                }
            }
            gameListItems = null;
            createGame.onClick.RemoveAllListeners();
            active = false;
        }
    }

    private void FetchGames() {
        StartCoroutine(Rest.Get(API.userGames, "userId=" + app.User.id, new Action<GameCollection>(delegate (GameCollection games) {
            this.games = games.games;
            UpdateGamesList();
        }), new Action<RestError>(delegate (RestError err) {
            Debug.Log(err.message);
        })));
    }

    void UpdateGamesList() {
        Vector2 previous = createGame.transform.position;
        gameListItems = new GameObject[games.Length];
        float scale = menu.GetScale();
        int i = 0;
        foreach(Game game in games) {
            GameObject newGameListItem = Instantiate(gameListItemPrefab) as GameObject;
            GameListItem gameListItem = newGameListItem.GetComponent<GameListItem>();
            gameListItem.Init(game, new Action<Game>(delegate (Game selectedGame) {
                Deactivate();
                menu.CompleteGameSelect(game);
            }));
            newGameListItem.transform.SetParent(gameList.transform, false);
            newGameListItem.transform.position = new Vector2(previous.x, previous.y - (110 / scale));
            previous = newGameListItem.transform.position;
            gameListItems[i++] = newGameListItem;
        }
    }

    void CreateGame() {
        Deactivate();
        gameCreation.Activate();
    }

    public void CompleteGameCreation(Game game) {
        menu.CompleteGameSelect(game);
    }

}
