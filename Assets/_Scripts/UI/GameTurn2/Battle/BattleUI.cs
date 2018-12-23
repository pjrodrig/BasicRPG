using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BattleUI : MonoBehaviour {

    App app;
    GameTurn2UI gameTurn;
    bool active;
    Vector3 homeButtons;
    Vector3 awayButtons;
    Battle battle;
    BattlePredictions battlePredictions;

    public GameObject thisObj;
    public CoinFlipUI coinFlip;
    public BattleAnimationsUI battleAnimations;
    public GameObject offenseOptions;
    public Button attack;
    public Button empoweredAttack;
    public Button magic;
    public Button item;
    public GameObject defenseOptions;
    public Button defend;
    public Button counter;
    public Button magicDefend;
    public Button surrender;
    public Text awayStats;
    public Text homeStats;
    public GameObject stats;

    public void Init(App app, GameTurn2UI gameTurn) {
        this.app = app;
        this.gameTurn = gameTurn;
        coinFlip.Init(this);
        battleAnimations.Init(this);
    }

    public void Activate(Battle battle) {
        if(!active) {
            thisObj.SetActive(true);
            homeButtons = offenseOptions.transform.position;
            awayButtons = defenseOptions.transform.position;
            this.battle = battle;
            battleAnimations.UpdateStats(battle);
            battle.phase = 0;
            coinFlip.Activate();
            UpdateStats();
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false);
            defend.onClick.RemoveAllListeners();
            counter.onClick.RemoveAllListeners();
            magicDefend.onClick.RemoveAllListeners();
            surrender.onClick.RemoveAllListeners();
            attack.onClick.RemoveAllListeners();
            empoweredAttack.onClick.RemoveAllListeners();
            magic.onClick.RemoveAllListeners();
            item.onClick.RemoveAllListeners();
            offenseOptions.SetActive(false);
            defenseOptions.SetActive(false);
            offenseOptions.transform.position = homeButtons;
            defenseOptions.transform.position = awayButtons;
            battlePredictions = null;
            stats.SetActive(false);
            battle = null;
            battleAnimations.Deactivate();
            active = false;
        }
    }

    void UpdateStats() {
        Stats home = battle.home.GetCurrentStats();
        Stats away = battle.away.GetCurrentStats();
        homeStats.text = home.atk + "\n\n\n" + home.mag + "\n\n\n" + home.def + "\n\n\n" + home.spd;
        awayStats.text = away.atk + "\n\n\n" + away.mag + "\n\n\n" + away.def + "\n\n\n" + away.spd;
    }

    public void SetFlipResult(bool attacking) {
        battle.attacking = attacking;
        StartRound();
    }

    void StartRound() {
        battle.NextPhase();
        if(battle.phase == 3) {
            Deactivate();
            gameTurn.EndTurn();
        } else {
            battlePredictions = new BattlePredictions(battle.GetAttacker().GetCurrentStats(), battle.GetDefender().GetCurrentStats());
            if(battle.away is Player) {

            } else {
                if(battle.attacking) {
                    battle.SetDefensiveAction((Battle.Action) Mathf.Floor(UnityEngine.Random.value * 3) + 4);
                } else {
                    battle.SetOffensiveAction((Battle.Action) Mathf.Floor(UnityEngine.Random.value * 3));
                }
            }
            SetupButtons();
        }
    }

    public void ResolveRound() {
        bool battleOver = false;
        if(battle.home.GetCurrentStats().hp == 0) {
            // battle.EndBattle(false);
            battleOver = true;
        } else if(battle.away.GetCurrentStats().hp == 0) {
            // battle.EndBattle(true);
            battleOver = true;
        }
        if(battle.phase == 2 || battleOver) {
            if(battleOver) {
                foreach(Player player in app.Game.players) {
                    if(battle.players.Remove(player.id)) {
                        player.playerData.isInBattle = false;
                    }
                }
                app.Game.battles.Remove(battle);
            }
            Deactivate();
            gameTurn.EndTurn();
        } else {
            StartRound();
        }
    }

    void Surrender() {
        // battle.EndBattle(!battle.attacking);
        foreach(Player player in app.Game.players) {
            if(battle.players.Remove(player.id)) {
                player.playerData.isInBattle = false;
            }
        }
        app.Game.battles.Remove(battle);
        Deactivate();
        gameTurn.EndTurn();
    }

    void Item() {

    }

    public void DealDamage() {
        Battle.Action offensiveAction = battle.GetOffensiveAction();
        Battle.Action defensiveAction = battle.GetDefensiveAction();
        bool reverse = offensiveAction == Battle.Action.EMPOWERED_ATTACK && defensiveAction == Battle.Action.COUNTER;
        BattleParticipant target = reverse ? battle.GetAttacker() : battle.GetDefender();
        Stats stats = target.GetCurrentStats();
        int damage = battlePredictions.GetDamage(offensiveAction, defensiveAction);
        damage = damage > 0 ? damage : 0;
        StartCoroutine(battleAnimations.ApplyDamage(reverse ? !battle.attacking : battle.attacking, target, stats.hp > damage ? damage : stats.hp));
        stats.hp = stats.hp > damage ? stats.hp - damage : 0;
    }

    void SetupButtons() {
        offenseOptions.SetActive(true);
        defenseOptions.SetActive(true);
        stats.SetActive(true);
        offenseOptions.transform.position = battle.attacking ? homeButtons : awayButtons;
        defenseOptions.transform.position = battle.attacking ? awayButtons : homeButtons;
        // attack.interactable = empoweredAttack.interactable = magic.interactable = item.interactable = attacking;
        // defend.interactable =  counter.interactable = magicDefend.interactable = surrender.interactable = !attacking;
        if(battle.attacking) {
            attack.onClick.AddListener(delegate () {
                battle.SetOffensiveAction(Battle.Action.ATTACK);
                battleAnimations.PerformAnimations(battle);
            });
            empoweredAttack.onClick.AddListener(delegate () {
                battle.SetOffensiveAction(Battle.Action.EMPOWERED_ATTACK);
                battleAnimations.PerformAnimations(battle);
            });
            magic.onClick.AddListener(delegate () {
                battle.SetOffensiveAction(Battle.Action.MAGIC);
                battleAnimations.PerformAnimations(battle);
            });
            item.onClick.AddListener(delegate () {
                battle.SetOffensiveAction(Battle.Action.ITEM);
                battleAnimations.PerformAnimations(battle);
            });
            defend.onClick.RemoveAllListeners();
            counter.onClick.RemoveAllListeners();
            magicDefend.onClick.RemoveAllListeners();
            surrender.onClick.RemoveAllListeners();

            //TESTING
            defend.onClick.AddListener(delegate () {
                battle.SetDefensiveAction(Battle.Action.DEFEND);
            });
            counter.onClick.AddListener(delegate () {
                battle.SetDefensiveAction(Battle.Action.COUNTER);
            });
            magicDefend.onClick.AddListener(delegate () {
                battle.SetDefensiveAction(Battle.Action.MAGIC_DEFEND);
            });
            surrender.onClick.AddListener(delegate () {
                battle.SetDefensiveAction(Battle.Action.SURRENDER);
            });
        } else {
            attack.onClick.RemoveAllListeners();
            empoweredAttack.onClick.RemoveAllListeners();
            magic.onClick.RemoveAllListeners();
            item.onClick.RemoveAllListeners();
            defend.onClick.AddListener(delegate () {
                battle.SetDefensiveAction(Battle.Action.DEFEND);
                battleAnimations.PerformAnimations(battle);
            });
            counter.onClick.AddListener(delegate () {
                battle.SetDefensiveAction(Battle.Action.COUNTER);
                battleAnimations.PerformAnimations(battle);
            });
            magicDefend.onClick.AddListener(delegate () {
                battle.SetDefensiveAction(Battle.Action.MAGIC_DEFEND);
                battleAnimations.PerformAnimations(battle);
            });
            surrender.onClick.AddListener(delegate () {
                battle.SetDefensiveAction(Battle.Action.SURRENDER);
                battleAnimations.PerformAnimations(battle);
            });

            //TESTING
            attack.onClick.AddListener(delegate () {
                battle.SetOffensiveAction(Battle.Action.ATTACK);
            });
            empoweredAttack.onClick.AddListener(delegate () {
                battle.SetOffensiveAction(Battle.Action.EMPOWERED_ATTACK);
            });
            magic.onClick.AddListener(delegate () {
                battle.SetOffensiveAction(Battle.Action.MAGIC);
            });
            item.onClick.AddListener(delegate () {
                battle.SetOffensiveAction(Battle.Action.ITEM);
            });
        }
    }

}