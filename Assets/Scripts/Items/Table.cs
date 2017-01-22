using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

    // Use this for initialization
    private Tile _tile;
    public Holdable heldItem;

    public void Awake()
    {
        var selectTarget = GetComponent<SelectTarget>();
        if (selectTarget != null)
        {
            selectTarget.Selected += SelectTarget_Selected;
        }
    }
    // Use this for initialization
    void Start()
    {
        _tile = Floor.GetCurrentFloor().GetTile(this.transform.localPosition);
    }

    private void SelectTarget_Selected(object sender, EventArgs e)
    {

        if (_tile != null && ActorSystem.Instance.SelectedActor != null)
        {
            MovementSystem.Instance.SelectTile(_tile,this);
        }
    }
}
