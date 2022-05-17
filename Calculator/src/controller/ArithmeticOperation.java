package controller;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayList;

import Model.EquationDTO;
import utility.Constants;

public class ArithmeticOperation {
	private EquationDTO equationDTO;
	
	public ArithmeticOperation(EquationDTO equationDTO) {
		this.equationDTO = equationDTO;
	}
	public String setResult(BigDecimal number) {
		String numberString = number.toString();
		
		if(number.remainder(new BigDecimal(1)).compareTo(new BigDecimal(0)) == 0) { //결과값이 정수인 경우
			if(number.toString().length() > 16) return String.format("%.15e", number);  //16자리 초과 -> 지수표현식
			return Long.toString(number.longValue());   
		}
		else if (numberString.substring(numberString.indexOf(".") + 1).equals("999999999999999")) return Integer.toString((Integer.parseInt(numberString.substring(0, numberString.indexOf("."))) + 1)); //결과값이 실수이면서 숫자의 최대길이인 16(소수점 포함 17)을 초과
		else if (number.toString().length() <= 17) return number.toString(); //결과값이 실수이면서 숫자의 최대길이인 16(소수점 포함 17)을 초과하지 않은 경우 -> 결과 그대로 저장
		else return String.format("%.15e", number); //결과값이 실수이면서 숫자의 최대길이인 16(소수점 포함 17)을 초과
	}
	public void calculateExpression() {
		BigDecimal firstValue = new BigDecimal(equationDTO.getFirstValue());      //첫번째 입력값
		BigDecimal secondValue = new BigDecimal(equationDTO.getSecondValue());    //두번째 입력값
		BigDecimal result;
		
		switch(equationDTO.getOperator()) {    //연산자에 따라 계산
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
			result = firstValue.divide(secondValue, 16, RoundingMode.HALF_EVEN);
			break;
		default:
			result = new BigDecimal("");
			break;
		}
		
		equationDTO.setResult(setResult(result));
	}
	public void setExpression(StringBuilder numberBuilder, String firstValue, String operator) {  //연산자 입력
		equationDTO.setFirstValue(firstValue);   //연산자 이전에 입력한 첫번째 값 저장
		equationDTO.setOperator(operator);                     //연산자 저장
		numberBuilder.setLength(0);                              //숫자 누적값 초기화
		numberBuilder.append("");                                //두번째 숫자 입력부터는 stringBuilder값을 0으로 초기화하지 않고 비움 -> ex)9+=입력값과 9+0=입력값을 다르게 구별하기 위함
	}
	public boolean isAlreadyEnteredOperator() {     //이미 연산자를 입력했는지 확인
		if(equationDTO.getOperator().equals("")) return Constants.IS_NOT_ENTERED_OPERATOR;
		return Constants.IS_ALREADY_ENTERED_OPERATOR;
	}
	public boolean isCalculationOver() {
		if(equationDTO.getResult().equals("")) return Constants.IS_NOT_CALCULATION_OVER; //계산이 아직 끝나지 않은 경우(현재 저장된 계산결과값이 없음)
		return Constants.IS_CALCULATION_OVER;  //이전 계산결과값이 남아있음
	}
	public String manageArithmeticOperation(StringBuilder numberBuilder, String operator, ArrayList<String> recordList) {
		String fristValue;
		
		if(isCalculationOver()) {    //계산완료 후 연산자 입력시 -> 현재 계산결과값이 첫번째값이 되고, 연산자도 바꿈
			fristValue = equationDTO.getResult();
			equationDTO.InitValue();                    //이전 계산결과값이 저장되어있는 DTO정보 초기화
			setExpression(numberBuilder, fristValue, operator); 
		}
		else if(isAlreadyEnteredOperator() && numberBuilder.toString().equals("")) {  //첫번째값, 연산자까지 입력 후 연산자 다시 입력 -> 연산자만 변경
			equationDTO.setOperator(operator);
		}
		else if (isAlreadyEnteredOperator()){    //첫번째값, 연산자, 두번째값까지 입력했지만 '='이 아닌 연산자 입력 -> 첫번째값이 연산결과값, 연산자 그대로, 두번째값 초기화
			equationDTO.setSecondValue(numberBuilder.toString());   //현재까지 입력한 두번째 값 DTO에 저장
			calculateExpression();                                  //현재까지 입력값 계산 후 DTO에 저장
			recordList.add(equationDTO.toString());                 //현재까지 완성된 계산식 계산기록리스트에 저장
			fristValue = equationDTO.getResult();                   //첫번째값 -> 현재 계산식의 계산결과
			equationDTO.InitValue();
			setExpression(numberBuilder, fristValue, operator);
		}
		else {   
			setExpression(numberBuilder, numberBuilder.toString(), operator);   //첫번째 값 입력 후 연산자 입력시 -> 연산자 정보 저장
		}

		return equationDTO.getFirstValue() + equationDTO.getOperator();  //연산자 입력 후 계산식 반환
	}
}
