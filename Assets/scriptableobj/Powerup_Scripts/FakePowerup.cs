using UnityEngine;

[CreateAssetMenu(fileName = "FakePower", menuName = "Collectibles/Powerups/Fake")]
public class FakePowerup : Powerups
{
    public override void Apply(GameObject Target)
    {
        Debug.Log(" DO NOTING");
    }
    public override void Remove(GameObject Target)
    {
        Debug.Log("NULL");
    }
}