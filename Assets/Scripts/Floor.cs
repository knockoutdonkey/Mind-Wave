using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private static Floor CurrentFloor;

    private Dictionary<Point, Tile> _tiles;
    private List<Gateway> _gateways;

 
    void Awake() {
        _tiles = new Dictionary<Point, Tile>();
        _gateways = new List<Gateway>();

        if (CurrentFloor == null)
        {
            CurrentFloor = this;
        }

        var rooms = GetComponentsInChildren<Room>();
        foreach (var room in rooms)
        {

            var tiles = room.GetComponentsInChildren<Tile>();
            foreach (var tile in tiles)
            {
                var point = new Point(tile.transform.localPosition);
                _tiles[point] = tile;
            }

            
        }
    }


    public static Floor GetCurrentFloor()
    {
        return CurrentFloor;
    }

    void Start()
    {
        
        MovementSystem.Instance.SetCurrentFloor(this);
        var rooms = GetComponentsInChildren<Room>();
        
        foreach (var room in rooms)
        {
            var gateways = room.GetComponents<Gateway>();
            foreach (var gate in gateways)
            {
                gate.findConnectedRooms(this);
                _gateways.Add(gate);
            }
            
        }
        if (Radio.instance != null)
        {
            Radio.checkRadio();
        }
    }



    public Tile GetTile(int x, int y) {
        return GetTile(new Point(x, y));
    }

    public Tile GetTile(Point point) {
        Tile tile = null;
        _tiles.TryGetValue(point, out tile);
        return tile;

    }

    public Tile GetTile(Vector3 position)
    {
        return GetTile(new Point(position));
    }

    public void CleanRadioWaves()
    {

        var rooms = GetComponentsInChildren<Room>();
        foreach (var room in rooms)
        {
            room.radioWaveActive = false;
        }
    }

    public void TempColorRoomTiles()
    {
        var rooms = GetComponentsInChildren<Room>();
        foreach (var room in rooms)
        {
            if (room.radioWaveActive)
            {
                room.colorTiles(Color.yellow);
            }
            else
            {
                room.colorTiles(Color.white);
            }
    }
    }
}
