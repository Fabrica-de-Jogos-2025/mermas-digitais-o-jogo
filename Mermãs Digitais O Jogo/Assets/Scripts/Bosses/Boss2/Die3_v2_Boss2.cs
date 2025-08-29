using UnityEngine;

public class Die3_v2_Boss2 : MonoBehaviour
{
    public Hability3_Boss2_2 Dest;
    void Update()
    {
        if (Dest.puzzleSolved)
        {
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
