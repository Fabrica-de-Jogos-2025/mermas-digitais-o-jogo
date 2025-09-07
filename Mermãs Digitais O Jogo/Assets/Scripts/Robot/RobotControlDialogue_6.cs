using UnityEngine;

public class RobotControlDialogue_6 : MonoBehaviour
{
    public RobotDialogue robotDialogue;
    public int dialogueStop;
    public Hability6 Dest;
    public bool valid;
    public DialogueHability dh;
    private bool playerInside = false;
    public int ds = -1;
    public GameObject UI_habillity;
    public GameObject puzzleSolved;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !UI_habillity.activeSelf)
        {
            //playerInside = true;
            ds = dialogueStop;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !UI_habillity.activeSelf)
        {
            //playerInside = false;
            ds = -1;
        }
    }

    void Start()
    {
        robotDialogue = FindFirstObjectByType<RobotDialogue>();
    }


    void Update()
    {
        if (UI_habillity.activeSelf)
        {
            valid = true;
            robotDialogue.permission = false;
        }

        if (valid)
        {
            if (puzzleSolved == null)
            {
                dh.permissionDialogueFigure = false;
                robotDialogue.permissionToProceed = true;
                robotDialogue.dialogueIndex = ds - 1;
                robotDialogue.NextMessage();
                valid = false;
                Destroy(this.gameObject);
            }
            else if ((robotDialogue.dialogueIndex + 1) == ds)
            {
                robotDialogue.permissionToProceed = false;
            }
        }
    }
}
