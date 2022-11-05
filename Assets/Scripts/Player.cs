using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public event Action OnAngileFeet;

    private const float _accelerationStartOffset = 10;
    private const int _accelerationFit = 4;
    private const int _normalAcceleration = 1;
    private const float _invokeDelay = 0.2f;

    [SerializeField] private float _speed;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] private float _endRotationTime = 0.2f;
    [SerializeField] private float _strength = 5;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _acceleration = 1;
    [SerializeField] private Win _win;
    [SerializeField] private ParticleSystem _warpEffect;

    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;
    private bool _isFeet = true;

    private void Awake()
    {
        _sensitivity = 0.1f;
        _rotation = Vector3.zero;
    }

    private void OnEnable()
    {
        _win.OnWin += GameStop;
    }

    private void OnDisable()
    {
        _win.OnWin -= GameStop;
    }

    private void Update()
    {
        CheckRotate();
        MoveForward();
        Rotate();
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * _speed * _acceleration * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Door door))
        {
            if (transform.rotation.eulerAngles.y > door.GetAngileToGetThrough() - _accelerationStartOffset && transform.rotation.eulerAngles.y < door.GetAngileToGetThrough() + _accelerationStartOffset)
            {
                AccelerationMoveStart();
            }
            else
            {
                AccelerationMoveStop();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Door door))
        {
            AccelerationMoveStop();
        }
    }
    private void AccelerationMoveStart()
    {
        _acceleration = _accelerationFit;
        if (_isFeet) StartCoroutine(DoorCheckFitEventDelay());
        _warpEffect.transform.gameObject.SetActive(true);
    }
    private void AccelerationMoveStop()
    {
        _warpEffect.transform.gameObject.SetActive(false);
        _acceleration = _normalAcceleration;
    }
    private IEnumerator DoorCheckFitEventDelay()
    {
        _isFeet = false;
        OnAngileFeet?.Invoke();
        yield return new WaitForSeconds(_invokeDelay);
        _isFeet = true;
    }

    private void Rotate()
    {
        if (_isRotating)
        {
            _mouseOffset = (Input.mousePosition - _mouseReference);
            _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            transform.Rotate(_rotation);
            _mouseReference = Input.mousePosition;
        }

    }

    private void CheckRotate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isRotating = true;
            _mouseReference = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isRotating = false;
            EndingRotate();
        }
    }

    private void EndingRotate()
    {
        switch (transform.rotation.eulerAngles.y)
        {
            case float i when i is > 0 and < 90:
                transform.DORotate(new Vector3(0, 90, 0), _endRotationTime);
                break;
            case float i when i is > 90 and < 180:
                transform.DORotate(new Vector3(0, 180, 0), _endRotationTime);
                break;
            case float i when i is > 180 and < 270:
                transform.DORotate(new Vector3(0, 270, 0), _endRotationTime);
                break;
            case float i when i is > 270 and < 360:
                transform.DORotate(new Vector3(0, 360, 0), _endRotationTime);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent(out Block block))
        {
            BounceBack();
        }
    }

    private void BounceBack()
    {
        _rigidbody.AddForce(Vector3.back * _strength, ForceMode.Impulse);
    }

    private void GameStop()
    {
        Destroy(this);
    }
}
