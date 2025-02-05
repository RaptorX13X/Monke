using UnityEngine;
using System;

public class Target : MonoBehaviour
{
    public event Action<Target> OnDestroyed;

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
    
    [SerializeField] private GameObject UIthing;

    private void Awake()
    {
        TurnMeOff();
    }

    public void TurnMeOn()
    {
        UIthing.SetActive(true);
    }

    public void TurnMeOff()
    {
        UIthing.SetActive(false);
    }
}
