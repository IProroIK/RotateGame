using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public event Action OnDoorPass;

    [SerializeField] private float _angileToGetThrough;

    public float GetAngileToGetThrough()
    {
        if (_angileToGetThrough > 360 || _angileToGetThrough < 0)
            throw new Exception("Not Possible angile, please set from 0 to 360");
        return _angileToGetThrough;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            PassageTroughDoor();
        }
    }
    private void PassageTroughDoor()
    {
        OnDoorPass?.Invoke();
    }
}
