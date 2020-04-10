using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGamePauser : MonoBehaviour
{
    public static bool running = false;

    public static DScriptRunnable[] scripts;
    public static int currentScript = -1;

    public static GameObject waitingForReleaseMovementGameObject;
    
    public enum STATE
    {
        WAITING,
        FREE
    }

    public static STATE state;

    private void Update()
    {
        if (!running) return;

        if (state == STATE.FREE)
        {
            currentScript += 1;
            if (currentScript == scripts.Length)
            {
                running = false;
                DGameSystem.canvasDialog.SetActive(false);
                DGameSystem.canvasCommon.SetActive(true);
                DGameSystem.canvasControl.SetActive(true);
                if (waitingForReleaseMovementGameObject != null)
                    waitingForReleaseMovementGameObject.GetComponent<DMovementExecutor>().enabled = true;
            }
            else
            {
                Debug.Log("calling start script " + currentScript.ToString());
                scripts[currentScript].Run();
                state = STATE.WAITING;
            }
        }
    }

    public static void StartScripts(DScriptRunnable[] input)
    {
        Debug.Log("Calling GamePauser.StartScripts");

        scripts = input;
        running = true;
        currentScript = -1;
        state = STATE.FREE;

        DGameSystem.canvasCommon.SetActive(false);
        DGameSystem.canvasControl.SetActive(true);
        DGameSystem.canvasDialog.SetActive(true);

    }

}
