using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

class Menu : MonoBehaviour {

    public Game game;
    public Button login;
    public Button signUp;
    public InputField username;

    void Start() {
        login.onClick.AddListener(Login);
        signUp.onClick.AddListener(SignUp);
    }

    //TODO: Validate usernames
    void Login() {
        StartCoroutine(Rest.Get(API.user, "username=" + username.text, new Action<User>(delegate (User user) {
            Debug.Log(user.ToString());
        }), new Action<RestError>(delegate (RestError err) {
            Debug.Log(err.message);
        })));
    }

    void SignUp() {
    }

}
