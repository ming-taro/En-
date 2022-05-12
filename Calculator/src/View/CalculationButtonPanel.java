package View;

import java.awt.Font;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JPanel;

import operationManagement.Constants;
import operationManagement.Constants.ButtonnOnCalculator;

public class CalculationButtonPanel extends JPanel implements ActionListener{
	private JButton[][] calculationButton;
	private ResultPanel resultPanel;
	private String[][] buttonName = {{"CE", "C", "←", "÷"}, {"7", "8", "9", "×"}, {"4", "5", "6", "－"}, {"1", "2", "3", "＋"}, {"±", "0", ".", "＝"}};
	
	
	public CalculationButtonPanel(ResultPanel resultPanel){
		calculationButton = new JButton[5][4];
		this.resultPanel = resultPanel;
		addButtonPanel();
	}
	
	private void addButtonPanel() {  //계산버튼패널
		setLayout(new GridLayout(5, 4, 2, 2));
		Font font = new Font("SansSerif", Font.BOLD, Constants.BUTTON_FONT_SIZE);

		for(int i=0; i<5; i++) {
			for(int j=0; j<4; j++) {
				calculationButton[i][j] = new JButton();
				add(calculationButton[i][j]);
				calculationButton[i][j].addActionListener(this);     //버튼클릭이벤트 연결
				calculationButton[i][j].setText(buttonName[i][j]);   //버튼이름 설정
				calculationButton[i][j].setFont(font);  
				calculationButton[i][j].setFocusPainted(false);
				calculationButton[i][j].setContentAreaFilled(false);             
			}
		}
	}

	@Override
	public void actionPerformed(ActionEvent event) {
		JButton button = (JButton) event.getSource();
		String buttonClicked = button.getText();

		System.out.print(buttonClicked);
		if(buttonClicked.charAt(0) >= '0' && buttonClicked.charAt(0) <= '9') {
			resultPanel.setInputLabel(buttonClicked);
		}
		else if(buttonClicked.equals(buttonName[0][0])) {
			resultPanel.setZero();
		}
		else if(buttonClicked.equals(buttonName[0][1])) {
			System.out.print(buttonClicked);
			resultPanel.removeInputLabel();
		}
		
		//System.out.print(Constants.ButtonnOnCalculator.valueOf("CE"));
		
	}
}
