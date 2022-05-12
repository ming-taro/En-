package view;
import java.awt.*;
import javax.swing.*;

import operationmanagement.*;

public class CalculatorFrame extends JFrame {
	private ResultPanel resultPanel;
	private CalculationButtonPanel calculationButtonPanel;
	
	public CalculatorFrame() {
		setPreferredSize(new Dimension(Constants.WIDTH, Constants.HEIGHT));
		setSize(Constants.WIDTH, Constants.HEIGHT);
		setTitle("Calculator");
		setDefaultCloseOperation(EXIT_ON_CLOSE);
		setLayout(new GridLayout(2, 0));
		setVisible(true);

		resultPanel = new ResultPanel();
		calculationButtonPanel = new CalculationButtonPanel(resultPanel);
		
		getContentPane().add(resultPanel);   //계산기 frame에 계산결과 패널 추가
		getContentPane().add(calculationButtonPanel);   //계산기 frame에 계산버튼 패널 추가
	}
	
	
}
