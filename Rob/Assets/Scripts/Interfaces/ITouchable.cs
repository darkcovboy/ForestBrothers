using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchable
{
    public void ConnectTo(Transform position);
    public void Disconnect();
}
