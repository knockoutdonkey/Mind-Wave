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
        if (SelectedActor != null) {
            SelectedActor.SetSelected(false);
        }

        SelectedActor = (SelectedActor == actor) ? null : actor;

        if (SelectedActor != null) {
            SelectedActor.SetSelected(true);
        }
    }
}
