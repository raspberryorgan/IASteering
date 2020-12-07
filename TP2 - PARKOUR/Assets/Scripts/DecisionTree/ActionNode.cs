using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionNode : INode
{
    public delegate void action();
    action _myAction;
    public ActionNode(action myAction)
    {
        _myAction = myAction;
    }
    public void Execute()
    {
        _myAction();
    }
}