package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ComponentListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.util.ArrayList;

import javax.swing.JButton;

import Model.EquationDTO;
import utility.Constants;
import view.CalculationButtonPanel;
import view.CalculatorFrame;
import view.EquationPanel;

public class Calculation implements ActionListener, KeyListener{
	private EquationPanel expressionPanel;
	private CalculationButtonPanel calculationButtonPanel;
	private CalculatorFrame calculatorFrame;
	private EquationDTO equationDTO;
	private ArrayList<String> recordList; //계산기록 저장
	private String expression;                       //입력한 계산식 누적
	private StringBuilder numberBuilder;             //숫자 입력값 누적
	private ArithmeticOperation arithmeticOperation; //'+', '-', '×', '÷' -> 사칙연산
	private EqualSign equalSign;                     //'=' -> 등호계산
	private Deletion numberDeletion;           //'C', 'CE', '←' -> 숫자 or 계산식 삭제

	public Calculation() {
		expressionPanel = new EquationPanel();                       //계산식 출력 패널
		calculationButtonPanel = new CalculationButtonPanel(this);   //버튼 클릭 패널
		
		equationDTO = new EquationDTO();
		recordList = new ArrayList<String>();
		numberBuilder = new StringBuilder();
		numberBuilder.append("0");
		
		calculatorFrame =  new CalculatorFrame(expressionPanel, calculationButtonPanel, recordList); //프레임에 패널 부착, 계산기록 리스트 연결
		
		arithmeticOperation = new ArithmeticOperation(equationDTO);
		equalSign = new EqualSign(equationDTO, arithmeticOperation);
		numberDeletion = new Deletion(equationDTO);
		
		calculatorFrame.addKeyListener(this);
		calculatorFrame.requestFocus();
		calculatorFrame.setFocusable(true);
	}
	public ArrayList<String> getRecordList(){
		return recordList;
	}
	public boolean isFirstInputForSecondNumber() {
		if(equationDTO.getOperator().equals("") || numberBuilder.toString().equals("0") == Constants.IS_VALUE) {  //연산자가 없거나(아직 첫번째값 입력중) 연산자 입력 후 두번째값을 이미 입력중인 경우 
			return Constants.IS_NOT_FIRST_INPUT; 
		}
		return Constants.IS_FIRST_INPUT;
	}
	public boolean isCalculationOver() {  //계산식을 모두 입력하고 결과출력까지 끝났는지 확인
		if(equationDTO.getResult().equals("")) return Constants.IS_NOT_CALCULATION_OVER; 
		return Constants.IS_CALCULATION_OVER;
	}
	public void setNumber(String number) {         //숫자입력
		if(numberBuilder.length() == 16 || numberBuilder.toString().equals("0") && number.equals("0")) return;  //숫자 16자리 초과 or 처음부터 0을 입력하는 경우 -> 입력값을 더이상 누적하지 않음
		
		if(isCalculationOver()) expression = numberDeletion.manageDeletion(numberBuilder, "C", expression);  //이전 입력까지의 계산이 끝나고 새로운 숫자를 입력하려고 할 때 -> 'C'기능 수행 후 입력값을 stringBuilder에 저장
		
		//처음 입력시 0이 아닌 숫자를 입력할 경우  or 연산자 입력 후 처음 숫자를 입력할 때(두번째 숫자입력 시작) -> stringBuilder를 비우고 입력값을 누적해나감
		if(numberBuilder.toString().equals("0") || isFirstInputForSecondNumber()) numberBuilder.setLength(0);   
		numberBuilder.append(number);   //숫자 입력값 누적
	}
	public void MultiplyNumberBySign() {
		if(numberBuilder.toString().charAt(0) == '-') numberBuilder.replace(0, 1, "");  //현재 입력값이 음수인 경우 -> 음의 부호를 지움
		else numberBuilder.insert(0, "-");   //현재 입력값이 양수인 경우 -> 숫자 앞에 음의 부호를 붙임
	}
	public void AddDot() {
		if(numberBuilder.indexOf(".") != -1) return;
		numberBuilder.append(".");
	}
	public void setCalculator(String buttonText) {
		String number;
		
		switch(buttonText.charAt(0)) {
		case 'C':
			expression = numberDeletion.manageDeletion(numberBuilder, buttonText, expression);
			expressionPanel.setExpressionLabel(expression, "0");   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
			break;
		case '←':
			number = numberDeletion.manageBackSpace(numberBuilder);
			expressionPanel.setExpressionLabel(expression, number);
			break;
		case '=':
			equalSign.calculateExpression(numberBuilder);  //등호 계산
			recordList.add(equationDTO.toString());
			expressionPanel.setExpressionLabel(equationDTO.getExpression(), equationDTO.getResult());  //완성된 계산식과 계산결과값 출력
			break;
		case '+':case '-':case '×':case '÷':  //사칙연산
			expression = arithmeticOperation.manageArithmeticOperation(numberBuilder, buttonText, recordList); 
			expressionPanel.setExpressionLabel(expression, equationDTO.getFirstValue());   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
			break;
		case '.': 
			AddDot();
			expressionPanel.setExpressionLabel(expression, numberBuilder.toString());
			break;
		case '±':  
			MultiplyNumberBySign();
			expressionPanel.setExpressionLabel(expression, numberBuilder.toString());   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
			break;
		case '0':case '1':case '2':case '3':case '4':case '5':case '6':case '7':case '8':case '9':
			setNumber(buttonText);
			expressionPanel.setExpressionLabel(expression, numberBuilder.toString());   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
		}
	}
	@Override
	public void actionPerformed(ActionEvent event) {
		JButton button = (JButton) event.getSource();
		String buttonText = button.getText();
		
		System.out.println("test");
		setCalculator(buttonText);
	}
	@Override
	public void keyPressed(KeyEvent event) {
		if(calculatorFrame.getPanelNumber() == Constants.RECORD_PANEL_MODE) {
			calculatorFrame.setFocusable(false);
		}
		else {
			calculatorFrame.requestFocus();
			calculatorFrame.setFocusable(true);
			setCalculator(event.getKeyChar() + "");
		}
	}
	@Override
	public void keyTyped(KeyEvent e) {
	}
	@Override
	public void keyReleased(KeyEvent e) {
	}
}