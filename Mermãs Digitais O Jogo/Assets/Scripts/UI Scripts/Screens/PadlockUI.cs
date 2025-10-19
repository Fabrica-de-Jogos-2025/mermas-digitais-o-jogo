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
        //coursesImages = GetComponent<Image[]>();
        //coursesImages = GetComponentsInChildren<Image>();

        /*GameObject[] imageObjects = GameObject.FindGameObjectsWithTag("LevelImage");
        coursesImages = new Image[imageObjects.Length];

        for (int i = 0; i < imageObjects.Length; i++)
        {
            coursesImages[i] = imageObjects[i].GetComponent<Image>();
        }*/

        string[] imageNames = { "LevelImage1", "LevelImage2", "LevelImage3" };
        coursesImages = new Image[imageNames.Length];

        for (int i = 0; i < imageNames.Length; i++)
        {
            GameObject obj = GameObject.Find(imageNames[i]);
            if (obj != null)
            {
                coursesImages[i] = obj.GetComponent<Image>();
            }
            else
            {
                Debug.LogWarning($"Objeto com nome {imageNames[i]} não foi encontrado.");
            }
        }


        //var padlockObjects = GameObject.FindGameObjectsWithTag("Padlock");
        /*GameObject[] padlockObjects = GameObject.FindGameObjectsWithTag("Padlock");

        padlocks = new GameObject[padlockObjects.Length];
        for (int i = 0; i < padlockObjects.Length; i++)
        {
            padlocks[i] = padlockObjects[i];
        }*/

        string[] padlockNames = { "PadlockObject1", "PadlockObject2", "PadlockObject3" };
        padlocks = new GameObject[padlockNames.Length];

        for (int i = 0; i < padlockNames.Length; i++)
        {
            GameObject obj = GameObject.Find(padlockNames[i]);
            if (obj != null)
            {
                padlocks[i] = obj;
            }
            else
            {
                Debug.LogWarning($"Objeto com nome {padlockNames[i]} não foi encontrado.");
            }
        }

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
            transition.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        /*if (transition != null)
        {
            for (int i = 0; i < padlocks.Length; i++)
            {
                Debug.Log(i + " " + transition.LevelComplete[i]);
                if (transition.LevelComplete[i])
                {
                    padlocks[i].SetActive(false);
                    coursesImages[i].color = Color.white;
                }
            }
        }*/
    }
}
