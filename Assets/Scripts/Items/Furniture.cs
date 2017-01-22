using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{

    private Tile _tile;

    public void Awake()
    {
        var selectTarget = GetComponent<SelectTarget>();
        if (selectTarget != null)
        {
            selectTarget.Selected += SelectTarget_Selected;
        }

        

    }
    // Use this for initialization
    void Start () {
        _tile = Floor.CurrentFloor.GetTile(this.transform.localPosition);
    }

    private void SelectTarget_Selected(object sender, EventArgs e)
    {

        if (_tile != null && ActorSystem.Instance.SelectedActor != null) {
            MovementSystem.Instance.SelectTile(_tile, this);
        }
    }

    public Tile tile { get { return _tile;} }
}
