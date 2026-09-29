using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override float RetrieveMoveInput()
    {
        return 1;
        //return Input.GetAxisRaw("Horizontal");
    }
    public override bool RetrieveJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }
    public override bool RetrieveJumpHoldInput()
    {
        return Input.GetButton("Jump");
    }
    public override bool RetrieveModeSwitch()
    {
        return Input.GetButtonDown("Fire3");
    }
    public override bool RespawnKey()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
    public override bool Power1()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }
    public override bool Pause()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
