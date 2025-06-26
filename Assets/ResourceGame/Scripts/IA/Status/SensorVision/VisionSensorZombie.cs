using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class VisionSensorZombie : VisionSensor
{
    [Header("Vision Attack")]
    public DataViewBase AttackVision = new DataViewBase();

    // Start is called before the first frame update
    private void Start()
    {
        LoadComponent();
    }
    public override void LoadComponent()
    {

        AttackVision.Owner = MainVision.Owner;
        base.LoadComponent();
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateScand();
    }
    private void OnValidate()
    {
        base.CreateMesh();
        AttackVision.CreateMesh();
    }

    public override void Scan()
    {

        EnemyView = null;
        MainVision.InSight = false;

        float min_dist = 10000000;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, MainVision.distance, ScanLayerMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            HealthHuman health = targetsInViewRadius[i].GetComponent<HealthHuman>();

            if (health != null &&
                IsNotIsThis(health.gameObject) &&
                !health.IsDead &&
                health.IfCanView &&
                MainVision.IsInSight(health.AimOffset)
                )
            {

                base.ExtractViewEnemy(ref min_dist, health);
            }
        }
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
            AttackVision.IsInSight(EnemyView.AimOffset);
        }
        else
            AttackVision.InSight = false;
    }
    private void OnDrawGizmos()
    {
        AttackVision.OnDrawGizmos();
        base.DrawGizmos();
    }
}
