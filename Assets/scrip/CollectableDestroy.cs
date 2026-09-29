using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CollectableDestroy : MonoBehaviour
{
    public Collectables collectables;
    public Collider2D ownCollider;
    public float duration;
    public ParticleSystem burst;
    public Rigidbody2D body;
    public Vector3 starting;
    [Header("UI")]
    public GameObject slider;
    private UnityEngine.UI.Slider sliderComp;
    public GameObject pos;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        slider = GameObject.FindGameObjectWithTag("UIPOWER");
        if (collectables.icon == null)
            gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        else
            gameObject.GetComponent<SpriteRenderer>().sprite = collectables.icon;
    }
    private void Start()
    {
        starting = transform.position;
        sliderComp = slider.GetComponent<UnityEngine.UI.Slider>();
    }
    public void Update()
    {
        body.linearVelocityY += body.transform.position.y >= starting.y + 1  ? (-collectables.flightspeed * Time.deltaTime) : (collectables.flightspeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ownCollider = collision;
        if (collision.tag == "Player")
        {
            string tag;
            tag = gameObject.tag;
            switch (tag)
            {
                case "powerup":
                    SoundController.instance.PlayCollectPower();
                    break;
                case "Coin":
                    SoundController.instance.PlayCoin();
                    break;
                case "energy":
                    SoundController.instance.PlayEnergy();
                    break;
                default:
                    break;
            }
            StartCoroutine("help");
        }
    }
    public IEnumerator help()
    {
        collectables.Apply(ownCollider.gameObject);
        gameObject.GetComponent<Collider2D>().enabled = false;
        Debug.Log(collectables.name);
        try
        {
            Instantiate(burst, gameObject.transform.position, Quaternion.identity);
            for (int i = 0; i < gameObject.transform.childCount; i++) 
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        if (collectables.duration > 0)
        {
            duration = collectables.duration;
            sliderComp.maxValue = collectables.duration;
            sliderComp.value = sliderComp.maxValue;
        }
        while (duration > 0) 
        {
            if(Shield.hit)
                duration = 0;
            yield return new WaitForSeconds(Time.deltaTime);
            duration -= Time.deltaTime;
            sliderComp.value = duration;
        }
        collectables.Remove(ownCollider.gameObject);
        Destroy(gameObject);
    }
}
