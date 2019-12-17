using System;
using System.Collections.Generic;
using Events;
using Managers;
using UnityEngine;
using Vuforia;

public class GameManager : MonoBehaviour, IObservableMainEvents, ITrackableEventHandler
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private event Action _onCompletedGame = delegate { };
    private event Action _onGameOver = delegate { };
    private TrackableBehaviour _vuforiaEvents;
    private Dictionary<TrackableBehaviour.Status, Action> _trackableCustomEvents = new Dictionary<TrackableBehaviour.Status, Action>();

    private void Awake()
    {
        Application.targetFrameRate = 30;

        if (_instance != null)
        {
            Destroy(this);
            _instance = this;
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        var mTrackableBehaviour = FindObjectOfType<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        
        // Custom events
        _trackableCustomEvents.Add(TrackableBehaviour.Status.NO_POSE, () => { UpdateManager.Instance.IsPause = true; });
        _trackableCustomEvents.Add(TrackableBehaviour.Status.LIMITED, () => { UpdateManager.Instance.IsPause = true; });
        _trackableCustomEvents.Add(TrackableBehaviour.Status.TRACKED, () => { UpdateManager.Instance.IsPause = false;});
        _trackableCustomEvents.Add(TrackableBehaviour.Status.DETECTED, () => { UpdateManager.Instance.IsPause = false;});
        _trackableCustomEvents.Add(TrackableBehaviour.Status.EXTENDED_TRACKED, () => { UpdateManager.Instance.IsPause = false;});
    }

    private void ExecuteWinner()
    {
        Debug.Log("I am a winner!!");
        _onCompletedGame();
    }

    private void ExecuteLoser()
    {
        Debug.Log("I am a loser!!");
        _onGameOver();
    }
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (_trackableCustomEvents != null && _trackableCustomEvents.ContainsKey(newStatus))
        {
            _trackableCustomEvents[newStatus]();
        }
    }

    #region IObservableMainEvents
    public void SubscribeCompletedGame(Action observer)
    {
        _onCompletedGame += observer;
    }

    public void UnSubscribeCompletedGame(Action observer)
    {
        _onCompletedGame -= observer;
    }

    public void SubscribeGameOver(Action observer)
    {
        _onGameOver -= observer;
    }

    public void UnSubscribeGameOver(Action observer)
    {
        _onGameOver -= observer;
    }
    #endregion IObservableMainEvents
}
