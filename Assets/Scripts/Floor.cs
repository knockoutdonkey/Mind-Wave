using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    private Dictionary<string, Tile> _tiles;

    void Awake() {
        _tiles = new Dictionary<string, Tile>();

        var rooms = GetComponentsInChildren<Room>();
        foreach (var room in rooms) {

            var tiles = room.GetComponentsInChildren<Tile>();
            foreach (var tile in tiles) {
                var pos = tile.transform.localPosition;
                _tiles[Mathf.RoundToInt(pos.x) + " " + Mathf.RoundToInt(pos.y)] = tile;
            }
        }
    }

    void Start() {
        MovementSystem.Instance.SetCurrentFloor(this);
    }

    public Tile GetTile(int x, int y) {
        Tile tile = null;
        _tiles.TryGetValue(x + " " + y, out tile);
        return tile;
    }

    public Tile GetTile(Point point) {
        return GetTile(point.X, point.Y);
    }

    public Tile GetTile(Vector3 position) {
        return GetTile((int)position.x, (int)position.y);
    }
}
