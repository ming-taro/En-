package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ComponentListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayList;

import javax.swing.JButton;

import Model.EquationDTO;
import utility.Constants;
import view.CalculatorButtonPanel;
import view.CalculatorFrame;
import view.EquationPanel;

public class Calculation implements ActionListener, KeyListener{
	private EquationPanel expressionPanel;
	private CalculatorButtonPanel calculationButtonPanel;
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
		calculationButtonPanel = new CalculatorButtonPanel(this);   //버튼 클릭 패널
		
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
	public void addNumber(String number) {         //숫자입력
		if(numberBuilder.length() == 16 || numberBuilder.indexOf(".") != -1 && numberBuilder.length() == 17 || numberBuilder.toString().equals("0") && number.equals("0")) return;  //숫자 16자리 초과 or 처음부터 0을 입력하는 경우 -> 입력값을 더이상 누적하지 않음
		
		if(isCalculationOver()) expression = numberDeletion.manageDeletion(numberBuilder, "C", expression);  //이전 입력까지의 계산이 끝나고 새로운 숫자를 입력하려고 할 때 -> 'C'기능 수행 후 입력값을 stringBuilder에 저장
		
		//처음 입력시 0이 아닌 숫자를 입력할 경우  or 연산자 입력 후 처음 숫자를 입력할 때(두번째 숫자입력 시작) -> stringBuilder를 비우고 입력값을 누적해나감
		if(numberBuilder.toString().equals("0") || isFirstInputForSecondNumber()) numberBuilder.setLength(0);   
		numberBuilder.append(number);   //숫자 입력값 누적
	}
	public void MultiplyNegativeNumber() {
		if(numberBuilder.toString().equals("0")) return;
		else if(numberBuilder.toString().charAt(0) == '-') numberBuilder.replace(0, 1, "");  //현재 입력값이 음수인 경우 -> 음의 부호를 지움
		else numberBuilder.insert(0, "-");   //현재 입력값이 양수인 경우 -> 숫자 앞에 음의 부호를 붙임
	}
	public void AddDot() {
		if(numberBuilder.indexOf(".") != -1) return;
		numberBuilder.append(".");
	}
	public String setNumber(BigDecimal number) {  //bigdecimal
		if(number.remainder(new BigDecimal(1)).compareTo(new BigDecimal(0)) == 0) {
			return Long.toString(number.longValue());   //결과값이 정수인 경우
		}
		if(number.toString().length() <= 17) {
			return number.toString(); //결과값이 실수이면서 숫자의 최대길이인 16(소수점 포함 17)을 초과하지 않은 경우 -> 결과 그대로 저장
		}
		System.out.print("[" + number + "]");
		
		String[] numberArray = number.toString().split("\\.");   //소수점 기준 -> 정수부분과 소수부분으로 나눔
		int length = 16 - numberArray[0].length();
		BigDecimal result = number.setScale(length, RoundingMode.HALF_UP);
		return result.toString();
	}
	public String FormatNumber(String numberToChange) {
		String number = setNumber(new BigDecimal(numberToChange));
		
		if(number.indexOf(".") == -1) return number.replaceAll(Constants.THOUSAND_SEPARATOR_REGEX, ","); //입력받은 

		String[] numberArray = number.split("\\.");  //소수점을 입력함
		number = numberArray[0].replaceAll(Constants.THOUSAND_SEPARATOR_REGEX, ",") + ".";
		
		if(numberArray.length == 2) number += numberArray[1];   //소수점 입력 후 숫자를 1개 이상 입력한 경우 -> 소수점 부분에는 콤마가 붙지 않는다
		return number;
	}
	public void setCalculator(String buttonText) {
		String number;

		calculatorFrame.requestFocus();
		calculatorFrame.setFocusable(true);
		
		switch(buttonText.charAt(0)) {
		case 'C':
			expression = numberDeletion.manageDeletion(numberBuilder, buttonText, expression);
			expressionPanel.setExpressionLabel(expression, "0");   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
			break;
		case '←':
			number = numberDeletion.manageBackSpace(numberBuilder);
			expressionPanel.setExpressionLabel(expression, FormatNumber(number));
			break;
		case '=':
			equalSign.calculateExpression(numberBuilder, recordList);  //등호 계산
			expressionPanel.setExpressionLabel(equationDTO.toString(), FormatNumber(equationDTO.getResult()));  //완성된 계산식과 계산결과값 출력
			break;
		case '+':case '-':case '×':case '÷':  //사칙연산
			expression = arithmeticOperation.manageArithmeticOperation(numberBuilder, buttonText, recordList); 
			expressionPanel.setExpressionLabel(expression, FormatNumber(equationDTO.getFirstValue()));   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
			break;
		case '.': 
			AddDot();
			expressionPanel.setExpressionLabel(expression, FormatNumber(numberBuilder.toString()));
			break;
		case '±':  
			MultiplyNegativeNumber();
			expressionPanel.setExpressionLabel(expression, FormatNumber(numberBuilder.toString()));   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
			break;
		case '0':case '1':case '2':case '3':case '4':case '5':case '6':case '7':case '8':case '9':
			addNumber(buttonText);
			expressionPanel.setExpressionLabel(expression, FormatNumber(numberBuilder.toString()));   //계산식 출력 패널에 누적된 계산식 갑과 입력값 출력
		}
	}
	@Override
	public void actionPerformed(ActionEvent event) {
		JButton button = (JButton) event.getSource();
		String buttonText = button.getText();
		
		setCalculator(buttonText);
	}
	@Override
	public void keyPressed(KeyEvent event) {
		String keyChar = event.getKeyChar() + "";
		
		switch(event.getKeyCode()) {
		case Constants.EQUAL_SIGN:
			keyChar = "=";      //키보드 입력 -> '='
			break;
		case Constants.BACKSPACE:
			keyChar = "←";      //키보드 입력 -> 'BackSpace'
			break;
		case Constants.DIVISION:
			keyChar = "÷";      //키보드 입력 -> '/'
			break;
		}
		if(keyChar.equals("*")) keyChar = "×";      //키보드 입력 -> '*'
		
		if(calculatorFrame.getPanelNumber() == Constants.BUTTON_PANEL_MODE) {   //계산기록을 볼 때는 키보드입력X
			setCalculator(keyChar);
		}
	}
	@Override
	public void keyTyped(KeyEvent e) {
	}
	@Override
	public void keyReleased(KeyEvent e) {
	}
}
