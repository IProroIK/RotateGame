using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Win : MonoBehaviour
{
    public event Action OnWin;
    [SerializeField] private List<ParticleSystem> _finishConfeties;
    private Vector3 _endRotatePosition = new Vector3(0, 359f, 0);
    private float timeToRotate = 2f;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.DORotate(_endRotatePosition, timeToRotate);
        OnWin?.Invoke();
        for(int i = 0; i < _finishConfeties.Count; i++)
        {
            _finishConfeties[i].Play();
        }

    }
}
