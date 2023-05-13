using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEnder : MonoBehaviour
{
    public event Action endGame;

    private void OnCollisionEnter(Collision collision)
    {
        endGame?.Invoke();
    }
}
