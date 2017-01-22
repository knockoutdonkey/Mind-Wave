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
        Floor currentFloor = Floor.GetCurrentFloor();

        
        Tile actorsTile = currentFloor.GetTile(actor.transform.localPosition);

        Room actorsRoom = actorsTile.GetComponentInParent<Room>();
        if (actorsRoom.radioWaveActive)
        {
            if (SelectedActor == actor)
            {
                SelectedActor.SetSelected(false);
                SelectedActor.GivePath(new Path(MovementSystem.FindPath(actorsTile, actor.HomeTile)));
                SelectedActor = null;
            }
            else
            {
                if (SelectedActor != null)
                {
                    Tile oldActorTile = currentFloor.GetTile(SelectedActor.transform.localPosition);
                    SelectedActor.GivePath(new Path(MovementSystem.FindPath(oldActorTile, SelectedActor.HomeTile)));
                    SelectedActor.SetSelected(false);
                   
                }
               
                    SelectedActor = actor;
                    SelectedActor.SetSelected(true);
            }
        }

        var actorType = (SelectedActor == null) ? ActorType.None : actor.Type;
        Radio.checkRadio(actorType);
    }
} 
