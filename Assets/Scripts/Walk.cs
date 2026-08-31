using UnityEngine;

public class Walk : IState
{
    int speed;
    PlayerController player;

    public Walk(PlayerController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        speed = 5;
        Debug.Log("Enter Walk");
    }

    public void Exit()
    {
        Debug.Log("Exit Walk");
    }

    public void UpdateState()
    {
        player.movement.x = Input.GetAxisRaw("Horizontal");
        player.movement.y = Input.GetAxisRaw("Vertical");
        player.rb.MovePosition(player.rb.position + player.movement * speed * Time.deltaTime);
        if(player.movement.x == 0 && player.movement.y == 0)
        {
            player.stateMachine.ChangeState(player.stateMachine.IdleState);
        }
    }
}
