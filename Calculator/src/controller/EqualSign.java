package controller;

import operationmanagement.Constants;
import operationmanagement.ExpressionDTO;

public class EqualSign {
	private ExpressionDTO expressionDTO;                //현재 입력한 값들
	private ArithmeticOperation arithmeticOperation;    //사칙연산 계산
	
	public EqualSign(ExpressionDTO expressionDTO) {
		this.expressionDTO = expressionDTO;
		arithmeticOperation = new ArithmeticOperation();
	}
	
	public void calculateOnceMore() {   
		
		arithmeticOperation.calculateExpression(expressionDTO);    //수학식 계산
	}
	public boolean isCalculationOver() {
		if(expressionDTO.getResult().equals("")) return Constants.IS_NOT_CALCULATION_OVER;   //결과값이 없음 
		return Constants.IS_CALCULATION_OVER;  //"="을 입력받았지만 이미 결과값이 있음 -> 첫번째 숫자를 현재의 결과값으로 두고 다시 계산
	}
	public void calculateExpression(String number) { 
		if(expressionDTO.getOperator() == "") {  //첫번째 숫자 입력 후 연산자 입력없이 바로 '='입력시 -> 결과 : 첫번째 입력값
			expressionDTO.setFirstValue(number);
			expressionDTO.setResult(expressionDTO.getFirstValue());
		}
		else if(isCalculationOver()) {            //현재 저장된 값에 대한 결과가 한번이라도 완료가 되었는지 확인(한번 이상 완료 후 '='을 또 입력하면 결과값이 달라지기 때문)
			expressionDTO.setFirstValue(expressionDTO.getResult());   //계산완료 후 '='버튼 클릭시 현재 결과값에 두번째 숫자를 한번 더 계산한다(첫번째 숫자가 현재의 결과값)
			arithmeticOperation.calculateExpression(expressionDTO);   //현재까지 입력한 값 계산 후 DTO에 저장
		}
		else {
			expressionDTO.setSecondValue(number); //계산완료된 적이 없음 -> 연산자 입력 후 누적된 두번째 숫자값 저장          예외) 3+=입력시 첫번째값=두번째값 -> 3+3=6
			arithmeticOperation.calculateExpression(expressionDTO);   //현재까지 입력한 값 계산 후 DTO에 저장
		}

		expressionDTO.setExpression();   //완성된 수학식 DTO에 저장

		System.out.println(expressionDTO.getFirstValue() + expressionDTO.getOperator() + expressionDTO.getSecondValue() + "=" + expressionDTO.getResult());
	}
	
}
