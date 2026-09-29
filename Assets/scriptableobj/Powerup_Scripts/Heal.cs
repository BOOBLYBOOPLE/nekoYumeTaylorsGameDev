using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Collectibles/Powerups/Heal")]
public class Heal : Powerups
{
    public override void Apply(GameObject Target)
    {
        StatsController.currenthp += +1;
    }
    public override void Remove(GameObject Target)
    {
        Debug.Log("NULL");
    }
}
