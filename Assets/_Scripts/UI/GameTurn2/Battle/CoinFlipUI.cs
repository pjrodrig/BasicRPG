using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CoinFlipUI : MonoBehaviour {

    BattleUI battle;
    bool active;
    float initialCoinWidth;
    bool flipping;
    int sidePicked;
    int randomPick;
    int flipSpeed;
    bool showHeads;
    bool grow;

    public GameObject thisObj;
    public Button heads;
    public Button tails;
    public RawImage coinHeads;
    public RawImage coinTails;

    public void Init(BattleUI battle) {
        this.battle = battle;
    }

    public void Activate() {
        if(!active) {
            thisObj.SetActive(true);
            if(initialCoinWidth == 0) {
                initialCoinWidth = coinHeads.rectTransform.rect.width;
            }
            heads.onClick.AddListener(delegate () {sidePicked = 1;});
            tails.onClick.AddListener(delegate () {sidePicked = 2;});
            randomPick = (int) Mathf.Floor(UnityEngine.Random.value * 2);
            flipSpeed = 5;
            sidePicked = 0;
            coinHeads.rectTransform.sizeDelta = new Vector2(0, coinHeads.rectTransform.rect.height);
            StartCoroutine(FlipCoin());
            active = true;
        }
    }
    
    public void Deactivate() {
        if(active) {
            thisObj.SetActive(false);
            heads.onClick.RemoveAllListeners();
            tails.onClick.RemoveAllListeners();
            sidePicked = 0;
            active = false;
        }
    }

    void FlipResult() {
        battle.SetFlipResult(randomPick + 1 == sidePicked);
        Deactivate();
    }

    IEnumerator FlipCoin() {
        flipping = true;
        while(flipping) {
            if(showHeads) {
                if(grow) {
                    coinHeads.rectTransform.sizeDelta = new Vector2(coinHeads.rectTransform.rect.width + (initialCoinWidth/flipSpeed), coinHeads.rectTransform.rect.height);
                    if(coinHeads.rectTransform.rect.width >= initialCoinWidth) {
                        grow = false;
                        if(sidePicked > 0) {
                            flipSpeed++;
                            flipping = randomPick == 1 || flipSpeed < 18;
                            if(!flipping) {
                                yield return new WaitForSeconds(2F);
                                FlipResult();
                            }
                        }
                    }
                } else {
                    coinHeads.rectTransform.sizeDelta = new Vector2(coinHeads.rectTransform.rect.width - (initialCoinWidth/flipSpeed), coinHeads.rectTransform.rect.height);
                    if(coinHeads.rectTransform.rect.width < 1) {
                        showHeads = false;
                        grow = true;
                        if(sidePicked > 0) {
                            flipSpeed++;
                        }
                    }
                }
            } else {
                if(grow) {
                    coinTails.rectTransform.sizeDelta = new Vector2(coinTails.rectTransform.rect.width + (initialCoinWidth/flipSpeed), coinTails.rectTransform.rect.height);
                    if(coinTails.rectTransform.rect.width >= initialCoinWidth) {
                        grow = false;
                        if(sidePicked > 0) {
                            flipSpeed++;
                            flipping = randomPick == 0 || flipSpeed < 18;
                            if(!flipping) {
                                yield return new WaitForSeconds(2F);
                                FlipResult();
                            }
                        }
                    }
                } else {
                    coinTails.rectTransform.sizeDelta = new Vector2(coinTails.rectTransform.rect.width - (initialCoinWidth/flipSpeed), coinTails.rectTransform.rect.height);
                    if(coinTails.rectTransform.rect.width < 1) {
                        showHeads = true;
                        grow = true;
                        if(sidePicked > 0) {
                            flipSpeed++;
                        }
                    }
                }
            }
            yield return new WaitForSeconds(0.00001F);
        }
    }
}