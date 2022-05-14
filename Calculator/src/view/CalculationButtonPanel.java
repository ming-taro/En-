package view;

import java.awt.*;
import java.awt.event.*;

import javax.swing.JButton;
import javax.swing.JPanel;

import utility.Constants;
import utility.ButtonOnCalculator.Button;

public class CalculationButtonPanel extends JPanel{
	private JButton[] calculationButton;
	private ActionListener buttonListener;
	
	public CalculationButtonPanel(ActionListener buttonListener){
		calculationButton = new JButton[20];
		this.buttonListener = buttonListener;
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
			calculationButton[index].addActionListener(buttonListener);     //버튼클릭이벤트 연결
			calculationButton[index].setText(button.getSymbol());   //버튼이름 설정
			calculationButton[index].setFont(font);  
			calculationButton[index].setFocusPainted(false);
			calculationButton[index].setContentAreaFilled(false);
		}
	}

}
