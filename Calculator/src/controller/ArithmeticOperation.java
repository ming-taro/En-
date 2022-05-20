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
	public BigDecimal getSecondValue(String secondValue) {  
		if(secondValue.equals("")) return new BigDecimal("0");   //'2='계산시 secondValue가 없는 상황에서 bigDecimal에대한 nullPointer오류가 나기 때문에 기본값'0'으로 초기화
		return new BigDecimal(secondValue);
	}
	public void calculateExpression() {
		BigDecimal firstValue = new BigDecimal(expressionDTO.getFirstValue());      //첫번째 입력값
		BigDecimal secondValue = getSecondValue(expressionDTO.getSecondValue());    //두번째 입력값
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
		expressionDTO.InitValue();                    //이전 계산결과값이 저장되어있는 DTO정보 초기화
		expressionDTO.setFirstValue(setNumber(firstValue));  //첫번째 값 저장
		expressionDTO.setOperator(operator);          //연산자 저장
		numberBuilder.setLength(0);                   //숫자 누적값 초기화
		numberBuilder.append("");                     
	}
	private boolean isThereCalculationResult() {
		if(expressionDTO.getResult().equals("")) return Constants.IS_THERE_NO_CALCULATION_RESULT; 
		return Constants.IS_THERE_CALCULATION_RESULT;
	}
	private boolean isThereOperator() {
		if(expressionDTO.getOperator().equals("")) return Constants.IS_THERE_NO_OPERATOR;    
		return Constants.IS_THERE_OPERATOR;  
	}
	private boolean isThereSecondValue(String secondValue) {
		if(isThereOperator() == Constants.IS_THERE_NO_OPERATOR || secondValue.equals("")) { 
			return Constants.IS_THERE_NO_SECOND_VALUE;   
		}
		return Constants.IS_THERE_SECOND_VALUE;   //두번째 값을 입력함 -> 입력받은 연산자가 있고, 현재 입력중인 값(=두번째값)이 있다
	}
	private boolean isOperatorChanged(String secondValue) {
		if(isThereCalculationResult() == Constants.IS_THERE_NO_CALCULATION_RESULT && secondValue.equals("")) {   
			return Constants.IS_OPERATOR_CHANGED; //결과값이 없고, 연산자 변경은 두번째값 입력 전(numberBuilder에 누적된 값이 없음)에만 가능
		}
		return Constants.IS_OPERATOR_NOT_CHANGED;
	}
	public String setNumber(String numberToChange) {   //DTO에 숫자 저장시 사용 -> '12.2200'입력시 의미없는 '00'을 없애기 위함
		double number = Double.parseDouble(numberToChange);
		
		if(number%1 == 0) return numberToChange;       //결과값이 정수인 경우
		
		String[] numberArray = numberToChange.split("\\.");
		String decimal;
		
		if(numberArray[1].charAt(0) == '0') decimal = "." + numberArray[1]; 
		else decimal = Double.toString(Double.parseDouble("."+numberArray[1])).replaceFirst("0", "");
		
		return numberArray[0] + decimal;
	}
	public void manageArithmeticOperation(StringBuilder numberBuilder, String operator, ArrayList<String> recordList) {
		String number = numberBuilder.toString();
		String firstValue = number;         //첫 연산자 입력시 첫번째값은 현재까지 numberBuilder에 누적된 값

		if(isOperatorChanged(number)) {               //연산자 변경 발생(ex '2-'입력 후 '+'입력 -> '2+')
			expressionDTO.setOperator(operator);
			return;
		}
		
		if(isThereSecondValue(number)) {              //첫 연산자 입력(=저장된 연산자,두번째값이 없음)시 if문을 실행하지 않고 setExpression만 실행(ex '2'입력 후 '+'입력  => '2+')
			expressionDTO.setSecondValue(setNumber(number));     //두번째값 입력 중 연산자 입력시  누적된 두번째값 DTO에 저장 후 계산
			calculateExpression();                    
		}	
		if(isThereCalculationResult()) firstValue = expressionDTO.getResult();   //계산 결과값이 첫번째 값이 됨 (ex '2+3'입력 후 '-'입력 => '5-' / ex '2+3=5'입력 후 '-'입력 => '5-')
		
		setExpression(numberBuilder, firstValue, operator);                      //계산식(첫번째값,연산자) 저장 및 누적된 입력값 초기화
	}
}
