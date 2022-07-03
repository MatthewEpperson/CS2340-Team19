using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUI : MonoBehaviour
{
    public Material GoBoardGrid;
    // Start is called before the first frame update
    // Start draws the static Go board
    void Start()
    {
        GameObject boardGrid = GameObject.Find("boardGrid");
        // Draw horizontal lines
        for(int i = 0; i < 9; i++){
            GameObject line = new GameObject("hLine");
            line.transform.parent = boardGrid.transform;
            line.AddComponent<LineRenderer>();
            LineRenderer lineRenderer;
            lineRenderer = line.GetComponent<LineRenderer>();
            List<Vector3> pos = new List<Vector3>();
            pos.Add(new Vector3(-80, 80-i*20));
            pos.Add(new Vector3(80, 80-i*20));
            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;
            lineRenderer.material.color = Color.black;
            lineRenderer.SetPositions(pos.ToArray());
            lineRenderer.useWorldSpace = true;
        }
        // draw verticle lines
         for(int i = 0; i < 9; i++){
            GameObject line = new GameObject("vLine");
            line.transform.parent = boardGrid.transform;
            line.AddComponent<LineRenderer>();
            LineRenderer lineRenderer;
            lineRenderer = line.GetComponent<LineRenderer>();
            List<Vector3> pos = new List<Vector3>();
            pos.Add(new Vector3(80-i*20, 80));
            pos.Add(new Vector3(80-i*20, -80));
            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;
            lineRenderer.material.color = Color.black;
            lineRenderer.SetPositions(pos.ToArray());
            lineRenderer.useWorldSpace = true;
        }
        // draw dots
        for(int i = 0; i < 2; i++){
            for(int j = 0; j< 2; j++){
                float x = 40 - i*80;
                float y = 40 - j*80;
                GameObject dot = new GameObject("dot");
                dot.transform.parent = boardGrid.transform;
                dot.AddComponent<LineRenderer>();
                LineRenderer lineRenderer;
                lineRenderer = dot.GetComponent<LineRenderer>();
                lineRenderer.material.color = Color.black;
                DrawPolygon(lineRenderer, 20, 1, new Vector3(x,y,0), 1f, 1f);
            }
        }
        for(int i = 0; i < 1; i++){
            GameObject dot = new GameObject("dot");
            dot.transform.parent = boardGrid.transform;
            dot.AddComponent<LineRenderer>();
            LineRenderer lineRenderer;
            lineRenderer = dot.GetComponent<LineRenderer>();
            lineRenderer.material.color = Color.black;
            DrawPolygon(lineRenderer, 20, 1, new Vector3(0,0,0), 1f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // LineRenderer helper to draw a polygon
    void DrawPolygon(LineRenderer lr, int vertexNumber, float radius, Vector3 centerPos, float startWidth, float endWidth)
    {
        lr.startWidth = startWidth;
        lr.endWidth = endWidth;
        lr.loop = true;
        float angle = 2 * Mathf.PI / vertexNumber;
        lr.positionCount = vertexNumber;

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                                                    new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                                    new Vector4(0, 0, 1, 0),
                                    new Vector4(0, 0, 0, 1));
            Vector3 initialRelativePosition = new Vector3(0, radius, 0);
            lr.SetPosition(i, centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition));
        }
    }
}
