using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _range; //radius of sphere
    [SerializeField] private Transform _centrePoint; //centre of the area the agent wants to move around in
    [SerializeField,Min(0.1f)] private float _speed;

    private void Start()
    {
        _navMeshAgent.speed = _speed;
    }

    private void Update()
    {
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance) //done with path
        {
            Vector3 point;

            if (RandomPoint(_centrePoint.position, _range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                _navMeshAgent.SetDestination(point);
            }
        }

    }

    public void StopMoving()
    {
        _navMeshAgent.isStopped = true;
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
