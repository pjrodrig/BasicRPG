using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerModel : MonoBehaviour {

    private readonly Quaternion TO_CAMERA = Quaternion.LookRotation(Vector3.back);

    PlayerData playerData;

    public GameObject thisObj;

    private Space space;

    public void Init(Space space, PlayerData playerData) {
        this.space = space;
        this.playerData = playerData;
        thisObj.transform.position = space.position;
    }

    public void SetSpace(Space space) {
        this.space = space;
    }

    public Space GetSpace() {
        return this.space;
    }

    public void TraversePath(List<Space> path, CameraModel camera, Action callback) {
        StartCoroutine(Walk(path, camera, callback));
    }

    //TODO: break up this method
    IEnumerator Walk(List<Space> path, CameraModel camera, Action callback) {
        camera.FocusPosition(space.position);
        Vector3 offset = new Vector3(0, 0.2F, 0);
        Vector3 startPos = Vector3.zero;
        Vector3 endPos;
        float startTime;
        float fracComplete;
        float speed;
        foreach(Space space in path) {
            if(startPos != Vector3.zero) {
                endPos = space.position + offset;
                Vector3 dirFromAtoB = (endPos - thisObj.transform.position).normalized;
                float dotProd = Vector3.Dot(dirFromAtoB, thisObj.transform.forward);
                if(dotProd <= 0.9) {
                    StartCoroutine((LocationUtil.RotateToFaceTarget(thisObj.transform, endPos, 0)));
                    yield return new WaitForSeconds(0.2F);
                }
                speed = Vector3.Distance(startPos, endPos) / 20F;
                startTime = Time.time;
                fracComplete = 0;
                while(fracComplete < 1) {
                    fracComplete = (Time.time - startTime) / speed;
                    thisObj.transform.position = Vector3.Lerp(startPos, endPos, fracComplete);
                    camera.FocusPosition(thisObj.transform.position);
                    yield return new WaitForSeconds(0.00001F);
                }
            }
            startPos = space.position + offset;
        }
        StartCoroutine(LocationUtil.RotateToDirection(thisObj.transform, TO_CAMERA, 3F));
        space = path[path.Count - 1];
        playerData.space = space.id;
        callback();
    }

    void OnDestroy() {
        Destroy(thisObj);
    }
}