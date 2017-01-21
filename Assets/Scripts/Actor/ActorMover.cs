using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMover : MonoBehaviour {

    public float MoveTime = 1f;

    private List<Tile> _path;
    private bool _moving;

    public void Awake() {
        _path = new List<Tile>();
    }

    public void SetPath(List<Tile> path) {
        _path = path;
    }

    void Update() {
        if (_path.Count > 0 && !_moving) {
            StartCoroutine(MoveToNextPoint());
        }
    }

    private IEnumerator MoveToNextPoint() {
        _moving = true;

        var currentTime = 0f;
        var startPostion = transform.localPosition;

        var targetPosition = _path[0].transform.localPosition;
        _path.RemoveAt(0);
        
        while (currentTime < MoveTime) {
            currentTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPostion, targetPosition, currentTime / MoveTime);
            yield return null;
        }

        _moving = false;
    }
}
