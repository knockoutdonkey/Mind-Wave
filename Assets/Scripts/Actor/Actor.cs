using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour {

    private ActorMover _actorMover;
    public Tile HomeTile;
    
    public void Awake() {
        var selectTarget = GetComponent<SelectTarget>();
        if (selectTarget != null) {
            selectTarget.Selected += SelectTarget_Selected;
        }

        _actorMover = GetComponent<ActorMover>();
    }

    public void Start() {
        ActorSystem.Instance.RegisterActor(this);
        HomeTile = Floor.GetCurrentFloor().GetTile(this.transform.localPosition);
    }

    private void SelectTarget_Selected(object sender, EventArgs e) {
        ActorSystem.Instance.SelectActor(this);
    }

    public void SetSelected(bool isSelected) {
        var image = GetComponent<Image>();
        if (image != null) {
            if (isSelected) {
                image.color = Color.magenta;
            } else {
                image.color = Color.white;
            }
        }
    }

    public void GivePath(List<Tile> path) {
        foreach (var tile in path) {
            tile.Highlight();
        }

        if (_actorMover != null) {
            _actorMover.SetPath(path);
        }
    }
}
