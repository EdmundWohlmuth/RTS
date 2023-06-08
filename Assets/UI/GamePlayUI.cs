using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayUI : MonoBehaviour
{
    public UnitControl unitController;

    // I'll want to do a check to se if the player has anything selected to make the cuttons clickable

    private void OnEnable()
    {
        VisualElement element = GetComponent<UIDocument>().rootVisualElement;

        Button buttonFormation = element.Q<Button>("ButtonFormation");
        Button buttonSetFire = element.Q<Button>("ButtonSetFire");
        Button buttonSquare = element.Q<Button>("ButtonSquare");
        Button buttonMerge = element.Q<Button>("ButtonMerge");

        buttonFormation.clicked += () => unitController.ToggleFormation();
        buttonSetFire.clicked += () => unitController.SetFiringRanges();
        buttonSquare.clicked += () => unitController.Square();
        buttonMerge.clicked += () => unitController.MergeUnits();
    }
}
