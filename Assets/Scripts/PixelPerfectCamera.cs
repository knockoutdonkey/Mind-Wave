using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PixelPerfectCamera : MonoBehaviour {

	void Start () {
        var screenHeight = Screen.height;

        var camera = GetComponent<Camera>();
        camera.orthographicSize = Screen.height / 200f;
	}
}
