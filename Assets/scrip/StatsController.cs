using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StatsController : MonoBehaviour
{
    /*[Header("Test")]
    awrwarar*/
    [Header("UI Control")]
    public UnityEngine.UI.Slider meter1;
    public TMP_Text text;
    public UnityEngine.UI.Image[] hearts;
    public Sprite full;
    public Sprite empty;
    private Canvas canvas;

    public PlayerStat stat;
    public static int currency;
    public static int hp;
    public int beat;
    [Range(0f, 100f)] public static float energy;
    public static int currenthp;
    public int numhearts;
    private void Awake()
    {
        currency = 0;
        hp = stat.stat1;
        energy = stat.stat2;
        Shield.active = false;
    }

    private void Start()
    {
        currenthp = hp;
        numhearts = currenthp;
        beat = GameManager.winAmt;
    }
    void Update()
    {
        text.SetText(currency.ToString() + " / " + beat.ToString());
        meter1.value = energy;
        if (currenthp > hp)
            currenthp = hp;
        if (StatsController.currenthp <= 0 || StatsController.energy <= 0)
            GameManager.instance.UpdateGameState(GameState.Lose);
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < currenthp ? full : empty;
            hearts[i].enabled = i < numhearts ? true : false;
        }
    }
}
