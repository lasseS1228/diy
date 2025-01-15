using UnityEngine;

public class moveParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Example radii for the rings
        float[] radii = { 10.0f, 15.0f, 20.0f };

        // Spawn rings with different radii and positions
        for (int i = 0; i < radii.Length; i++)
        {
            //TODO for Tomorrow delete instantiatePRefab from prefab build it here
            //THis To become management of all rings
            RingDraw.InstantiatePrefab(radii[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 100.0f * Time.deltaTime);
    }
}
