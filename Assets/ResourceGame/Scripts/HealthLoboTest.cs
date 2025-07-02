using UnityEngine;

/// <summary>
/// Simple test script to verify HealthLobo compilation and method accessibility
/// </summary>
public class HealthLoboTest : MonoBehaviour
{
    void Start()
    {
        // Test if we can access a HealthLobo component
        var healthLobo = GetComponent<HealthLobo>();
        if (healthLobo != null)
        {
            Debug.Log("✅ HealthLobo component found!");
            
            // Test method accessibility
            Debug.Log($"IsHungry: {healthLobo.IsHungry}");
            Debug.Log($"IsSleepy: {healthLobo.IsSleepy}");
            Debug.Log($"IsHunting: {healthLobo.IsHunting}");
            
            // Test that all methods exist and can be called
            if (healthLobo.IsHungry)
            {
                healthLobo.StartHunting();
                healthLobo.EatPrey();
                healthLobo.StopHunting();
            }
            
            if (healthLobo.IsSleepy)
            {
                healthLobo.Sleep();
                healthLobo.StopSleeping();
            }
            
            Debug.Log("✅ All HealthLobo methods are accessible!");
        }
        else
        {
            Debug.LogWarning("⚠️ No HealthLobo component found on this GameObject");
        }
        
        // Clean up - destroy this test component
        Destroy(this);
    }
}
