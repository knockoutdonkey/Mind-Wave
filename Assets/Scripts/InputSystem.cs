using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputSystem : MonoBehaviour {

    public UnityEngine.UI.GraphicRaycaster _graphicsRaycaster;

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
    }

    private List<RaycastResult> Raycast() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        PointerEventData ped = new PointerEventData(null);
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        _graphicsRaycaster.Raycast(ped, results);
        return results;
    }
}
