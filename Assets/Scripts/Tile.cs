using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
