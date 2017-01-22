using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorMover : MonoBehaviour {

    public float MoveTimePerTile = 1f;

    private Path _path;

    private ActorAnimator _actorAnimator;
    private bool _moving;
    private Actor _actor;

    public void Awake() {
        _path = new Path();
    }

    void Start()
    {
        _actor = this.GetComponent<Actor>();
        if (_actor == null)
        {
            Logger.Log("ActorMover with out Actor. Seriously?!");
        }
    }

    public void SetPath(Path path)
    {
        _path = path;
        _actorAnimator = GetComponent<ActorAnimator>();
    }

    void Update() {
        if (_path.Tiles.Count > 0 && !_moving) {
            StartCoroutine(MoveToNextPoint());
        }
    }

    private IEnumerator MoveToNextPoint() {

        if (_actorAnimator != null) {
            _actorAnimator.SetWalkingSpeed(MoveTimePerTile);
        }

        _moving = true;

        var currentTime = 0f;
        var startPostion = transform.localPosition;

        var targetPosition = _path.Tiles[0].transform.localPosition;
        tileCheck();
        _path.Tiles.RemoveAt(0);

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
            Holdable.moveItem(_actor.item, transform.localPosition);
            yield return null;
        }
        Logger.Log(_path.Tiles.Count);
        Logger.Log(_path.EndTable == null);
        if (_path.Tiles.Count == 0 && _path.EndTable != null)
        {
            _actor.swapWithTable(_path.EndTable);
        }

        if (_actorAnimator != null) {
            Debug.Log("hey");
            _actorAnimator.SetWalkingSpeed(0f);
        }
        _moving = false;
    }

    private void SetFacingDirection(bool isRight) {
        transform.localScale = (isRight) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }

    private void tileCheck()
    {
        if (_path.Tiles[0].GetComponent<Gateway>() != null)
        {
            _path.Tiles[0].GetComponent<Gateway>().open = true;
            Radio.checkRadio();
        }
        if (_path.Tiles[0].GetComponentInParent<Room>() != null)
        {
            Debug.Log(_path.Tiles[0].GetComponentInParent<Room>());
            if (!_path.Tiles[0].GetComponentInParent<Room>().radioWaveActive)
            {
                ActorSystem.Instance.SelectActor(ActorSystem.Instance.SelectedActor);
            }
        }
    }
}
