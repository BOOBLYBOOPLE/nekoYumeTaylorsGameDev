using System.Collections;
using UnityEngine;

public class kill : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int duration = 2;
    public void Start()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "kill")
        {
            SoundController.instance.PlayHitSound();
            StartCoroutine("DisableTrigger");
        }
    }
    public IEnumerator DisableTrigger()
    {
        if (Shield.active)
            Shield.hit = true;
        else
            StatsController.currenthp -= 1;
        gameObject.GetComponent<Collider2D>().enabled = false;
        while (duration > 0)
        {
            spriteRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.material.color = Color.white;
            yield return new WaitForSeconds(0.5f);
            duration--;
        }
        duration = 2;
        gameObject.GetComponent<Collider2D>().enabled = true;
        Shield.hit = false;
    }
}
