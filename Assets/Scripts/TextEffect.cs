using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextEffect : MonoBehaviour
{
    private int _lifeTime = 2;
    [SerializeField] private float _offsetY = 5;
    [SerializeField] private float _timeToMove = 0.5f;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _scoreAdd;
    void OnEnable()
    {
        StartCoroutine(AddEffectLifeTime());
        _scoreAdd.transform.DOLocalMove(new Vector3(_scoreAdd.transform.position.x, _scoreAdd.transform.position.y + _offsetY, _scoreAdd.transform.position.z), _timeToMove);
    }

    private void OnDisable()
    {
        StopCoroutine(AddEffectLifeTime());
    }

    private IEnumerator AddEffectLifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        _canvas.gameObject.SetActive(false);
    }
}
