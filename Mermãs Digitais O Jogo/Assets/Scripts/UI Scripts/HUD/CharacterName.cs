using UnityEngine;

public class CharacterName : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private Vector3 offset;

    private Camera cam;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        cam = Camera.main;
        Vector3 offset = new Vector3(0, 0, 0);
        
    }
    private void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);

        if (transform.position != pos)
        {
            transform.position = pos;
        }
    }
}
