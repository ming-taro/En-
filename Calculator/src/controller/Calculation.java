package controller;
import java.math.BigDecimal;
import java.util.ArrayList;
import Model.ExpressionDTO;
import utility.Constants;

public class Calculation {
	private ExpressionDTO expressionDTO;                //현재 입력한 값들
	private ArithmeticOperation arithmeticOperation;    //사칙연산 계산
	private ExpressionCheck expressionCheck;
	private FormatOfExpression formatOfExpression;
	
	public Calculation(ExpressionDTO expressionDTO, ArithmeticOperation arithmeticOperation, ExpressionCheck expressionCheck, FormatOfExpression formatOfExpression) {
		this.expressionDTO = expressionDTO;
		this.arithmeticOperation = arithmeticOperation;
		this.expressionCheck = expressionCheck;
		this.formatOfExpression = formatOfExpression;
	}
	private void checkStackOverflow() {
		String result = expressionDTO.getResult();
		int beginIndex;
		
		if(result.equals("") || expressionCheck.isDividedByZero()) return;
		
		result = String.format("%.15e", new BigDecimal(result));
		beginIndex = result.indexOf("e") + 2;
		
		if(Integer.parseInt(result.substring(beginIndex)) >= 10000) {
			expressionDTO.setResult("오버플로");
		}
	}
	public void setFirstValue(String number) {
		if(expressionCheck.isNegateOperation(expressionDTO.getFirstValue())) return;
		expressionDTO.setFirstValue(formatOfExpression.removeZeroAfterValue(number));
	}
	public void setSecondValue(String number) {
		if(expressionCheck.isNegateOperation(expressionDTO.getSecondValue())) return;
		expressionDTO.setSecondValue(formatOfExpression.removeZeroAfterValue(number));
	}
	public void removeZeroAfterValue(StringBuilder numberBuilder) {
		for(int index = numberBuilder.length() - 1; index >= 0; index--) {
			if(numberBuilder.charAt(index) == '0') numberBuilder.setCharAt(index, ' ');
			else break;
		}
	}
	public void addToRecordList(ArrayList<ExpressionDTO> recordList) {
		if(expressionDTO.getOperator().equals("÷") && expressionDTO.getSecondValue().equals("")) {
			expressionDTO.setResult("0으로 나눌 수 없습니다.");
			return;
		}
		
		if(expressionCheck.isStackOverflow()) return;
		
		recordList.add(new ExpressionDTO(expressionDTO.getFirstValue(), expressionDTO.getOperator(), expressionDTO.getSecondValue(), expressionDTO.getResult()));
	}
	public void calculateExpression(String number, ArrayList<ExpressionDTO> recordList) { 

		if(expressionCheck.isCalculationOver()) {                     //이전 계산값이 남아있음(ex '2+3='입력 후 '5'라는 결과값이 남아있는 상태에서 '='을 입력함)
			expressionDTO.setFirstValue(expressionDTO.getResult());   //현재 결과값에 두번째 숫자를 한번 더 계산한다(첫번째 숫자가 현재의 결과값)
		}
		else if(expressionCheck.isThereSecondValue(number)) {         //두번째 숫자 입력중 '='을 입력함(ex '2+3'입력 후 '='을 입력함)
			setSecondValue(number);                      //누적된 두번째 숫자값 저장
		}
		else if(expressionCheck.isThereOperator()) {                     //연산자입력 후 '='을 입력함(ex '2+'입력 후 '='을 입력함)
			expressionDTO.setSecondValue(expressionDTO.getFirstValue());  
		}
		else {                                           //숫자입력 후 '='을 입력함(ex '2'입력 후 '='을 입력함)
			expressionDTO.setFirstValue(formatOfExpression.removeZeroAfterValue(number)); 
		}
		
		arithmeticOperation.calculateExpression();        //현재까지 입력한 값 계산 후 DTO에 저장
		
		checkStackOverflow();
		addToRecordList(recordList);
	}
}
