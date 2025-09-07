using UnityEngine;

public class RobotControlDialogue_2 : MonoBehaviour
{
    public RobotDialogue robotDialogue;
    public int dialogueStop;
    public Hability2 CLOSE_1, CLOSE_2, CLOSE_3, CLOSE_4;
    public bool valid;
    public DialogueHability dh;
    private bool playerInside = false;
    public int ds = -1;
    public GameObject UI_habillity;


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
            if (CLOSE_1.close && CLOSE_2.close && CLOSE_3.close && CLOSE_4.close)
            {
                dh.permissionDialogueFigure = false;
                robotDialogue.permissionToProceed = true;
                robotDialogue.dialogueIndex = ds - 1;
                robotDialogue.NextMessage();
                valid = false;
            }
            else if ((robotDialogue.dialogueIndex + 1) == ds)
            {
                robotDialogue.permissionToProceed = false;
            }
        }
    }
}
