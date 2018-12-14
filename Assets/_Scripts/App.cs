using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class App : MonoBehaviour {

    public Menu menu;
    public User User {get;set;}
    public Game Game {get;set;}

    void Start() {
        InitClasses();
        InitGame();
    }

    void InitClasses() {
        menu.Init(this);
    }

    void InitGame() {
        menu.Activate();
    }

    public void CompleteGameSelect() {
        
    }
}
