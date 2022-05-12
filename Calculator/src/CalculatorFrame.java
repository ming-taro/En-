import java.awt.*;
import javax.swing.*;
import operationManagement.*;

public class CalculatorFrame extends JFrame {
	private JPanel inputPanel;
	private JPanel buttonPanel;
	private JButton[][] calculationButton;
	private JButton recordButton;
	
	public CalculatorFrame() {
		setPreferredSize(new Dimension(Constants.WIDTH, Constants.HEIGHT));
		setSize(Constants.WIDTH, Constants.HEIGHT);
		setTitle("Calculator");
		setDefaultCloseOperation(EXIT_ON_CLOSE);
		setLayout(new GridLayout(2, 0));
		setVisible(true);
		
		inputPanel = new JPanel();
		buttonPanel = new JPanel();
		
		calculationButton = new JButton[5][4];
		recordButton = new JButton("T");
		
		addInputPanel();
		addButtonPanel();
	}
	public void addInputPanel() {
		inputPanel.setBackground(Color.red);
		inputPanel.setLayout(new GridLayout(3, 0));
		
		JPanel recordPanel = new JPanel();
		recordPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		recordPanel.add(recordButton);
		
		recordButton.setPreferredSize(new Dimension(Constants.RECORD_BUTTON_SIZE,Constants.RECORD_BUTTON_SIZE));

		inputPanel.add(recordPanel);
		
		getContentPane().add(inputPanel);   //계산기 frame에 계산결과 패널 추가
	}
	public void addButtonPanel() {
		buttonPanel.setLayout(new GridLayout(5, 4));
		String[][] buttonName = {{"CE", "C", "←", "÷"}, {"7", "8", "9", "×"}, {"4", "5", "6", "－"}, {"1", "2", "3", "＋"}, {"±", "0", ".", "＝"}};
		Font font = new Font("SansSerif", Font.BOLD, Constants.BUTTON_FONT_SIZE);
		
		for(int i=0; i<5; i++) {
			for(int j=0; j<4; j++) {
				calculationButton[i][j] = new JButton();
				buttonPanel.add(calculationButton[i][j]);
				calculationButton[i][j].setText(buttonName[i][j]);   
				calculationButton[i][j].setFont(font);               
			}
		}
		getContentPane().add(buttonPanel);   //계산기 frame에 입력버튼 패널 추가
	}
}
