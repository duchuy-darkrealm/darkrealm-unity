using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSD_001 : DInteractableObject
{
    public DScriptDialog[] dialogs;

    public override void Interact()
    {
        GetComponent<DMovementExecutor>().enabled = false;
        DGamePauser.waitingForReleaseMovementGameObject = this.gameObject;
        DGameSystem.player.GetComponent<DMovement>().FaceToGameObject(this.gameObject);

        dialogs = new DScriptDialog[3];
        for (int i = 0; i < dialogs.Length; i++)
            dialogs[i] = new DScriptDialog();
        dialogs[0].sentences = new List<string> { "This is Script 1, sentensce 1", "THIS IS SEN2 ", "THIS IS SEN 3" };
        dialogs[1].sentences = new List<string> { "This is Script 1, sentensce 1", "THIS IS SEN2 ", "THIS IS SEN 3" };
        dialogs[2].sentences = new List<string> { "This is Script 1, sentensce 1", "THIS IS SEN2 ", "THIS IS SEN 3" };
        DGamePauser.StartScripts(dialogs);
    }

}
