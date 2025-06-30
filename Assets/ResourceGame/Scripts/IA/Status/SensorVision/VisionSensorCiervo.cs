using UnityEngine;

public class VisionSensorCiervo : VisionSensor
{
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
        Scan(); // Cambiar a Scan() en lugar de UpdateScand()
        UpdateScand();
    }

    public override void UpdateScand()
    {
        base.UpdateScand(); // Llamar al método base primero
        
        if (EnemyView != null)
        {
            // El ciervo está viendo un enemigo
            if (EnemyView.typeAgent == TypeAgent.Lobo || EnemyView.typeAgent == TypeAgent.Jabali)
            {
                Debug.Log($"🦌 Ciervo detectó amenaza: {EnemyView.typeAgent}");
            }
        }
    }

    public override void Scan()
    {
        base.Scan();
    }

    private void OnDrawGizmos()
    {
        if (MainVision == null || !MainVision.IsDrawGizmo) return;
        
        Gizmos.color = Color.green;
        Vector3 position = transform.position + Vector3.up * MainVision.height;
        Gizmos.DrawWireSphere(position, MainVision.distance);
        
        Vector3 forward = transform.forward * MainVision.distance;
        Vector3 right = Quaternion.AngleAxis(MainVision.angle / 2, Vector3.up) * forward;
        Vector3 left = Quaternion.AngleAxis(-MainVision.angle / 2, Vector3.up) * forward;
        
        Gizmos.DrawRay(position, right);
        Gizmos.DrawRay(position, left);
    }
}
