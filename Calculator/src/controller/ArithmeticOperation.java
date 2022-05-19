package controller;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayList;

import Model.ExpressionDTO;
import utility.Constants;

public class ArithmeticOperation {
	private ExpressionDTO expressionDTO;
	
	public ArithmeticOperation(ExpressionDTO expressionDTO) {
		this.expressionDTO = expressionDTO;
	}
	public String divideNumber(BigDecimal firstValue, BigDecimal secondValue) {
		if(secondValue.doubleValue() == 0) {
			expressionDTO.setSecondValue("");
			return "0으로 나눌 수 없습니다.";
		}
		
		double result = firstValue.divide(secondValue, 16, RoundingMode.HALF_EVEN).doubleValue();
		
		if(Double.toString(result).length() <= 16) return Double.toString(result);
		return firstValue.divide(secondValue, 16, RoundingMode.HALF_EVEN).toString();
	}
	public void calculateExpression() {
		BigDecimal firstValue = new BigDecimal(expressionDTO.getFirstValue());      //첫번째 입력값
		BigDecimal secondValue = new BigDecimal(expressionDTO.getSecondValue());    //두번째 입력값
		BigDecimal result;
	
		switch(expressionDTO.getOperator()) {    //연산자에 따라 계산
		case "+":
			result = firstValue.add(secondValue);
			break;
		case "-":
			result = firstValue.subtract(secondValue);
			break;
		case "×":
			result = firstValue.multiply(secondValue);
			break;
		case "÷":
			expressionDTO.setResult(divideNumber(firstValue, secondValue));
			return;
		default:
			result = firstValue;    //'2='입력시(두번째값X,연산자X) -> 결과값 = 첫번째입력값 
			break;
		}
		expressionDTO.setResult(result.toString());
	}
	public void addToRecordList(StringBuilder numberBuilder, String operator, ArrayList<String> recordList) {
		String fristValue;
		
		if(expressionDTO.getResult().equals("0으로 나눌 수 없습니다.")) return;    //0으로 나누려고 한 경우 -> 기록을 저장하지 않음
		
		recordList.add(expressionDTO.toString());                 //현재까지 완성된 계산식 계산기록리스트에 저장
		fristValue = expressionDTO.getResult();                   //첫번째값 -> 현재 계산식의 계산결과
		expressionDTO.InitValue();                                //DTO값 초기화
		setExpression(numberBuilder, fristValue, operator);     //DTO값 재설정
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
	public String setNumber(String numberToChange) {
		double number = Double.parseDouble(numberToChange);
		
		if(number%1 == 0) return numberToChange;   //결과값이 정수인 경우
		return Double.toString(number); //결과값이 실수
	}
	public void manageArithmeticOperation(StringBuilder numberBuilder, String operator, ArrayList<String> recordList) {
		String number = setNumber(numberBuilder.toString());
		String fristValue;
		
		if(isCalculationOver()) {    //계산완료 후 연산자 입력시 -> 현재 계산결과값이 첫번째값이 되고, 연산자도 바꿈
			fristValue = expressionDTO.getResult();
			expressionDTO.InitValue();                    //이전 계산결과값이 저장되어있는 DTO정보 초기화
			setExpression(numberBuilder, fristValue, operator); 
		}
		else if(isAlreadyEnteredOperator() && numberBuilder.toString().equals("")) {  //첫번째값, 연산자까지 입력 후 연산자 다시 입력 -> 연산자만 변경
			expressionDTO.setOperator(operator);
		}
		else if (isAlreadyEnteredOperator()){    //첫번째값, 연산자, 두번째값까지 입력했지만 '='이 아닌 연산자 입력 -> 첫번째값이 연산결과값, 연산자 그대로, 두번째값 초기화
			expressionDTO.setSecondValue(number);   //현재까지 입력한 두번째 값 DTO에 저장
			calculateExpression();                                  //현재까지 입력값 계산 후 DTO에 저장	
			addToRecordList(numberBuilder, operator, recordList);   //계산기록 저장
		}
		else {   
			setExpression(numberBuilder, number, operator);   //첫번째 값 입력 후 연산자 입력시 -> 연산자 정보 저장
		}
	}
}
