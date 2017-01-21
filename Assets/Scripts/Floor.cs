using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    private Dictionary<string, Tile> _tiles;
    private List<Gateway> _gateways; 

    void Awake() {
        _tiles = new Dictionary<string, Tile>();
        _gateways = new List<Gateway>();

        var rooms = GetComponentsInChildren<Room>();
        foreach (var room in rooms) {

            var tiles = room.GetComponentsInChildren<Tile>();
            foreach (var tile in tiles) {
                var point = new Point(tile.transform.localPosition);
                _tiles[point.X + " " + point.Y] = tile;
            }

            var gateways = room.GetComponentsInChildren<Gateway>();
            foreach (var gate in gateways)
            {
                gate.findConnectedRooms(this);
                _gateways.Add(gate);
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
        return GetTile(new Point(position));
    }
}
