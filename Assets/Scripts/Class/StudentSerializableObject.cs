using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New StudentSerializableObject", menuName = "StudentSerializableObject")]
public class StudentSerializableObject : ScriptableObject
{

    public string chosenName = "";
    
    [Header("Seated image")]
    public Sprite seatedImage;

    [Header("Portrait image")]
    public Sprite portrait;

    [Header("Names to be picked at random")]
    public List<string> Names = new();

    [Header("Base learning state")]
    public float STAT_LEARNING = 0;

    [Header("Student description")]
    public string DESC = "";

    //[Header("Modifier for the entire lane")]
    //public float LANE_MODIFIER;

    [Header("Modifier for the entire row")]
    public float ROW_MODIFIER = 0;

    [Header("Prerequisite to meet.")]
    public StudentEffectPrerequisite prereq;

    [Header("Number related to prereq. Be it happiness level, or row, or howm any neighbors,.")]
    public float PREREQ_ARGUMENT = 0;

    [Header("Effect of prerequisite being met.")]
    public StudentEffectType effect;

    [Header("Effect argument 1/left")]
    public float EFFECT_ARG_ONE = 0;

    [Header("Effect argument 2/right")]
    public float EFFECT_ARG_two = 0;
}



public enum StudentEffectPrerequisite
{
    NEEDS_NOTHING,
    NEEDS_HAPPINESS_LEVEL,
    NEEDS_SPECIFIC_ROW,
    NEEDS_NEIGHBORS
}

public enum StudentEffectType
{
    None,
    GAIN_ALL_OTHER_ROWS,
    GAIN_OWN_ROW,
    GAIN_SELF,
    SET_AVERAGE_OF_NEIGHBORS,
    GAIN_SELF_AND_NEIGHBORS,
    GAIN_NEIGHBORS_LEFT_RIGHT_DIFFERENCE

}


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