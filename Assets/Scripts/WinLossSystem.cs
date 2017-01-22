using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLossSystem : MonoBehaviour {

    public List<Tile> VictoryTiles;
	
    void Update() {
        foreach (var actor in ActorSystem.Instance.AllActors) {
            foreach (var tile in VictoryTiles) {
                var actorPoint = new Point(actor.transform.localPosition);
                var tilePoint = new Point(tile.transform.localPosition);
                if (actorPoint.Equals(tilePoint)) {
                    SceneLoader.LoadNextLevel();
                }
            }
        }
    }
}
