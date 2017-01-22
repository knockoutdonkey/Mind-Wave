using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private static Floor CurrentFloor;

    private Dictionary<string, Tile> _tiles;
    private List<Gateway> _gateways;

    void Awake()
    {
        CurrentFloor = this;
        Logger.Log(CurrentFloor);
        _tiles = new Dictionary<string, Tile>();
        _gateways = new List<Gateway>();

        var rooms = GetComponentsInChildren<Room>();
        foreach (var room in rooms)
        {

            var tiles = room.GetComponentsInChildren<Tile>();
            foreach (var tile in tiles)
            {
                var point = new Point(tile.transform.localPosition);
                _tiles[point.X + " " + point.Y] = tile;
            }

            
        }
    }


    public static Floor GetCurrentFloor()
    {
        return CurrentFloor;
    }

    void Start()
    {
        if (CurrentFloor == null)
        {
            CurrentFloor = this;
        }
        MovementSystem.Instance.SetCurrentFloor(this);
        var rooms = GetComponentsInChildren<Room>();
        
        foreach (var room in rooms)
        {
            var gateways = room.GetComponentsInChildren<Gateway>();
            foreach (var gate in gateways)
            {
                gate.findConnectedRooms(this);
                _gateways.Add(gate);
            }
        }

    }

    public Tile GetTile(int x, int y)
    {
        Tile tile = null;
        _tiles.TryGetValue(x + " " + y, out tile);
        return tile;
    }

    public Tile GetTile(Point point)
    {
        return GetTile(point.X, point.Y);
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
