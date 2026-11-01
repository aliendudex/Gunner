using UnityEngine;

public class Attack : IState
{
    PlayerController player;

    public Attack(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animator.SetBool("Attack", true);
        player.animator.SetBool("Idle", false);
        player.animator.SetBool("Move", false);
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
        if (player.movement.x != 0 || player.movement.y != 0)
        {
            player.stateMachine.ChangeState(player.stateMachine.WalkState);
        }
        if (player.movement.x == 0 && player.movement.y == 0)
        {
            player.stateMachine.ChangeState(player.stateMachine.IdleState);
        }
        if (player.perish == true) player.stateMachine.ChangeState(player.stateMachine.HurtState);
        if (player.Health <= 0) player.stateMachine.ChangeState(player.stateMachine.DieState);
    }
}


