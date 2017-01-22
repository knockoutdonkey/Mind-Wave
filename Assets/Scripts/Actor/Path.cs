using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Path
{
    private List<Tile> _tiles;
    private Table _endTable;

    public Path(List<Tile> tiles)
    {
        _tiles = tiles;
        _endTable = null;
    }

    public Path(List<Tile> tiles, Table table)
    {
        _tiles = tiles;
        _endTable = table;
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
}
