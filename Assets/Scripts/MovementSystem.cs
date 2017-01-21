using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour {

    public static MovementSystem Instance;

    public Tile SelectedTile;

    void Awake() {
        Instance = this;
    }

    public void SelectTile(Tile tile) {
        SelectedTile = tile;
        var actor = ActorSystem.Instance.SelectedActor;
        if (actor != null) {
            actor.transform.position = tile.transform.position;
        }
    }
}
