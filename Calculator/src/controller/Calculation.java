package controller;
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
		
		if(number%1 == 0) return numberToChange;   //결과값이 정수인 경우
		return Double.toString(number); //결과값이 실수
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
		if(isThereOperator() == Constants.IS_THERE_NO_OPERATOR || secondValue.equals("")) { 
			return Constants.IS_THERE_NO_SECOND_VALUE;   
		}
		return Constants.IS_THERE_SECOND_VALUE;  //두번째 값을 입력함 -> 입력받은 연산자가 있고, 현재 입력중인 값(=두번째값)이 있다
	}
	public void addToRecordList(ArrayList<String> recordList) {
		if(expressionDTO.getOperator().equals("÷") && expressionDTO.getSecondValue().equals("")) {
			expressionDTO.setResult("0으로 나눌 수 없습니다.");
		}
		else recordList.add(expressionDTO.toString());
	}
	public void calculateExpression(String number, ArrayList<String> recordList) { 

		if(isThereCalculationResult()) {                              //이전 계산값이 남아있음(ex '2+3='입력 후 '5'라는 결과값이 남아있는 상태에서 '='을 입력함)
			expressionDTO.setFirstValue(expressionDTO.getResult());   //현재 결과값에 두번째 숫자를 한번 더 계산한다(첫번째 숫자가 현재의 결과값)
		}
		else if(isThereSecondValue(number)) {            //두번째 숫자 입력중 '='을 입력함(ex '2+3'입력 후 '='을 입력함)
			expressionDTO.setSecondValue(setNumber(number));        //누적된 두번째 숫자값 저장
		}
		else if(isThereOperator()) {                     //연산자입력 후 '='을 입력함(ex '2+'입력 후 '='을 입력함)
			expressionDTO.setSecondValue(expressionDTO.getFirstValue());  
		}
		else {                                           //숫자입력 후 '='을 입력함(ex '2'입력 후 '='을 입력함)
			expressionDTO.setFirstValue(setNumber(number)); 
		}

		arithmeticOperation.calculateExpression();        //현재까지 입력한 값 계산 후 DTO에 저장
		addToRecordList(recordList);
	}
}
