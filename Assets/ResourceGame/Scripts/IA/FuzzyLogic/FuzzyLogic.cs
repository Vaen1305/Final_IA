using System.Collections.Generic;
using UnityEngine;
public class FuzzyLogic
{
    public float Evaluate(float input, List<FuzzyRule> rules)
    {
        float output = 0f;
        float totalWeight = 0f;

        foreach (var rule in rules)
        {
            float weight = rule.Condition(input);
            output += weight * rule.Output;
            totalWeight += weight;
        }

        return totalWeight != 0 ? output / totalWeight : 0;
    }
}

public class FuzzyRule
{
    public System.Func<float, float> Condition;
    public float Output;

    public FuzzyRule(System.Func<float, float> condition, float output)
    {
        Condition = condition;
        Output = output;
    }
}
