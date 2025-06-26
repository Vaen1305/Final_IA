using UnityEngine;

public class HealthJabali : HealthHuman
{
    [Header("Territory")]
    public Vector3 territoryCenter;
    public float territoryRadius = 20f;

    [Header("Hunger")]
    public float hunger = 100f;
    public float maxHunger = 100f;
    public float hungerDepletionRate = 1f; // por segundo

    public bool IsHungry { get => hunger <= 30; }
    public bool IsInTerritory { get => Vector3.Distance(transform.position, territoryCenter) <= territoryRadius; }

    public override void LoadComponent()
    {
        base.LoadComponent();
        territoryCenter = transform.position; // El centro del territorio es su posición inicial
        hunger = maxHunger;
    }

    void Update()
    {
        hunger = Mathf.Max(0, hunger - hungerDepletionRate * Time.deltaTime);
    }

    public void Eat()
    {
        hunger = maxHunger;
    }
}
