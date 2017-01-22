using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LayeringSystem : MonoBehaviour {
	
	// Update is called once per frame.
	void Update () {

        bool updated = true;
        while (updated) {
            updated = false;

            Transform child = transform.GetChild(0);

            for (int i = 1; i < transform.childCount; i++) {
                var nextChild = transform.GetChild(i);
                if (child.localPosition.y < nextChild.localPosition.y) {
                    nextChild.SetSiblingIndex(i - 1);
                    updated = true;
                } else {
                    child = nextChild;
                }
            }
        }
    }
}
