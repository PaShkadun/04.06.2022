using System;
using UnityEngine;

public interface IUpdatable
{
    void Tick();
}

public class EnemyTank : IUpdatable
{
    private Transform _transform;

    public EnemyTank(Transform transform)
    {
        _transform = transform;
    }

    public void Tick()
    {
        _transform.LookAt(StaticDatas.playerPosition);
        _transform.Translate(StaticDatas._vector3);
    }
}