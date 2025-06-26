using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterVehicleLand : AICharacterVehicle
{
    protected NavMeshAgent agent;
    public float rangeWander = 10.0f;
    public Vector3 pointWander = Vector3.zero;
    public Vector3 positionEvade = Vector3.zero;

    public override void LoadComponent()
    {
        base.LoadComponent();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void LookToEnemy()
    {
        if (_VisionSensor.EnemyView == null) return;
        Vector3 dir = (_VisionSensor.EnemyView.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public override void LookToPosition(Vector3 position)
    {
        Vector3 dir = (position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public override void MoveToEnemy()
    {
        if (_VisionSensor.EnemyView == null) return;
        LookToEnemy();
        agent.SetDestination(_VisionSensor.EnemyView.transform.position);
    }

    public override void MoveToAllied()
    {
        if (_VisionSensor.AlliedView == null) return;
        LookToPosition(_VisionSensor.AlliedView.transform.position);
        agent.SetDestination(_VisionSensor.AlliedView.transform.position);
    }

    public override void MoveToResource()
    {
        VisionSensorCivil visionSensorCivil = _VisionSensor as VisionSensorCivil;
        if (visionSensorCivil != null && visionSensorCivil.ResourceView != null)
        {
            this.MoveToPosition(visionSensorCivil.ResourceView.transform.position);
        }
    }

    public override void MoveToPosition(Vector3 position)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 50, NavMesh.AllAreas))
        {
            agent.SetDestination(position);
        }
    }

    public override void MoveToEvadeEnemy()
    {
        if (_VisionSensor.EnemyView == null) return;

        Vector3 dir = (transform.position - _VisionSensor.EnemyView.transform.position).normalized;
        positionEvade = transform.position + dir * 10;
        LookToPosition(positionEvade);
        this.MoveToPosition(positionEvade);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public override void Wander()
    {
        float distance = (transform.position - pointWander).magnitude;
        if (distance < 1)
        {
            if (RandomPoint(transform.position, rangeWander, out pointWander))
            {
                this.MoveToPosition(pointWander);
            }
        }
        else
        {
            if (Framerate > arrayRate[index])
            {
                index++;
                index = index % arrayRate.Length;
                if (RandomPoint(transform.position, rangeWander, out pointWander))
                {
                    this.MoveToPosition(pointWander);
                }
                Framerate = 0;
            }
            Framerate += Time.deltaTime;
        }
    }
}
