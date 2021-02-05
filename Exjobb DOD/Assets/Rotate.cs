using Unity.Entities;

[GenerateAuthoringComponent]
public struct Rotate : IComponentData
{
    public float RadiansPerSecond;
}