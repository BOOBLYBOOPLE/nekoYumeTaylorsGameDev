using UnityEngine;

[CreateAssetMenu(fileName = "Currency", menuName = "Collectibles/Currency")]
public class Currency : Collectables
{
    public int value;
    public override void Apply(GameObject Target)
    {
        StatsController.currency += value;
    }
    public override void Remove(GameObject Target)
    {
        Debug.Log("DO NOTHING");
    }
}
