package view;

import java.awt.*;
import java.awt.event.*;

import javax.swing.JButton;
import javax.swing.JPanel;
import javax.swing.border.LineBorder;

import controller.EventHandlingForMouse;
import utility.Constants;
import utility.ButtonOnCalculator.Button;

public class CalculatorButtonPanel extends JPanel{
	private JButton[] calculationButton;
	private ActionListener buttonListener;
	private EventHandlingForMouse mouseListener;
	
	public CalculatorButtonPanel(ActionListener buttonListener, EventHandlingForMouse mouseListener){
		calculationButton = new JButton[20];
		this.buttonListener = buttonListener;
		this.mouseListener = mouseListener;
		setBackground(new Color(230, 230, 230));
		addButtonPanel();
	}
	public void deactivateOperatorButton() {
		calculationButton[3].setEnabled(false);
		calculationButton[7].setEnabled(false);
		calculationButton[11].setEnabled(false);
		calculationButton[15].setEnabled(false);
	}
	public void activateOperatorButton() {
		calculationButton[3].setEnabled(true);
		calculationButton[7].setEnabled(true);
		calculationButton[11].setEnabled(true);
		calculationButton[15].setEnabled(true);
	}
	private void addButtonPanel() {  //계산버튼패널
		setLayout(new GridLayout(5, 4, 2, 2));
		Font font = new Font("SansSerif", Font.BOLD, Constants.BUTTON_FONT_SIZE);

		int index;
		
		for(Button button : Button.values()) {
			index = button.getIndex();
			calculationButton[index] = new JButton();
			add(calculationButton[index]);
			
			calculationButton[index].addMouseListener(mouseListener);                //마우스 이벤트 연결
			calculationButton[index].addActionListener(buttonListener);     //버튼클릭 이벤트 연결
			
			calculationButton[index].setText(button.getSymbol());   //버튼이름 설정
			calculationButton[index].setFont(font);  
			calculationButton[index].setBorderPainted(false);
			calculationButton[index].setFocusPainted(false);
			
			if(index == 0 || index == 1 || index == 2 || index == 3 || index == 7 || index == 11 || index == 15) calculationButton[index].setBackground(new Color(240, 240, 240));
			else calculationButton[index].setBackground(Color.white);
		}
		
		calculationButton[19].setBackground(new Color(138, 186, 224));
	}
}
