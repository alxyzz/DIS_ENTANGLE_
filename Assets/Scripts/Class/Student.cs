using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student
{
    public string chosenName;

    public float EFFECTIVE_HAPPINESS //happiness post all modifiers
    {
        get
        {
            return 0;
        }
    }






    [Header("Seated image")]
    public Sprite seatedImage;

    [Header("Portrait image")]
    public Sprite portrait;

    [Header("Names to be picked at random")]
    public List<string> Names = new();

    [Header("Base happiness state")]
    public float STAT_LEARNING;

    [Header("Student description")]
    public string DESC;

    //[Header("Modifier for the entire lane")]
    //public float LANE_MODIFIER;

    [Header("Modifier for the entire row")]
    public float ROW_MODIFIER = 0;

    public StudentPrerequisite prereq;

    [Header("Effect of prerequisite being met.")]
    public StudentEffects effect;
}
