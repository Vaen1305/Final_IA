using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IACharacterHumanVehicle : AICharacterVehicleLand, IIACharacterHumanAction
{
    private FuzzyLogic fuzzyLogic;
    private List<FuzzyRule> evadeRules;

    public IACharacterHumanVehicle()
    {
        fuzzyLogic = new FuzzyLogic();
        evadeRules = new List<FuzzyRule>
        {
            new FuzzyRule(x => x < 5f ? 1f : 0f, 1f), // Muy cercano
            new FuzzyRule(x => x >= 5f && x < 10f ? 1f : 0f, 0.5f), // Moderadamente cercano
            new FuzzyRule(x => x >= 10f ? 1f : 0f, 0f) // Lejano
        };
    }

    public float EvaluateEvasion(float distance)
    {
        return fuzzyLogic.Evaluate(distance, evadeRules);
    }

    public override void LoadComponent()
    {
        base.LoadComponent();
    }

    public void InCave(ItemCave _cave)
    {
        agent.isStopped = true;
        transform.position = _cave.IntroCavePosition.position;
    }

    public void OutCave(ItemCave _cave)
    {
        transform.position = _cave.OutCavePosition.position;
        agent.isStopped = false;
    }
}
