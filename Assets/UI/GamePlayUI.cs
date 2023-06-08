using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayUI : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement element = GetComponent<UIDocument>().rootVisualElement;

        Button buttonFormation = element.Q<Button>("ButtonFormation");
        Button buttonSetFire = element.Q<Button>("ButtonSetFire");
        Button buttonSquare = element.Q<Button>("ButtonSquare");
        Button buttonMerge = element.Q<Button>("ButtonMerge");

        // buttonFormation.clicked += () => //Put Meathod Here\\
    }
}
