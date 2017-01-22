using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour {

    public static MovementSystem Instance;

    public Tile SelectedTile;

    private Floor _currentFloor;

    void Awake() {
        Instance = this;
    }

    public void SelectTile(Tile tile) {
        SelectedTile = tile;
        var actor = ActorSystem.Instance.SelectedActor;
        if (actor != null) {
            var actorPos = actor.transform.localPosition;

            var startTile = _currentFloor.GetTile(actorPos);
            var endTile = SelectedTile;

            if (startTile == null) {
                Debug.Log("Character is not on a tile: " + actorPos);
                return;
            }

            var path = new Path(FindPath(startTile, endTile));
            actor.GivePath(path);
        }
    }

    public void SelectTile(Tile tile, Table endTable)
    {
        SelectedTile = tile;
        var actor = ActorSystem.Instance.SelectedActor;
        if (actor != null)
        {
            var actorPos = actor.transform.localPosition;

            var startTile = _currentFloor.GetTile(actorPos);
            var endTile = SelectedTile;

            if (startTile == null)
            {
                Debug.Log("Character is not on a tile: " + actorPos);
                return;
            }

            var path = new Path(FindPath(startTile, endTile), endTable);
            actor.GivePath(path);
        }
    }

    public void SelectTile(Tile tile, Furniture endSeat)
    {
        SelectedTile = tile;
        var actor = ActorSystem.Instance.SelectedActor;
        if (actor != null)
        {
            var actorPos = actor.transform.localPosition;

            var startTile = _currentFloor.GetTile(actorPos);
            var endTile = SelectedTile;

            if (startTile == null)
            {
                Debug.Log("Character is not on a tile: " + actorPos);
                return;
            }

            var path = new Path(FindPath(startTile, endTile), endSeat);
            actor.GivePath(path);
        }
    }

    public void SetCurrentFloor(Floor floor) {
        _currentFloor = floor;
    }

    public static List<Tile> FindPath(Tile startTile, Tile endTile) {
        var nodes = new Dictionary<string, Node>();

        var startNode = new Node(0, startTile, null);

        nodes[startTile.GetPoint().GetCoord()] = startNode;

        var tileSearched = true;
        while (tileSearched) { // fix this if to end if no more new tiles

            tileSearched = false;

            var oldNodes = new Dictionary<string, Node>(nodes);
            foreach (var node in oldNodes.Values) {

                var nodePoint = node.Tile.GetPoint();

                if (!node.Visited) {
                    tileSearched = true;

                    // Check if the node is the final node.
                    if (node.Tile == endTile) {
                        return CreatePath(node);
                    }
                    node.Visited = true;

                    // Add all neighbors as new nodes.
                    for (int x = -1; x <= 1; x++) {
                        for (int y = -1; y <= 1; y++) {
                            var neighborPoint = new Point(nodePoint.X + x, nodePoint.Y + y);
                            var neighborTile = Instance._currentFloor.GetTile(neighborPoint);

                            if (neighborTile != null && !neighborTile.blocking) {
                                var extraDistance = Mathf.Sqrt(x * x + y * y);
                                var newDistance = node.Distance;
                                if (extraDistance > 1 && neighborTile.GetComponent<Gateway>() != null)
                                {
                                    newDistance += 2;
                                }
                                else
                                {
                                    newDistance += extraDistance;
                                }
                                var newNode = new Node(newDistance, neighborTile, node);

                                Node oldNode;
                                nodes.TryGetValue(neighborPoint.GetCoord(), out oldNode);

                                if (oldNode == null || oldNode.Distance > newNode.Distance) {
                                    nodes[neighborPoint.GetCoord()] = newNode;
                                }
                            }
                        }
                    }
                }
            }
        }

        return new List<Tile>();
    }

    private static List<Tile> CreatePath(Node targetNode) {
        var path = new List<Tile>();
        var currentNode = targetNode;

        while (currentNode != null) {
            path.Insert(0, currentNode.Tile);
            currentNode = currentNode.PreviousNode;
        }

        return path;
    }

    private class Node {
        public float Distance;
        public Tile Tile;
        public Node PreviousNode;
        public bool Visited;

        public Node(float distance, Tile tile, Node previousNode) {
            Distance = distance;
            Tile = tile;
            PreviousNode = previousNode;
            Visited = false;
        }
    }
}
