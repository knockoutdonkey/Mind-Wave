using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gateway : MonoBehaviour
{

    public bool open;

    public Tile Exit;

    public Room[] connectedRooms = new Room[2];

    public void findConnectedRooms(Floor floor)
    {
        int roomsIdx =0;
        // Add all neighbors as new nodes.
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (roomsIdx > 1)
                {
                    break;
                }
                var gateTile = GetComponent<Tile>();
                var neighborTile = floor.GetTile(gateTile.transform.position);
                if (neighborTile != null)
                {
                    connectedRooms[roomsIdx] = neighborTile.GetComponentInParent<Room>();
                }
                roomsIdx++;
            }
        }
    }

    public Room getNeighbor(Room start)
    {
        bool connected = false;
        Room Neighbor = null;
        foreach (Room room in connectedRooms)
        {

            if (room.name == start.name)
            {
                connected = true;
            }
            else
            {
                Neighbor = room;
            }
            
        }
        if (connected)
        {
            return Neighbor;
        }
        return null;
    }


}
