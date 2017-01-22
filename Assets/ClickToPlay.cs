using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToPlay : MonoBehaviour {
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("hey");
            SceneLoader.LoadNextLevel();
        }
    }
}
