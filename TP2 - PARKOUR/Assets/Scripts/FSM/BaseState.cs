using System.Collections.Generic;
using UnityEngine;
using System;
public class BaseState<T>
{
    public Action OnAwake = delegate { };
    public Action Execute = delegate { };
    public Action Sleep = delegate { };
    string name;
    Dictionary<T, BaseState<T>> _dic = new Dictionary<T, BaseState<T>>();

    public void AddTransition(T input, BaseState<T> state)
    {
        if (!_dic.ContainsKey(input))
        {
            _dic.Add(input, state);
            name = state.ToString();
        }
    }
    public void RemoveTransition(T input)
    {
        if (_dic.ContainsKey(input))
            _dic.Remove(input);
    }
    public BaseState<T> GetState(T input)
    {
        if (_dic.ContainsKey(input))
            return _dic[input];
        return null;
    }
    public string DebugState()
    {
        return name;
    }
}
