using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DStat", menuName = "DStat")]
public class DStat : ScriptableObject
{
    public string characterName;
    public Sprite mugshot;

    public Sprite[] stand_up;
    public Sprite[] stand_down;
    public Sprite[] stand_left;
    public Sprite[] stand_right;

    public Sprite[] go_up;
    public Sprite[] go_down;
    public Sprite[] go_left;
    public Sprite[] go_right;

    public Sprite[] run_up;
    public Sprite[] run_down;
    public Sprite[] run_left;
    public Sprite[] run_right;

    public float speed = 4f;
    public float baseExp = 200;
    public float currentExp = 0;
    public float nextLvlExp = 0;
    public float hp = 200;
    public float maxhp = 200;
    public float dame = 20;
    public float attackRange = 2f;
    public float visionRange = 4f;
    public float attackCountDown = 1f;

    public string attackObjectName;

    public Sprite[] gethit_up;
    public Sprite[] gethit_down;
    public Sprite[] gethit_left;
    public Sprite[] gethit_right;

    public Sprite[] attack_up;
    public Sprite[] attack_down;
    public Sprite[] attack_left;
    public Sprite[] attack_right;
}
