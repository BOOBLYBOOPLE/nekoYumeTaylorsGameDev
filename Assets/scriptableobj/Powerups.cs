using UnityEngine;

public class Powerups : Collectables
{
    public int id;

    public override void Apply(GameObject Target)
    {
        active = true;
    }
    public override void Remove(GameObject Target)
    {
        active = false;
    }

}
