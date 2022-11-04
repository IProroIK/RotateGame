using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelRestarter : MonoBehaviour
{
    [SerializeField] private Button _restart;
    private void Awake()
    {
        _restart.onClick.AddListener(RestartLevel);
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
