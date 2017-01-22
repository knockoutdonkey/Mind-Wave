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
    public Gateway LastGateway;
    public Tile LastTile;
    public bool willSit = false; 
    public Furniture seat;
    public bool sitting;
    public bool Scary = false;
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
        HomeTile = Floor.CurrentFloor.GetTile(this.transform.localPosition);
        LastTile = HomeTile;
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

    public Tile GetCurrentTile()
    {
        return Floor.CurrentFloor.GetTile(transform.localPosition);
    }

    public Room GetCurrentRoom()
    {
        return Floor.CurrentFloor.GetTile(transform.localPosition).GetComponentInParent<Room>();
    }

    public void GivePath(Path path) {
        Floor.CurrentFloor.TempColorRoomTiles(Type);
        sitting = false;

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

    public void runAway(Room ScaryRoom)
    {
        Gateway safeGate = LastGateway;
        if (safeGate == null)
        {
            foreach (Gateway attachedGate in GetCurrentRoom()._gateways)
            {
                if (!attachedGate.vent)
                {
                    safeGate = attachedGate;
                }
            }
        }

        Tile gotoTile = HomeTile;
        if (HomeTile.GetRoom() == ScaryRoom)
        {
            foreach (Room safeRoom in safeGate._rooms)
            {
                if (safeRoom != ScaryRoom)
                {
                    gotoTile = safeGate.findSafeTile(ScaryRoom);
                }
            }
        }

        GivePath(new Path(MovementSystem.FindPath(this.GetCurrentTile(), gotoTile)));
    }
}
