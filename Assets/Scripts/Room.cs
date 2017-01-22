using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    private Dictionary<string, Tile> _tiles;
    private List<Gateway> _gateways;

    public bool radioWaveActive;

	void Awake ()
	{
        _gateways = new List<Gateway>();

        radioWaveActive = false;
        _tiles = new Dictionary<string, Tile>();

        var tiles = GetComponentsInChildren<Tile>();
        foreach (var tile in tiles) {
            var point = new Point(tile.transform.localPosition);
            _tiles[point.X + " " + point.Y] = tile;
        }
	}

    public Tile GetTile(int x, int y) {
        Tile tile = null;
        _tiles.TryGetValue(x + " " + y, out tile);
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
        foreach (Gateway gateway in _gateways)
        {
            foreach (Room room in gateway._rooms)
            {
                room.radioWaveActive = true;
            }
        }
    }

    public void colorTiles(Color color)
    {
        foreach (KeyValuePair<string,Tile> tile in _tiles)
        {
            tile.Value.Highlight(color);
        }
    }

}
