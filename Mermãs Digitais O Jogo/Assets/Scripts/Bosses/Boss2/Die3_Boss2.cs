using UnityEngine;

public class Die3_Boss2 : MonoBehaviour
{
    public Hability3_Boss2 Dest;
    void Update()
    {
        if (Dest.puzzleSolved)
        {
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
