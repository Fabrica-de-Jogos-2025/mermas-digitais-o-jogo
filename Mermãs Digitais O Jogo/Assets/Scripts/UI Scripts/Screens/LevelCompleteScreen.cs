using System.Collections;
using UnityEngine;

public class LevelCompleteScreen : MonoBehaviour
{
    [SerializeField] private Loader loader;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitAnimation());
    }

    private IEnumerator WaitAnimation()
    {
        anim.SetInteger("transition", 4);
        yield return new WaitForSeconds(5f);
        loader.CarregarFase("Level Selector");
    }
}
