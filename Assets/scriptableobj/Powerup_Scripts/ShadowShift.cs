using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Shadow Shift", menuName = "Collectibles/Powerups/Shadow Shift")]
public class ShadowShift : Powerups
{
    public override void Apply(GameObject Target)
    {
        Target.GetComponent<SpriteRenderer>().material.color = Color.grey;
        Transform t = Target.transform;
        for (int i = 0; i < t.childCount; i++)
            if (t.GetChild(i).gameObject.tag == "hitbox")
                t.GetChild(i).gameObject.GetComponent<Collider2D>().enabled = false;
    }
    public override void Remove(GameObject Target)
    {
        Target.GetComponent<SpriteRenderer>().material.color = Color.white;
        Transform t = Target.transform;
        for (int i = 0; i < t.childCount; i++)
            if (t.GetChild(i).gameObject.tag == "hitbox")
                t.GetChild(i).gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
