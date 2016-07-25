using UnityEngine;
using System.Collections;

public class MeshDuplicator : MonoBehaviour {

	public GameObject meshToClone;


	void Start () {

		Mesh refMesh = meshToClone.GetComponent<MeshFilter>().mesh;

		Mesh mesh = new Mesh();
		this.GetComponent<MeshFilter>().mesh = mesh;
		mesh.vertices = refMesh.vertices;
		mesh.uv = refMesh.uv;
		mesh.triangles = refMesh.triangles;

		this.GetComponent<MeshRenderer>().materials = meshToClone.GetComponent<MeshRenderer>().materials;

	
	}
	

	void Update () {
	
	}
}
