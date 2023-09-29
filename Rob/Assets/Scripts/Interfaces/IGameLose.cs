using UnityEngine;
using UnityEngine.Events;

public interface IGameLose
{
    public event UnityAction OnGameLossing;
}