using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

    public GameObject playerCamera;
    public GameObject player;
    public GameObject s0_1, s0_2, s0_3, s1_1, s1_3, s1_4, s2_0, s2_1, s2_2, s2_3, s3_2, s3_3, s3_4;
    public GameObject turnMenu;
    public Text diceDisplay;
    public GameObject stopRollButton;
    public GameObject diceRollContainer;

    private bool rollingDice;
    private int currentRole;

	// Use this for initialization
	private void Start () {
        Vector3 startingPos = s2_0.transform.position;
        player.transform.SetPositionAndRotation(
            new Vector3(startingPos.x, startingPos.y + 2.5F, startingPos.z),
            player.transform.rotation
        );
        FocusPlayer();
	}

    void FocusPlayer() {
        Vector3 playerPos =player.transform.position;
        playerCamera.transform.SetPositionAndRotation(
            new Vector3(playerPos.x, playerPos.y + 5F, playerPos.z - 10F),
            playerCamera.transform.rotation
        );
    }

    IEnumerator FocusMap() {
        Vector3 startPos = playerCamera.transform.position;
        Vector3 endPos = new Vector3(0, 10, -10) + startPos;
        float startTime = Time.time;
        float fracComplete = 0;
        while(fracComplete < 1) {
            fracComplete = (Time.time - startTime) / 0.5F;
            playerCamera.transform.position = Vector3.Slerp(startPos, endPos, fracComplete);
            yield return new WaitForSeconds(0.00001F);
        }
    }

    public void RollDice() {
        turnMenu.SetActive(false);
        diceRollContainer.SetActive(true);
        rollingDice = true;
        StartCoroutine(RandomDiceNumber());
        StartCoroutine(FocusMap());
    }

    IEnumerator RandomDiceNumber() {
        float last = 0;
        float randomNumber;
        while(rollingDice) {
            while(rollingDice && (randomNumber = Mathf.Floor(UnityEngine.Random.value * 6) + 1) != last) {
                last = randomNumber;
                diceDisplay.text = randomNumber + "";
                yield return new WaitForSeconds(0.05F);
            }
        }
    }

    public void StopRollingDice() {
        rollingDice = false;
        stopRollButton.SetActive(false);
        currentRole = (int)Mathf.Floor(UnityEngine.Random.value * 6) + 1;
        diceDisplay.text = currentRole + "";
        StartCoroutine(PromptForMovement());
    }

    IEnumerator PromptForMovement() {
        yield return new WaitForSeconds(1F);
        Vector3 startPos = diceDisplay.transform.position;
        Vector3 endPos = new Vector3(420, 200, 0) + startPos;
        float startTime = Time.time;
        float fracComplete = 0;
        while(fracComplete < 1) {
            fracComplete = (Time.time - startTime) / 0.5F;
            diceDisplay.transform.position = Vector3.Slerp(startPos, endPos, fracComplete);
            yield return new WaitForSeconds(0.00001F);
        }
    }

}
