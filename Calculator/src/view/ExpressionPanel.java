package view;

import java.awt.*;
import java.awt.event.*;
import java.text.NumberFormat;

import javax.swing.*;

import operationmanagement.Constants;

public class ExpressionPanel extends JPanel implements ActionListener{
	private JButton recordButton;
	private JLabel expressionLabel;
	private JLabel inputLabel;
	
	public ExpressionPanel() {
		recordButton = new JButton("T");
		expressionLabel = new JLabel("");
		inputLabel = new JLabel("0");
		
		setLayout(new GridLayout(3, 0));

		addRecordPanel();
		addExpressionPanel();
		addInputPanel();
	}
	
	private void addRecordPanel() {
		JPanel recordPanel = new JPanel();
		JLabel label = new JLabel("표준                                                                                     ");
		
		label.setFont(new Font("SansSerif", Font.BOLD, Constants.BUTTON_FONT_SIZE));
		recordButton.setPreferredSize(new Dimension(Constants.RECORD_BUTTON_SIZE,Constants.RECORD_BUTTON_SIZE));
		recordButton.addActionListener((ActionListener) this);
		
		recordPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		recordPanel.add(label);
		recordPanel.add(recordButton);
		
		add(recordPanel);
	}
	private void addExpressionPanel() {
		JPanel expressionPanel = new JPanel();

		expressionLabel.setFont(new Font("SansSerif", Font.PLAIN, Constants.EXPRESSION_FONT_SIZE));
		expressionPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		expressionPanel.add(expressionLabel);
		
		add(expressionPanel);
	}
	private void addInputPanel() {
		JPanel inputPanel = new JPanel();

		inputLabel.setFont(new Font("SansSerif", Font.BOLD, Constants.INPUT_FONT_SIZE));
		inputPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		inputPanel.add(inputLabel);
		
		add(inputPanel);
	}
	public void setExpressionLabel(String expression, String number) {
		expressionLabel.setText(expression);   //계산식 누적값 출력
		inputLabel.setText(number.replaceAll("\\B(?=(\\d{3})+(?!\\d))", ",")); //숫자입력 누적값 출력
	}
	public void removeInputLabel(StringBuilder number) {
		number.setLength(0);
		inputLabel.setText("0");
	}
	public void setZero(StringBuilder number) {
		number.setLength(0);
		inputLabel.setText("0");
	}
	@Override
	public void actionPerformed(ActionEvent e) { //계산기록보기
		
		
		
	}
}
