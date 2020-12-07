using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Guard))]
public class GuardController : MonoBehaviour
{

    public LayerMask obstacleMask;
    public float viewRange;
    public float shootRange;
    public float angle;

    FSM<GuardStates> _fsm;
    Guard _g;
    LineofSight lineofSight;
    QuestionNode _questionSight;
    QuestionNode _questionRange;
    QuestionNode _questionAnalyze;
    ActionNode _actionPatrol;
    ActionNode _actionChase;
    ActionNode _actionShoot;
    ActionNode _actionAnalyze;
    INode _init;
    public void Start()
    {
        _g = GetComponent<Guard>();
        _g.StartGuard(obstacleMask);

        //DecisionTree     
        _actionPatrol = new ActionNode(() => GoToState(GuardStates.patrol));
        _actionChase = new ActionNode(() => GoToState(GuardStates.chase));
        _actionShoot = new ActionNode(() => GoToState(GuardStates.shoot));
        _actionAnalyze = new ActionNode(() => GoToState(GuardStates.analyzeSurroundings));

        _questionRange = new QuestionNode(IsInRange, _actionShoot, _actionChase);
        _questionAnalyze = new QuestionNode(_g.IsAnalyzing, _actionAnalyze, _actionPatrol);
        _questionSight = new QuestionNode(() => lineofSight.IsInSight(_g.character), _questionRange, _questionAnalyze);

        _init = _questionSight;

        //Patrol es el idle
        var patrol = new BaseState<GuardStates>();
        var analyzeSurrounds = new BaseState<GuardStates>();
        var chase = new BaseState<GuardStates>();
        var shoot = new BaseState<GuardStates>();

        lineofSight = new LineofSight(transform, viewRange, angle, obstacleMask);

        //Add Executionz
        patrol.Execute = _g.MoveWaypointRoute;
        analyzeSurrounds.OnAwake = _g.StartAnalyze;
        analyzeSurrounds.Execute = _g.Analyze;
        chase.Execute = _g.MoveSeekPlayer;
        shoot.Execute = _g.Shoot;

        _g.SetPatrolCallback(() => GoToState(GuardStates.analyzeSurroundings));

        //Aca le pones el nombre a los estadoh
        patrol.AddTransition(GuardStates.analyzeSurroundings, analyzeSurrounds);
        patrol.AddTransition(GuardStates.chase, chase);
        patrol.AddTransition(GuardStates.shoot, shoot);
        analyzeSurrounds.AddTransition(GuardStates.patrol, patrol);
        analyzeSurrounds.AddTransition(GuardStates.chase, chase);
        analyzeSurrounds.AddTransition(GuardStates.shoot, shoot);
        chase.AddTransition(GuardStates.patrol, patrol);
        chase.AddTransition(GuardStates.analyzeSurroundings, patrol);
        chase.AddTransition(GuardStates.shoot, shoot);
        shoot.AddTransition(GuardStates.patrol, patrol);
        shoot.AddTransition(GuardStates.chase, chase);

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
    bool IsInRange()
    {
        float distance = Vector3.Distance(transform.position, _g.character.position);
        return (distance < shootRange);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        if (Application.isPlaying)
        {
            if (lineofSight.IsInSight(_g.character))
            {
                Gizmos.color = Color.white;
            }
        }
        Gizmos.DrawRay(transform.position, transform.forward * viewRange);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * viewRange);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * viewRange);
    }
}
