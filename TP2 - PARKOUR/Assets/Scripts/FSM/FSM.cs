using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{
    BaseState<T> _current;
    public FSM(BaseState<T> init)
    {
        if (init != null)
            SetInit(init);
    }
    public FSM() { }
    public void SetInit(BaseState<T> init)
    {
        _current = init;
        _current.OnAwake();
    }
    public void OnUpdate()
    {
        _current.Execute();
    }
    public void Transition(T input)
    {
        BaseState<T> newState = _current.GetState(input);
        if (newState == null) return;
        _current.Sleep();
        newState.OnAwake();
        _current = newState;
    }
    public string DebugState()
    {
        return _current.GetType().ToString();
    }
    public bool CanTransicion(T input)
    {
        return _current.GetState(input) != null;
    }
}
public enum GuardStates
{
    patrol,
    analyzeSurroundings,
    chase,
    shoot
}
public enum PlayerStates
{
    idle,
    walk,
    jump,
    wallJump
}