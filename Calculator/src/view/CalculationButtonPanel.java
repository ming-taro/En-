package view;

import java.awt.*;
import java.awt.event.*;

import javax.swing.JButton;
import javax.swing.JPanel;

import operationmanagement.Constants;
import operationmanagement.ButtonOnCalculator.Button;

public class CalculationButtonPanel extends JPanel implements ActionListener{
	private JButton[] calculationButton;
	private ExpressionPanel expressionPanel;
	public CalculationButtonPanel(ExpressionPanel resultPanel){
		calculationButton = new JButton[20];
		this.expressionPanel = resultPanel;
		addButtonPanel();
	}
	
	private void addButtonPanel() {  //계산버튼패널
		setLayout(new GridLayout(5, 4, 2, 2));
		Font font = new Font("SansSerif", Font.BOLD, Constants.BUTTON_FONT_SIZE);
		
		int index;
		
		for(Button button : Button.values()) {
			index = button.getIndex();
			calculationButton[index] = new JButton();
			add(calculationButton[index]);
			calculationButton[index].addActionListener(this);     //버튼클릭이벤트 연결
			calculationButton[index].setText(button.getSymbol());   //버튼이름 설정
			calculationButton[index].setFont(font);  
			calculationButton[index].setFocusPainted(false);
			calculationButton[index].setContentAreaFilled(false);
		}
	}

	@Override
	public void actionPerformed(ActionEvent event) {
		JButton button = (JButton) event.getSource();
		String buttonClicked = button.getText();

		System.out.print(buttonClicked);
		if(buttonClicked.charAt(0) >= '0' && buttonClicked.charAt(0) <= '9') {
			expressionPanel.setInputLabel(buttonClicked);
		}
		else if(buttonClicked.equals(calculationButton[0].getText())) {
			expressionPanel.setZero();
		}
		else if(buttonClicked.equals(calculationButton[1].getText())) {
			System.out.print(buttonClicked);
			expressionPanel.removeInputLabel();
		}
	}

}