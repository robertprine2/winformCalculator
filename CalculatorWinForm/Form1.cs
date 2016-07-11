using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorWinForm
{
    public partial class Form1 : Form
    {
        // The answer that is computed and then changed to a string
        Double resultAmount = 0;
        // operator that was clicked
        String operatorUsed = "";
        // keeps track so people don't use two operators next to eachother
        bool isOperationPerformed = false;
        // allows for clearing of the typing screen after equal button is pressed
        bool justPressedEquals = false;
        // makes it so that you can't press an operator button twice
        bool justPressedOperator = false;
        public Form1()
        {
            InitializeComponent();
        }

        // when numbers or . are clicked
        private void button_click(object sender, EventArgs e)
        {
            // if there is currently a 0 on screen or the last button clicked was an operator
            if ((result.Text == "0") || (isOperationPerformed))
            {
                result.Clear();
            }
            // changes the bool to false so that you knew a number or decimal has been clicked most recently and another operator can be used
            isOperationPerformed = false;
            // the text that is being sent when the user clicks on a button
            Button button = (Button)sender;
            // if the . button is clicked
            if (button.Text == ".")
            {
                // if the result doesn't have a . yet add one, but otherwise don't do anything
                if (!result.Text.Contains("."))
                {
                    result.Text += button.Text;
                    problem.Text += button.Text;
                    justPressedOperator = false;
                }
            }
            // clears typing screen if the last button pressed was equals
            else if (justPressedEquals)
            {
                result.Text = "";
                justPressedEquals = false;
                result.Text += button.Text;
                problem.Text += button.Text;
                justPressedOperator = false;
            }
            // add the clicked button's text to the problem view and result view
            else
            {
                result.Text += button.Text;
                problem.Text += button.Text;
                justPressedOperator = false;
            }
        }
        // method for when you click an operator
        private void operator_click(object sender, EventArgs e)
        {
            // The button's text that is being sent
            Button button = (Button)sender;
            // if an operator has just been pressed do nothing
            if (justPressedOperator == false)
            {
                if (resultAmount != 0)
                {
                    // solves the problem so far and puts it in the typing view
                    equals.PerformClick();
                    operatorUsed = button.Text;
                    problem.Text = resultAmount + " " + operatorUsed;
                    isOperationPerformed = true;
                    justPressedOperator = true;
                }
                else
                {
                    // just puts the button text into the typing view
                    operatorUsed = button.Text;
                    // moves string in calculator view to resultAmount Variable and changes it to a double
                    resultAmount = Double.Parse(result.Text);
                    problem.Text += operatorUsed;
                    isOperationPerformed = true;
                    justPressedOperator = true;
                }
            }
        }
        // clears the history and the current typing view
        private void clear_Click(object sender, EventArgs e)
        {
            result.Text = "0";
            resultAmount = 0;
            justPressedOperator = false;
        }
        // clears just the typing view so you don't have to retype everything
        private void clearCurrent_Click(object sender, EventArgs e)
        {
            result.Text = "0";
            justPressedOperator = false;
        }
        // solves problems entered into calculator
        private void equals_Click(object sender, EventArgs e)
        {
            // finds the operator that was clicked to solve the problem correctly
            switch(operatorUsed)
            {
                case "+":
                    resultAmount = (resultAmount + Double.Parse(result.Text));
                    break;
                case "-":
                    resultAmount = (resultAmount - Double.Parse(result.Text));
                    break;
                case "*":
                    resultAmount = (resultAmount * Double.Parse(result.Text));
                    break;
                case "/":
                    resultAmount = (resultAmount / Double.Parse(result.Text));
                    break;
                default:
                    break;
            } // end switch
            // prints answer to problem in typing screen
            result.Text = resultAmount.ToString();
            resultAmount = Double.Parse(result.Text);
            // resets the history
            problem.Text = "";
            // allows the next button click to clear the typing screen
            justPressedEquals = true;
            // resets the equations answer amount
            resultAmount = 0;
            justPressedOperator = false;
        }
    }
}
