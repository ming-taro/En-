package controller;

import Model.EquationDTO;
import utility.Constants;

public class NumberDeletion {
	private EquationDTO equationDTO;
	public NumberDeletion(EquationDTO equationDTO) {
		this.equationDTO = equationDTO;
	}
	public boolean isCalculationOver() {
		if(equationDTO.getResult().equals("")) return Constants.IS_NOT_CALCULATION_OVER;  //결과값이 없음 -> 아직 계산이 끝나지 않음
		return Constants.IS_CALCULATION_OVER;
	}
	public String manageDeletion(StringBuilder numberBuilder, String buttonClicked, String expression) {//CE: 현재 숫자 입력값만 삭제, C: 입력값, 수식 누적값 삭제
		numberBuilder.setLength(0);            //누적된 입력값 삭제
		numberBuilder.append("0");
		
		if(buttonClicked.equals("CE") && isCalculationOver() || buttonClicked.equals("C")) {  //계산결과 후 CE or C클릭  
			equationDTO.InitValue();         //계산식, DTO 저장값 초기화
			expression = "";                   
		}
		
		return expression;
	}
	public String manageBackSpace(StringBuilder numberBuilder) {  //'←' : 방금 입력한 숫자 하나 지우기	
		if(equationDTO.getResult() != "") return equationDTO.getResult();          //결과출력 후 backspace클릭할 경우 -> 값을 지울 수 없고, 입력부분에는 현재 결과값이 그대로 보여짐
		if(numberBuilder.toString().equals("0")) return numberBuilder.toString();
		if(numberBuilder.toString().equals("")) return equationDTO.getFirstValue();  //입력값이 없는데도 지우려고 할 경우

		numberBuilder.deleteCharAt(numberBuilder.length() - 1);  //마지막에 입력한 숫자 하나 지우기
		if(numberBuilder.length() == 0) numberBuilder.append("0");//입력값을 모두 지웠다면 초기값인 0을 stringbuilder에 넣음 
		return numberBuilder.toString();
	}
}
