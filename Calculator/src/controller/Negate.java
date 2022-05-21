package controller;

import Model.ExpressionDTO;

public class Negate {
	private ExpressionDTO expressionDTO;
	
	public Negate(ExpressionDTO expressionDTO){
		this.expressionDTO = expressionDTO;
	}
	
	private void multiplyNegativeNumber(StringBuilder numberBuilder) {
		if(numberBuilder.toString().equals("0")) return;  
		
		if(numberBuilder.toString().charAt(0) == '-') numberBuilder.replace(0, 1, "");  //현재 입력값이 음수인 경우 -> 음의 부호를 지움
		else numberBuilder.insert(0, "-");   //현재 입력값이 양수인 경우 -> 숫자 앞에 음의 부호를 붙임
	}
	
	public void manageNegate(StringBuilder numberBuilder) {
		if(numberBuilder.toString().equals("0")) return;  //0은 negative연산 X
		
		if(expressionDTO.getFirstValue().equals("") || expressionDTO.getSecondValue().equals("") && numberBuilder.toString().equals("") == false) { //첫번째값입력시 or 두번째값 입력시 -> 숫자앞에 '+' or '-'
			multiplyNegativeNumber(numberBuilder);
			return;
		}
		
		String firstValue;
		
		if(expressionDTO.getResult().equals("") == false) {   //계산완료 후 negate -> 결과값이 첫번째값이 되고 negate연산 적용(ex '2+3=5'입력 후 negate -> 'negate(5)') 
			firstValue = expressionDTO.getResult();
			expressionDTO.InitValue();
			expressionDTO.setFirstValue(String.format("negate(%s)",firstValue));
			numberBuilder.setLength(0);
			numberBuilder.append(firstValue);
			return;
		}
		
		if(expressionDTO.getSecondValue().equals("")) {
			expressionDTO.setSecondValue(String.format("negate(%s)", expressionDTO.getFirstValue()));    //두번째값 첫 입력시 'negate(첫번째값)'으로 표기
			numberBuilder.append(expressionDTO.getFirstValue());
		}
		else {
			expressionDTO.setSecondValue(String.format("negate(%s)", expressionDTO.getSecondValue()));   //두번째값 입력 중 이미 negate연산을 한 번 이상 한 경우 -> 'negate(두번째값)'으로 표기
		}
		
		multiplyNegativeNumber(numberBuilder);
	}
}
//0일 때는 negative연산 X
//'2+'입력 후 negate입력 -> '2+negate(2)'(-2) -> '2+negate(negate(2))'(2) -> ...
//'2+'입력 후 negate입력 -> '2+negate(2)'(-2) -> '3'입력 -> '2+'(3)
//'2+'입력 후 negate입력 -> '2+negate(negate(2))'(2) -> '+'입력 -> '4+'(4)

