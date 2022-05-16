package view;

import java.awt.*;
import java.awt.event.*;
import java.text.NumberFormat;

import javax.swing.*;

import utility.Constants;

public class EquationPanel extends JPanel{
	private JButton recordButton;
	private JLabel expressionLabel;
	private JLabel inputLabel;
	
	public JLabel getInputLabel() {
		return inputLabel;
	}
	public EquationPanel() {		
		ImageIcon icon = new ImageIcon("image\\clock.png");
		Image img = icon.getImage();
		Image changeImg = img.getScaledInstance(Constants.BUTTON_SIZE, Constants.BUTTON_SIZE, Image.SCALE_SMOOTH);
		ImageIcon changeIcon = new ImageIcon(changeImg);

		recordButton = new JButton(changeIcon);
		expressionLabel = new JLabel("");
		inputLabel = new JLabel("0");

		setLayout(new GridLayout(3, 0));

		addRecordPanel();
		addExpressionPanel();
		addInputPanel();
	}
	public void setActionListener(ActionListener listener) {
		recordButton.addActionListener(listener);
	}
	public void setComponentListener(ComponentListener listener) {
		inputLabel.addComponentListener(listener);
		expressionLabel.addComponentListener(listener);
	}
	private void addRecordPanel() {
		JPanel recordPanel = new JPanel();
		
		recordButton.setPreferredSize(new Dimension(Constants.BUTTON_SIZE,Constants.BUTTON_SIZE));  //계산기록 버튼	
		recordButton.setBorderPainted(false);
		recordButton.setFocusPainted(false);
		recordButton.setContentAreaFilled(false);
		
		recordPanel.setBackground(new Color(230, 230, 230));
		recordPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		recordPanel.add(recordButton);
		
		add(recordPanel);
	}
	private void addExpressionPanel() {
		JPanel expressionPanel = new JPanel();
		
		expressionPanel.setBackground(new Color(230, 230, 230));
		expressionLabel.setFont(new Font("SansSerif", Font.PLAIN, Constants.EXPRESSION_FONT_SIZE));
		expressionPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		expressionPanel.add(expressionLabel);
		
		add(expressionPanel);
	}
	private void addInputPanel() {
		JPanel inputPanel = new JPanel();
		
		inputPanel.setBackground(new Color(230, 230, 230));
		inputLabel.setFont(new Font("SansSerif", Font.BOLD, Constants.INPUT_FONT_SIZE));
		inputPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		inputPanel.add(inputLabel);
		
		add(inputPanel);
	}
	public void setExpressionLabel(String expression, String number) {
		expressionLabel.setText(expression);   //계산식 누적값 출력
		inputLabel.setText(number); //숫자입력 누적값 출력
	}
	public void removeInputLabel(StringBuilder number) {
		number.setLength(0);
		inputLabel.setText("0");
	}
	public void setZero(StringBuilder number) {
		number.setLength(0);
		inputLabel.setText("0");
	}
}
