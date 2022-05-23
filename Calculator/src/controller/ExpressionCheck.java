package controller;

import Model.ExpressionDTO;
import utility.Constants;

public class ExpressionCheck {
	private StringBuilder numberBuilder;
	private ExpressionDTO expressionDTO;
	
	public ExpressionCheck(StringBuilder numberBuilder, ExpressionDTO expressionDTO) {
		this.numberBuilder = numberBuilder;
		this.expressionDTO = expressionDTO;
	}
	
	public boolean isFirstInput() {
		if(numberBuilder.toString().equals("0") || isNegateOperation(numberBuilder.toString())) {
			return Constants.IS_FIRST_INPUT;
		}
		return Constants.IS_NOT_FIRST_INPUT; 
	}
	public boolean isCalculationOver() {  //계산식을 모두 입력하고 결과출력까지 끝났는지 확인
		if(expressionDTO.getResult().equals("")) return Constants.IS_NOT_CALCULATION_OVER; //계산값이 아직 없음 -> 계산이 완료되지 않음
		return Constants.IS_CALCULATION_OVER;
	}
	public boolean isMaximumInputRangeExceeded(String number) {
		if(numberBuilder.indexOf(".") != -1 && numberBuilder.length() == 17) return Constants.IS_RANGE_EXCEEDED;  //소수입력 -> 소수점 포함 최대길이 17
		if(numberBuilder.indexOf(".") == -1 && numberBuilder.length() == 16) return Constants.IS_RANGE_EXCEEDED;  //자연수입력 -> 최대길이 16
		if(numberBuilder.toString().equals("0") && number.equals("0")) return Constants.IS_RANGE_EXCEEDED;
		return Constants.IS_RANGE_NOT_EXCEEDED;
	}
	public boolean isPointEntered() {
		if(numberBuilder.indexOf(".") != -1) return Constants.IS_POINT_ENTERED;
		return Constants.IS_POINT_NOT_ENTERED;
	}
	public boolean isDividedByZero() {
		if(expressionDTO.getResult().equals("0으로 나눌 수 없습니다.")) return Constants.IS_DIVIDED_BY_ZERO;
		return Constants.IS_NOT_DIVIDED_BY_ZERO;
	}
	public boolean isResultUndefined() {
		if(expressionDTO.getResult().equals("정의되지 않은 결과입니다.")) return Constants.IS_DIVIDED_BY_ZERO;
		return Constants.IS_NOT_DIVIDED_BY_ZERO;
	}
	public boolean isDeletionButtonPressed(String buttonText) {
		if(buttonText.equals("=") || buttonText.equals("←")) return Constants.IS_DELETION_BUTTON_PRESSED;
		return Constants.IS_NOT_DELETION_BUTTON_PRESSED;
	}
	public boolean isNegateOperation(String value) {
		if(value.indexOf("negate") != -1) return Constants.IS_NEGATE_OPERATION;
		return Constants.IS_NOT_NEGATE_OPERATION;
	}
	public boolean isStackOverflow() {
		if(expressionDTO.getResult().equals("오버플로")) return true;
		return false;
	}
	public boolean isThereOperator() {
		if(expressionDTO.getOperator().equals("")) return Constants.IS_THERE_NO_OPERATOR;    
		return Constants.IS_THERE_OPERATOR;  
	}
	public boolean isThereSecondValue(String secondValue) {
		if(isThereOperator() == Constants.IS_THERE_NO_OPERATOR || secondValue.equals("")) { 
			return Constants.IS_THERE_NO_SECOND_VALUE;   
		}
		return Constants.IS_THERE_SECOND_VALUE;  //두번째 값을 입력함 -> 입력받은 연산자가 있고, 현재 입력중인 값(=두번째값)이 있다
	}
	public boolean isOperatorChanged(String secondValue) {
		if(isCalculationOver() == Constants.IS_NOT_CALCULATION_OVER && secondValue.equals("")) {   
			return Constants.IS_OPERATOR_CHANGED; //결과값이 없고, 연산자 변경은 두번째값 입력 전(numberBuilder에 누적된 값이 없음)에만 가능
		}
		return Constants.IS_OPERATOR_NOT_CHANGED;
	}
}
