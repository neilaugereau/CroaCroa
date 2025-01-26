using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDefeat : MonoBehaviour
{
    public UnityEvent onDefeat;

    public void Defeat()
    {
        onDefeat.Invoke();
    }
}
