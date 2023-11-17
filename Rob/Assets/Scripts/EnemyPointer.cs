using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPointer : MonoBehaviour
{
    private Player _player;
    private EnemyPointerView _enemyPointer;
    private Camera _camera;

    [Inject]
    public void Constructor(Player player, EnemyPointerView enemyPointerView)
    {
        _player = player;
        _enemyPointer = enemyPointerView;
    }

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 fromPlayerToEnemy = transform.position - _player.Body.position;

        Ray ray = new Ray(_player.Body.position, fromPlayerToEnemy);

        Debug.DrawRay(_player.Body.position, fromPlayerToEnemy);

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        float minDistance = Mathf.Infinity;
        int planeIndex = 0;

        for (int i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out float distance))
            {
                if(distance < minDistance)
                {
                    minDistance = distance;
                    planeIndex = i;
                }
            }
        }

        minDistance = Mathf.Clamp(minDistance, 0, fromPlayerToEnemy.magnitude);

        if(fromPlayerToEnemy.magnitude > minDistance)
        {
            _enemyPointer.Show();
        }
        else
        {
            _enemyPointer.Hide();
        }

        Vector3 worldPosition = ray.GetPoint(minDistance);
        _enemyPointer.transform.position = _camera.WorldToScreenPoint(worldPosition);
        _enemyPointer.transform.rotation = GetIconRotation(planeIndex);
    }
    //Дописать поворот для стрелки
    private Quaternion GetIconRotation(int planeIndex)
    {
        switch(planeIndex)
        {
            case 0:
                return Quaternion.Euler(0f,0f,0f);
            case 1:
                return Quaternion.Euler(0f, 0f, 180f);
            case 2:
                return Quaternion.Euler(0f, 0f, -90f);
            case 3:
                return Quaternion.Euler(0f, 0f, 90f);
            default:
                return Quaternion.identity;
        }
    }
}
