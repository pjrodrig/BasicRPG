using UnityEngine;
using System.Collections.Generic;

public class EventManager {

    private readonly int SPECIAL_EVENT_CHANCE = 20;

    private List<Player> players;

    public EventManager(List<Player> players) {
        this.players = players;
    }

    public void PickRandomEvent() {
        SpaceEvent event;
        if(Random.Range(1, 100) > 20) {
            event = GetBattleEvent();
        } else {
            event = GetSpecialEvent();
        }
    }

    private BattleSpaceEvent GetBattleEvent() {
        return new BattleSpaceEvent
    };
}
