using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Gateway : MonoBehaviour
{

    public bool open;

    public List<Room> _rooms;

    void Awake()
    {
        _rooms = new List<Room>();
    }

    public void findConnectedRooms(Floor floor)
    {
        Room room = this.GetComponent<Room>();
        if (room != null)
        {
            var Tiles = room.GetComponentsInChildren<Tile>();
            foreach (Tile tile in Tiles)
            {
                addRoom(floor, 1, 0,room,tile);
                addRoom(floor, -1, 0, room, tile);
                addRoom(floor, 0, 1, room, tile);
                addRoom(floor, 0, -1, room, tile);
            }
        }

    }


    private void addRoom(Floor floor, int xOffset, int yOffset,Room gateRoom, Tile gateTile)
    {
        var neighborPoint = new Point(gateTile.transform.localPosition, xOffset, yOffset);
        var neighborTile = floor.GetTile(neighborPoint);
        if (neighborTile != null)
        {
            Room connectedRoom = neighborTile.GetComponentInParent<Room>();
            if (connectedRoom != gateRoom)
            {
                _rooms.Add(connectedRoom);
                connectedRoom.AddGateway(this);
            }
        }

    }

}
