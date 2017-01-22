using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public static Radio instance;
    public AudioClip _RadioClip;

    private Room currentLocation;
    private ActorType _currentType;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        checkRadio(ActorType.None);
    }

    public static void checkRadio(ActorType actorType)
    {
        if (instance == null) return;

        instance.currentLocation = Floor.GetCurrentFloor().GetTile(instance.transform.localPosition).GetComponentInParent<Room>();
        Floor.GetCurrentFloor().CleanRadioWaves();
        instance.currentLocation.SendRadioWaves();
        Floor.GetCurrentFloor().TempColorRoomTiles(actorType);

        instance.PlayRadioSound(actorType);
    }

    private void PlayRadioSound(ActorType soundType) 
    {
        var source = GetComponent<AudioSource>();
        Debug.Log(soundType);
        if (source != null && soundType != _currentType) {
            source.Play();
            switch (soundType) {
                case ActorType.None:
                    source.pitch = .7f;
                    break;
                case ActorType.Mom:
                    source.pitch = 1.3f;
                    break;
                case ActorType.Maid:
                    source.pitch = 1f;
                    break;
                case ActorType.Grampa:
                    source.pitch = -.3f;
                    break;
            }

            _currentType = soundType;
        }
    }
}
