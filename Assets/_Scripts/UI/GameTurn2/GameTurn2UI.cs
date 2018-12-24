using UnityEngine;
using UnityEngine.UI;

public class GameTurn2UI : MonoBehaviour {

    App app;
    bool active;
    bool itemUsed;
    bool scrollUsed;
    TestMap testMap;

    public GameObject thisObj;
    public BattleUI battle;
    public ItemSpinnerUI itemSpinner;

    public void Init(App app, TestMap testMap) {
        this.app = app;
        this.testMap = testMap;
        battle.Init(app, this);
        itemSpinner.Init(this);
    }

    public void Activate() {
        if(!active) {
            thisObj.SetActive(true);
            if(app.ActivePlayer.playerData.isInBattle) {
                bool found = false;
                foreach(Battle battleInstance in app.Game.battles) {
                    foreach(int userId in battleInstance.players) {
                        if(app.ActivePlayer.id == userId) {
                            battleInstance.home = app.ActivePlayer;
                            battleInstance.away = battleInstance.monster;
                            battle.Activate(battleInstance);
                            found = true;
                            break;
                        }
                    }
                    if(found) {
                        break;
                    }
                }
            } else {
                GetSpaceEvent(testMap.GetSpace(app.ActivePlayer.playerData.space));
            }
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false);
            active = false;
        }
    }

    public void GetSpaceEvent(Space space) {
        switch(space.type) {
            case Space.Type.EVENT:
                StartEvent();
                break;
            case Space.Type.ITEM_SHOP:
                break;
            case Space.Type.MAGIC_SHOP:
                break;
            case Space.Type.WEAPON_SHOP:
                break;
            case Space.Type.ITEM:
                itemSpinner.Activate(ItemPool.GetItemPool(space.level), app.ActivePlayer.playerData.inventory);
                break;
            case Space.Type.SCROLL:
                itemSpinner.Activate(ItemPool.GetItemPool(space.level), app.ActivePlayer.playerData.inventory);
                break;
            case Space.Type.WEAPON:
                itemSpinner.Activate(ItemPool.GetItemPool(space.level), app.ActivePlayer.playerData.inventory);
                break;
            case Space.Type.SHIELD:
                itemSpinner.Activate(ItemPool.GetItemPool(space.level), app.ActivePlayer.playerData.inventory);
                break;
        }
    }

    void StartEvent() {
        Battle battleInstance = new Battle(app.ActivePlayer, new Monster());
        app.Game.battles.Add(battleInstance);
        battle.Activate(battleInstance);
    }

    public void EndTurn() {
        Deactivate();
        app.AdvanceTurn();
    }
}