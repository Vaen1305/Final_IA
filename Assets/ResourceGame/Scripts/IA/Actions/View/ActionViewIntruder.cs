using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/ Node View")]
public class ActionViewIntruder : ActionView
{
    public override TaskStatus OnUpdate()
    {
        // Solo funciona para Jabalí
        if (!(_AICharacterVehicle.health is HealthJabali healthJabali))
        {
            return TaskStatus.Failure;
        }

        // Buscar intrusos en el territorio
        bool intruderFound = CheckForIntrudersInTerritory(healthJabali);
        
        if (intruderFound)
        {
            Debug.Log($"🚨 {gameObject.name}: ¡INTRUSO DETECTADO EN TERRITORIO!");
            return TaskStatus.Success;
        }
        
        return TaskStatus.Failure;
    }
    
    private bool CheckForIntrudersInTerritory(HealthJabali healthJabali)
    {
        // Buscar Lobos en el territorio
        GameObject[] lobos = GameObject.FindGameObjectsWithTag("Lobo");
        foreach (GameObject lobo in lobos)
        {
            if (lobo != null && lobo != gameObject)
            {
                float distanceFromCenter = Vector3.Distance(lobo.transform.position, healthJabali.territoryCenter);
                if (distanceFromCenter <= healthJabali.territoryRadius)
                {
                    float distanceToJabali = Vector3.Distance(transform.position, lobo.transform.position);
                    Debug.Log($"🐺 {gameObject.name}: Lobo {lobo.name} en territorio (Dist: {distanceToJabali:F1}m)");
                    return true;
                }
            }
        }
        
        // Buscar Ciervos en el territorio
        GameObject[] ciervos = GameObject.FindGameObjectsWithTag("Ciervo");
        foreach (GameObject ciervo in ciervos)
        {
            if (ciervo != null)
            {
                float distanceFromCenter = Vector3.Distance(ciervo.transform.position, healthJabali.territoryCenter);
                if (distanceFromCenter <= healthJabali.territoryRadius)
                {
                    float distanceToJabali = Vector3.Distance(transform.position, ciervo.transform.position);
                    Debug.Log($"🦌 {gameObject.name}: Ciervo {ciervo.name} en territorio (Dist: {distanceToJabali:F1}m)");
                    return true;
                }
            }
        }
        
        return false;
    }
}
