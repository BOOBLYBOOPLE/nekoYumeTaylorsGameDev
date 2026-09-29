using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject deathPanel;
    public GameObject pausePanel;
    public GameObject WinPanel;
    public GameObject SettingPanel;

    public RectTransform panel;
    private void Start()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
        panel = SettingPanel.GetComponent<RectTransform>();
        panel.localScale = Vector3.zero;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }
    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        deathPanel.SetActive(state == GameState.Lose);
        pausePanel.SetActive(state == GameState.Paused);
        WinPanel.SetActive(state == GameState.LevelEnd);
        if (state == GameState.Settings)
            panel.localScale = Vector3.one;
        else
            panel.localScale = Vector3.zero;
    }
}
