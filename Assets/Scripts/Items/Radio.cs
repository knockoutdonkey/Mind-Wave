using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public static Radio instance;

    public AudioClip _staticClip;
    public AudioClip _momClip;
    public AudioClip _grampaClip;
    public AudioClip _mainClip;

    private Room currentLocation;
    private ActorType _currentType;

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

        var actorType = (ActorSystem.Instance.SelectedActor == null) ? ActorType.None : ActorSystem.Instance.SelectedActor.Type;

        instance.currentLocation = Floor.CurrentFloor.GetTile(instance.transform.localPosition).GetComponentInParent<Room>();
        Floor.CurrentFloor.CleanRadioWaves();
        instance.currentLocation.SendRadioWaves();

        Floor.CurrentFloor.TempColorRoomTiles(actorType);
        instance.PlayRadioSound(actorType);
    }

    private void PlayRadioSound(ActorType soundType) 
    {
        var source = GetComponent<AudioSource>();
        if (source != null && soundType != _currentType) {
            switch (soundType) {
                case ActorType.None:
                    source.clip = _staticClip;
                    break;
                case ActorType.Mom:
                    source.clip = _momClip;
                    break;
                case ActorType.Maid:
                    source.clip = _mainClip;
                    break;
                case ActorType.Grampa:
                    source.clip = _grampaClip;
                    break;
            }
            source.Play();
            _currentType = soundType;
        }
    }
}
