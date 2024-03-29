package controller;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayList;

import Model.ExpressionDTO;
import utility.Constants;

public class ArithmeticOperation {
	private ExpressionDTO expressionDTO;
	private ExpressionCheck expressionCheck;
	private FormatOfExpression formatOfExpression;
	
	public ArithmeticOperation(ExpressionDTO expressionDTO, ExpressionCheck expressionCheck, FormatOfExpression formatOfExpression) {
		this.expressionDTO = expressionDTO;
		this.expressionCheck = expressionCheck;
		this.formatOfExpression = formatOfExpression;
	}
	private String divideNumber(BigDecimal firstValue, BigDecimal secondValue) {
		if(firstValue.doubleValue() == 0 && secondValue.doubleValue() == 0) {
			expressionDTO.setSecondValue("");
			return "정의되지 않은 결과입니다.";
		}
		if(secondValue.doubleValue() == 0) {
			expressionDTO.setSecondValue("");
			return "0으로 나눌 수 없습니다.";
		}
		
		return firstValue.divide(secondValue, 1000, RoundingMode.HALF_UP).toString();
	}
	private String multiplyNegativeNumber(String number) {
		StringBuilder numberBuilder = new StringBuilder().append(number);
		
		if(numberBuilder.toString().equals("0")) return "0";  
		
		if(numberBuilder.toString().charAt(0) == '-') numberBuilder.replace(0, 1, "");  //현재 입력값이 음수인 경우 -> 음의 부호를 지움
		else numberBuilder.insert(0, "-");   //현재 입력값이 양수인 경우 -> 숫자 앞에 음의 부호를 붙임
		
		return numberBuilder.toString();
	}
	private String setNegateOperation(String value) {
		int numberOfNegateOperation = value.length() - value.replace("(", "").length();    //negate연산 횟수
		int beginIndex = value.lastIndexOf("(") + 1;
		int endIndex = value.indexOf(")");
		
		if(numberOfNegateOperation%2 == 0) value = value.substring(beginIndex, endIndex);  //연산횟수가 짝수 -> '+'
		else value = multiplyNegativeNumber(value.substring(beginIndex, endIndex));                          //연산횟수가 홀수 -> '-'
		
		return value;
	}
	private BigDecimal getValue(String value) {  
		if(value.equals("")) return new BigDecimal("0");   //'2='계산시 secondValue가 없는 상황에서 bigDecimal에대한 nullPointer오류가 나기 때문에 기본값'0'으로 초기화
		
		if(expressionCheck.isNegateOperation(value)) value = setNegateOperation(value);   //negate연산입력이 있었다면 괄호를 풀고 적절한 부호를 붙임
			
		return new BigDecimal(value);
	}
	public void calculateExpression() {
		BigDecimal firstValue = getValue(expressionDTO.getFirstValue());      //첫번째 입력값
		BigDecimal secondValue = getValue(expressionDTO.getSecondValue());    //두번째 입력값
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
	public void addToRecordList(ArrayList<ExpressionDTO> recordList) {
		if(expressionCheck.isDividedByZero() || expressionCheck.isResultUndefined()) return;
		if(expressionCheck.isStackOverflow()) return;
		
		if(recordList.size() == 20) recordList.remove(0);
		recordList.add(new ExpressionDTO(expressionDTO.getFirstValue(), expressionDTO.getOperator(), expressionDTO.getSecondValue(), expressionDTO.getResult()));
	}
	private void setExpression(StringBuilder numberBuilder, String firstValue, String operator) {  //연산자 입력
		expressionDTO.InitValue();                    //이전 계산결과값이 저장되어있는 DTO정보 초기화
		expressionDTO.setFirstValue(formatOfExpression.removeZeroAfterValue(firstValue));  //첫번째 값 저장
		expressionDTO.setOperator(operator);          //연산자 저장
		numberBuilder.setLength(0);                   //숫자 누적값 초기화
		numberBuilder.append("");                     
	}	
	
	public void manageArithmeticOperation(StringBuilder numberBuilder, String operator, ArrayList<ExpressionDTO> recordList) {
		String number = numberBuilder.toString();
		String firstValue = number;         //첫 연산자 입력시 첫번째값은 현재까지 numberBuilder에 누적된 값

		if(expressionCheck.isOperatorChanged(number)) {          //연산자 변경 발생(ex '2-'입력 후 '+'입력 -> '2+')
			expressionDTO.setOperator(operator);
			return;
		}
		
		if(expressionCheck.isThereSecondValue(number)) {         //첫 연산자 입력(=저장된 연산자,두번째값이 없음)시 if문을 실행하지 않고 setExpression만 실행(ex '2'입력 후 '+'입력  => '2+')
			expressionDTO.setSecondValue(formatOfExpression.removeZeroAfterValue(number));     //두번째값 입력 중 연산자 입력시  누적된 두번째값 DTO에 저장 후 계산
			calculateExpression();                               //입력된 정보로 계산 수행
		}	

		if(expressionCheck.isDividedByZero() || expressionCheck.isResultUndefined()) return;   //계산수행 중 0으로 나누거나 0을 0으로 나누려 했을 때 -> 계산 종료
		if(expressionCheck.isCalculationOver()) {
			addToRecordList(recordList);
			firstValue = expressionDTO.getResult();   //계산을 정상적으로 완료한 후 -> 계산 결과값이 첫번째 값이 됨 (ex '2+3'입력 후 '-'입력 => '5-' / ex '2+3=5'입력 후 '-'입력 => '5-')
		}
			
		setExpression(numberBuilder, firstValue, operator);                      //계산식(첫번째값,연산자) 저장 및 누적된 입력값 초기화
	}
}
