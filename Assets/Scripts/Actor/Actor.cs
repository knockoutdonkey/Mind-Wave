using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour {

    private ActorMover _actorMover;
    public Tile HomeTile;
    public Holdable item;
    public ActorType Type;
    
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

    public void GivePath(Path path) {
        Floor.GetCurrentFloor().TempColorRoomTiles(Type);

        if (_actorMover != null) {
            _actorMover.SetPath(path);
        }
    }

    public void swapWithTable(Table table)
    {
        Holdable temp = this.item;
        this.item = table.heldItem;
        table.heldItem = temp;
        Holdable.moveItem(table.heldItem, table.transform.localPosition);
        Holdable.moveItem(item, this.transform.localPosition);
    }


}
