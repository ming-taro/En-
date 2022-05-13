package controller;

import operationmanagement.Constants;
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
		
		if(result%1 == 0) expressionDTO.setResult(Long.toString((long)result));
		else expressionDTO.setResult(Double.toString(result));
	}
	public void setExpression(StringBuilder numberBuilder, String firstValue, String operator) {  //연산자 입력
		expressionDTO.setFirstValue(firstValue);   //연산자 이전에 입력한 첫번째 값 저장
		expressionDTO.setOperator(operator);                     //연산자 저장
		numberBuilder.setLength(0);                              //숫자 누적값 초기화
		numberBuilder.append("");                                //두번째 숫자 입력부터는 stringBuilder값을 0으로 초기화하지 않고 비움 -> ex)9+=입력값과 9+0=입력값을 다르게 구별하기 위함
	}
	public boolean isAlreadyEnteredOperator() {     //이미 연산자를 입력했는지 확인
		if(expressionDTO.getOperator().equals("")) return Constants.IS_NOT_ENTERED_OPERATOR;
		return Constants.IS_ALREADY_ENTERED_OPERATOR;
	}
	public boolean isCalculationOver() {
		if(expressionDTO.getResult().equals("")) return Constants.IS_NOT_CALCULATION_OVER; //계산이 아직 끝나지 않은 경우(현재 저장된 계산결과값이 없음)
		return Constants.IS_CALCULATION_OVER;  //이전 계산결과값이 남아있음
	}
	public String manageArithmeticOperation(StringBuilder numberBuilder, String operator) {
		String fristValue;
		
		if(isCalculationOver()) {   //계산완료 후 연산자 입력시 -> 현재 계산결과값이 첫번째값이 되고, 연산자도 바꿈
			fristValue = expressionDTO.getResult();
			expressionDTO.InitValue();                    //이전 계산결과값이 저장되어있는 DTO정보 초기화
			setExpression(numberBuilder, fristValue, operator); 
		}
		else if(isAlreadyEnteredOperator()) {             //첫번째값, 연산자까지 입력 후 연산자 다시 입력 -> 연산자만 변경
			expressionDTO.setOperator(operator);
		}
		else {   
			setExpression(numberBuilder, numberBuilder.toString(), operator);   //첫번째 값 입력 후 연산자 입력시 -> 연산자 정보 저장
		}
		
		return expressionDTO.getFirstValue() + expressionDTO.getOperator();  //연산자 입력 후 계산식 반환
	}
}
