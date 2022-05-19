package controller;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayList;

import Model.ExpressionDTO;
import utility.Constants;


public class Calculation {
	private ExpressionDTO expressionDTO;                //현재 입력한 값들
	private ArithmeticOperation arithmeticOperation;    //사칙연산 계산
	
	public Calculation(ExpressionDTO expressionDTO, ArithmeticOperation arithmeticOperation) {
		this.expressionDTO = expressionDTO;
		this.arithmeticOperation = arithmeticOperation;
	}
	public String setNumber(String numberToChange) {
		double number = Double.parseDouble(numberToChange);
		
		if(number%1 == 0) return Long.toString((long)number);   //결과값이 정수인 경우
		if(Double.toString(number).length() <= 16) return Double.toString(number); //결과값이 실수이면서 숫자의 최대길이인 16을 초과하지 않은 경우 -> 결과 그대로 저장
		
		String[] numberArray = Double.toString(number).split("\\.");
		int length = 16 - numberArray[0].length();
		BigDecimal result = new BigDecimal(number).setScale(length, RoundingMode.HALF_UP);
		
		System.out.println(length + "//" + new BigDecimal(number).setScale(length, RoundingMode.HALF_UP));
		return result.toString();
	}
	public boolean isThereCalculationResult() {
		if(expressionDTO.getResult().equals("")) return Constants.IS_THERE_NO_CALCULATION_RESULT; 
		return Constants.IS_THERE_CALCULATION_RESULT;
	}
	public boolean isThereOperator() {
		if(expressionDTO.getOperator().equals("")) return Constants.IS_THERE_NO_OPERATOR;    
		return Constants.IS_THERE_OPERATOR;  
	}
	public boolean isThereSecondValue(String secondValue) {
		if(secondValue.equals("")) return Constants.IS_THERE_NO_SECOND_VALUE;   //결과값이 없음 
		return Constants.IS_THERE_SECOND_VALUE;  //"="을 입력받았지만 이미 결과값이 있음 -> 첫번째 숫자를 현재의 결과값으로 두고 다시 계산
	}
	public void addToRecordList(ArrayList<String> recordList) {
		if(expressionDTO.getOperator().equals("÷") && expressionDTO.getSecondValue().equals("")) {
			expressionDTO.setResult("0으로 나눌 수 없습니다.");
		}
		else recordList.add(expressionDTO.toString());
	}
	public void calculateExpression(StringBuilder numberbuilder, ArrayList<String> recordList) { 
		String number = numberbuilder.toString();
		
		if(isThereCalculationResult()) {                              //이전 계산값이 남아있음(ex '2+3='입력 후 '5'라는 결과값이 남아있는 상태에서 '='을 입력함)
			expressionDTO.setFirstValue(expressionDTO.getResult());   //현재 결과값에 두번째 숫자를 한번 더 계산한다(첫번째 숫자가 현재의 결과값)
		}
		else if(isThereSecondValue(number)) {            //두번째 숫자(numberbuilder) 입력중 '='을 입력함(ex '2+3'입력 후 '='을 입력함)
			expressionDTO.setSecondValue(number);        //numberbuilder에 누적된 두번째 숫자값 저장
		}
		else if(isThereOperator()) {                     //연산자입력 후 '='을 입력함(ex '2+'입력 후 '='을 입력함)
			expressionDTO.setSecondValue(expressionDTO.getFirstValue());    
			numberbuilder.append(expressionDTO.getFirstValue());
		}
		else {                                           //숫자입력 후 '='을 입력함(ex '2'입력 후 '='을 입력함)
			expressionDTO.setFirstValue(number); 
		}

		arithmeticOperation.calculateExpression();        //현재까지 입력한 값 계산 후 DTO에 저장
		addToRecordList(recordList);
	}
}
