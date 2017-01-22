using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputSystem : MonoBehaviour {

    public UnityEngine.UI.GraphicRaycaster _graphicsRaycaster;

    public float CameraSpeed = 1f;

	void Start () {
		
	}
	
	void Update () {

        if (Input.GetMouseButtonDown(0)) {

            var results = Raycast();

            foreach (var result in results) {
                var selectTarget = result.gameObject.GetComponent<SelectTarget>();

                if (selectTarget != null) {
                    selectTarget.Select();
                }
            }
        } else if (Input.GetMouseButtonDown(1)) {
            var results = Raycast();

            foreach (var result in results) {
                var moveTarget = result.gameObject.GetComponent<MoveTarget>();

                if (moveTarget != null) {
                    moveTarget.SelectMove();
                }
            }
        }

        if (Input.GetKeyDown("r")) {
            SceneLoader.RestartCurrentLevel();
        }

        var cameraDirection = Vector3.zero;
        if (Input.GetKey("w")) {
            cameraDirection += Vector3.up;
        }
        if (Input.GetKey("a")) {
            cameraDirection += Vector3.left;
        }
        if (Input.GetKey("s")) {
            cameraDirection += Vector3.down;
        }
        if (Input.GetKey("d")) {
            cameraDirection += Vector3.right;
        }
        MoveCamera(cameraDirection);
    }

    private List<RaycastResult> Raycast() {
      //  var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        PointerEventData ped = new PointerEventData(null);
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        _graphicsRaycaster.Raycast(ped, results);
        return results;
    }
    
    private void MoveCamera(Vector3 direction) {
        Camera.main.transform.Translate(direction * CameraSpeed * Time.deltaTime);
    }
}
