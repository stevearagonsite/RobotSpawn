using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void ExecuteWinner()
    {
        Debug.Log("I am a winner!!");
    }

    private void ExecuteLoser()
    {
        Debug.Log("I am a loser!!");
    }

}
