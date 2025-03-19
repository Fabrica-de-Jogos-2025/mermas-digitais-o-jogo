using UnityEngine;

public class die_3 : MonoBehaviour
{

    public Hability3 Dest;
    
    public GameObject habilityCard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Dest.puzzleSolved)
        {
            if (Dest.cardAppear)
            {    
                habilityCard.SetActive(true);
            }
            Destroy(this.gameObject);
        }
    }
}
