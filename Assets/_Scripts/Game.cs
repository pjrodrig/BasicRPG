using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    private User user;

    void Start() {

    }
    
    void Update() {
    }

    public void SetUser(User user) {
        this.user = user;
    }
}
