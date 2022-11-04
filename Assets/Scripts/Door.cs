using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    private const int _acceleration = 4;
    private const int _normalAcceleration = 1;
    private const float _invokeDelay = 0.2f;
    private const float _accelerationStartOffset = 10;

    public event Action OnDoorPass;
    public event Action OnAngileFeet;

    [SerializeField] private float _angileToGetThrough;
    [SerializeField] private Player _player;
    [SerializeField] private ParticleSystem _warpEffect;

    private bool _isFeet = true;
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
            _player.acceleration = _normalAcceleration;
        }
    }
    private void PassageTroughDoor()
    {
        OnDoorPass?.Invoke();
        _warpEffect.transform.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if( other.TryGetComponent(out Player player))
        {
            AccelerationMoveStartStop();
        }
    }

    private void AccelerationMoveStartStop()
    {
        if (_player.currentAngle > _angileToGetThrough - _accelerationStartOffset && _player.currentAngle < _angileToGetThrough + _accelerationStartOffset)
        {
            _player.acceleration = _acceleration;
            if(_isFeet) StartCoroutine(SpawnerDelay());
            _warpEffect.transform.gameObject.SetActive(true);
        }
        else
        {
            _warpEffect.transform.gameObject.SetActive(false);
            _player.acceleration = _normalAcceleration;
        }
    }

    private IEnumerator SpawnerDelay()
    {
        _isFeet = false;
        OnAngileFeet?.Invoke();
        yield return new WaitForSeconds(_invokeDelay);
        _isFeet = true;
    }
}
