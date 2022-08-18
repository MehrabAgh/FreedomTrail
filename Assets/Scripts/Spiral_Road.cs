using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral_Road : MonoBehaviour {

    [SerializeField]float CurveStepDistance = 20;
    [SerializeField]float MaxCurvelevel = 20;
    [SerializeField]float PathLenght = 10;

    List<Vector3> points = new List<Vector3>();
    List<Vector3> vert = new List<Vector3>();
    public Material mtrl;





    private void pointcal(int i , int c = 1)
    {
        

            float rnd = Random.Range(0, MaxCurvelevel);
            rnd *= c;

            points.Add(new Vector3(x: PathLenght / 2, y: 0, z: CurveStepDistance * i));
            points.Add(new Vector3(x: -PathLenght / 2, y: 0, z: CurveStepDistance * i));

            points.Add(new Vector3(x: rnd + (PathLenght / 2), y: 0, z: CurveStepDistance * i + CurveStepDistance / 2));
            points.Add(new Vector3(x: rnd - (PathLenght / 2), y: 0, z: CurveStepDistance * i + CurveStepDistance / 2));

            points.Add(new Vector3(x: PathLenght / 2, y: 0, z: CurveStepDistance * (i + 1)));
            points.Add(new Vector3(x: -PathLenght / 2, y: 0, z: CurveStepDistance * (i + 1)));
        
    }

	void Start () 
    {
        StartCoroutine(generate_road());
	}

    private IEnumerator generate_road()
    {
        int c = 1;
        for (int i = 0; true; i++)
        {
            c *= -1;
            points.Clear();
            pointcal(i, c);
            vert.Clear();
            CalculateVerts();
            GenerateObject();
            yield return new WaitForSeconds(0.5f);
        }
    }

	void Update () 
    {
        
	}

    private void CalculateVerts()
    {
       
            for (float t = 0; t < 5; t++)
            {
                Vector3 p0 = Vector3.Lerp(points[0], points[2], t / 5);
                Vector3 p1 = Vector3.Lerp(points[2], points[4], t / 5);
                Vector3 p2 = Vector3.Lerp(p0, p1, t / 5); // this is gonna be one of the verteces
                Vector3 p3 = p2 - new Vector3(PathLenght, 0, 0);
                vert.Add(p2);
                vert.Add(p3);
                
            }
            vert.Add(points[3 + 1]);
            vert.Add(points[4 + 1]);

        }


    private void GenerateObject()
    {
        GameObject SpiralRoad = new GameObject("road");
        Mesh mesh = new Mesh();
        MeshFilter MF = SpiralRoad.AddComponent<MeshFilter>() as MeshFilter;
        MeshRenderer MR = SpiralRoad.AddComponent<MeshRenderer>() as MeshRenderer;
        MeshCollider col = SpiralRoad.AddComponent<MeshCollider>() as MeshCollider;
        MF.mesh = mesh;
        

        mesh.vertices = vert.ToArray();
        mesh.triangles = CalculateTries().ToArray();

        mesh.RecalculateNormals();
        MR.sharedMaterial = mtrl;
        col.sharedMesh = mesh;
    }

    private List<int> CalculateTries()
    {
        List<int> t = new List<int>();
        for (int i = 0; i < vert.Count - 3; i += 2)
        {
            t.Add(i);
            t.Add(i + 1);
            t.Add(i + 2);
            t.Add(i + 1);
            t.Add(i + 3);
            t.Add(i + 2);
        }
        return t;
    }

    }





