using System.Collections;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "trigger":
                SoundController.instance.PlaySpeed();
                gameObject.GetComponentInParent<Move>().maxSpeed += 2;
                gameObject.GetComponentInParent<Move>().holder = gameObject.GetComponentInParent<Move>().maxSpeed;
                Destroy(collision.gameObject);
                break;
            case "wall":
                StartCoroutine("stop");
                SoundController.instance.PlayBlocked();
                Destroy(collision.gameObject);
                break;
            case "teleport":
                GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = collision.gameObject.transform.position;
                SoundController.instance.PlayTeleport();
                StatsController.energy += 10;
                Debug.Log(collision.gameObject.transform.position);
                break;
            case "SLOW":
                SoundController.instance.PlayPower();
                gameObject.GetComponentInParent<Move>().maxSpeed -= 2;
                gameObject.GetComponentInParent<Move>().holder = gameObject.GetComponentInParent<Move>().maxSpeed;
                Destroy(collision.gameObject);
                break;
            case "win":
                if (StatsController.currency < GameManager.winAmt)
                {
                    GameManager.instance.UpdateGameState(GameState.Lose);
                }
                else if (StatsController.currency >= GameManager.winAmt)
                {
                    GameManager.instance.UpdateGameState(GameState.LevelEnd);
                    SoundController.instance.PlayEndLevel();
                }
                BGM.instance.StopMusic();
                Time.timeScale = 0;
                break;
            case "Finish":
                StatsController.currenthp = 0;
                GameManager.instance.UpdateGameState(GameState.Lose);
                break;
            default:
                break;
        }

    }
    public IEnumerator stop()
    {
        gameObject.GetComponentInParent<Move>().maxSpeed = 0;
        yield return new WaitForSeconds(1);
        gameObject.GetComponentInParent<Move>().maxSpeed = gameObject.GetComponentInParent<Move>().holder;
    }
}
