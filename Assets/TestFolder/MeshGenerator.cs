using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {

	private Mesh mesh;
	private Vector3[] vertices;
	private int[] triangles;

	public Material mat;

	public int xSize = 20;
	public int zSize = 20;

	private void Start()
	{
		mesh = new Mesh();

		GetComponent<MeshRenderer>().material = mat;

		GetComponent<MeshFilter>().mesh = mesh;

		CreateMesh();
		UpdateMesh();
	}

	private void CreateMesh()
	{
		vertices = new Vector3[(xSize + 1) * (zSize + 1)];

		for (int index = 0, z = 0; z <= zSize; z++)
		{
			for (int x = 0; x <= xSize; x++)
			{
				vertices[index] = new Vector3((x + Random.Range(-.4f, .4f)), 0, (z + Random.Range(-.4f, .4f)));
				index++;
			}

		}

		triangles = new int[xSize * zSize * 6];

		int vert = 0;
		int tris = 0;

		for (int z = 0; z < zSize; z++)
		{
			for (int x = 0; x < xSize; x++)
			{
				triangles[tris + 0] = vert + 0;
				triangles[tris + 1] = vert + xSize + 1;
				triangles[tris + 2] = vert + 1;
				triangles[tris + 3] = vert + 1;
				triangles[tris + 4] = vert + xSize + 1;
				triangles[tris + 5] = vert + xSize + 2;

				vert++;
				tris += 6;
			}
			vert++;
		}

	}

	void UpdateMesh()
	{
		mesh.Clear();

		mesh.vertices = vertices;
		mesh.triangles = triangles;

		mesh.RecalculateNormals();
	}

	private void OnDrawGizmos()
	{
		if (vertices == null)
		{
			return;
		}

		for (int i = 0; i < vertices.Length; i++)
		{
			Gizmos.DrawSphere(vertices[i], .1f);
		}
	}
}
