using UnityEngine;

public class VisionSensorLobo : VisionSensor
{
    [Header("Vision Attack")]
    public DataViewBase AttackVision = new DataViewBase();

    private void Start()
    {
        LoadComponent();
    }
    public override void LoadComponent()
    {
        AttackVision.Owner = GetComponent<Health>();
        base.LoadComponent();
    }

    void Update()
    {
        this.UpdateScand();
    }
    private void OnValidate()
    {
        base.CreateMesh();
        AttackVision.CreateMesh();
    }
    public override void UpdateScand()
    {
        base.UpdateScand();
        if (EnemyView != null)
        {
            AttackVision.IsInSight(EnemyView.AimOffset);
        }
        else
        {
            AttackVision.InSight = false;
        }
    }
    public override void Scan()
    {
        EnemyView = null;
        MainVision.InSight = false;
        float min_dist_enemy = float.MaxValue;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, MainVision.distance, ScanLayerMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Health health = targetsInViewRadius[i].GetComponent<Health>();
            if (health != null && IsNotIsThis(health.gameObject) && !health.IsDead && MainVision.IsInSight(health.AimOffset))
            {
                if (!IsAllies(health))
                {
                    float dist = (transform.position - health.transform.position).magnitude;
                    if (dist < min_dist_enemy)
                    {
                        EnemyView = health;
                        min_dist_enemy = dist;
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        AttackVision.OnDrawGizmos();
        base.DrawGizmos();
    }
}
