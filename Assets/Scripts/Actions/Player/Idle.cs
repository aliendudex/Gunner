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
        player.animator.SetBool("Idle", true);
        player.animator.SetBool("Move", false);
    }

    public void Exit()
    {
        player.animator.SetBool("Idle", false);
    }

    public void UpdateState()
    {
        player.movement.x = Input.GetAxisRaw("Horizontal");
        player.movement.y = Input.GetAxisRaw("Vertical");
        if (player.movement.x != 0 || player.movement.y != 0)
        {
            player.stateMachine.ChangeState(player.stateMachine.WalkState);
        }
        //if (Input.GetKeyDown(KeyCode.E)) player.stateMachine.ChangeState(player.stateMachine.ShootState);
        if (player.hurted == true) player.stateMachine.ChangeState(player.stateMachine.HurtState);
        if (player.Contact.CurrentHealth <= 0) player.stateMachine.ChangeState(player.stateMachine.DieState);
    }
}
