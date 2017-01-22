using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSystem : MonoBehaviour {

    public static ActorSystem Instance;

    public Actor SelectedActor;

    // Note: Do not mutate this list from outside this class.
    public List<Actor> AllActors;

    void Awake() {
        Instance = this;
    }

    public void RegisterActor(Actor actor) {
        AllActors.Add(actor);
    }

    public void SelectActor(Actor actor) {
        
        Tile actorsTile = Floor.CurrentFloor.GetTile(actor.transform.localPosition);

        Radio.checkRadio();

        Floor.CurrentFloor.TempColorRoomTiles();
        Room actorsRoom = actorsTile.GetComponentInParent<Room>();
        if (actorsRoom.radioWaveActive)
        {
            if (SelectedActor == actor)
            {
                SelectedActor.SetSelected(false);
                SelectedActor.GivePath(new Path(MovementSystem.FindPath(actorsTile, SelectedActor.seat != null ? SelectedActor.seat.tile :SelectedActor.HomeTile), SelectedActor.seat));
                SelectedActor = null;
            }
            else
            {
                if (SelectedActor != null)
                {
                    Tile oldActorTile = Floor.CurrentFloor.GetTile(SelectedActor.transform.localPosition);
                    SelectedActor.GivePath(new Path(MovementSystem.FindPath(oldActorTile, SelectedActor.seat != null ? SelectedActor.seat.tile : SelectedActor.HomeTile), SelectedActor.seat));
                    SelectedActor.SetSelected(false);
                   
                }
               
                    SelectedActor = actor;
                    SelectedActor.SetSelected(true);
            }
        }
    }
} 
