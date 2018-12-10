using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Text.RegularExpressions;

public class Auth : MonoBehaviour {

    Menu menu;
    bool active = false;
    
    public GameObject thisObj;
    public Button login;
    public Button signUp;
    public InputField username;
    public GameObject usernameNotFound;
    public GameObject usernameAlreadyExists;

    public void Init(Menu menu) {
        this.menu = menu;
    }

    public void Activate() {
        if(!active) {
            login.onClick.AddListener(Login);
            signUp.onClick.AddListener(SignUp);
            username.onValueChanged.AddListener(delegate {ClearErrorMessages();});
            thisObj.SetActive(true);
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            login.onClick.RemoveAllListeners();
            signUp.onClick.RemoveAllListeners();
            username.onValueChanged.RemoveAllListeners();
            ClearErrorMessages();
            thisObj.SetActive(false);
            active = false;
        }
    }

    void Login() {
        if(username.text.Length > 0) {
            StartCoroutine(Rest.Get(API.user, "username=" + UnityWebRequest.EscapeURL(username.text), new Action<User>(delegate (User user) {
                Deactivate();
                menu.CompleteAuth(user);
            }), new Action<RestError>(delegate (RestError err) {
                if(err.errorCode == 404) {
                    usernameNotFound.SetActive(true);
                } else {
                    Debug.Log(err.message);
                }
            })));
        }
    }

    void SignUp() {
        if(username.text.Length > 0) {
            StartCoroutine(Rest.Post(API.user, null, new User(username.text), new Action<User>(delegate (User user) {
                Deactivate();
                menu.CompleteAuth(user);
            }), new Action<RestError>(delegate (RestError err) {
                if(err.errorCode == 409) {
                    usernameAlreadyExists.SetActive(true);
                } else {
                    Debug.Log(err.message);
                }
            })));
        }
    }

    void ClearErrorMessages() {
        usernameNotFound.SetActive(false);
        usernameAlreadyExists.SetActive(false);
    }

}
