using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    private Dictionary<string, Tile> _tiles;

	void Awake () {
        _tiles = new Dictionary<string, Tile>();

        var tiles = GetComponentsInChildren<Tile>();
        foreach (var tile in tiles) {
            var point = new Point(tile.transform.localPosition);
            _tiles[point.X + " " + point.Y] = tile;
        }
	}

    public Tile GetTile(int x, int y) {
        Tile tile = null;
        _tiles.TryGetValue(x + " " + y, out tile);
        return tile;
    }
}
