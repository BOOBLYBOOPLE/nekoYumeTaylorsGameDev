using JetBrains.Annotations;
using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Play,
    LevelEnd,
    Lose,
    Paused,
    Settings
}
public class GameManager : MonoBehaviour
{
    //stuff
    public static GameManager instance;
    public GameState gamestate;
    public GameObject hud;
    public static event Action<GameState> OnGameStateChanged;
    public int setterwin;
    public static int winAmt;
    public X2Currency up;
    public float startSpeed;
    
    //INgaym
    public GameObject player;
    public InputController input;
    public Transform playerTransform;
    public Transform pos;
    public Transform tracker;
    public Vector3 spawnpos;
    public CinemachineCamera cam;
    public Move container;
    public bool DIED;
    public bool paused;
    public GameObject playercur;
    private void Awake()
    {
        instance = this;
        winAmt = this.setterwin;
    }
    private void Start()
    {
        DIED = false;
        UpdateGameState(GameState.Play);
    }
    public void UpdateGameState(GameState state)
    {
        gamestate = state;
        switch (state)
        {
            case GameState.Paused:
                PauseGame();
                TurnOffHud();
                break;
            case GameState.LevelEnd:
                Destroy(playercur);
                TurnOffHud();
                break;
            case GameState.Play:
                GameStart();
                ResumeGame();
                TurnOnHud();
                CameraControl();
                break;
            case GameState.Lose:
                HandleRespawn();
                TurnOffHud();
                break;
            case GameState.Settings:
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke(state);
    }
    private void Update()
    {
        UpdateGameState(gamestate);
        Debug.Log(DIED);
    }
    public void GameStart()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineCamera>();
        pos = GameObject.FindGameObjectWithTag("tracker").GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("spawn").GetComponent<Transform>();
        tracker = GameObject.FindGameObjectWithTag("ShieldEff").GetComponent<Transform>();
        if (!DIED)
        {
            spawnpos = playerTransform.position;
            up.currency.value = up.hold;
            playercur = Instantiate(player, spawnpos, Quaternion.identity);
            container = playercur.GetComponent<Move>();
            container.holder = startSpeed;
            container.maxSpeed = startSpeed;

            DIED = true;
        }
    }
    public void CameraControl()
    {
        pos.transform.position = playercur.transform.position;
        tracker.transform.position = new Vector3(pos.transform.position.x, 0, 0);
        if (container.dir.x < 0)
            cam.GetComponent<CinemachinePositionComposer>().TargetOffset.x = -9;
        else
            cam.GetComponent<CinemachinePositionComposer>().TargetOffset.x = 9;
        if (Shield.active)
            pos.GetComponent<SpriteRenderer>().enabled = true;
        else
            pos.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void HandleRespawn()
    {
        Destroy(playercur);   
        if (input.RespawnKey() && playercur.IsDestroyed())
        {
            DIED = false;
            UpdateGameState(GameState.Play);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void Continue()
    {
        UpdateGameState(GameState.Play);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (input.Pause() && gamestate == GameState.Play)
        {
            UpdateGameState(GameState.Paused);
        }
    }
    public void TurnOffHud()
    {
        hud.SetActive(false);
    }
    public void TurnOnHud()
    {
        hud.SetActive(true);
    }
    public void returnButt()
    {
        UpdateGameState(GameState.Paused);
    }
    public void SettingsButt()
    {
        UpdateGameState(GameState.Settings);
    }
}