package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayList;

import javax.swing.JButton;

import Model.ExpressionDTO;
import utility.Constants;
import view.CalculatorButtonPanel;
import view.CalculatorFrame;
import view.ExpressionPanel;
import view.RecordPanel;

public class CalculationManagement implements ActionListener, KeyListener{
	private ExpressionPanel expressionPanel;
	private CalculatorButtonPanel calculationButtonPanel;
	private RecordPanel recordPanel;
	private CalculatorFrame calculatorFrame;
	private ExpressionDTO expressionDTO;
	private ArrayList<ExpressionDTO> recordList; //계산기록 저장
	private StringBuilder numberBuilder;             //숫자 입력값 누적
	private ExpressionCheck expressionCheck;         //계산식 검사(ex 계산이 끝났는지, 연산자를 입력했는지 ...)
	private ArithmeticOperation arithmeticOperation; //'+', '-', '×', '÷' 
	private Calculation calculation;                 //'=' 
	private Deletion numberDeletion;                 //'C', 'CE', '←'
	private Negate negate;                           //'±'
	private FormatOfExpression formatOfExpression;
	
	public CalculationManagement() {
		expressionDTO = new ExpressionDTO();
		recordList = new ArrayList<ExpressionDTO>();
		numberBuilder = new StringBuilder();
		numberBuilder.append("0");
		expressionCheck = new ExpressionCheck(numberBuilder, expressionDTO);
		formatOfExpression = new FormatOfExpression(expressionDTO, expressionCheck);
		
		expressionPanel = new ExpressionPanel();                       //계산식 출력 패널
		calculationButtonPanel = new CalculatorButtonPanel(this);     //버튼 클릭 패널
		recordPanel = new RecordPanel(expressionCheck, recordList);
		calculatorFrame =  new CalculatorFrame(expressionPanel, calculationButtonPanel, recordPanel); //프레임에 패널 부착, 계산기록 리스트 연결

		arithmeticOperation = new ArithmeticOperation(expressionDTO, expressionCheck, formatOfExpression);
		calculation = new Calculation(expressionDTO, arithmeticOperation, expressionCheck, formatOfExpression);
		numberDeletion = new Deletion(expressionDTO, expressionCheck);
		negate = new Negate(expressionDTO, expressionCheck);
		
		
		calculatorFrame.addKeyListener(this);
		calculatorFrame.requestFocus();
		calculatorFrame.setFocusable(true);
	}
	public ArrayList<ExpressionDTO> getRecordList(){
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
			number = formatOfExpression.setNumber(expressionDTO.getResult());
			break;
		case '+':case '-':case '×':case '÷':  //사칙연산
			arithmeticOperation.manageArithmeticOperation(numberBuilder, buttonText, recordList); 
			number = formatOfExpression.setNumber(expressionDTO.getFirstValue());
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
		
		if(number != "") {
			expressionPanel.setExpressionLabel(formatOfExpression.getExpression(), formatOfExpression.formatNumber(number));   //계산식 패널에 계산식, 입력값 출력
		}
	}
	private void setCalculator(String buttonText) {
		calculatorFrame.requestFocus();
		calculatorFrame.setFocusable(true);   //키보드입력 활성화
		
		takeButtonOnCalculator(buttonText);   //버튼기능 수행

		if(expressionCheck.isDividedByZero() || expressionCheck.isResultUndefined() || expressionCheck.isStackOverflow()) {
			calculationButtonPanel.deactivateOperatorButton();  //'÷0'실행시 연산자버튼 비활성화
		}
		else {
			calculationButtonPanel.activateOperatorButton();
		}

		recordPanel.setRecordPanel();
	}
	@Override
	public void actionPerformed(ActionEvent event) {  //계산기 버튼 이벤트
		JButton button = (JButton) event.getSource();
		String buttonText = button.getText();
		
		if((expressionCheck.isDividedByZero() || expressionCheck.isResultUndefined() || expressionCheck.isStackOverflow()) && expressionCheck.isDeletionButtonPressed(buttonText)) {
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
		
		if((expressionCheck.isDividedByZero() || expressionCheck.isResultUndefined() || expressionCheck.isStackOverflow()) && expressionCheck.isDeletionButtonPressed(keyChar)) {
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
