using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class MoveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;

        // The in keyword on the RotationSpeed_SpawnAndRemove component tells the job scheduler that this job will not write to rotSpeedSpawnAndRemove
        Entities
            .WithName("MoveSystem")
            .ForEach((ref Translation position, ref Move moveData) =>
            {
                moveData.timePassed += deltaTime;
                if (moveData.timePassed > 1.1f)
                {
                    moveData.moveSpeed = -moveData.moveSpeed;
                    moveData.timePassed = 0.0f;
                }
                position.Value += moveData.moveSpeed * math.up() * deltaTime;           
            })
            .ScheduleParallel();
    }
}
