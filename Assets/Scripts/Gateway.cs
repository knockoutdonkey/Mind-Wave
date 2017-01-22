using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gateway : MonoBehaviour
{

    public bool open;

    public Tile Exit;

    public List<Room> _rooms;

    void Awake()
    {
        _rooms = new List<Room>();
    }

    public void findConnectedRooms(Floor floor)
    {
        addRoom(floor, 1, 0);
        addRoom(floor, -1, 0);
        addRoom(floor, 0, 1);
        addRoom(floor, 0, -1);
    }

    private void addRoom(Floor floor, int xOffset, int yOffset)
    {
        var gateTile = GetComponent<Tile>();
        var neighborPoint = new Point(gateTile.transform.localPosition, xOffset, yOffset);
        var neighborTile = floor.GetTile(neighborPoint);
        if (neighborTile != null)
        {
            Room connectedRoom = neighborTile.GetComponentInParent<Room>();
            _rooms.Add(connectedRoom);
            connectedRoom.AddGateway(this);
        }

    }
}
