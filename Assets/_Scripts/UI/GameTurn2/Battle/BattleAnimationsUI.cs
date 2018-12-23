using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BattleAnimationsUI : MonoBehaviour {

    BattleUI battle;

    public GameObject home;
    public GameObject away;
    public GameObject homeAttack;
    public GameObject awayAttack;
    public GameObject homeAttackSwing;
    public GameObject awayAttackSwing;
    public GameObject homeEmpower;
    public GameObject awayEmpower;
    public GameObject homeEmpowerAttack;
    public GameObject awayEmpowerAttack;
    public GameObject homeMagic;
    public GameObject awayMagic;
    public GameObject homeMagicBuildup;
    public GameObject awayMagicBuildup;
    public GameObject homeDefend;
    public GameObject awayDefend;
    public GameObject homeFailDefend;
    public GameObject awayFailDefend;
    public GameObject homeMagicDefend;
    public GameObject awayMagicDefend;
    public GameObject homeFailMagicDefend;
    public GameObject awayFailMagicDefend;
    public GameObject homeFlag;
    public GameObject awayFlag;
    public Image maxHpBar;
    public Image homeHpBar;
    public Text homeHpText;
    public Image awayHpBar;
    public Text awayHpText;

    public void Init(BattleUI battle) {
        this.battle = battle;
    }

    public void Deactivate() {
        homeAttack.SetActive(false);
        awayAttack.SetActive(false);
        homeAttackSwing.SetActive(false);
        awayAttackSwing.SetActive(false);
        homeEmpower.SetActive(false);
        awayEmpower.SetActive(false);
        homeEmpowerAttack.SetActive(false);
        awayEmpowerAttack.SetActive(false);
        homeMagic.SetActive(false);
        awayMagic.SetActive(false);
        homeMagicBuildup.SetActive(false);
        awayMagicBuildup.SetActive(false);
        homeDefend.SetActive(false);
        awayDefend.SetActive(false);
        homeFailDefend.SetActive(false);
        awayFailDefend.SetActive(false);
        homeMagicDefend.SetActive(false);
        awayMagicDefend.SetActive(false);
        homeFailMagicDefend.SetActive(false);
        awayFailMagicDefend.SetActive(false);
        homeFlag.SetActive(false);
        awayFlag.SetActive(false);
    }

    public void UpdateStats(Battle battleInstance) {
        int currentHp = battleInstance.home.GetCurrentStats().hp;
        int maxHp = battleInstance.home.GetStats().hp;
        homeHpBar.rectTransform.sizeDelta = new Vector2(((float)currentHp/maxHp) * maxHpBar.rectTransform.rect.width, homeHpBar.rectTransform.rect.height);
        homeHpText.text = currentHp + "/" + maxHp;
        currentHp = battleInstance.away.GetCurrentStats().hp;
        maxHp = battleInstance.away.GetStats().hp;
        awayHpBar.rectTransform.sizeDelta = new Vector2(((float)currentHp/maxHp) * maxHpBar.rectTransform.rect.width, homeHpBar.rectTransform.rect.height);
        awayHpText.text = currentHp + "/" + maxHp;
    }

    public void PerformAnimations(Battle battleInstance) {
        bool attacking = battleInstance.attacking;
        Battle.Action attack = battleInstance.GetOffensiveAction();
        Battle.Action defense = battleInstance.GetDefensiveAction();
        if(defense == Battle.Action.SURRENDER) {
            Surrender(attacking);
        } else {
            switch(attack) {
                case Battle.Action.ATTACK:
                    Attack(attacking);
                    PerformDefense(attacking, defense, defense != Battle.Action.DEFEND);
                    break;
                case Battle.Action.MAGIC:
                    Magic(attacking);
                    PerformDefense(attacking, defense, defense != Battle.Action.MAGIC_DEFEND);
                    break;
                case Battle.Action.EMPOWERED_ATTACK: // empowered attack
                    GameObject empower = attacking ? homeEmpower : awayEmpower;
                    StartCoroutine(FadeIn(empower, delegate () {
                        empower.SetActive(false);
                        bool failDefend = defense != Battle.Action.COUNTER;
                        if(failDefend) {
                            EmpowerAttack(attacking);
                        }
                        PerformDefense(attacking, defense, failDefend);
                    }));
                    break;
                case Battle.Action.ITEM:
                    PerformDefense(attacking, defense, false);
                    break;
            }
        }
    }

    void PerformDefense(bool attacking, Battle.Action defense, bool fail) {
        GameObject defendObj;
        switch (defense) {
            case Battle.Action.DEFEND:
                defendObj = attacking ? awayDefend : homeDefend;
                StartCoroutine(FadeIn(defendObj, delegate () {
                    if(fail) {
                        defendObj.SetActive(false);
                        defendObj = attacking ? awayFailDefend : homeFailDefend;
                    }
                    StartCoroutine(FadeOut(defendObj, 2F));
                }));
                break;
            case Battle.Action.MAGIC_DEFEND:
                defendObj = attacking ? awayMagicDefend : homeMagicDefend;
                StartCoroutine(FadeIn(defendObj, delegate () {
                    if(fail) {
                        defendObj.SetActive(false);
                        defendObj = attacking ? awayFailMagicDefend : homeFailMagicDefend;
                    }
                    StartCoroutine(FadeOut(defendObj, 2F));
                }));
                break;
            case Battle.Action.COUNTER:
                if(!fail) {
                    Attack(!attacking);
                }
                break;
        }
    }

    public IEnumerator ApplyDamage(bool attacking, BattleParticipant target, int damage) {
        int damageDealt = 0;
        int maxHp = target.GetStats().hp;
        int currentHp = target.GetCurrentStats().hp;
        Image hpBar = attacking ? awayHpBar : homeHpBar;
        Text hpText = attacking ? awayHpText : homeHpText;
        for(float i = 1; i <= 10; i++) {
            damageDealt = (int)(damage * i/10);
            hpBar.rectTransform.sizeDelta = new Vector2(((float)(currentHp - damageDealt)/maxHp) * maxHpBar.rectTransform.rect.width, hpBar.rectTransform.rect.height);
            hpText.text = currentHp - damageDealt + "/" + maxHp;
            yield return new WaitForSeconds(0.001F);
        }
        battle.ResolveRound();
    }

    IEnumerator AttackSwing(bool attacking, Action callback) {
        GameObject attackSwing = attacking ? awayAttackSwing : homeAttackSwing;
        attackSwing.SetActive(true);
        yield return new WaitForSeconds(0.4F);
        StartCoroutine(Shake(attacking ? home : away));
        Quaternion rotation = attackSwing.transform.rotation;
        for(int i = 0; i < 10; i++) {
            attackSwing.transform.Rotate(0, 0, 10 * (attacking ? -1 : 1));
            yield return new WaitForSeconds(0.001F);
        }
        callback();
        yield return new WaitForSeconds(0.2F);
        attackSwing.SetActive(false);
        attackSwing.transform.rotation = rotation;
    }

    void Attack(bool attacking) {
        StartCoroutine(AttackSwing(attacking, delegate () {
            GameObject attackObj = attacking ? awayAttack : homeAttack;
            StartCoroutine(FadeIn(attackObj, delegate () {
                battle.DealDamage();
                StartCoroutine(FadeOut(attackObj, 0));
            }));
        }));
    }

    void Magic(bool attacking) {
        GameObject magicBuildup = attacking ? homeMagicBuildup : awayMagicBuildup;
        StartCoroutine(FadeIn(magicBuildup, delegate () {
            magicBuildup.SetActive(false);
            StartCoroutine(Shake(attacking ? home : away));
            GameObject magicObj = attacking ? awayMagic : homeMagic;
            StartCoroutine(FadeIn(magicObj, delegate () {
                battle.DealDamage();
                StartCoroutine(FadeOut(magicObj, 0));
            }));
        }));
    }

    void EmpowerAttack(bool attacking) {
        StartCoroutine(AttackSwing(attacking, delegate () {
            GameObject attack = attacking ? awayEmpowerAttack : homeEmpowerAttack;
            StartCoroutine(FadeIn(attack, delegate () {
                battle.DealDamage();
                StartCoroutine(FadeOut(attack, 0));
            }));
        }));
    }

    void Surrender(bool attacking) {
        StartCoroutine(WaveFlag(attacking ? awayFlag : homeFlag, 
            attacking ? -1 : 1));
    }

    IEnumerator WaveFlag(GameObject flag, int swingDirection) {
        flag.SetActive(true);
        yield return new WaitForSeconds(0.4F);
        Quaternion rotation = flag.transform.rotation;
        for(int i = 0; i < 72; i++) {
            flag.transform.Rotate(0, 10 * swingDirection, 0);
            yield return new WaitForSeconds(0.03F);
        }
        yield return new WaitForSeconds(0.2F);
        flag.SetActive(false);
        flag.transform.rotation = rotation;
    }

    IEnumerator Shake(GameObject unit) {
        for(int i = 0; i < 5; i++) {
            unit.transform.position = (Vector2) unit.transform.position + Vector2.one;
            yield return new WaitForSeconds(0.001F);
        }
        for(int i = 0; i < 5; i++) {
            unit.transform.position = (Vector2) unit.transform.position - Vector2.one;
            yield return new WaitForSeconds(0.001F);
        }
    }

    IEnumerator FadeIn(GameObject obj, Action action) {
        obj.SetActive(true);
        CanvasRenderer renderer = obj.GetComponent<CanvasRenderer>();
        for(int i = 0; i < 100; i++) {
            Color color = renderer.GetColor();
            color.a = i * 0.01F;
            renderer.SetColor(color);
            yield return new WaitForSeconds(0.001F);
        }
        if(action != null) {
            action();
        }
    }

    IEnumerator FadeOut(GameObject obj, float delay) {
        obj.SetActive(true);
        yield return new WaitForSeconds(delay);
        CanvasRenderer renderer = obj.GetComponent<CanvasRenderer>();
        for(int i = 0; i < 100; i++) {
            Color color = renderer.GetColor();
            color.a = 1 - (i * 0.01F);
            renderer.SetColor(color);
            yield return new WaitForSeconds(0.01F);
        }
        obj.SetActive(false);
    }

}