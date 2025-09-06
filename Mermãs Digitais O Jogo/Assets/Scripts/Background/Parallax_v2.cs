using UnityEngine;

public class Parallax_v2 : MonoBehaviour
{
    private float startPos;
    private float startPosy;

    private Camera cam;
    [Range (0f, 1f)]
    public float parallaxEffect;
    public float parallaxEffecty = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = FindAnyObjectByType<Camera>();
        startPos = transform.position.x;
        startPosy = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float distanceY = cam.transform.position.y * parallaxEffecty;
        transform.position = new Vector3(startPos + distance, startPosy + distanceY, transform.position.z);
    }
}
