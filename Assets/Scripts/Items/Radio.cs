using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public static Radio instance;

    private Room currentLocation;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        checkRadio();
    }

    public static void checkRadio()
    {
        if (instance == null) return;

        instance.currentLocation = Floor.CurrentFloor.GetTile(instance.transform.localPosition).GetComponentInParent<Room>();
        Floor.CurrentFloor.CleanRadioWaves();
        instance.currentLocation.SendRadioWaves();
        Floor.CurrentFloor.TempColorRoomTiles();
    }

}
