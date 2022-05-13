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
	private StringBuilder expressionBuilder;        //입력한 계산식 누적
	private StringBuilder numberBuilder;            //숫자 입력값 누적
	private ArithmeticOperation arithmeticOperation;
	
	public ExpressionCalculation() {
		expressionPanel = new ExpressionPanel();                     //계산식 출력 패널
		calculationButtonPanel = new CalculationButtonPanel(this);   //버튼 클릭 패널
		CalculatorFrame calculatorFrame =  new CalculatorFrame(expressionPanel, calculationButtonPanel);
		expressionDTO = new ExpressionDTO();
		expressionBuilder = new StringBuilder();
		numberBuilder = new StringBuilder();
		arithmeticOperation = new ArithmeticOperation();
	}
	public void setNumber(String number) {
		if(numberBuilder.length() == 16 || numberBuilder.length() == 0 && number.equals("0") ) return;  //숫자 16자리 초과 or 처음부터 0을 입력하는 경우 -> 입력값을 더이상 누적하지 않음
		
		numberBuilder.append(number);   //숫자 입력값 누적
	}
	public void setExpression(String operator) {
		expressionDTO.setFirstValue(numberBuilder.toString());   //연산자 이전에 입력한 첫번째 값 저장
		expressionDTO.setOperator(operator.charAt(0));           //연산자 저장
		expressionBuilder.append(expressionDTO.getFirstValue()).append(expressionDTO.getOperator());
		numberBuilder.setLength(0);                              //숫자 누적값 초기화
	}
	public void setZero(String buttonClicked) {
		if(buttonClicked.equals("CE")) {      //CE : 현재 숫자 입력값만 삭제
			numberBuilder.setLength(0);
		}
		else {                                //C  : 입력값, 수식 누적값 삭제			 
			expressionBuilder.setLength(0);
			numberBuilder.setLength(0);
			expressionDTO.InitValue();       
		}
	}
	public void calculateExpression() {
		//if(expressionDTO.getResult().equals("") == Constants.IS_CALCULATION_OVER) return;  //이미 계산값 출력 후 '='을 또 선택하면 입력값 무시 
		
		expressionDTO.setSecondValue(numberBuilder.toString());   //연산자 입력 후 누적된 두번째 숫자값 저장
		expressionBuilder.append(expressionDTO.getSecondValue() + "＝");
		arithmeticOperation.calculateExpression(expressionDTO);   //현재까지 입력한 값 계산
		numberBuilder.setLength(0);
		numberBuilder.append(expressionDTO.getResult());
		//System.out.println(expressionDTO.getResult());
	}
	
	@Override
	public void actionPerformed(ActionEvent event) {
		JButton button = (JButton) event.getSource();
		String buttonClicked = button.getText();

		if(buttonClicked.charAt(0) >= '0' && buttonClicked.charAt(0) <= '9') {
			setNumber(buttonClicked);
		}
		else if(buttonClicked.charAt(0) == 'C') {   
			setZero(buttonClicked);
		}
		else if(buttonClicked.equals("＝")) {
			calculateExpression();
		}
		else{  
			setExpression(buttonClicked);     //+, -, *, /
		}
		
		expressionPanel.setExpressionLabel(expressionBuilder, numberBuilder);   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
	}
}
