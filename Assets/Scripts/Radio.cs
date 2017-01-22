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



    public void checkRadio()
    {
        currentLocation = Floor.GetCurrentFloor().GetTile(this.transform.localPosition).GetComponentInParent<Room>();
        Floor.GetCurrentFloor().CleanRadioWaves();
        currentLocation.SendRadioWaves();
    }

}
