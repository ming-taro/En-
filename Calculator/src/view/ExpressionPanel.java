package view;

import java.awt.*;
import java.awt.event.*;
import java.text.NumberFormat;

import javax.swing.*;

import utility.Constants;

public class ExpressionPanel extends JPanel{
	private JButton recordButton;
	private JLabel expressionLabel;
	private JLabel inputLabel;
	
	public JLabel getInputLabel() {
		return inputLabel;
	}
	public ExpressionPanel() {		
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
	public void setVisibleForRecordButton() {
		recordButton.setVisible(true);
	}
	public void setInvisibleForRecordButton() {
		recordButton.setVisible(false);
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
		int width = (int) getPreferredSize().getWidth();
		expressionLabel.setText(expression);   //계산식 누적값 출력
		inputLabel.setText(number); //숫자입력 누적값 출력
		
		increaseInputLabel();
		decreaseInputLabel();
		
		setPreferredSize(new Dimension(width, 204));
	}
	public void decreaseInputLabel() {
		if(inputLabel.getPreferredSize().getWidth() < getSize().getWidth()) return;
		
		Font fontBeforeChange = inputLabel.getFont();
		Font fontAfterChange = new Font(fontBeforeChange.getName(), fontBeforeChange.getStyle(), Constants.INPUT_FONT_SIZE);
		int fontSize = 0;
		
		while(true) {
			fontBeforeChange = inputLabel.getFont();
			fontAfterChange = new Font(fontBeforeChange.getName(), fontBeforeChange.getStyle(), fontSize);
            inputLabel.setFont(fontAfterChange);
            if(inputLabel.getPreferredSize().getWidth() > getSize().getWidth() - 10) {
            	fontAfterChange = new Font(fontBeforeChange.getName(), fontBeforeChange.getStyle(), --fontSize);
                inputLabel.setFont(fontAfterChange);
                break;
            }
            fontSize++;    
        }
	}
	public void increaseInputLabel() {
		Font fontBeforeChange = inputLabel.getFont();
		Font fontAfterChange = new Font(fontBeforeChange.getName(), fontBeforeChange.getStyle(), fontBeforeChange.getSize());
		int fontSize = 0;
		
		while(true) {
			fontBeforeChange = inputLabel.getFont();
			fontAfterChange = new Font(fontBeforeChange.getName(), fontBeforeChange.getStyle(), fontSize);
            inputLabel.setFont(fontAfterChange);
            if(inputLabel.getFont().getSize() == Constants.INPUT_FONT_SIZE) break;
            fontSize++;    
        }
	}
}
