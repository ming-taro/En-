package controller;

import Model.EquationDTO;
import utility.Constants;

public class EqualSign {
	private EquationDTO equationDTO;                //현재 입력한 값들
	private ArithmeticOperation arithmeticOperation;    //사칙연산 계산
	
	public EqualSign(EquationDTO equationDTO, ArithmeticOperation arithmeticOperation) {
		this.equationDTO = equationDTO;
		this.arithmeticOperation = arithmeticOperation;
	}
	public boolean isCalculationOver() {
		if(equationDTO.getResult().equals("")) return Constants.IS_NOT_CALCULATION_OVER;   //결과값이 없음 
		return Constants.IS_CALCULATION_OVER;  //"="을 입력받았지만 이미 결과값이 있음 -> 첫번째 숫자를 현재의 결과값으로 두고 다시 계산
	}
	public void calculateExpression(StringBuilder numberbuilder) { 
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

		//equationDTO.setExpression();   //완성된 수학식 DTO에 저장

		System.out.println(equationDTO.getFirstValue() + equationDTO.getOperator() + equationDTO.getSecondValue() + "=" + equationDTO.getResult());
	}
	
}
