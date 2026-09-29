using UnityEngine;

public class CollisionScript : MonoBehaviour
{

    public ParticleSystem jumppart;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Instantiate(jumppart, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
            SoundController.instance.PlayLandSound();
        }
    }
}
