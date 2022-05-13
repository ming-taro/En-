package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;

import operationmanagement.Constants;
import operationmanagement.ExpressionDTO;
import view.CalculationButtonPanel;
import view.CalculatorFrame;
import view.ExpressionPanel;

public class ExpressionCalculation implements ActionListener{
	private ExpressionPanel expressionPanel;
	private CalculationButtonPanel calculationButtonPanel;
	private ExpressionDTO expressionDTO;
	private String expression;                      //입력한 계산식 누적
	private StringBuilder numberBuilder;            //숫자 입력값 누적
	private EqualSign equalSign;            //'=' -> 등호계산
	private NumberDeletion numberDeletion;  //'C', 'CE', '←' -> 숫자 or 계산식 삭제
	
	public ExpressionCalculation() {
		expressionPanel = new ExpressionPanel();                     //계산식 출력 패널
		calculationButtonPanel = new CalculationButtonPanel(this);   //버튼 클릭 패널
		CalculatorFrame calculatorFrame =  new CalculatorFrame(expressionPanel, calculationButtonPanel);
		expressionDTO = new ExpressionDTO();
		numberBuilder = new StringBuilder();
		numberBuilder.append("0");
		equalSign = new EqualSign(expressionDTO);
		numberDeletion = new NumberDeletion(expressionDTO);
	}
	public boolean isFirstInputForSecondNumber() {
		if(expressionDTO.getOperator().equals("") || numberBuilder.toString().equals("0") == Constants.IS_VALUE) {  //연산자가 없거나(아직 첫번째값 입력중) 연산자 입력 후 두번째값을 이미 입력중인 경우 
			return Constants.IS_NOT_FIRST_INPUT; 
		}
		return Constants.IS_FIRST_INPUT;
	}
	public void setNumber(String number) {         //숫자입력
		if(numberBuilder.length() == 16 || numberBuilder.toString().equals("0") && number.equals("0")) return;  //숫자 16자리 초과 or 처음부터 0을 입력하는 경우 -> 입력값을 더이상 누적하지 않음
		
		//처음 입력시 0이 아닌 숫자를 입력할 경우  or 연산자 입력 후 처음 숫자를 입력할 때(두번째 숫자입력 시작) -> stringBuilder를 비우고 입력값을 누적해나감
		if(numberBuilder.toString().equals("0") || isFirstInputForSecondNumber()) numberBuilder.setLength(0);   
		numberBuilder.append(number);   //숫자 입력값 누적
	}
	public void setExpression(String operator) {  //연산자 입력
		expressionDTO.setFirstValue(numberBuilder.toString());   //연산자 이전에 입력한 첫번째 값 저장
		expressionDTO.setOperator(operator);                     //연산자 저장
		numberBuilder.setLength(0);                              //숫자 누적값 초기화
		numberBuilder.append("0");
		expression = expressionDTO.getFirstValue() + expressionDTO.getOperator();
	}
	
	@Override
	public void actionPerformed(ActionEvent event) {
		JButton button = (JButton) event.getSource();
		String buttonClicked = button.getText();

		switch(buttonClicked.charAt(0)) {
		case 'C':
			expression = numberDeletion.manageDeletion(numberBuilder, buttonClicked, expression);
			expressionPanel.setExpressionLabel(expression, "0");   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
			break;
		case '←':
			numberDeletion.manageBackSpace(numberBuilder);
			expressionPanel.setExpressionLabel(expression, numberBuilder.toString());
			break;
		case '=':
			equalSign.calculateExpression(numberBuilder.toString());  //등호 계산
			expressionPanel.setExpressionLabel(expressionDTO.getExpression(), expressionDTO.getResult());  //완성된 계산식과 계산결과값 출력
			break;
		case '+':case '-':case '×':case '÷':
			setExpression(buttonClicked);   
			expressionPanel.setExpressionLabel(expression, expressionDTO.getFirstValue());   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
			break;
		case '.':case '±':
			break;
		default:    //숫자입력
			setNumber(buttonClicked);
			expressionPanel.setExpressionLabel(expression, numberBuilder.toString());   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
		}
	}
}
