using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student
{
    public string chosenName;
    public SeatRow row;
    public float EFFECTIVE_HAPPINESS //happiness post all modifiers
    {
        get
        {
            float b = STAT_LEARNING + row.ROW_MODIFIER;
            return b;
        }
    }


    #region neighbor modifiers

   





    //     Possible prerequisits:
    //None = always active
    //if own happiness stat is above or below a certain value
    //if in a certain row
    //if self has neighbours



    //    Effects:
    //all other rows get +X
    //own row gets +X
    //self has +X
    //Sets own happiness to the combined value of neighbours
    //Neighbouring students and self get +X
    //Left neighbour gets +X and right neighbour +X


    #endregion
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

    public StudentEffectPrerequisite prereq = StudentEffectPrerequisite.NEEDS_NOTHING;

    [Header("Prerequisite argument")]
    public float PREREQ_ARGUMENT = 0;

    [Header("Effect of prerequisite being met.")]
    public StudentEffectType effect= StudentEffectType.None;


    [Header("Effect argument 1/left")]
    public float EFFECT_ARG_ONE = 0;

    [Header("Effect argument 2/right")]
    public float EFFECT_ARG_two = 0;
}
