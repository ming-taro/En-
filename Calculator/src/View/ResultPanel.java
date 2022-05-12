package View;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import operationManagement.Constants;

public class ResultPanel extends JPanel implements ActionListener{
	private JButton recordButton;
	private JLabel expressionLabel;
	private JLabel inputLabel;
	public ResultPanel() {
		recordButton = new JButton("T");
		expressionLabel = new JLabel("");
		inputLabel = new JLabel("");
		
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
		inputLabel.setText(inputLabel.getText() + input);
	}
	@Override
	public void actionPerformed(ActionEvent e) { //계산기록보기
		//expressionLabel.setText(expressionLabel.getText() + "d");
		//inputValueLabel.setText(inputValueLabel.getText() + "d");
		
	}
}
