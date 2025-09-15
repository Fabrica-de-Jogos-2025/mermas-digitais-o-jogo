using UnityEngine;
using UnityEngine.UI;

public class PadlockUI : MonoBehaviour
{
    [SerializeField] private Transition transition;
    [SerializeField] private GameObject[] padlocks;
    [SerializeField] private Image[] coursesImages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame

    private void Start()
    {
        transition = FindAnyObjectByType<Transition>();
        coursesImages = GetComponent<Image[]>();
    }
    void Update()
    {
        if (transition != null)
        {
            for (int i = 0; i < padlocks.Length; i++)
            {
                if (transition.LevelComplete[i])
                {
                    padlocks[i].SetActive(false);
                    coursesImages[i].color = Color.white;
                }
            }
        }
    }
}
