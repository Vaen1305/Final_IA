using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/ Node Range")]
public class ActionWait : ActionNodeActions
{
     
 
    public float FrameRate = 0;

    public bool randomWait = false;
    
    
    // The time to wait
    public float waitDuration;
     


    public override void OnStart()
    {
        base.OnStart();
      
         
    }

}