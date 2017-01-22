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
        
        Radio.instance.checkRadio();
        Floor.GetCurrentFloor().TempColorRoomTiles();
        Room actorsRoom = actorsTile.GetComponentInParent<Room>();
        if (actorsRoom.radioWaveActive)
        {
            if (SelectedActor == actor)
            {
                SelectedActor.SetSelected(false);
                SelectedActor.GivePath(MovementSystem.FindPath(actorsTile, actor.HomeTile));
            }
            if (SelectedActor != null)
            {
                SelectedActor.SetSelected(false);
            }

            SelectedActor = (SelectedActor == actor) ? null : actor;

            if (SelectedActor != null)
            {
                SelectedActor.SetSelected(true);
            }
        }
    }
}
