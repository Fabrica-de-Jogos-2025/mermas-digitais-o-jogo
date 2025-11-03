using UnityEngine;
using System.Collections;

public class Teste_Die_5_2_Boss3 : MonoBehaviour
{

    public Teste_Hability5_2 Dest;
    private bool isDestroyed = false;
    public Boss3_Attack Z;
    private PlayerMovement player;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip finalizationSound;
    private bool pemissao = false;
    
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }
    void Update()
    {
        if (!isDestroyed && Dest.puzzleSolved && !pemissao)
        {
            pemissao = true;
            StartCoroutine(desabilitarAposFinalizarAudio());
        }
    }

    private IEnumerator desabilitarAposFinalizarAudio()
    {
        sfxAcess.Audio(finalizationSound);
        yield return new WaitForSeconds(finalizationSound.length);
        player.IsFrozen = false;
        isDestroyed = true;
        Z.z = true;
        Destroy(this.gameObject);
    }
}