package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;

import operationmanagement.ExpressionDTO;
import view.CalculationButtonPanel;
import view.CalculatorFrame;
import view.ExpressionPanel;

public class ExpressionCalculation implements ActionListener{
	private ExpressionPanel expressionPanel;
	private CalculationButtonPanel calculationButtonPanel;
	private ExpressionDTO expression;
	
	public ExpressionCalculation() {
		expressionPanel = new ExpressionPanel();
		calculationButtonPanel = new CalculationButtonPanel(this);
		CalculatorFrame calculatorFrame =  new CalculatorFrame(expressionPanel, calculationButtonPanel);
	}
	
	public void setExpression(ExpressionDTO expression) {
		this.expression = expression;
	}

	@Override
	public void actionPerformed(ActionEvent event) {
		JButton button = (JButton) event.getSource();
		String buttonClicked = button.getText();

		System.out.print(buttonClicked);
		if(buttonClicked.charAt(0) >= '0' && buttonClicked.charAt(0) <= '9') {
			expressionPanel.setInputLabel(buttonClicked);
		}
		else if(buttonClicked.equals("CE")) {
			expressionPanel.setZero();
		}
		else if(buttonClicked.equals("C")) {
			System.out.print(buttonClicked);
			expressionPanel.removeInputLabel();
		}
	}
}
