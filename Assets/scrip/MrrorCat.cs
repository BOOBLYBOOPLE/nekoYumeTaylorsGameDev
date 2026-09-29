using System.Collections;
using UnityEngine;

public class MrrorCat : MonoBehaviour
{
    public GameObject dest;
    public Vector3 destpos;
    public float speed;
    public Collider2D col;
    public bool trigger;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destpos = dest.transform.position;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Debug.Log(gameObject.transform.position);
        Debug.Log(destpos);
        if(trigger)
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destpos, speed * Time.deltaTime);
        if (gameObject.transform.position == destpos)
        {
            animator.SetBool("isActive", false);
            Invoke("boom", 3f); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        col = collision;
        if(collision.tag == "Player")
        {
            SoundController.instance.PlayMirror();
            gameObject.GetComponent<Collider2D>().enabled = false;
            animator.SetBool("isActive", true);
            trigger = true;
        }
    }
    public void boom()
    {
        Destroy(gameObject);
    }
}
