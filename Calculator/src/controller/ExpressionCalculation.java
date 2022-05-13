package controller;

import operationmanagement.ExpressionDTO;
import view.CalculationButtonPanel;
import view.CalculatorFrame;
import view.ExpressionPanel;

public class ExpressionCalculation {
	private ExpressionPanel expressionPanel;
	private CalculationButtonPanel calculationButtonPanel;
	private ExpressionDTO expression;
	
	public ExpressionCalculation() {
		expressionPanel = new ExpressionPanel();
		calculationButtonPanel = new CalculationButtonPanel(expressionPanel);
		CalculatorFrame calculatorFrame =  new CalculatorFrame(expressionPanel, calculationButtonPanel);
	}
	
	public void setExpression(ExpressionDTO expression) {
		this.expression = expression;
	}
}
