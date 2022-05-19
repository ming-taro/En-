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
	public boolean isCalculationOver() {
		if(expressionDTO.getResult().equals("")) return Constants.IS_NOT_CALCULATION_OVER;   //결과값이 없음 
		return Constants.IS_CALCULATION_OVER;  //"="을 입력받았지만 이미 결과값이 있음 -> 첫번째 숫자를 현재의 결과값으로 두고 다시 계산
	}
	public void addToRecordList(ArrayList<String> recordList) {
		if(expressionDTO.getOperator().equals("÷") && expressionDTO.getSecondValue().equals("")) {
			expressionDTO.setResult("0으로 나눌 수 없습니다.");
		}
		else recordList.add(expressionDTO.toString());
	}
	public void calculateExpression(StringBuilder numberbuilder, ArrayList<String> recordList) { 
		String number = numberbuilder.toString();
		
		if(expressionDTO.getOperator().equals("")) {  //첫번째 숫자 입력 후 연산자 입력없이 바로 '='입력시 -> 결과 : 첫번째 입력값
			expressionDTO.setFirstValue(number);
			expressionDTO.setResult(expressionDTO.getFirstValue());  //DTO에 첫번째값, 결과값 저장
		}
		else if(number.equals("")) {   //첫번째 숫자, 연산자 입력 후 두번째 숫자 입력 없이 바로 '='입력시 -> 결과 : 첫번째 숫자 (연산자) 첫번째 숫자
			expressionDTO.setSecondValue(expressionDTO.getFirstValue());
			arithmeticOperation.calculateExpression();
			numberbuilder.append(expressionDTO.getFirstValue());
		}
		else if(isCalculationOver()) {            //현재 저장된 값에 대한 결과가 한번이라도 완료가 되었는지 확인(한번 이상 완료 후 '='을 또 입력하면 결과값이 달라지기 때문)
			expressionDTO.setFirstValue(expressionDTO.getResult());   //계산완료 후 '='버튼 클릭시 현재 결과값에 두번째 숫자를 한번 더 계산한다(첫번째 숫자가 현재의 결과값)
			arithmeticOperation.calculateExpression();   //현재까지 입력한 값 계산 후 DTO에 저장
		}
		else {
			expressionDTO.setSecondValue(number); //계산완료된 적이 없음 -> 연산자 입력 후 누적된 두번째 숫자값 저장          예외) 3+=입력시 첫번째값=두번째값 -> 3+3=6
			arithmeticOperation.calculateExpression();   //현재까지 입력한 값 계산 후 DTO에 저장
		}

		addToRecordList(recordList);
	}
}
