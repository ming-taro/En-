package view;

import java.awt.*;
import java.awt.event.*;

import javax.swing.JButton;
import javax.swing.JPanel;
import javax.swing.border.LineBorder;

import utility.Constants;
import utility.ButtonOnCalculator.Button;

public class CalculationButtonPanel extends JPanel{
	private JButton[] calculationButton;
	private ActionListener buttonListener;
	
	public CalculationButtonPanel(ActionListener buttonListener){
		calculationButton = new JButton[20];
		this.buttonListener = buttonListener;
		setBackground(new Color(230, 230, 230));
		addButtonPanel();
	}
	
	private void addButtonPanel() {  //계산버튼패널
		setLayout(new GridLayout(5, 4, 2, 2));
		Font font = new Font("SansSerif", Font.BOLD, Constants.BUTTON_FONT_SIZE);
		LineBorder lineBorder = new LineBorder(Color.white, 0, false);

		int index;
		
		for(Button button : Button.values()) {
			index = button.getIndex();
			calculationButton[index] = new JButton();
			add(calculationButton[index]);
			calculationButton[index].addActionListener(buttonListener);     //버튼클릭이벤트 연결
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
