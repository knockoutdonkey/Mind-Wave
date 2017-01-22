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
        if (!roomCheck())
        {
            _moving = false;
            yield break;
        }
        _actor.LastTile = _path.Tiles[0];
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
            Holdable.moveItem(_actor.item, transform.localPosition + new Vector3(0, 0, -1));
            yield return null;
        }

        if (_path.Tiles.Count == 0)
        {
            if (  _path.EndTable != null)
            {
                _actor.swapWithTable(_path.EndTable);
            }

            if (_path.EndSeat != null && _actor.willSit)
            {
                _actor.seat =_path.EndSeat;
                _actor.HomeTile = _path.EndSeat.tile;
                _actor.sitting = true;
                if (_actor.Scary)
                foreach (Actor checkActor in ActorSystem.Instance.AllActors)
                {
                    if (checkActor.GetCurrentRoom() == _actor.GetCurrentRoom() &&  checkActor != _actor)
                    {
                        checkActor.runAway(checkActor.GetCurrentRoom());
                    }

                }

            }
        }

        if (_actorAnimator != null) {
            _actorAnimator.SetWalkingSpeed(0f);
        }
        _moving = false;
    }

    private void SetFacingDirection(bool isRight) {
        transform.localScale = (isRight) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }

    private bool roomCheck()
    {
        Room checkRoom = _path.Tiles[0].GetComponentInParent<Room>();

        if (checkRoom != null)
        {


            var gate = checkRoom.GetComponent<Gateway>();
            if (gate != null)
            {
                if (gate.locked)
                {
                    if (_actor.item != null && _actor.item.isKey)
                    {
                        gate.open = true;
                        gate.locked = false;
                        Holdable key = _actor.item;
                        _actor.item = null;
                        Destroy(key.gameObject);
                        _actor.LastGateway = gate;
                        Radio.checkRadio();
                    }
                    else
                    {
                        _actor.GivePath(new Path(new List<Tile>() {_actor.LastTile}));
                        return false;
                    }
                }
                else
                {
                    gate.open = true;
                    _actor.LastGateway = gate;
                }
                Radio.checkRadio();
            }

            if (!_path.Tiles[0].GetComponentInParent<Room>().radioWaveActive)
            {
                ActorSystem.Instance.SelectActor(ActorSystem.Instance.SelectedActor);
            }

            foreach (Actor checkActor in ActorSystem.Instance.AllActors)
            {
                if (checkActor.GetCurrentRoom() == checkRoom && checkActor.Scary && checkActor.sitting && checkActor != _actor)
                {
                    _actor.runAway(checkRoom);
                }
                
            }

        }
        return true;
    }

}
