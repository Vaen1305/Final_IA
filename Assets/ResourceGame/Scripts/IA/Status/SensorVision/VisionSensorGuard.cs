using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class VisionSensorGuard : VisionSensor
{
    [Header("Vision Fire")]
    public DataViewBase FireVision = new DataViewBase();
    public DataViewBase AttackVision = new DataViewBase();

    private void Start()
    {
        LoadComponent();
    }
    public override void LoadComponent()
    {

        FireVision.Owner = MainVision.Owner;
        AttackVision.Owner = MainVision.Owner;
        base.LoadComponent();
    }

    void Update()
    {
        this.UpdateScand();
    }
    private void OnValidate()
    {
        base.CreateMesh();
        FireVision.CreateMesh();
        AttackVision.CreateMesh();
    }
    public override void UpdateScand()
    {
        if (Framerate > arrayRate[index])
        {
            index++;
            index = index % arrayRate.Length;
            this.Scan();
            Framerate = 0;
        }
        Framerate += Time.deltaTime;


        if (EnemyView != null)
        {
            FireVision.IsInSight(EnemyView.AimOffset);
            AttackVision.IsInSight(EnemyView.AimOffset);
        }
        else {
            FireVision.InSight = false; 
            AttackVision.InSight = false;
        }
            
    }
    public override void Scan()
    {

        EnemyView = null;

        MainVision.InSight = false;
        float min_dist = float.MaxValue;

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, MainVision.distance, ScanLayerMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Health health = targetsInViewRadius[i].GetComponent<Health>();

            if (health != null &&
                IsNotIsThis(health.gameObject) &&
                !health.IsDead &&
                MainVision.IsInSight(health.AimOffset)
                )
            {

                if (!IsAllies(health))
                {
                    float dist = (transform.position - health.transform.position).magnitude;
                    if (dist < min_dist)
                    {
                        EnemyView = health;
                        min_dist = dist;
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        FireVision.OnDrawGizmos();
        AttackVision.OnDrawGizmos();
        base.DrawGizmos();
    }
}
