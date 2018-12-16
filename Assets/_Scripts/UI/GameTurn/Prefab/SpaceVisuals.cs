using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceVisuals : MonoBehaviour {

    public GameObject spaceHighlight;
    public GameObject spacePointer;

    private bool highlight = false;

    public void HighlightSpaces(List<Vector3> spaces) {
        this.highlight = true;
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        Vector3 highlightOffset = new Vector3(0, -0.2F, 0);
        Vector3 pointerOffset = new Vector3(0, 7F, 0);
        foreach(Vector3 space in spaces) {
            StartCoroutine(SpaceHighlightPulse(Instantiate(this.spaceHighlight, space + highlightOffset, rotation)));
            StartCoroutine(SpacePointerBob(Instantiate(this.spacePointer, space + pointerOffset, rotation)));
        }
    }

    public void EndHighlight() {
        this.highlight = false;
    }

    IEnumerator SpaceHighlightPulse(GameObject spaceHighlight) {
        spaceHighlight.SetActive(true);
        bool grow = true;
        int count = 0;
        while(this.highlight) {
            count++;
            if(grow) {
                spaceHighlight.transform.localScale += new Vector3(0.02F, 0, 0.02F);
            } else {
                spaceHighlight.transform.localScale += new Vector3(-0.02F, 0, -0.02F);
            }
            if (count == 50) {
                count = 0;
                grow = !grow;
            }
            yield return new WaitForSeconds(0.01F);
        }
        Destroy(spaceHighlight);
    }

    IEnumerator SpacePointerBob(GameObject spacePointer) {
        spacePointer.SetActive(true);
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        bool rise = true;
        int count = 0;
        while(this.highlight) {
            count++;
            if(rise) {
                spacePointer.transform.SetPositionAndRotation(spacePointer.transform.position + new Vector3(0, 0.025F, 0), rotation);
            } else {
                spacePointer.transform.SetPositionAndRotation(spacePointer.transform.position + new Vector3(0, -0.025F, 0), rotation);
            }
            if(count == 50) {
                count = 0;
                rise = !rise;
            }
            yield return new WaitForSeconds(0.01F);
        }
        Destroy(spacePointer);
    }
}