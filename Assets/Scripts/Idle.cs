using UnityEditor;
using UnityEngine;

public class Idle : IState
{
    PlayerController player;

    public Idle(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Enter Idle");
    }

    public void Exit()
    {
        Debug.Log("Exit Idle");
    }

    public void UpdateState()
    {
        player.movement.x = Input.GetAxisRaw("Horizontal");
        player.movement.y = Input.GetAxisRaw("Vertical");
        if(player.movement.x != 0 || player.movement.y != 0)
        {
            player.stateMachine.ChangeState(player.stateMachine.WalkState);
        }
    }
}
