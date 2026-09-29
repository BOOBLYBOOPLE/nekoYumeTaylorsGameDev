using UnityEngine;

public abstract class Collectables : ScriptableObject
{
    public Sprite icon;
    public float flightspeed;
    public float moveSpeed;
    public static bool active;
    [Range(0f, 10f)] public float duration;
    public abstract void Apply(GameObject Target);
    public abstract void Remove(GameObject Target);
}
