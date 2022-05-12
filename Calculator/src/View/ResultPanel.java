package View;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import operationManagement.Constants;

public class ResultPanel extends JPanel implements ActionListener{
	private JButton recordButton;
	private JLabel expressionLabel;
	private JLabel inputValueLabel;
	public ResultPanel() {
		recordButton = new JButton("T");
		expressionLabel = new JLabel("");
		inputValueLabel = new JLabel("");
		
		setLayout(new GridLayout(3, 0));

		addRecordPanel();
		addExpressionPanel();
		addInputPanel();
	}
	
	public void addRecordPanel() {
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
	public void addExpressionPanel() {
		JPanel expressionPanel = new JPanel();

		expressionLabel.setFont(new Font("SansSerif", Font.PLAIN, Constants.EXPRESSION_FONT_SIZE));
		expressionPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		expressionPanel.add(expressionLabel);
		
		add(expressionPanel);
	}
	public void addInputPanel() {
		JPanel inputPanel = new JPanel();

		inputValueLabel.setFont(new Font("SansSerif", Font.BOLD, Constants.INPUT_FONT_SIZE));
		inputPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		inputPanel.add(inputValueLabel);
		
		add(inputPanel);
	}

	@Override
	public void actionPerformed(ActionEvent e) {
		expressionLabel.setText(expressionLabel.getText() + "d");
		inputValueLabel.setText(inputValueLabel.getText() + "d");
		
	}
}
