using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(fileName = "X2", menuName = "Collectibles/Powerups/X2")]
public class X2Currency : Powerups
{
    public Currency currency;
    public int hold = 1;
    public override void Apply(GameObject Target)
    {
        Target.GetComponent<SpriteRenderer>().material.color = Color.magenta;
        currency.value = currency.value * 2;
    }
    public override void Remove(GameObject Target)
    {
        Target.GetComponent<SpriteRenderer>().material.color = Color.white;
        currency.value = hold;
    }
}
