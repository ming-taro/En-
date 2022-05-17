package controller;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayList;

import Model.EquationDTO;
import utility.Constants;

public class EqualSign {
	private EquationDTO equationDTO;                //현재 입력한 값들
	private ArithmeticOperation arithmeticOperation;    //사칙연산 계산
	
	public EqualSign(EquationDTO equationDTO, ArithmeticOperation arithmeticOperation) {
		this.equationDTO = equationDTO;
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
		if(equationDTO.getResult().equals("")) return Constants.IS_NOT_CALCULATION_OVER;   //결과값이 없음 
		return Constants.IS_CALCULATION_OVER;  //"="을 입력받았지만 이미 결과값이 있음 -> 첫번째 숫자를 현재의 결과값으로 두고 다시 계산
	}
	public void addToRecordList(ArrayList<String> recordList) {
		if(equationDTO.getOperator().equals("÷") && equationDTO.getSecondValue().equals("0")) {
			equationDTO.setResult("0으로 나눌 수 없습니다.");
		}
		else recordList.add(equationDTO.toString());
	}
	public void calculateExpression(StringBuilder numberbuilder, ArrayList<String> recordList) { 
		String number = numberbuilder.toString();
		
		if(equationDTO.getOperator() == "") {  //첫번째 숫자 입력 후 연산자 입력없이 바로 '='입력시 -> 결과 : 첫번째 입력값
			equationDTO.setFirstValue(number);
			equationDTO.setResult(equationDTO.getFirstValue());
		}
		else if(number.equals("")) {   //첫번째 숫자, 연산자 입력 후 두번째 숫자 입력 없이 바로 '='입력시 -> 결과 : 첫번째 숫자 (연산자) 첫번째 숫자
			equationDTO.setSecondValue(equationDTO.getFirstValue());
			arithmeticOperation.calculateExpression();
			numberbuilder.append(equationDTO.getFirstValue());
		}
		else if(isCalculationOver()) {            //현재 저장된 값에 대한 결과가 한번이라도 완료가 되었는지 확인(한번 이상 완료 후 '='을 또 입력하면 결과값이 달라지기 때문)
			equationDTO.setFirstValue(equationDTO.getResult());   //계산완료 후 '='버튼 클릭시 현재 결과값에 두번째 숫자를 한번 더 계산한다(첫번째 숫자가 현재의 결과값)
			arithmeticOperation.calculateExpression();   //현재까지 입력한 값 계산 후 DTO에 저장
		}
		else {
			equationDTO.setSecondValue(number); //계산완료된 적이 없음 -> 연산자 입력 후 누적된 두번째 숫자값 저장          예외) 3+=입력시 첫번째값=두번째값 -> 3+3=6
			arithmeticOperation.calculateExpression();   //현재까지 입력한 값 계산 후 DTO에 저장
		}
		
		addToRecordList(recordList);
		
		System.out.println(setNumber(equationDTO.getFirstValue()) + equationDTO.getOperator() + setNumber(equationDTO.getSecondValue()) + "=" + setNumber(equationDTO.getResult()));
	}
	
}
