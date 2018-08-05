using UnityEngine;

public class Game : MonoBehaviour {

    public GameObject player;
    public GameObject s0_1, s0_2, s0_3, s1_1, s1_3, s1_4, s2_0, s2_1, s2_2, s2_3, s3_2, s3_3, s3_4;
    public GameObject turnMenu;

    private GameObject activeMenu;


	// Use this for initialization
	private void Start () {
        Vector3 startingPos = s2_0.transform.position;
        player.transform.SetPositionAndRotation(new Vector3(startingPos.x, startingPos.y + (float)2.5, startingPos.z), player.transform.rotation);
	}

	// Update is called once per frame
	private void Update () {

	}

    public void RollDice() {

    }
}
