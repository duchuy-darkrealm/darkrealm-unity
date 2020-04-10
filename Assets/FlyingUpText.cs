using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMesh))]
public class FlyingUpText : MonoBehaviour
{
    public float countdowntime = 1f;
    public string textAnimation = "flyup";
    public float animationspeed = 2f;

    private TextMesh textmesh;
    private Vector3 position;
    private float count = 0;

    void Awake()
    {
        textmesh = GetComponent<TextMesh>();
        SetAndStart("Can you see it?", Color.red);
    }

    void Update()
    {
        count += Time.deltaTime;
        if (count > countdowntime)
        {
            textmesh.text = "";
            Destroy(gameObject);
        }

        UpdatePosition();
    }

    public void SetAndStart(string text, Color textcolor)
    {       
        textmesh.text = text;
        textmesh.color = textcolor;
        count = 0;
    }

    private void OnEnable()
    {
        count = 0;
    }

    void UpdatePosition()
    {
        if (textAnimation == "flyup") DoFlyUpText();
    }

    void DoFlyUpText()
    {
        position = transform.position;
        position += new Vector3(0f, animationspeed);
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime *0.3f);
    }
}
