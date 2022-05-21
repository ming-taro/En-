package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ComponentListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.text.DecimalFormat;
import java.util.ArrayList;

import javax.swing.JButton;

import Model.ExpressionDTO;
import utility.Constants;
import view.CalculatorButtonPanel;
import view.CalculatorFrame;
import view.ExpressionPanel;

public class CalculationManagement implements ActionListener, KeyListener{
	private ExpressionPanel expressionPanel;
	private CalculatorButtonPanel calculationButtonPanel;
	private CalculatorFrame calculatorFrame;
	private ExpressionDTO expressionDTO;
	private ArrayList<String> recordList; //계산기록 저장
	private StringBuilder numberBuilder;             //숫자 입력값 누적
	private ExpressionCheck expressionCheck;         //계산식 검사(ex 계산이 끝났는지, 연산자를 입력했는지 ...)
	private ArithmeticOperation arithmeticOperation; //'+', '-', '×', '÷' 
	private Calculation calculation;                 //'=' 
	private Deletion numberDeletion;                 //'C', 'CE', '←'
	private Negate negate;                           //'±'

	public CalculationManagement() {
		expressionPanel = new ExpressionPanel();                       //계산식 출력 패널
		calculationButtonPanel = new CalculatorButtonPanel(this);   //버튼 클릭 패널
		
		expressionDTO = new ExpressionDTO();
		recordList = new ArrayList<String>();
		numberBuilder = new StringBuilder();
		numberBuilder.append("0");
		expressionCheck = new ExpressionCheck(numberBuilder, expressionDTO);
		
		calculatorFrame =  new CalculatorFrame(expressionPanel, calculationButtonPanel, recordList); //프레임에 패널 부착, 계산기록 리스트 연결
		
		arithmeticOperation = new ArithmeticOperation(expressionDTO, expressionCheck);
		calculation = new Calculation(expressionDTO, arithmeticOperation, expressionCheck);
		numberDeletion = new Deletion(expressionDTO, expressionCheck);
		negate = new Negate(expressionDTO);
		
		
		calculatorFrame.addKeyListener(this);
		calculatorFrame.requestFocus();
		calculatorFrame.setFocusable(true);
	}
	public ArrayList<String> getRecordList(){
		return recordList;
	}
	private void addNumber(String number) {              //숫자입력
		if(expressionCheck.isMaximumInputRangeExceeded(number)) return;  //숫자입력범위를 넘어서는 경우 -> 더이상 숫자를 누적하지 않음
		
		if(expressionCheck.isNegateOperation(expressionDTO.getSecondValue())) setCalculator("CE");
		if(expressionCheck.isCalculationOver()) setCalculator("C");      //이전 입력까지의 계산이 끝나고 새로운 숫자를 입력하려고 할 때 -> 'C'기능 수행
		if(expressionCheck.isFirstInput()) numberBuilder.setLength(0);   //숫자를 처음 입력할 때 -> stringBuilder를 비움
		
		numberBuilder.append(number);                    //숫자 입력값 누적
	}
	private void addPoint() {
		if(expressionCheck.isCalculationOver()) setCalculator("C");     //계산완료 후 첫 입력부터 '.'입력 -> ex)2+3=5 출력 후 '.'입력 => 'C'기능 수행 후 '0.'
		else if(expressionCheck.isNegateOperation(expressionDTO.getSecondValue())) {   //negate연산중에 '.'입력시 negate연산값 초기화 -> '0.'으로 바뀜
			expressionDTO.setSecondValue("");
			numberBuilder.setLength(0);
		}
		else if(expressionCheck.isPointEntered()) return;               //계산은 아직 끝나지 않았지만 현재 입력중인 숫자가 이미 실수
			
		if(numberBuilder.length() == 0) numberBuilder.append("0"); 
		numberBuilder.append("."); 
	}
	private String setNumber(String numberToChange) { 
		if(expressionCheck.isDividedByZero() || expressionCheck.isStackOverflow()) return expressionDTO.getFirstValue();
		if(expressionCheck.isNegateOperation(numberToChange)) return numberToChange;
		if(expressionCheck.isStackOverflow()) return expressionDTO.getResult();
		
		BigDecimal number = new BigDecimal(numberToChange);
		
		if(number.remainder(new BigDecimal(1)).compareTo(new BigDecimal(0)) == 0) { //결과값이 정수인 경우
			if(number.toString().length() > 16) return String.format("%.15e", number);
			return Long.toString(number.longValue());   
		}
		if(number.toString().length() <= 17) {
			return number.toString(); //결과값이 실수이면서 숫자의 최대길이인 16(소수점 포함 17)을 초과하지 않은 경우 -> 결과 그대로 저장
		}
		
		String[] numberArray = number.toString().split("\\.");   //소수점 기준 -> 정수부분과 소수부분으로 나눔
		int length = 16 - numberArray[0].length();
		
		if(numberArray[0].equals("0") && numberArray[1].length() >= 16) return String.format("%.15e", number);
		
		BigDecimal result = number.setScale(length, RoundingMode.HALF_UP);
		return result.toString();
	}
	private String formatNumber(String number) {
		if(expressionCheck.isDividedByZero() || expressionCheck.isStackOverflow()) return expressionDTO.getResult();
		
		if(number.indexOf(".") == -1) return number.replaceAll(Constants.THOUSAND_SEPARATOR_REGEX, ","); //소수점을 입력하지 않은 경우 -> 1000단위 콤마만 표시

		String[] numberArray = number.split("\\.");             //소수점을 입력함(소수점 기준으로 문자열 분리(numberArray[0] : 정수부분 / numberArray[1] : 소수부분))
		number = numberArray[0].replaceAll(Constants.THOUSAND_SEPARATOR_REGEX, ",") + ".";  //정수 부분만 콤마 표시
		
		if(numberArray.length == 2) number += numberArray[1];   //소수점 입력 후 숫자를 1개 이상 입력한 경우 -> 소수점 뒤에 소수부분을 붙임
		return number;
	}
	private String getExpression() {   //입력중인 숫자 위에 출력할 계산식
		String firstValue = expressionDTO.getFirstValue();     //첫 번째 입력값
		String secondeValue = expressionDTO.getSecondValue();  //두 번째 입력값
		
		if(firstValue.equals("") || expressionCheck.isNegateOperation(firstValue) || expressionCheck.isStackOverflow()) return "";   //첫번째값 입력중 -> 아직 작성된 계산식 없음(빈 문자열 리턴)
		if(expressionDTO.getOperator().equals("")) return setNumber(expressionDTO.getFirstValue()) + " = ";  //첫번째값 입력 후 '=' 입력 -> 계산식 : (첫번째값) (=)
		if(secondeValue.equals("")) return setNumber(firstValue) + expressionDTO.getOperator();              //첫번째값 입력 후 연산자를 입력함 -> 계산식 : (첫번째값) (연산자) 
		if(expressionCheck.isCalculationOver()) return setNumber(expressionDTO.getFirstValue()) + " " + expressionDTO.getOperator() + " " + setNumber(expressionDTO.getSecondValue()) + " = "; //모든 값 입력 완료 -> 계산식 : (첫번째값) (연산자) (두번째값)
		return setNumber(expressionDTO.getFirstValue()) + " " + expressionDTO.getOperator() + " " + setNumber(expressionDTO.getSecondValue()); //두번째값으로 negate입력시
	}
	private void takeButtonOnCalculator(String buttonText) {
		String number = "";    //계산식 패널에 출력할 입력중인 숫자값
		
		switch(buttonText.charAt(0)) {
		case 'C':
			numberDeletion.manageDeletion(numberBuilder, buttonText);
			number = "0";  //입력값을 지움 -> 입력칸에는 항상 기본값으로 '0'표기
			break;
		case '←':
			number = numberDeletion.manageBackSpace(numberBuilder);
			break;
		case '=':
			calculation.calculateExpression(numberBuilder.toString(), recordList);  //등호 계산
			number = setNumber(expressionDTO.getResult());
			break;
		case '+':case '-':case '×':case '÷':  //사칙연산
			arithmeticOperation.manageArithmeticOperation(numberBuilder, buttonText, recordList); 
			number = setNumber(expressionDTO.getFirstValue());
			break;
		case '.': 
			addPoint();
			number = numberBuilder.toString();
			break;
		case '±':  
			negate.manageNegate(numberBuilder);
			number = numberBuilder.toString();
			break;
		case '0':case '1':case '2':case '3':case '4':case '5':case '6':case '7':case '8':case '9':
			addNumber(buttonText);
			number = numberBuilder.toString();
		}
		
		if(number != "") expressionPanel.setExpressionLabel(getExpression(), formatNumber(number));   //계산식 패널에 계산식, 입력값 출력
	}
	private void setCalculator(String buttonText) {
		calculatorFrame.requestFocus();
		calculatorFrame.setFocusable(true);   //키보드입력 활성화
		
		takeButtonOnCalculator(buttonText);   //버튼기능 수행

		if(expressionCheck.isDividedByZero() || expressionCheck.isStackOverflow()) {
			calculationButtonPanel.deactivateOperatorButton();  //'÷0'실행시 연산자버튼 비활성화
		}
		else calculationButtonPanel.activateOperatorButton();
	}
	@Override
	public void actionPerformed(ActionEvent event) {  //계산기 버튼 이벤트
		JButton button = (JButton) event.getSource();
		String buttonText = button.getText();
		
		if((expressionCheck.isDividedByZero() || expressionCheck.isStackOverflow()) && expressionCheck.isDeletionButtonPressed(buttonText)) {
			buttonText = "C";   //'÷0'실행 후 '=' or '←'클릭 == 'C'버튼
		}
		
		setCalculator(buttonText);
	}
	@Override
	public void keyPressed(KeyEvent event) {         //키보드 이벤트
		String keyChar = event.getKeyChar() + "";
		
		if(calculatorFrame.getPanelNumber() == Constants.RECORD_PANEL_MODE) return;   //계산기록을 볼 때는 키보드입력X

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
		case Constants.ESC:     //키보드 입력 -> 'Esc'(='C'기능)
			keyChar = "C";
			break;
		}
		if(keyChar.equals("*")) keyChar = "×";      //키보드 입력 -> '*'
		
		if((expressionCheck.isDividedByZero() || expressionCheck.isStackOverflow()) && expressionCheck.isDeletionButtonPressed(keyChar)) {
			keyChar = "C";   //'÷0'실행 후 '='입력 == 'C'버튼
		}
		setCalculator(keyChar);
	}
	@Override
	public void keyTyped(KeyEvent e) {
	}
	@Override
	public void keyReleased(KeyEvent e) {
	}
}
