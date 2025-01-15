using UnityEngine;

public class RingDraw : MonoBehaviour
{
    public LineRenderer circleRenderer;
    public float radius = 10.0f; // Adjustable radius per ring
    public int steps = 100; // Number of steps for the circle to draw
    public float rotationSpeed = 50.0f; // Rotation speed in degrees per second

    private void Start()
    {
        circleRenderer.useWorldSpace = false; // Ensures rotation affects the circle
        DrawCircle(steps, radius); // Draw the initial circle
    }

    private void Update()
    {
         // Rotate the ring every frame
    }

    private void DrawCircle(int steps, float radius)
    {
        circleRenderer.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            // Add a gap in the circle if desired
            currentRadian -= currentRadian / 10;

            float x = Mathf.Cos(currentRadian) * radius;
            float y = Mathf.Sin(currentRadian) * radius;

            Vector3 currentPosition = new Vector3(x, y, 0);
            circleRenderer.SetPosition(currentStep, currentPosition);
        }

        // Optional: Add collision to the ring using an EdgeCollider2D
        AddEdgeCollider();
    }

    private void AddEdgeCollider()
    {
        EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();

        Vector3[] v3Positions = new Vector3[circleRenderer.positionCount];
        circleRenderer.GetPositions(v3Positions);

        Vector2[] positions = new Vector2[circleRenderer.positionCount];
        for (int i = 0; i < circleRenderer.positionCount; i++)
        {
            positions[i] = new Vector2(v3Positions[i].x, v3Positions[i].y);
        }

        edge.points = positions;
    }

    // Static method to instantiate the prefab with a given radius
    public static RingDraw InstantiatePrefab(float radius)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Ring"); // Load prefab from Resources
        if (prefab == null)
        {
            Debug.LogError("Prefab not found! Make sure the prefab is located in Resources/Prefabs.");
            return null;
        }

        // Instantiate the prefab and set its position
        GameObject instance = Instantiate(prefab, new Vector3(0.0f ,0.0f,0.0f), Quaternion.identity);
        RingDraw ringDraw = instance.GetComponent<RingDraw>();

        if (ringDraw != null)
        {
            ringDraw.radius = radius;
            ringDraw.DrawCircle(ringDraw.steps, radius); // Redraw the circle with the new radius
        }

        return ringDraw;
    }
}
