using UnityEngine;

public class Die : IState
{
    PlayerController player;

    public Die(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animator.SetBool("Die",true);
    }

    public void Exit()
    {
    }

    public void UpdateState()
    {
        if (player.Render.sprite == player.Sprites[1]) player.LessHealth();
    }
}

