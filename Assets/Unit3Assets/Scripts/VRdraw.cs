using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRdraw : MonoBehaviour
{
   [SerializeField]
        private Transform drawingStartPoint; // 起始畫筆的 Transform
        public Material material;

        [SerializeField, Range(0, 1.0f)]
        private float minDistanceBeforeNewPoint = 0.2f; // 最小繪圖距離

        [SerializeField]
        private float lineDefaultWidth = 0.010f; // 固定的線條寬度

        [SerializeField]
        private Color lineColor = Color.red; // 固定的線條顏色

        private bool isDrawing = false; // 玩家是否正在畫

        private List<LineRenderer> lines = new List<LineRenderer>(); // 管理所有繪圖的線條

        private LineRenderer currentLineRenderer;

        private Vector3 prevPointDistance = Vector3.zero;

        private int positionCount = 0;

        void Start()
        {
            if (drawingStartPoint == null)
            {
                Debug.LogError("Drawing start point is not assigned.");
            }
        }

        void Update()
        {
            HandleDrawing();
        }

        private void HandleDrawing()
        {

              
        
             
            

            if (isDrawing)
            {
                UpdateLine();
            }
        }

        public void StartDrawing()
        {
            isDrawing = true;
            AddNewLineRenderer();
        }

        public void StopDrawing()
        {
            isDrawing = false;
        }

        private void AddNewLineRenderer()
        {
            positionCount = 0;
            GameObject lineObject = new GameObject($"LineRenderer_{lines.Count}");
            lineObject.transform.position = drawingStartPoint.position;
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
            
            lineRenderer.startWidth = lineDefaultWidth;
            lineRenderer.endWidth = lineDefaultWidth;
            lineRenderer.useWorldSpace = true;
            lineRenderer.material = material;
            lineRenderer.positionCount = 1;
            lineRenderer.numCapVertices = 90;
            lineRenderer.SetPosition(0, drawingStartPoint.position);

            currentLineRenderer = lineRenderer;
            lines.Add(lineRenderer);
        }

        private void UpdateLine()
        {
            if (prevPointDistance == Vector3.zero)
            {
                prevPointDistance = drawingStartPoint.position;
            }

            if (Vector3.Distance(prevPointDistance, drawingStartPoint.position) >= minDistanceBeforeNewPoint)
            {
                Vector3 newPosition = drawingStartPoint.position;
                AddPoint(newPosition);
                prevPointDistance = newPosition;
            }
        }

        private void AddPoint(Vector3 position)
        {
            currentLineRenderer.SetPosition(positionCount, position);
            positionCount++;
            currentLineRenderer.positionCount = positionCount + 1;
            currentLineRenderer.SetPosition(positionCount, position);
        }
}

 public static class MaterialUtils 
    {
        public static Material CreateMaterial(Color color, string name, string shaderName = "Standard")
        {
            Material material = new Material(Shader.Find(shaderName));
            material.name = name;
            material.color = color;
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", color);
            return material;
        }
    }