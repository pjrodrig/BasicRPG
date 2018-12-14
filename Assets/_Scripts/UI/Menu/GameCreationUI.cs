using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;

public class GameCreationUI : MonoBehaviour {

    App app;
    GameSelectUI gameSelect;
    bool active = false;

    public GameObject thisObj;
    public Dropdown playersDropdown;
    public InputField playerInvite1;
    public InputField playerInvite2;
    public InputField playerInvite3;
    public GameObject playerInvite2GO;
    public GameObject playerInvite3GO;
    public Button sendInvites;

    public Text usersDoNotExist;
    public GameObject usersDoNotExistObj;
    public GameObject cannotInviteSelf;
    public GameObject cannotInviteTwice;

    public void Init(App app, GameSelectUI gameSelect) {
        this.app = app;
        this.gameSelect = gameSelect;
    }

    public void Activate() {
        if(!active) {
            thisObj.SetActive(true);
            playersDropdown.onValueChanged.AddListener(delegate {UpdateInviteList();});
            sendInvites.onClick.AddListener(FetchUsers);
            ClearErrorMessages();
            UpdateInviteList();
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false);
            playersDropdown.onValueChanged.RemoveAllListeners();
            sendInvites.onClick.RemoveAllListeners();
            active = false;
        }
    }

    void ClearErrorMessages() {
        usersDoNotExistObj.SetActive(false);
        cannotInviteSelf.SetActive(false);
        cannotInviteTwice.SetActive(false);
    }

    void UpdateInviteList() {
        playerInvite2GO.SetActive(playersDropdown.value > 0);
        playerInvite3GO.SetActive(playersDropdown.value > 1);
    }

    void FetchUsers() {
        if(ValidateUsers()) {
            StartCoroutine(Rest.Get(API.users, "usernames=" + GetEncodedUsernames(), new Action<UserCollection>(delegate (UserCollection users) {
                CreateGame(users.users);
            }), new Action<RestError>(delegate (RestError err) {
                if(err.errorCode == 404) {
                    usersDoNotExist.text = err.message;
                    usersDoNotExistObj.SetActive(true);
                } else {
                    Debug.Log(err.message);
                }
            })));
        }
    }

    bool ValidateUsers() {
        ClearErrorMessages();
        string self = app.User.name;
        if(playerInvite1.text == self) {
            cannotInviteSelf.SetActive(true);
            return false;
        }
        if(playersDropdown.value > 0) {
            if(playerInvite2.text == self) {
                cannotInviteSelf.SetActive(true);
                return false;
            }
            if(playerInvite1.text == playerInvite2.text) {
                cannotInviteTwice.SetActive(true);
                return false;
            }
        }
        if(playersDropdown.value > 1) {
            if(playerInvite3.text == self) {
                cannotInviteSelf.SetActive(true);
                return false;
            }
            if(playerInvite1.text == playerInvite3.text) {
                cannotInviteTwice.SetActive(true);
                return false;
            }
            if(playerInvite2.text == playerInvite3.text) {
                cannotInviteTwice.SetActive(true);
                return false;
            }
        }
        return true;
    }

    void CreateGame(User[] users) {
        Player[] players = new Player[users.Length + 1];
        for(int i = 0; i < users.Length; i++) {
            players[i] = new Player(users[i].id, users[i].name);
        }
        players[users.Length] = (new Player(app.User.id, app.User.name));
        StartCoroutine(Rest.Post(API.game, null, new Game(players), new Action<Game>(delegate (Game game) {
            gameSelect.CompleteGameCreation(game);
        }), new Action<RestError>(delegate (RestError err) {
            Debug.Log(err.message);
        })));
    }

    string GetEncodedUsernames() {
        StringBuilder sb = new StringBuilder(UnityWebRequest.EscapeURL(playerInvite1.text));
        if(playersDropdown.value > 0) {
            sb.Append(",");
            sb.Append(UnityWebRequest.EscapeURL(playerInvite2.text));
        }
        if(playersDropdown.value > 1) {
            sb.Append(",");
            sb.Append(UnityWebRequest.EscapeURL(playerInvite3.text));
        }
        return sb.ToString();
    }

}
