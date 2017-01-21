using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSystem : MonoBehaviour {

    public static ActorSystem Instance;

    public Actor SelectedActor;

    void Awake() {
        Instance = this;
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
