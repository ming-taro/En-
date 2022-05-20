package controller;

import Model.ExpressionDTO;

public class Negate {
	private ExpressionDTO expressionDTO;
	
	public Negate(ExpressionDTO expressionDTO){
		this.expressionDTO = expressionDTO;
	}
	
	public void multiplyNegativeNumber(StringBuilder numberBuilder) {
		if(numberBuilder.toString().equals("0")) return;
		
		if(numberBuilder.toString().charAt(0) == '-') numberBuilder.replace(0, 1, "");  //현재 입력값이 음수인 경우 -> 음의 부호를 지움
		else numberBuilder.insert(0, "-");   //현재 입력값이 양수인 경우 -> 숫자 앞에 음의 부호를 붙임
	}
	
	public void manageNegate(StringBuilder numberBuilder) {
		//0일 때는 negative연산 X
		//'2+'입력 후 negate입력 -> '2+negate(2)'(-2) -> '2+negate(negate(2))'(2) -> ...
		//'2+'입력 후 negate입력 -> '2+negate(2)'(-2) -> '3'입력 -> '2+'(3)
		//'2+'입력 후 negate입력 -> '2+negate(negate(2))'(2) -> '+'입력 -> '4+'(4)
		
		if(numberBuilder.toString().equals("0")) return;  //0은 negative연산 X
		
		if(expressionDTO.getFirstValue().equals("") || expressionDTO.getSecondValue().equals("") && numberBuilder.toString().equals("") == false) { //첫번째값입력시 or 두번째값 입력시 -> 숫자앞에 '+' or '-'
			multiplyNegativeNumber(numberBuilder);
			return;
		}
		
		if(expressionDTO.getSecondValue().equals("")) {
			expressionDTO.setSecondValue("negate(" + expressionDTO.getFirstValue() + ")");
			numberBuilder.append(expressionDTO.getFirstValue());
		}
		else {

			System.out.print("오");
			expressionDTO.setSecondValue("negate(" + expressionDTO.getSecondValue() + ")");
		}
		
		multiplyNegativeNumber(numberBuilder);
	}
}
