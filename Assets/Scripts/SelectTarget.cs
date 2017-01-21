using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTarget : MonoBehaviour {

    public event EventHandler Selected;

	public void Select() {
        if (Selected != null) {
            Selected.Invoke(this, EventArgs.Empty);
        }
    }
}
