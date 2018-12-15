using UnityEngine;
using System;
using System.Collections;

public class CameraModel : MonoBehaviour {

    private readonly float CAMERA_MOVEMENT_MULTIPLIER = 0.1F;
    private readonly float CAMERA_MOVEMENT_DECAY = 0.95F;
    private readonly float CAMERA_DECAY_THRESHOLD = 0.1F;

    Vector3 dragPos;
    Vector3 initalDragPos;
    Vector3 initalCameraPos;
    Vector3 previousCameraPos;
    Vector3 delta = Vector3.zero;
    bool dragging;

    public Camera thisObj;
    public bool Locked;

    void Update() {
        if(!Locked) {
            previousCameraPos = thisObj.transform.position;
            if (Input.GetMouseButton(0)) {
                if(!dragging){
                    dragging = true;
                    initalDragPos = Input.mousePosition;
                    initalCameraPos = thisObj.transform.position;
                }
                dragPos = Input.mousePosition;
                thisObj.transform.position = new Vector3(initalCameraPos.x + ((initalDragPos.x - dragPos.x) * (CAMERA_MOVEMENT_MULTIPLIER/ (Screen.height / 800))), 
                                                     initalCameraPos.y, 
                                                     initalCameraPos.z - ((dragPos.y - initalDragPos.y) * (CAMERA_MOVEMENT_MULTIPLIER / (Screen.height / 800))));
                delta = thisObj.transform.position - previousCameraPos;
            } else {
                dragging = false;
            }
            if(!dragging) {
                if(Math.Abs(delta.x) > CAMERA_DECAY_THRESHOLD || Math.Abs(delta.z) > CAMERA_DECAY_THRESHOLD) {
                    thisObj.transform.position = thisObj.transform.position + new Vector3(delta.x * CAMERA_MOVEMENT_DECAY, 0, delta.z * CAMERA_MOVEMENT_DECAY);
                    delta = thisObj.transform.position - previousCameraPos;
                }
            }
        }
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
        Vector3 startPos = thisObj.transform.position;
        Vector3 endPos = new Vector3(0, 10, -10) + startPos;
        float startTime = Time.time;
        float fracComplete = 0;
        while(fracComplete < 1) {
            fracComplete = (Time.time - startTime) / 0.5F;
            thisObj.transform.position = Vector3.Slerp(startPos, endPos, fracComplete);
            yield return new WaitForSeconds(0.00001F);
        }
    }

}