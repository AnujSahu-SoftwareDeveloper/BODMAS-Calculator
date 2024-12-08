using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI currentInputTxt;
    internal string currentString = "";
    internal string operatorsList = "+-X/";
    internal bool isNewCalculation;
    private void Awake()
    {
        currentInputTxt.text = "";
        currentString = "";
        isNewCalculation = false;
    }
    /// <summary>
    /// Take user input from UI calculator button
    /// </summary>
    /// <param name="str"></param>
    public void TakeUserInput(string str)
    {
        if (operatorsList.Contains(str) && operatorsList.Contains(str))
        {
            if (isNewCalculation)
                isNewCalculation = false;
            if (currentString.Length > 0)
            {
                //avoid multiple operator add consicutively on the expression
                if (operatorsList.Contains(currentString[currentString.Length - 1]))
                {
                    currentString = currentString.Substring(0, currentString.Length - 1) + str;
                }
                else
                {
                    currentString += str;
                }
            }
            else
            {
                //if try to put any operator from starting 
                if(operatorsList.Contains(str))
                {
                    currentString = "0" + str;
                }
            }
            
        }
        else
        {
            if(isNewCalculation)
            {
                currentString = str;
                isNewCalculation = false;
            }
            else
            {
                currentString += str;
            }
            
        }
        SetInputTxt(currentString);
    }
    /// <summary>
    /// OnClick All clear button
    /// </summary>
    public void OnClickACBtn()
    {
        currentString = "";
        SetInputTxt(currentString);
    }
    /// <summary>
    /// On click Equal button
    /// </summary>
    public void OnclickEqualBtn()
    {
        if(!string.IsNullOrEmpty(currentString))
        {
            currentString = CheckCurrentString(currentString);
            currentString = GetCurrentInputStr(currentString);
            isNewCalculation = true;
            SetInputTxt(currentString);
        }
    }
    /// <summary>
    /// remove the operator if it exist at the end of the expression
    /// </summary>
    /// <param name="str">expression</param>
    /// <returns></returns>
    private string CheckCurrentString(string str)
    {
        string resStr = str;
        if(str.Length>0)
        {
            if(operatorsList.Contains(str[str.Length-1]))
            {
                str = str.Remove(str.Length - 1);
                resStr = str;
            }
        }
        return resStr;
    }
    /// <summary>
    /// Set the expression on the UI Text
    /// </summary>
    /// <param name="str"></param>
    private void SetInputTxt(string str)
    {
        currentInputTxt.text = str;
    }
    private string GetCurrentInputStr(string str)
    {
        string s = CalculatorManager.Instance.CalculateExpression(str).ToString();
        return s;
    }
    
}
