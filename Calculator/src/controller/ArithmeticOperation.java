package controller;

import operationmanagement.ExpressionDTO;

public class ArithmeticOperation {
	private ExpressionDTO expressionDTO;
	
	public ArithmeticOperation(ExpressionDTO expressionDTO) {
		this.expressionDTO = expressionDTO;
	}
	
	public void calculateExpression() {
		double firstValue = Double.parseDouble(expressionDTO.getFirstValue());      //첫번째 입력값
		double secondValue = Double.parseDouble(expressionDTO.getSecondValue());    //두번째 입력값
		double result = 0;
		
		switch(expressionDTO.getOperator()) {    //연산자에 따라 계산
		case "+":
			result = firstValue + secondValue;
			break;
		case "-":
			result = firstValue - secondValue;
			break;
		case "×":
			result = firstValue * secondValue;
			break;
		case "÷":
			result = firstValue / secondValue;
			break;
		}
		
		if(result%1 == 0) expressionDTO.setResult(Integer.toString((int)result));
		else expressionDTO.setResult(Double.toString(result));
	}
	public void setExpression(StringBuilder numberBuilder, String operator) {  //연산자 입력
		expressionDTO.setFirstValue(numberBuilder.toString());   //연산자 이전에 입력한 첫번째 값 저장
		expressionDTO.setOperator(operator);                     //연산자 저장
		numberBuilder.setLength(0);                              //숫자 누적값 초기화
		numberBuilder.append("0");
	}
	public String manageArithmeticOperation(StringBuilder numberBuilder, String operator) {
		if(expressionDTO.getFirstValue().equals("") == false && expressionDTO.getSecondValue().equals("")) { 
			setExpression(numberBuilder, operator);   //첫번째 값 입력 후 연산자 입력시 
			return expressionDTO.getFirstValue() + expressionDTO.getOperator();  //연산자 입력 후 계산식 반환
		}
		
		
		
		return ""; //---->삭제할 코드
	}
}
