using UnityEngine;

public class Hurt : IState
{
    PlayerController player;

    public Hurt(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animator.SetBool("Hurt", true);
        player.animator.SetBool("Attack", false);
        player.animator.SetBool("Idle", false);
        player.animator.SetBool("Move", false);
    }

    public void Exit()
    {
        player.animator.SetBool("Hurt", false);
    }

    public void UpdateState()
    {
        //player.movement.x = Input.GetAxisRaw("Horizontal");
        //player.movement.y = Input.GetAxisRaw("Vertical");
        //if (player.movement.x != 0 || player.movement.y != 0)
        //{
        //    player.stateMachine.ChangeState(player.stateMachine.WalkState);
        //}
        //if (player.movement.x == 0 && player.movement.y == 0)
        //{
        //    player.stateMachine.ChangeState(player.stateMachine.IdleState);
        //}
        //if(Input.GetKeyDown(KeyCode.E)) player.stateMachine.ChangeState(player.stateMachine.ShootState);
        if (player.Health <= 0) player.stateMachine.ChangeState(player.stateMachine.DieState);
        if(player.perish == false) player.stateMachine.ChangeState(player.stateMachine.IdleState);
    }
}

