using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorManager : MonoBehaviour
{
    public static CalculatorManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public double CalculateExpression(string expression)
    {
        double result_final = 0d;
        List<double> numbers = new List<double>();
        List<char> operators = new List<char>();
        string currentNumber = "";
        //Here we strore a decimal Numbers in a numbers List and store Operators in operators List
        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];
            if (char.IsDigit(c) || c == '.') 
            {
                currentNumber += c;
            }
            else 
            {
                if (!string.IsNullOrEmpty(currentNumber))
                {
                    numbers.Add(double.Parse(currentNumber));
                    currentNumber = "";
                }
                operators.Add(c); 
            }
        }
        //Now we add a Last decimal number after last operator
        if (!string.IsNullOrEmpty(currentNumber))
        {
            numbers.Add(double.Parse(currentNumber));
        }
        //first calculate multiplication and division 
        for (int i = 0; i < operators.Count; i++)
        {
            if (operators[i] == 'X' || operators[i] == '/')
            {
                //calculate multiply or division
                double result = operators[i] == 'X' ? numbers[i] * numbers[i + 1] : numbers[i] / numbers[i + 1];
                // replace the current number with the result and remove the next number
                numbers[i] = result;
                numbers.RemoveAt(i + 1);
                // remove the operator from operator list
                operators.RemoveAt(i);
            }

        }
        // Now we calculate addition and subtraction 
        int op_index = 0;
        while (op_index < operators.Count)
        {
            if (operators[op_index] == '+' || operators[op_index] == '-')
            {
                // calculate addition and subtraction
                double result = operators[op_index] == '+' ? numbers[op_index] + numbers[op_index + 1] : numbers[op_index] - numbers[op_index + 1];
                // same we replace the current number with the result and remove the next number
                numbers[op_index] = result;
                numbers.RemoveAt(op_index + 1);
                // remove the operator from operator list
                operators.RemoveAt(op_index);
            }
        }
        // now only result is left at a numbers list at 0 index
        result_final = numbers[0];
        return result_final;
    }
}
