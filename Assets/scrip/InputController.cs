using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract float RetrieveMoveInput();
    public abstract bool RetrieveJumpInput();
    public abstract bool RetrieveJumpHoldInput();
    public abstract bool RetrieveModeSwitch();
    public abstract bool RespawnKey();
    public abstract bool Power1();
    public abstract bool Pause();
}
