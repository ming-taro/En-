package controller;

import java.math.BigDecimal;
import java.math.RoundingMode;

import Model.ExpressionDTO;
import utility.Constants;

public class FormatOfExpression {
	private ExpressionDTO expressionDTO;
	private ExpressionCheck expressionCheck;
	
	public FormatOfExpression(ExpressionDTO expressionDTO, ExpressionCheck expressionCheck) {
		this.expressionDTO = expressionDTO;
		this.expressionCheck = expressionCheck;
	}
	public void setExpressionDTO(ExpressionDTO expressionDTO) {
		this.expressionDTO = expressionDTO;
	}
	public String removeZeroAfterValue(String number) {
		StringBuilder numberBuilder = new StringBuilder();
		numberBuilder.append(number);
		
		if(number.indexOf("E") != -1) return number;
		if(number.indexOf(".") == -1) return number;
		
		for(int index = numberBuilder.length() - 1; index >= 0; index--) {
			if(numberBuilder.charAt(index) == '0') numberBuilder.setCharAt(index, ' ');
			else if(numberBuilder.charAt(index) == '.'){
				numberBuilder.setCharAt(index, ' ');
				break;
			}
			else break;
		}
		
		return numberBuilder.toString().trim();
	}
	public String setNumber(String numberToChange) { 
		if(expressionCheck.isDividedByZero() || expressionCheck.isStackOverflow()) return expressionDTO.getFirstValue();
		if(expressionCheck.isNegateOperation(numberToChange)) return numberToChange;
		if(expressionCheck.isStackOverflow()) return expressionDTO.getResult();
		if(numberToChange.equals("")) return "";
		numberToChange = removeZeroAfterValue(numberToChange);

		BigDecimal number = new BigDecimal(numberToChange);
		
		if(number.remainder(new BigDecimal(1)).compareTo(new BigDecimal(0)) == 0) {     //결과값이 정수인 경우
			if(number.toString().length() > 16) return String.format("%.15e", number);  //17자리 이상 -> 지수표현방식
			return numberToChange;                 //16자리 이하 -> 정수 그대로 출력
		}
		if(numberToChange.indexOf("E") == -1 && number.toString().length() <= 17) {
			System.out.println("아");
			return String.format("%.15f", number); //결과값이 실수이면서 숫자의 최대길이인 16(소수점 포함 17)을 초과하지 않은 경우 -> 결과 그대로 저장
		}
		
		int cnt = 0;
		for(int index = 0; index <= 4; index++) {
			if(index == 1) continue;
			if(numberToChange.charAt(index) == '0') cnt++;
		}
		
		if(numberToChange.indexOf("E") == -1 && cnt < 4) {
			String[] numberArray = number.toString().split("\\.");   //소수점 기준 -> 정수부분과 소수부분으로 나눔
			int length = 16 - numberArray[0].length();
			BigDecimal result = number.setScale(length, RoundingMode.HALF_UP);
			return result.toString();
		}
		
		String firstValue = Double.toString(Double.parseDouble(numberToChange));  //지수표현식으로 바꿈
		return firstValue.replace("E", "e");    
		
		//String secondValue = numberToChange.substring(numberToChange.indexOf("E")); //원본값이 이미 지수표현식으로 되어있었다면 원본의 'E-n'표현부분을 뒤에 붙여줌
		//return (firstValue + secondValue).replace("E", "e");
	}
	public String formatNumber(String number) {
		if(expressionCheck.isDividedByZero() || expressionCheck.isStackOverflow()) return expressionDTO.getResult();
		
		if(number.indexOf(".") == -1) return number.replaceAll(Constants.THOUSAND_SEPARATOR_REGEX, ","); //정수 -> 1000단위 콤마만 표시

		String[] numberArray = number.split("\\.");             //소수점을 입력함(소수점 기준으로 문자열 분리(numberArray[0] : 정수부분 / numberArray[1] : 소수부분))
		number = numberArray[0].replaceAll(Constants.THOUSAND_SEPARATOR_REGEX, ",") + ".";  //정수 부분만 콤마 표시
		
		if(numberArray.length == 2) number += numberArray[1];   //소수점 입력 후 숫자를 1개 이상 입력한 경우 -> 소수점 뒤에 소수부분을 붙임
		return number;
	}
	public String getExpression() {   //입력중인 숫자 위에 출력할 계산식
		String firstValue = expressionDTO.getFirstValue();     //첫 번째 입력값
		String secondeValue = expressionDTO.getSecondValue();  //두 번째 입력값
		
		if(firstValue.equals("") || expressionCheck.isNegateOperation(firstValue)) {
			return firstValue;
		}
		if(expressionCheck.isStackOverflow()) {
			return "";   //첫번째값 입력중 -> 아직 작성된 계산식 없음(빈 문자열 리턴)
		}
		if(expressionDTO.getOperator().equals("")) {
			return setNumber(expressionDTO.getFirstValue()) + " = ";  //첫번째값 입력 후 '=' 입력 -> 계산식 : (첫번째값) (=)
		}
		if(secondeValue.equals("")) {
			return setNumber(firstValue) + expressionDTO.getOperator();              //첫번째값 입력 후 연산자를 입력함 -> 계산식 : (첫번째값) (연산자) 
		}
		if(expressionCheck.isCalculationOver()) {
			return setNumber(expressionDTO.getFirstValue()) + " " + expressionDTO.getOperator() + " " + setNumber(expressionDTO.getSecondValue()) + " = "; //모든 값 입력 완료 -> 계산식 : (첫번째값) (연산자) (두번째값)
		}
		return setNumber(expressionDTO.getFirstValue()) + " " + expressionDTO.getOperator() + " " + setNumber(expressionDTO.getSecondValue()); //두번째값으로 negate입력시
	}
}
