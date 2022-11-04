using UnityEngine;

public class WinUI : MonoBehaviour
{
    [SerializeField] private GameObject _winUI;
    [SerializeField] private Win _win;
    private void Awake()
    {
        _winUI.SetActive(false);
    }

    private void OnEnable()
    {
        _win.OnWin += ActiveWinUI;
    }

    private void OnDisable()
    {
        _win.OnWin -= ActiveWinUI;
    }

    private void ActiveWinUI()
    {
        _winUI.SetActive(true);
    }
}
