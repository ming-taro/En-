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
	private String expression;        //입력한 계산식 누적
	private StringBuilder numberBuilder;            //숫자 입력값 누적
	private EqualSign equalSign;
	
	public ExpressionCalculation() {
		expressionPanel = new ExpressionPanel();                     //계산식 출력 패널
		calculationButtonPanel = new CalculationButtonPanel(this);   //버튼 클릭 패널
		CalculatorFrame calculatorFrame =  new CalculatorFrame(expressionPanel, calculationButtonPanel);
		expressionDTO = new ExpressionDTO();
		numberBuilder = new StringBuilder();
		equalSign = new EqualSign(expressionDTO);
	}
	public void setNumber(String number) {         //숫자입력
		if(numberBuilder.length() == 16 || numberBuilder.length() == 0 && number.equals("0") ) return;  //숫자 16자리 초과 or 처음부터 0을 입력하는 경우 -> 입력값을 더이상 누적하지 않음
	
		numberBuilder.append(number);   //숫자 입력값 누적
	}
	public void setExpression(String operator) {  //연산자 입력
		expressionDTO.setFirstValue(numberBuilder.toString());   //연산자 이전에 입력한 첫번째 값 저장
		expressionDTO.setOperator(operator.charAt(0));           //연산자 저장
		expression = expressionDTO.getFirstValue()+expressionDTO.getOperator();
		numberBuilder.setLength(0);                              //숫가입력값 누적 초기화
	}
	public void setZero(String buttonClicked) {//CE: 현재 숫자 입력값만 삭제, C: 입력값, 수식 누적값 삭제
		numberBuilder.setLength(0);            //누적된 입력값 삭제
		
		if(buttonClicked.equals("CE") && expressionDTO.getResult().equals("") == Constants.IS_CALCULATION_OVER || buttonClicked.equals("C")) {  //계산결과 후 CE or C클릭 
			expression = "";  
			expressionDTO.InitValue();         //계산식, DTO 저장값 초기화
		}
	}

	@Override
	public void actionPerformed(ActionEvent event) {
		JButton button = (JButton) event.getSource();
		String buttonClicked = button.getText();

		switch(buttonClicked.charAt(0)) {
		case 'C':
			setZero(buttonClicked);
			expressionPanel.setExpressionLabel(expression, "0");   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
			break;
		case '=':
			expression = equalSign.calculateExpression(expression, numberBuilder);
			expressionPanel.setExpressionLabel(expression, expressionDTO.getResult());
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
