using UnityEngine;

public class Parallax_v3 : MonoBehaviour
{
    private float startPos;
    private float startPosy;

    private Camera cam;
    [Range(0f, 1f)]
    public float parallaxEffect;
    [Range(0f, 1f)]
    public float parallaxEffecty;

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

    /*void Update()
{
    float distanceX = cam.transform.position.x * parallaxEffect;
    float targetY = cam.transform.position.y * parallaxEffecty;
    float smoothY = Mathf.Lerp(transform.position.y, startPosy + targetY, Time.deltaTime * 3f);

    transform.position = new Vector3(startPos + distanceX, smoothY, transform.position.z);
}*/
}
