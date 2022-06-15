using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndlessTerrain : MonoBehaviour
{

	public float scale = 5f;

	const float viewerMoveThresholdForChunkUpdate = 25f;
	const float sqrViewerMoveThresholdForChunkUpdate = viewerMoveThresholdForChunkUpdate * viewerMoveThresholdForChunkUpdate;

	public float ViewDistance = 250;

	public static float MaxViewDst = 250;

	public Transform viewer;
	public GameObject mapChunkPrefab;

	public static Vector3 viewerPosition;
	Vector3 viewerPositionOld;
	//static MapGenerator mapGenerator;
	float chunkSize;
	int chunksVisibleInViewDst;

	Dictionary<Vector3, SpaceChunk> spaceChunkDictionary = new Dictionary<Vector3, SpaceChunk>();
	static List<SpaceChunk> spaceChunksVisibleLastUpdate = new List<SpaceChunk>();

	void Start()
	{
		Debug.Log("Initializing Endless map");

		//mapGenerator = FindObjectOfType<MapGenerator>();

		MaxViewDst = ViewDistance;

		chunkSize = mapChunkPrefab.transform.localScale.x;/*MapGenerator.mapChunkSize - 1;*/
		chunksVisibleInViewDst = Mathf.RoundToInt(MaxViewDst / chunkSize);

		UpdateVisibleChunks();
	}

	void Update()
	{
		viewerPosition = new Vector2(viewer.position.x, viewer.position.z) / 1;

		if ((viewerPositionOld - viewerPosition).sqrMagnitude > sqrViewerMoveThresholdForChunkUpdate)
		{
			viewerPositionOld = viewerPosition;
			UpdateVisibleChunks();
		}
	}

	void UpdateVisibleChunks()
	{

		for (int i = 0; i < spaceChunksVisibleLastUpdate.Count; i++)
		{
			spaceChunksVisibleLastUpdate[i].SetVisible(false);
		}
		spaceChunksVisibleLastUpdate.Clear();

		int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
		int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

		for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++)
		{
			for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++)
			{
				Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

				if (spaceChunkDictionary.ContainsKey(viewedChunkCoord))
				{
					spaceChunkDictionary[viewedChunkCoord].UpdateSpaceChunk();
				}
				else
				{
					spaceChunkDictionary.Add(viewedChunkCoord, new SpaceChunk(viewedChunkCoord, chunkSize, mapChunkPrefab, transform));
				}

			}
		}
	}

	public class SpaceChunk
	{

		GameObject chunkObject;
		Vector3 position;
		Bounds bounds;

		public SpaceChunk(Vector3 coord, float size, GameObject prefab,Transform parent)
		{
			position = coord * size;
			bounds = new Bounds(position, Vector3.one * size);
			Vector3 positionV3 = new Vector3(position.x, 0, position.y);

			chunkObject = Instantiate(prefab, positionV3, Quaternion.identity, parent);
		}


		public void UpdateSpaceChunk()
		{
			if (true)
			{
				float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
				bool visible = viewerDstFromNearestEdge <= MaxViewDst;

				if (visible)
				{
					spaceChunksVisibleLastUpdate.Add(this);
				}

				SetVisible(visible);
			}
		}

		public void SetVisible(bool visible)
		{
			chunkObject.SetActive(visible);
		}

		public bool IsVisible()
		{
			return chunkObject.activeSelf;
		}

	}

}