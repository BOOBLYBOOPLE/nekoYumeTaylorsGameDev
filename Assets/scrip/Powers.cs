using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Powers : MonoBehaviour//, IDataPersistence
{
    private int count;
    private bool unlocked;
    public Vector3 sizered;
    private Vector3 sizeori;
    public InputController controller;
    public bool activeSmall;
    public Animator animator;
    public float rateSmall;
    private Sprite ori;
    public ParticleSystem poof;

    private void Start()
    {
        sizeori = gameObject.transform.localScale;
        animator = GetComponent<Animator>();
        ori = GameObject.FindGameObjectWithTag("GameController").GetComponent<Image>().sprite;
    }

    private void Update()
    {
        count = StatsController.currency;
        animator.SetBool("isCat", activeSmall);
        if (controller.Power1() && !activeSmall)
        {
            activeSmall = true;
            Instantiate(poof, new Vector3( gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        }
        else if (controller.Power1() && activeSmall)
            activeSmall = false;
        gameObject.transform.localScale = activeSmall && StatsController.energy > 0 ? sizered : sizeori;
        StatsController.energy -= gameObject.transform.localScale == sizered ? rateSmall * (Time.deltaTime * 1.80f) : 0;
    }
}
