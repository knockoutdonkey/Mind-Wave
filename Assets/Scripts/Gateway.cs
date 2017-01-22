using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Gateway : MonoBehaviour
{

    public bool open;
    public bool locked;
    private Room _room;
    public List<Room> _rooms;
    public bool vent = false;

    void Awake()
    {
        _rooms = new List<Room>();
    }

    void Start()
    {
        _room = this.GetComponent<Room>();
    }

    public void findConnectedRooms()
    {
        if (_room != null)
        {
            var Tiles = _room.GetComponentsInChildren<Tile>();
            foreach (Tile tile in Tiles)
            {
                addRoom( 1, 0,tile);
                addRoom(-1, 0, tile);
                addRoom( 0, 1, tile);
                addRoom( 0, -1, tile);
            }
        }

    }


    private void addRoom( int xOffset, int yOffset, Tile gateTile)
    {
        var neighborPoint = new Point(gateTile.transform.localPosition, xOffset, yOffset);
        var neighborTile = Floor.CurrentFloor.GetTile(neighborPoint);
        if (neighborTile != null)
        {
            Room connectedRoom = neighborTile.GetComponentInParent<Room>();
            if (connectedRoom != _room)
            {
                _rooms.Add(connectedRoom);
                connectedRoom.AddGateway(this);
            }
        }

    }

    public Tile findSafeTile(Room badRoom)
    {
        Tile findTile;
        var Tiles = _room.GetComponentsInChildren<Tile>();
        foreach (Tile tile in Tiles)
        {
            
            
            findTile = checkTileConnection(1, 0, badRoom, tile);
            if (findTile != null) { return findTile; }
            findTile = checkTileConnection(-1, 0, badRoom, tile);
            if (findTile != null) { return findTile; }
            findTile = checkTileConnection(0, 1, badRoom, tile);
            if (findTile != null) { return findTile; }
            findTile = checkTileConnection(0, -1, badRoom, tile);
            if (findTile != null) { return findTile; }
        }
        return null;
    }

    private Tile checkTileConnection(int xOffset, int yOffset, Room badRoom, Tile gateTile)
    {
        var neighborPoint = new Point(gateTile.transform.localPosition, xOffset, yOffset);
        var neighborTile = Floor.CurrentFloor.GetTile(neighborPoint);
        if (neighborTile != null && neighborTile.GetRoom() != badRoom && neighborTile.GetRoom() != _room)
        {
            return neighborTile;
        }
        return null;
    }
}
