using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    private Dictionary<Point, Tile> _tiles;
    private List<Gateway> _gateways;

    public bool radioWaveActive;

	void Awake ()
	{
        _gateways = new List<Gateway>();

        radioWaveActive = false;
        _tiles = new Dictionary<Point, Tile>();


        var tiles = GetComponentsInChildren<Tile>();
        foreach (var tile in tiles) {
            var point = new Point(tile.transform.localPosition);
            _tiles[point] = tile;
        }
	}

    public Tile GetTile(int x, int y) {
        Tile tile = null;
        _tiles.TryGetValue(new Point(x, y), out tile);
        return tile;
    }

    public void AddGateway(Gateway gate)
    {
        if (!_gateways.Contains(gate))
        {
            _gateways.Add(gate);
        }

    }

    public void SendRadioWaves()
    {
        this.radioWaveActive = true;
        foreach (Gateway gateway in _gateways)
        {
            if (gateway.open)
            {
                foreach (Room room in gateway._rooms)
                {
                    room.radioWaveActive = true;
                    foreach (Gateway Othergateway in room._gateways)
                    {
                        var OthergateRoom = Othergateway.GetComponent<Room>();
                        if (OthergateRoom != null)
                        {
                            OthergateRoom.radioWaveActive = true;
                        }
                    }
                }
            }
            var gateRoom = gateway.GetComponent<Room>();
            if (gateRoom != null)
            {
                gateRoom.radioWaveActive = true;
            }
        }
    }

    public void colorTiles(Color color)
    {
        foreach (KeyValuePair<Point,Tile> tile in _tiles)
        {
            tile.Value.Highlight(color);
        }
    }

}
