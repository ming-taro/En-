package controller;

import operationmanagement.ExpressionDTO;

public class ArithmeticOperation {
	
	public void calculateExpression(ExpressionDTO expressionDTO) {
		double firstValue = Double.parseDouble(expressionDTO.getFirstValue());      //첫번째 입력값
		double secondValue = Double.parseDouble(expressionDTO.getSecondValue());    //두번째 입력값
		double result = 0;
		
		switch(expressionDTO.getOperator()) {    //연산자에 따라 계산
		case '+':
			result = firstValue + secondValue;
			break;
		case '-':
			result = firstValue - secondValue;
			break;
		case '×':
			result = firstValue * secondValue;
			break;
		case '÷':
			result = firstValue / secondValue;
			break;
		}
		
		if(result%1 == 0) expressionDTO.setResult(Integer.toString((int)result));
		else expressionDTO.setResult(Double.toString(result));
	}
}
