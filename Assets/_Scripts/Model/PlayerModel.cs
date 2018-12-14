using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerModel : MonoBehaviour {

    private readonly Quaternion TO_CAMERA = Quaternion.LookRotation(Vector3.back);

    public GameObject go;

    private Space space;

    public void Initialize(Space space) {
        this.space = space;
    }

    public void SetSpace(Space space) {
        this.space = space;
    }

    public Space GetSpace() {
        return this.space;
    }

    public void TraversePath(List<Space> path) {
        StartCoroutine(Walk(path));
    }

    //TODO: break up this method
    IEnumerator Walk(List<Space> path) {
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
                Vector3 dirFromAtoB = (endPos - go.transform.position).normalized;
                float dotProd = Vector3.Dot(dirFromAtoB, go.transform.forward);
                if(dotProd <= 0.9) {
                    StartCoroutine((LocationUtil.RotateToFaceTarget(go.transform, endPos, 0)));
                    yield return new WaitForSeconds(0.2F);
                }
                speed = Vector3.Distance(startPos, endPos) / 20F;
                startTime = Time.time;
                fracComplete = 0;
                while(fracComplete < 1) {
                    fracComplete = (Time.time - startTime) / speed;
                    go.transform.position = Vector3.Lerp(startPos, endPos, fracComplete);
                    // playerCamera.FocusPosition(go.transform.position);
                    yield return new WaitForSeconds(0.00001F);
                }
            }
            startPos = space.position + offset;
        }
        StartCoroutine(LocationUtil.RotateToDirection(go.transform, TO_CAMERA, 3F));
        this.space = path[path.Count - 1];
        //TODO callback?
    }
}