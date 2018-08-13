using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    public Camera go;//GameObject

	void Start() {

	}

    public void FocusPosition(Vector3 position) {
        GetComponent<Camera>().transform.SetPositionAndRotation(
            new Vector3(position.x, position.y + 10F, position.z - 12F),
            GetComponent<Camera>().transform.rotation
        );
    }

    public void ZoomOutToMap() {
        StartCoroutine(FocusMap());
    }

    public IEnumerator FocusMap() {
        Vector3 startPos = GetComponent<Camera>().transform.position;
        Vector3 endPos = new Vector3(0, 10, -10) + startPos;
        float startTime = Time.time;
        float fracComplete = 0;
        while(fracComplete < 1) {
            fracComplete = (Time.time - startTime) / 0.5F;
            GetComponent<Camera>().transform.position = Vector3.Slerp(startPos, endPos, fracComplete);
            yield return new WaitForSeconds(0.00001F);
        }
    }

}
