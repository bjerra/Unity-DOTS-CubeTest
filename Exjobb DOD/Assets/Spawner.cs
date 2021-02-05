using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

public class Spawner : MonoBehaviour
{
	public Mesh mesh;
	public Material material;
	public int spawnRounds;
	public int spawnAmount;
	public float spawnInterval;

	private EntityManager entityManager;
	private EntityArchetype archetype;

	private void Start()
	{
		entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
		archetype = entityManager.CreateArchetype(
		 typeof(Translation),
		 typeof(Rotation),
		 typeof(RenderMesh),
		 typeof(RenderBounds),
		 typeof(LocalToWorld),
		  typeof(Rotate),
		 typeof(Move));


		StartCoroutine(SpawnSequence());
	}
	private IEnumerator SpawnSequence()
	{
		for(int i =0;i < spawnRounds; i++)
        {
			int rowCount = (int)Mathf.Sqrt(spawnAmount);

			for (var x = 0; x < rowCount; x++)
			{
				for (var y = 0; y < rowCount; y++)
				{
					var position = new float3(x * 10, 0, y * 10);
					CreateEntity(position);
				}
			}
			for (int frame = 0; frame < 50; frame++)
				yield return new WaitForEndOfFrame();
		}
		Application.Quit();
	}

	private void CreateEntity(float3 position)
    {
		Entity entity = entityManager.CreateEntity(archetype);
		entityManager.AddComponentData(entity, new Translation { Value = position });
		entityManager.AddComponentData(entity, new Rotation { Value = quaternion.identity });
		entityManager.AddComponentData(entity, new Move { moveSpeed = 100, timePassed = 0 });
		entityManager.AddComponentData(entity, new Rotate { RadiansPerSecond = 5});

		entityManager.AddSharedComponentData(entity, new RenderMesh
			{
				mesh = mesh,
				material = material
			});
	}
   
}
