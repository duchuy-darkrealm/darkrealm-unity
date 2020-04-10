using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DDialog : NetworkBehaviour, IPointerDownHandler
{
    public bool running = false;

    public TextMeshProUGUI dialog_text;
    public Image dialog_img;

    public List<string> sentences;
    public Sprite[] mugshots;

    private bool typeSentenceDone = true;
    private int index = 0;
    private bool finish = true;

    public IEnumerator TypeSentence(string sentence)
    {
        typeSentenceDone = false;

        string st = "";
        foreach (char x in sentence)
        {
            st += x;
            dialog_text.text = st;
            yield return null;
        }
        typeSentenceDone = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!running) return;

        if (typeSentenceDone)
        {
            index += 1;
            if (index == sentences.Count)
            {
                DGamePauser.state = DGamePauser.STATE.FREE;
                running = false;
                return;
            }
                

            StartCoroutine("TypeSentence", sentences[index]);
            typeSentenceDone = false;
        }
    }

    public void StartDialog(List<string> sens, Sprite[] mugs)
    {
        sentences = sens;
        mugshots = mugs;
        typeSentenceDone = false;
        index = 0;
        StartCoroutine("TypeSentence", sentences[index]);
        running = true;
    }
}