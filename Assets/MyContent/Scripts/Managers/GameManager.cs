using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Vuforia;

public class GameManager : MonoBehaviour, ITrackableEventHandler
{
    private Dictionary<TrackableBehaviour.Status, Action> _trackableCustomEvents = new Dictionary<TrackableBehaviour.Status, Action>();

    private void Awake()
    {
        Application.targetFrameRate = 30;
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
    }

    private void ExecuteLoser()
    {
        Debug.Log("I am a loser!!");
    }
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (_trackableCustomEvents != null && _trackableCustomEvents.ContainsKey(newStatus))
        {
            _trackableCustomEvents[newStatus]();
        }
    }
}
