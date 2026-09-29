using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "Collectibles/Powerups/Shield")]
public class Shield : Powerups
{
    public static bool hit;
    public override void Apply(GameObject Target)
    {
        active = true;
    }
    public override void Remove(GameObject Target)
    {
        active = false;
    }
}
