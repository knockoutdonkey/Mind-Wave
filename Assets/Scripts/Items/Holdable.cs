using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : MonoBehaviour
{


    private Tile _tile;
    public bool isKey = false;


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
        _tile = Floor.CurrentFloor.GetTile(this.transform.localPosition);
    }

    private void SelectTarget_Selected(object sender, EventArgs e)
    {
        if (_tile != null && ActorSystem.Instance.SelectedActor != null)
        {
          //  ActorSystem.Instance.SelectedActor.item = this;
          //  this.transform.position = ActorSystem.Instance.SelectedActor.gameObject.transform.position;
           // MovementSystem.Instance.SelectTile(_tile);
        }
    }

    public static void moveItem(Holdable item, Vector3 pos)
    {
        if (item != null)
        {
            item.transform.localPosition = pos;
        }

    }
}
