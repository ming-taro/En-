package controller;

import operationmanagement.ExpressionDTO;

public class EqualSign {
	private ExpressionDTO expressionDTO;
	private ArithmeticOperation arithmeticOperation;
	
	public EqualSign(ExpressionDTO expressionDTO) {
		this.expressionDTO = expressionDTO;
		arithmeticOperation = new ArithmeticOperation();
	}
	
	public String calculateExpression(String expression, StringBuilder numberBuilder) {
		expressionDTO.setSecondValue(numberBuilder.toString());   //연산자 입력 후 누적된 두번째 숫자값 저장
		expression += expressionDTO.getSecondValue() + "＝";       //완성된 계산식
		arithmeticOperation.calculateExpression(expressionDTO);   //현재까지 입력한 값 계산 후 DTO에 저장
		numberBuilder.setLength(0);                               //
		numberBuilder.append(expressionDTO.getResult());
		
		System.out.println(expressionDTO.getFirstValue() + expressionDTO.getOperator() + expressionDTO.getSecondValue() + "=" + expressionDTO.getResult());
		
		return expression;
	}
	
}
