using UnityEngine;

[CreateAssetMenu(fileName = "Energy", menuName = "Collectibles/Energy")]
public class Energy : Collectables
{
    public float amount;
    public override void Apply(GameObject Target)
    {
        StatsController.energy += amount;
    }
    public override void Remove(GameObject Target)
    {
        Debug.Log("NULL");
    }
}
