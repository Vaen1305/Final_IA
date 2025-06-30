using UnityEngine;

public class VisionSensorJabali : VisionSensor
{
    [Header("Territory Vision")]
    public DataViewBase TerritoryVision = new DataViewBase();

    private void Start()
    {
        LoadComponent();
    }

    public override void LoadComponent()
    {
        base.LoadComponent();
    }

    void Update()
    {
        UpdateScand();
    }

    public override void UpdateScand()
    {
        if (EnemyView != null)
        {
            // El jabalí está viendo un enemigo
            var healthJabali = health as HealthJabali;
            if (healthJabali != null && healthJabali.IsInTerritory)
            {
                // Está en su territorio, puede atacar
            }
        }
        else
        {
            // No ve enemigos
        }
    }

    public override void Scan()
    {
        base.Scan();
    }

    private void OnDrawGizmos()
    {
        if (MainVision == null || !MainVision.IsDrawGizmo) return;
        
        Gizmos.color = Color.red;
        Vector3 position = transform.position + Vector3.up * MainVision.height;
        Gizmos.DrawWireSphere(position, MainVision.distance);
        
        Vector3 forward = transform.forward * MainVision.distance;
        Vector3 right = Quaternion.AngleAxis(MainVision.angle / 2, Vector3.up) * forward;
        Vector3 left = Quaternion.AngleAxis(-MainVision.angle / 2, Vector3.up) * forward;
        
        Gizmos.DrawRay(position, right);
        Gizmos.DrawRay(position, left);
    }
}
