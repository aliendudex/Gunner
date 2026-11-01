using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine
{
    public IState IdleState;
    public IState WalkState;
    public IState ShootState;
    public IState HurtState;
    public IState DieState;
    public IState CurrentState;

    public StateMachine(PlayerController player)
    {
        IdleState = new Idle(player);
        WalkState = new Walk(player);
        ShootState = new Attack(player);
        HurtState = new Hurt(player);
        DieState = new Die(player);
        CurrentState = IdleState;
        CurrentState.Enter();
    }

    public void ChangeState(IState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void UpdateMachine()
    {
        CurrentState.UpdateState();
    }
}
