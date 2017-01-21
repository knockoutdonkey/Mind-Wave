using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    private Dictionary<Point, Tile> _tiles;

    void Awake() {
        _tiles = new Dictionary<Point, Tile>();

        var rooms = GetComponentsInChildren<Room>();
        foreach (var room in rooms) {

            var tiles = room.GetComponentsInChildren<Tile>();
            foreach (var tile in tiles) {
                var point = new Point(tile.transform.localPosition);
                _tiles[point] = tile;
            }
        }
    }

    void Start() {
        MovementSystem.Instance.SetCurrentFloor(this);
    }

    public Tile GetTile(int x, int y) {
        return GetTile(new Point(x, y));
    }

    public Tile GetTile(Point point) {
        Tile tile = null;
        _tiles.TryGetValue(point, out tile);
        return tile;
    }

    public Tile GetTile(Vector3 position) {
        return GetTile(new Point(position));
    }
}
