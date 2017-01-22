using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DoorAnimator : MonoBehaviour {

    public Sprite DoorOpen;
    public Sprite DoorClosed;

    public Gateway ControllingGateway;

    private Image _doorImage;

    void Awake() {
        _doorImage = GetComponent<Image>();
    }

    void Update() {
        if (_doorImage == null || ControllingGateway == null) {
            return;
        }

        if (ControllingGateway.open) {
            _doorImage.sprite = DoorOpen;
        } else {
            _doorImage.sprite = DoorClosed;
        }
    }
}
