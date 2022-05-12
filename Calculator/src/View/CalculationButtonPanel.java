package View;

import java.awt.Font;
import java.awt.GridLayout;

import javax.swing.JButton;
import javax.swing.JPanel;

import operationManagement.Constants;

public class CalculationButtonPanel extends JPanel {
	private JButton[][] calculationButton;
	
	public CalculationButtonPanel(){
		calculationButton = new JButton[5][4];
		addButtonPanel();
	}
	
	public void addButtonPanel() {
		setLayout(new GridLayout(5, 4));
		String[][] buttonName = {{"CE", "C", "←", "÷"}, {"7", "8", "9", "×"}, {"4", "5", "6", "－"}, {"1", "2", "3", "＋"}, {"±", "0", ".", "＝"}};
		Font font = new Font("SansSerif", Font.BOLD, Constants.BUTTON_FONT_SIZE);

		for(int i=0; i<5; i++) {
			for(int j=0; j<4; j++) {
				calculationButton[i][j] = new JButton();
				add(calculationButton[i][j]);
				calculationButton[i][j].setText(buttonName[i][j]);   
				calculationButton[i][j].setFont(font);               
			}
		}
	}
}
