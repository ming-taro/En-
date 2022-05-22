package controller;

import Model.ExpressionDTO;
import utility.Constants;

public class Negate {
	private ExpressionDTO expressionDTO;
	private ExpressionCheck expressionCheck;
	
	public Negate(ExpressionDTO expressionDTO, ExpressionCheck expressionCheck){
		this.expressionDTO = expressionDTO;
		this.expressionCheck = expressionCheck;
	}
	private void multiplyNegativeNumber(StringBuilder numberBuilder) {
		if(numberBuilder.toString().equals("0")) return;  
		
		if(numberBuilder.toString().charAt(0) == '-') numberBuilder.replace(0, 1, "");  //현재 입력값이 음수인 경우 -> 음의 부호를 지움
		else numberBuilder.insert(0, "-");   //현재 입력값이 양수인 경우 -> 숫자 앞에 음의 부호를 붙임
	}
	private void setNegateToFirstValue(StringBuilder numberBuilder) {
		String result = expressionDTO.getResult();
		
		expressionDTO.InitValue();
		expressionDTO.setFirstValue(String.format("negate(%s)", result));
		numberBuilder.setLength(0);
		numberBuilder.append(result);
		multiplyNegativeNumber(numberBuilder);
	}
	public void manageNegate(StringBuilder numberBuilder) {
		
		if(expressionCheck.isCalculationOver()) {     //계산이 끝난 후 negate -> negate(결과값)을 첫번째 값으로 저장하면서 새로운 계산 시작
			setNegateToFirstValue(numberBuilder);
			return;
		}
		else if(numberBuilder.length() == 0) {
			expressionDTO.setSecondValue(String.format("negate(%s)", expressionDTO.getFirstValue()));    //두번째값 첫 입력시 'negate(첫번째값)'으로 표기
			numberBuilder.append(expressionDTO.getFirstValue());
			multiplyNegativeNumber(numberBuilder);
			return;
		}
		
		if(expressionDTO.getFirstValue().equals("") || expressionCheck.isThereOperator() && expressionDTO.getSecondValue().equals("")) {      //첫번째값입력시 or 두번째값 입력시 -> 숫자앞에 '+' or '-'
			multiplyNegativeNumber(numberBuilder);
			System.out.println("<3>");
			return;
		}
		
		if(expressionDTO.getFirstValue().indexOf("negate") != -1) {
			expressionDTO.setFirstValue(String.format("negate(%s)", expressionDTO.getFirstValue()));     //첫번째값 입력 중 이미 negate연산을 한 번 이상 한 경우 -> 'negate(첫번째값)'으로 표기
		}
		if(expressionDTO.getSecondValue().indexOf("negate") != -1) {
			expressionDTO.setSecondValue(String.format("negate(%s)", expressionDTO.getSecondValue()));   //두번째값 입력 중 이미 negate연산을 한 번 이상 한 경우 -> 'negate(두번째값)'으로 표기
		}
		
		multiplyNegativeNumber(numberBuilder);
	}
}
//0일 때는 negative연산 X
//'2+'입력 후 negate입력 -> '2+negate(2)'(-2) -> '2+negate(negate(2))'(2) -> ...
//'2+'입력 후 negate입력 -> '2+negate(2)'(-2) -> '3'입력 -> '2+'(3)
//'2+'입력 후 negate입력 -> '2+negate(negate(2))'(2) -> '+'입력 -> '4+'(4)

