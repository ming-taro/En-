import java.awt.*;
import javax.swing.*;
import operationManagement.*;

public class CalculatorFrame extends JFrame {
	private JPanel resultPanel;
	private JPanel buttonPanel;
	
	public CalculatorFrame() {
		setPreferredSize(new Dimension(Constants.WIDTH, Constants.HEIGHT));
		setSize(Constants.WIDTH, Constants.HEIGHT);
		setTitle("Calculator");
		setDefaultCloseOperation(EXIT_ON_CLOSE);
		setLayout(new GridLayout(2, 0));
		setVisible(true);
		//getContentPane().add(new JButton("오아"));
		getContentPane().add(new JPanel().add(new JButton("이야")));
		resultPanel = new JPanel();
		buttonPanel = new JPanel();
		
		AddResultPanel();
	}
	public void AddResultPanel() {
		resultPanel.setLayout(new GridLayout(5, 4));
		JButton[][] button = new JButton[5][4];
		String[][] buttonName = {{"CE", "C", "←", "÷"}, {"7", "8", "9", "×"}, {"4", "5", "6", "－"}, {"1", "2", "3", "＋"}, {"±", "0", ".", "＝"}};
		
		for(int i=0; i<5; i++) {
			for(int j=0; j<4; j++) {
				button[i][j] = new JButton(buttonName[i][j]);
				resultPanel.add(button[i][j]);
			}
		}
		getContentPane().add(resultPanel);
		
	}
}
