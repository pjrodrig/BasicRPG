using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerModel : MonoBehaviour {

    private readonly Quaternion TO_CAMERA = Quaternion.LookRotation(Vector3.back);

    public GameObject thisObject;

    private Space space;

    public void Init(Space space) {
        this.space = space;
        thisObject.transform.position = space.position;
    }

    public void SetSpace(Space space) {
        this.space = space;
    }

    public Space GetSpace() {
        return this.space;
    }

    public void TraversePath(List<Space> path, CameraModel camera, Action<Space> callback) {
        StartCoroutine(Walk(path, camera, callback));
    }

    //TODO: break up this method
    IEnumerator Walk(List<Space> path, CameraModel camera, Action<Space> callback) {
        // playerCamera.FocusPosition(space.position);
        Vector3 offset = new Vector3(0, 0.2F, 0);
        Vector3 startPos = Vector3.zero;
        Vector3 endPos;
        float startTime;
        float fracComplete;
        float speed;
        foreach(Space space in path) {
            if(startPos != Vector3.zero) {
                endPos = space.position + offset;
                Vector3 dirFromAtoB = (endPos - thisObject.transform.position).normalized;
                float dotProd = Vector3.Dot(dirFromAtoB, thisObject.transform.forward);
                if(dotProd <= 0.9) {
                    StartCoroutine((LocationUtil.RotateToFaceTarget(thisObject.transform, endPos, 0)));
                    yield return new WaitForSeconds(0.2F);
                }
                speed = Vector3.Distance(startPos, endPos) / 20F;
                startTime = Time.time;
                fracComplete = 0;
                while(fracComplete < 1) {
                    fracComplete = (Time.time - startTime) / speed;
                    thisObject.transform.position = Vector3.Lerp(startPos, endPos, fracComplete);
                    // playerCamera.FocusPosition(go.transform.position);
                    yield return new WaitForSeconds(0.00001F);
                }
            }
            startPos = space.position + offset;
        }
        StartCoroutine(LocationUtil.RotateToDirection(thisObject.transform, TO_CAMERA, 3F));
        space = path[path.Count - 1];
        callback(space);
    }
}