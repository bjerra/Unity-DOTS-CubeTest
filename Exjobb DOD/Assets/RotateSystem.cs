using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class RotateSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;

        // The in keyword on the RotationSpeed_SpawnAndRemove component tells the job scheduler that this job will not write to rotSpeedSpawnAndRemove
        Entities
            .WithName("RotateSystem")
            .ForEach((ref Rotation rotation, ref Rotate rotateData) =>
            {
                rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(math.up(), rotateData.RadiansPerSecond * deltaTime));
            })
            .ScheduleParallel();
    }
}
