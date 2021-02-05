using Unity.Entities;

[GenerateAuthoringComponent]
public struct Move : IComponentData
{
    public float moveSpeed;
    public float timePassed;
}
