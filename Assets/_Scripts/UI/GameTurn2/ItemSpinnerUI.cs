using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Reflection;

public class ItemSpinnerUI : MonoBehaviour {

    GameTurn2UI gameTurn;
    bool active;
    bool spinning;
    int slow;
    Quaternion rotation;
    Thing[] things;

    public GameObject thisObj;
    public Button stop;
    public GameObject spinner;
    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;
    public Image slot5;
    public Image slot6;
    public Image slot7;
    public Image slot8;
    public Text slot1Text;
    public Text slot2Text;
    public Text slot3Text;
    public Text slot4Text;
    public Text slot5Text;
    public Text slot6Text;
    public Text slot7Text;
    public Text slot8Text;

    public void Init(GameTurn2UI gameTurn) {
        this.gameTurn = gameTurn;
    }

    public void Activate(String[] thingStrings, Inventory inventory) {
        if(!active) {
            thisObj.SetActive(true);
            PopulateSlots(thingStrings);
            rotation = spinner.transform.rotation;
            stop.onClick.AddListener(StopSpin);
            StartSpin(inventory);
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false);
            spinner.transform.rotation = rotation;
            stop.onClick.RemoveAllListeners();
            active = false;
        }
    }

    void PopulateSlots(string[] thingStrings) {
        things = new Thing[8];
        Text[] slots = {slot1Text, slot2Text, slot3Text, slot4Text, slot5Text, slot6Text, slot7Text, slot8Text};
        int random;
        Thing thing;
        for(int i = 0; i < 8; i++) {
            random = (int)Math.Floor(UnityEngine.Random.value * thingStrings.Length);
            thing = (Thing) Activator.CreateInstance(Assembly.GetExecutingAssembly().GetType(thingStrings[random]));
            things[i] = thing;
            slots[i].text = thing.GetName();
        }
    }

    void StartSpin(Inventory inventory) {
        spinning = true;
        StartCoroutine(Spin(inventory));
    }

    void StopSpin() {
        spinning = false;
        slow = (int)Mathf.Floor(UnityEngine.Random.value * 16) + 30;
    }

    IEnumerator Spin(Inventory inventory) {
        int i = 0;
        while(spinning || i%2 != 0 || slow > 0) {
            i++;
            switch(i%16) {
                case 0:
                    i = 0;
                    slot1.color = Color.green;
                    slot8.color = Color.gray;
                    break;
                case 2:
                    slot1.color = Color.gray;
                    slot2.color = Color.green;
                    break;
                case 4:
                    slot2.color = Color.gray;
                    slot3.color = Color.green;
                    break;
                case 6:
                    slot3.color = Color.gray;
                    slot4.color = Color.green;
                    break;
                case 8:
                    slot4.color = Color.gray;
                    slot5.color = Color.green;
                    break;
                case 10:
                    slot5.color = Color.gray;
                    slot6.color = Color.green;
                    break;
                case 12:
                    slot6.color = Color.gray;
                    slot7.color = Color.green;
                    break;
                case 14:
                    slot7.color = Color.gray;
                    slot8.color = Color.green;
                    break;
                default:
                    break;
            }
            spinner.transform.Rotate(0, 0, -22.5F);
            if(slow > 0) {
                slow--;
                yield return new WaitForSeconds(0.03F);
            } else {
                yield return new WaitForSeconds(0.001F);
            }
        }
        yield return new WaitForSeconds(2F);
        AddThing(inventory, i%16/2);
    }

    void AddThing(Inventory inventory, int slot) {
        Thing thing = things[slot];
        if(thing is Item) {
            inventory.AddItem((Item) thing);
        } else if(thing is Scroll) {

        } 
        // else if(thing is Weapon) {

        // } else if(thing is Shield) {

        // }
        Deactivate();
        gameTurn.EndTurn();
    }
}