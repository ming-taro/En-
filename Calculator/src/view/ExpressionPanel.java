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
	private StringBuilder number;
	
	public ExpressionPanel() {
		recordButton = new JButton("T");
		expressionLabel = new JLabel("");
		inputLabel = new JLabel("0");
		number = new StringBuilder();
		
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
	public void setInputLabel(String input) {
		if(number.length() == 16 || number.length() == 0 && input.equals("0")) return;
		number.append(input);
		inputLabel.setText(number.toString().replaceAll("\\B(?=(\\d{3})+(?!\\d))", ","));
	}
	public void removeInputLabel() {
		number.setLength(0);
		inputLabel.setText("0");
	}
	public void setZero() {
		number.setLength(0);
		inputLabel.setText("0");
	}
	@Override
	public void actionPerformed(ActionEvent e) { //계산기록보기
		
		
		
	}
}
