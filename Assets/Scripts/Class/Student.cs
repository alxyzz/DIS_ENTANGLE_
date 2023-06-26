using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student
{
    public string chosenName;

    [Header("Seated image")]
    public Sprite seatedImage;

    [Header("Portrait image")]
    public Sprite portrait;

    [Header("Names to be picked at random")]
    public List<string> Names = new();

    [Header("Base learning state")]
    public float STAT_LEARNING;

    [Header("Student description")]
    public string DESC;

    //[Header("Modifier for the entire lane")]
    //public float LANE_MODIFIER;

    [Header("Modifier for the entire row")]
    public float ROW_MODIFIER = 0;
}
