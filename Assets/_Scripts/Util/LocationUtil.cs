using UnityEngine;
using System.Collections;

public class LocationUtil {

    private static readonly float WAIT_CONSTANT = 0.00001F;

    /**
    Rotates a transform to face a direction over a specified time
    @param transform - Transform to rotate
    @param target - location to look at
    **/
    public static IEnumerator RotateToFaceTarget(Transform transform, Vector3 target, float milliseconds) {
        return RotateToDirection(transform, Quaternion.LookRotation(target - transform.position), milliseconds);
    }

    /**
    TODO: figure out the speed of rotation
    Rotates a transform to face a direction over a specified time
    @param transform - Transform to rotate
    @param direction - Direction to look
    **/
    public static IEnumerator RotateToDirection(Transform transform, Quaternion direction, float milliseconds) {
        float startTime = Time.time;
        float fracComplete = 0;
        while(fracComplete < 1) {
            fracComplete = (Time.time - startTime) / 0.5F; //TODO: replace this with milliseconds calculation
            transform.rotation = Quaternion.Slerp(transform.rotation, direction, fracComplete);//TODO: this should probably be Lerp
            yield return new WaitForSeconds(WAIT_CONSTANT);
        }
    }

    public static IEnumerator SlerpVector(Transform transform, Vector3 endPos, float milliseconds) {
        Debug.Log("start: " + Time.time);
        Vector3 startPos = transform.position;
        float startTime = Time.time;
        float fracComplete = 0;
        while(fracComplete < 1) {
            fracComplete = (Time.time - startTime) / 0.5F;//TODO: replace this with milliseconds calculation
            transform.position = Vector3.Slerp(startPos, endPos, fracComplete);
            yield return new WaitForSeconds(WAIT_CONSTANT);
        }
        Debug.Log("end: " + Time.time);
    }

}