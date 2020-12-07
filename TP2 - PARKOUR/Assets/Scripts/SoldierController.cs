using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Soldier))]
public class SoldierController : MonoBehaviour
{
    public LayerMask obstacleMask;
    public float viewRange;
    public float shootRange;
    public float angle;

    FSM<GuardStates> _fsm;
    Soldier _s;
    LineofSight lineofSight;
    QuestionNode _questionSight;
    QuestionNode _questionAnalyze;
    ActionNode _actionPatrol;
    ActionNode _actionChase;
    ActionNode _actionAnalyze;
    INode _init;
    public void Start()
    {
        _s = GetComponent<Soldier>();
        _s.StartGuard(obstacleMask);
        lineofSight = new LineofSight(transform, viewRange, angle, obstacleMask);

        //DecisionTree     
        _actionPatrol = new ActionNode(() => GoToState(GuardStates.patrol));
        _actionChase = new ActionNode(() => GoToState(GuardStates.chase));
        _actionAnalyze = new ActionNode(() => GoToState(GuardStates.analyzeSurroundings));

        _questionAnalyze = new QuestionNode(_s.IsAnalyzing, _actionAnalyze, _actionPatrol);
        _questionSight = new QuestionNode(() => lineofSight.IsInSight(_s.character), _actionChase, _questionAnalyze);

        _init = _questionSight;

        //Patrol es el idle
        var patrol = new BaseState<GuardStates>();
        var analyzeSurrounds = new BaseState<GuardStates>();
        var chase = new BaseState<GuardStates>();


        //Add Executionz
        patrol.Execute = _s.MoveWaypointRoute;
        analyzeSurrounds.OnAwake = _s.StartAnalyze;
        analyzeSurrounds.Execute = _s.Analyze;
        chase.Execute = _s.MoveSeekPlayer;

        _s.SetPatrolCallback(() => GoToState(GuardStates.analyzeSurroundings));

        //Aca le pones el nombre a los estadoh
        patrol.AddTransition(GuardStates.analyzeSurroundings, analyzeSurrounds);
        patrol.AddTransition(GuardStates.chase, chase);
        analyzeSurrounds.AddTransition(GuardStates.patrol, patrol);
        analyzeSurrounds.AddTransition(GuardStates.chase, chase);
        chase.AddTransition(GuardStates.patrol, patrol);
        chase.AddTransition(GuardStates.analyzeSurroundings, patrol);

        _fsm = new FSM<GuardStates>(patrol);
    }
    public void Update()
    {
        _init.Execute();
        _fsm.OnUpdate();

    }
    //Fix
    void GoToState(GuardStates next)
    {
        if (_fsm.CanTransicion(next))
        {
            _fsm.Transition(next);
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        if (Application.isPlaying)
        {
            if (lineofSight.IsInSight(_s.character))
            {
                Gizmos.color = Color.white;
            }
        }
        Gizmos.DrawRay(transform.position, transform.forward * viewRange);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * viewRange);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * viewRange);
    }
}
