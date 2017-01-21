using System;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
    
    public void Awake() {
        var selectTarget = GetComponent<SelectTarget>();
        if (selectTarget != null) {
            selectTarget.Selected += SelectTarget_Selected;
        }
    }

    private void SelectTarget_Selected(object sender, EventArgs e) {
        ActorSystem.Instance.SelectActor(this);
    }

    public void SetSelected(bool isSelected) {
        if (isSelected) {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void GivePath(List<Tile> path) {
        foreach (var tile in path) {
            tile.Highlight();
        }
    }
}
