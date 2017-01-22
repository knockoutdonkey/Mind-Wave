using System;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

    public bool blocking;

    void Awake() {
        var moveTarget = GetComponent<MoveTarget>();

        if (moveTarget != null) {
            moveTarget.MoveSelected += MoveTarget_MoveSelected;
        }
    }

    private void MoveTarget_MoveSelected(object sender, EventArgs e) {
        MovementSystem.Instance.SelectTile(this);
    }

    public Point GetPoint() {
        return new Point(transform.localPosition);
    }

    public Room GetRoom()
    {
        return this.GetComponentInParent<Room>();
    }

    public void Highlight() {
        var image = GetComponent<Image>();
        if (image != null) {
            image.color = Color.cyan;
        }
    }

    public void Highlight(Color color, bool isWavy)

    {
        var image = GetComponent<Image>();
        if (image == null) return;

        if (isWavy) {
            image.material = MaterialReference.GetWavyMaterial();
        } else {
            image.material = null;
        }

        image.color = color;
    }
}
