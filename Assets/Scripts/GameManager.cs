using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public UnityEvent<float> SpeedEvent;
    public GameObject Player;
    public LayerMask groundLayer;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is NULL!");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        groundLayer = LayerMask.NameToLayer("Ground");
    }
}
