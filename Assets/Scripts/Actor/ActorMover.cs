using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorMover : MonoBehaviour {

    public float MoveTimePerTile = 1f;

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

        var distance = (targetPosition - startPostion).magnitude;
        var moveTime = MoveTimePerTile * distance;

        if (startPostion.x > targetPosition.x) {
            SetFacingDirection(false);
        } else if (startPostion.x < targetPosition.x) {
            SetFacingDirection(true);
        }
        
        while (currentTime < moveTime) {
            currentTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPostion, targetPosition, currentTime / moveTime);
            yield return null;
        }

        _moving = false;
    }

    private void SetFacingDirection(bool isRight) {
        transform.localScale = (isRight) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }
}
