using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Path
{
    private List<Tile> _tiles;
    private Table _endTable;
    private Furniture _endSeat;

    public Path(List<Tile> tiles)
    {
        _tiles = tiles;
        _endTable = null;
        _endSeat = null;
    }

    public Path(List<Tile> tiles, Table table)
    {
        _tiles = tiles;
        _endTable = table;
        _endSeat = null;
    }

    public Path(List<Tile> tiles, Furniture seat)
    {
        _tiles = tiles;
        _endTable = null;
        _endSeat = seat;
    }

    public List<Tile> Tiles
    {
        get
        {
            return _tiles ??  new List<Tile>();

        }
    }

    public Table EndTable
    {
        get { return _endTable; }
    }

    public Furniture EndSeat
    {
        get { return _endSeat; }
    }
}
