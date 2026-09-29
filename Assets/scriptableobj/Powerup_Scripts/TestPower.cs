using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TestPower", menuName = "Collectibles/Powerups/TestPower")]
public class TestPower : Powerups
{
    public float extraSpeed;

    public override void Apply(GameObject Target)
    {
        Target.GetComponent<Move>().maxSpeed += extraSpeed;

    }
    public override void Remove(GameObject Target)
    {
        if (Target.GetComponent<Move>().maxSpeed == 0 || Target.GetComponent<Move>().maxSpeed != Target.GetComponent<Move>().holder + extraSpeed)
        {
            Target.GetComponent<Move>().maxSpeed = Target.GetComponent<Move>().holder;
        }
        else
            Target.GetComponent<Move>().maxSpeed -= extraSpeed;
    }
}
