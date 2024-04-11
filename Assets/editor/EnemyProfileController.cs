using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyProfileControll))]
public class EnemyProfileControllerEditor : Editor
{
    SerializedProperty eProfileProp;
    SerializedProperty attackValueProp;
    SerializedProperty defensiveValueProp;
    SerializedProperty idleValueProp;
    SerializedProperty lowAttackProbabilityProp;
    SerializedProperty midAttackProbabilityProp;
    SerializedProperty highAttackProbabilityProp;
    SerializedProperty lowDefenseProbabilityProp;
    SerializedProperty midDefenseProbabilityProp;
    SerializedProperty highDefenseProbabilityProp;

    void OnEnable()
    {
        eProfileProp = serializedObject.FindProperty("eProfile");
        attackValueProp = eProfileProp.FindPropertyRelative("attackValue");
        defensiveValueProp = eProfileProp.FindPropertyRelative("defensiveValue");
        idleValueProp = eProfileProp.FindPropertyRelative("idleValue");
        lowAttackProbabilityProp = eProfileProp.FindPropertyRelative("lowAttackProbability");
        midAttackProbabilityProp = eProfileProp.FindPropertyRelative("midAttackProbability");
        highAttackProbabilityProp = eProfileProp.FindPropertyRelative("highAttackProbability");
        lowDefenseProbabilityProp = eProfileProp.FindPropertyRelative("lowDefenseProbability");
        midDefenseProbabilityProp = eProfileProp.FindPropertyRelative("midDefenseProbability");
        highDefenseProbabilityProp = eProfileProp.FindPropertyRelative("highDefenseProbability");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(eProfileProp);

        // Ensure sum of attack, defense, and idle values is 100
        int sumValues = attackValueProp.intValue + defensiveValueProp.intValue + idleValueProp.intValue;
        if (sumValues != 100)
        {
            float scaleFactor = 100f / sumValues;
            attackValueProp.intValue = Mathf.RoundToInt(attackValueProp.intValue * scaleFactor);
            defensiveValueProp.intValue = Mathf.RoundToInt(defensiveValueProp.intValue * scaleFactor);
            idleValueProp.intValue = Mathf.RoundToInt(idleValueProp.intValue * scaleFactor);
        }

        // Volume control sliders for attack, defense, and idle values
        EditorGUILayout.LabelField("Attack Value", EditorStyles.boldLabel);
        attackValueProp.intValue = EditorGUILayout.IntSlider(attackValueProp.intValue, 0, 100);

        EditorGUILayout.LabelField("Defensive Value", EditorStyles.boldLabel);
        defensiveValueProp.intValue = EditorGUILayout.IntSlider(defensiveValueProp.intValue, 0, 100);

        EditorGUILayout.LabelField("Idle Value", EditorStyles.boldLabel);
        idleValueProp.intValue = EditorGUILayout.IntSlider(idleValueProp.intValue, 0, 100);

        // Ensure sum of attack probabilities is 100
        float attackSum = lowAttackProbabilityProp.intValue + midAttackProbabilityProp.intValue + highAttackProbabilityProp.intValue;
        if (attackSum != 100)
        {
            float scaleFactor = 100f / attackSum;
            lowAttackProbabilityProp.intValue = Mathf.RoundToInt(lowAttackProbabilityProp.intValue * scaleFactor);
            midAttackProbabilityProp.intValue = Mathf.RoundToInt(midAttackProbabilityProp.intValue * scaleFactor);
            highAttackProbabilityProp.intValue = Mathf.RoundToInt(highAttackProbabilityProp.intValue * scaleFactor);
        }

        // Ensure sum of defense probabilities is 100
        float defenseSum = lowDefenseProbabilityProp.intValue + midDefenseProbabilityProp.intValue + highDefenseProbabilityProp.intValue;
        if (defenseSum != 100)
        {
            float scaleFactor = 100f / defenseSum;
            lowDefenseProbabilityProp.intValue = Mathf.RoundToInt(lowDefenseProbabilityProp.intValue * scaleFactor);
            midDefenseProbabilityProp.intValue = Mathf.RoundToInt(midDefenseProbabilityProp.intValue * scaleFactor);
            highDefenseProbabilityProp.intValue = Mathf.RoundToInt(highDefenseProbabilityProp.intValue * scaleFactor);
        }

        // Volume control sliders for attack and defense probabilities
        EditorGUILayout.LabelField("Attack Probabilities", EditorStyles.boldLabel);
        lowAttackProbabilityProp.intValue = EditorGUILayout.IntSlider("Low", lowAttackProbabilityProp.intValue, 0, 100);
        midAttackProbabilityProp.intValue = EditorGUILayout.IntSlider("Mid", midAttackProbabilityProp.intValue, 0, 100);
        highAttackProbabilityProp.intValue = EditorGUILayout.IntSlider("High", highAttackProbabilityProp.intValue, 0, 100);

        EditorGUILayout.LabelField("Defense Probabilities", EditorStyles.boldLabel);
        lowDefenseProbabilityProp.intValue = EditorGUILayout.IntSlider("Low", lowDefenseProbabilityProp.intValue, 0, 100);
        midDefenseProbabilityProp.intValue = EditorGUILayout.IntSlider("Mid", midDefenseProbabilityProp.intValue, 0, 100);
        highDefenseProbabilityProp.intValue = EditorGUILayout.IntSlider("High", highDefenseProbabilityProp.intValue, 0, 100);

        serializedObject.ApplyModifiedProperties();
    }
}
