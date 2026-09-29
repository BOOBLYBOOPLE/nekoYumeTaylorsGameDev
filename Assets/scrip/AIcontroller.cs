using UnityEngine;

[CreateAssetMenu(fileName = "AIcontroller", menuName = "InputController/AIcontroller")]
public class AIcontroller : InputController
{
    public override float RetrieveMoveInput()
    {
        return 1f;
    }
    public override bool RetrieveJumpInput()
    {
        return true;
    }
    public override bool RetrieveJumpHoldInput()
    {
        return false;
    }
    public override bool RetrieveModeSwitch()
    {
        throw new System.NotImplementedException();
    }
    public override bool RespawnKey()
    {
        throw new System.NotImplementedException();
    }
    public override bool Power1()
    {
        throw new System.NotImplementedException();
    }
    public override bool Pause()
    {
        throw new System.NotImplementedException();
    }
}
