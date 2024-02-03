using UnityEngine;
using UnityEngine.Events;

public interface IGameResultHandler
{
    public event UnityAction OnGameWinning;
}
