using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DScriptDialog : DScriptRunnable
{
    public Sprite[] mugshots;
    public List<string> sentences;

    public override void Run()
    {
        GameObject.Find("DDialog").GetComponent<DDialog>().StartDialog(sentences, mugshots);
        Debug.Log("Calling Start Dialog");
    }
}
