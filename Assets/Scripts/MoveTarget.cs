using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour {

    public event EventHandler MoveSelected;

    public void SelectMove() {
        if (MoveSelected != null) {
            MoveSelected.Invoke(this, EventArgs.Empty);
        }
    }
}
