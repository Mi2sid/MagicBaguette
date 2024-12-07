using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform startPoint;
    public Transform endPoint;

    void Update()
    {
        lineRenderer.positionCount = 5; // Exemple avec 5 segments
        lineRenderer.SetPosition(0, new Vector3(0.0f, 0.0f, 0.0f));
        for (int i = 1; i < lineRenderer.positionCount; i++)
        {
            float t = i / (float)(lineRenderer.positionCount - 1);
            Vector3 position = Vector3.Lerp( new Vector3(0.0f, 0.0f, 0.0f),  new Vector3(0.0f, 0.0f, 10.0f), t);
            float size = 0.5f;
            position += new Vector3(Random.Range(-size, size), Random.Range(-size, size), Random.Range(-size, size)); // Effet zigzag

            lineRenderer.SetPosition(i, position);
        }
    }
}
