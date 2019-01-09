using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour {

    TurnOptionsUI turnOptions;
    MoveUI move;
    bool active;
    Inventory inventory;
    GameObject[] slots;

    public GameObject thisObj;
    public GameObject itemOptions;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot5;
    public GameObject slot6;
    public GameObject slot7;
    public GameObject slot8;
    public Button items;
    public Button scrolls;
    public Button back;
    public Button drop;
    public Button use;
    public Button cancel;
    public Text itemName;
    public Text itemDescription;
    public RawImage itemImage;

    public void Init(TurnOptionsUI turnOptions, MoveUI move) {
        this.turnOptions = turnOptions;
        this.move = move;
        this.slots = new GameObject[]{slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8};
    }

    public void Activate(Player player, bool itemUsed, bool scrollUsed) {
        if(!active) {
            Inventory inventory = player.playerData.inventory;
            items.interactable = false;
            scrolls.interactable = true;
            thisObj.SetActive(true);
            PopulateSlots(inventory.items);
            items.onClick.AddListener(delegate() {
                PopulateSlots(inventory.items);
                items.interactable = false;
                scrolls.interactable = true;
            });
            scrolls.onClick.AddListener(delegate() {
                PopulateSlots(inventory.scrolls);
                scrolls.interactable = false;
                items.interactable = true;
            });
            back.onClick.AddListener(delegate() {
                Deactivate();
                turnOptions.Activate();
            });
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false);
            foreach(GameObject slot in slots) {
                ((Button)slot.GetComponent(typeof(Button))).onClick.RemoveAllListeners();
            }
            items.onClick.RemoveAllListeners();
            scrolls.onClick.RemoveAllListeners();
            back.onClick.RemoveAllListeners();
            drop.onClick.RemoveAllListeners();
            use.onClick.RemoveAllListeners();
            cancel.onClick.RemoveAllListeners();
            itemOptions.SetActive(false);
            active = false;
        }
    }

    void PopulateSlots<T>(List<T> inventory) where T : InventoryObject {
        for(int i = 0; i < 8; i++) {
            if(i < inventory.Count) {
                SetSlot(i, inventory[i], inventory);
            } else {
                SetSlot(i, null, inventory);
            }
        }
    }

    void SetSlot<T>(int index, InventoryObject invObj, List<T> inventory) where T : InventoryObject {
        GameObject slot = slots[index];
        slot.SetActive(invObj != null);
        Button slotButton = (Button) slot.GetComponent(typeof(Button));
        if(invObj != null) {
            ((Text)slotButton.GetComponentInChildren(typeof(Text))).text = invObj.GetName();
            slotButton.onClick.AddListener(delegate () {
                ViewSlot(index, invObj, inventory);
            });
        } else {
            slotButton.onClick.RemoveAllListeners();
        }
    }

    void ViewSlot<T>(int index, InventoryObject invObj, List<T> inventory) where T : InventoryObject {
        itemName.text = invObj.GetName();
        itemDescription.text = invObj.GetDescription();
        ((Text)drop.GetComponentInChildren(typeof(Text))).text = "Drop";
        drop.onClick.AddListener(delegate() {
            ((Text)drop.GetComponentInChildren(typeof(Text))).text = "Confirm";
            drop.onClick.AddListener(delegate() {
                inventory.RemoveAt(index);
                CancelViewSlot();
                PopulateSlots(inventory);
            });
        });

        use.onClick.AddListener(delegate() {
            Use(invObj, delegate() {
                inventory.RemoveAt(index);
                Deactivate();
            });
        });

        cancel.onClick.AddListener(CancelViewSlot);

        itemOptions.SetActive(true);
    }

    void CancelViewSlot() {
        drop.onClick.RemoveAllListeners();
        use.onClick.RemoveAllListeners();
        cancel.onClick.RemoveAllListeners();
        itemOptions.SetActive(false);
    }

    void Use(InventoryObject invObj, Action callback) {
        if(invObj is Item) {
            if(invObj is MovementModifier) {
                
            }
            callback();
        } else if(invObj is Scroll) {

        }
    }
}