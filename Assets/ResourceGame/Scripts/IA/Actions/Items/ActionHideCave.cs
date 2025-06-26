using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("IA SC/Node Resouces Action")]
public class ActionHideCave : ActionWait
{
    public bool Intro = false;
    public bool RecentlyExited = false;
    public float exitCooldownDuration = 5f;
    private float exitCooldownTime = 0f;

    public override void OnStart()
    {
        base.OnStart();
        Debug.Log("OnStart called");
        FrameRate = 0f;
    }

    public override TaskStatus OnUpdate()
    {
        Debug.Log("OnUpdate called");

        if (_AICharacterVehicle.health.IsDead)
        {
            Debug.Log("Character is dead");
            return TaskStatus.Failure;
        }
        else
        {
            if (RecentlyExited)
            {
                exitCooldownTime += Time.deltaTime;
                if (exitCooldownTime >= exitCooldownDuration)
                {
                    RecentlyExited = false;
                    exitCooldownTime = 0f;
                }
                return TaskStatus.Failure;
            }

            if (_AICharacterVehicle._VisionSensor is VisionSensorCivil)
            {
                VisionSensorCivil visionSensor = (VisionSensorCivil)_AICharacterVehicle._VisionSensor;

                if (visionSensor.AccommodationView != null && visionSensor.ResourcesVision.InSight)
                {
                    ItemCave current = ((ItemCave)visionSensor.AccommodationView);
                    if (current != null && !Intro)
                    {
                        Debug.Log("Current cave found and not in cave yet");
                        if (_AICharacterAction is AICharacterActionLandCivil)
                        {
                            ((AICharacterActionLandCivil)_AICharacterAction).InCave(current);
                            ((AICharacterVehicleLandCivil)_AICharacterVehicle).InCave(current);
                        }
                        else if (_AICharacterAction is AICharacterActionLandMilitar)
                        {
                            ((AICharacterActionLandMilitar)_AICharacterAction).InCave(current);
                            ((AICharacterVehicleLandMilitar)_AICharacterVehicle).InCave(current);
                        }
                        Intro = true;
                        Debug.Log("Entered cave");
                        return TaskStatus.Success;
                    }

                    if (Intro)
                    {
                        Debug.Log("Inside cave, waiting to exit. FrameRate: " + FrameRate + " waitDuration: " + waitDuration);
                        if (FrameRate > waitDuration)
                        {
                            if (_AICharacterAction is AICharacterActionLandCivil)
                            {
                                ((AICharacterActionLandCivil)_AICharacterAction).OutCave(current);
                                ((AICharacterVehicleLandCivil)_AICharacterVehicle).OutCave(current);
                                Debug.Log("Exited cave (Civil)");
                            }
                            else if (_AICharacterAction is AICharacterActionLandMilitar)
                            {
                                ((AICharacterActionLandMilitar)_AICharacterAction).OutCave(current);
                                ((AICharacterVehicleLandMilitar)_AICharacterVehicle).OutCave(current);
                                Debug.Log("Exited cave (Militar)");
                            }
                            Debug.Log("Exited cave");
                            Intro = false;
                            Debug.Log("false");
                            FrameRate = 0;
                            RecentlyExited = true;
                            return TaskStatus.Failure;
                        }
                        FrameRate += Time.deltaTime;
                        return TaskStatus.Running;
                    }
                }
            }
            return TaskStatus.Failure;
        }
    }
}


