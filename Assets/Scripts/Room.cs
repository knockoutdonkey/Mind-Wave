using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    private Dictionary<string, Tile> _tiles;

	void Awake () {
        _tiles = new Dictionary<string, Tile>();

        var tiles = GetComponentsInChildren<Tile>();
        foreach (var tile in tiles) {
            var pos = tile.transform.position;
            _tiles[((int)pos.x) + " " + ((int)pos.y)] = tile;
        }
	}

    public Tile GetTile(int x, int y) {
        Tile tile = null;
        _tiles.TryGetValue(x + " " + y, out tile);
        return tile;
    }
}
