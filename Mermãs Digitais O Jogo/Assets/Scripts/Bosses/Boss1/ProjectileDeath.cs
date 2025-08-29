using UnityEngine;
using System.Collections;

public class ProjectileDeath : MonoBehaviour
{
    void Update()
    {
        ExecuteOnce();
    }

    
    void ExecuteOnce()
    {
        gameObject.SetActive(true);
        StartCoroutine(WaitAndDeactivate());
    }

    IEnumerator WaitAndDeactivate()
    {
        float animationDuration = 4 * 0.25f;
        yield return new WaitForSeconds(animationDuration);
        gameObject.SetActive(false);
    }

}
