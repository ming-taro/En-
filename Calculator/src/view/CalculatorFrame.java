package view;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.*;

import utility.*;

public class CalculatorFrame extends JFrame implements ActionListener{
	private JPanel recordPanel;
	private EquationPanel equationPanel;
	private CalculationButtonPanel calculationButtonPanel;
	private int panelNumber;
	
	public CalculatorFrame(EquationPanel equationPanel, CalculationButtonPanel calculationButtonPanel) {
		setPreferredSize(new Dimension(Constants.WIDTH, Constants.HEIGHT));
		setSize(Constants.WIDTH, Constants.HEIGHT);
		setTitle("Calculator");
		setDefaultCloseOperation(EXIT_ON_CLOSE);
		setLayout(new GridLayout(2, 0));
		setVisible(true);

		this.equationPanel = equationPanel;
		this.calculationButtonPanel = calculationButtonPanel;
		
		equationPanel.setActionListener(this);          //계산식 출력 패널의 기록보기 버튼에 event연결
		getContentPane().add(equationPanel);            //계산기 frame에 계산식 출력 패널 추가
		getContentPane().add(calculationButtonPanel);   //계산기 frame에 계산버튼 패널 추가
		
		recordPanel = new JPanel();
		recordPanel.setBackground(Color.red);
		panelNumber = 0;
	}

	@Override
	public void actionPerformed(ActionEvent event) {
		getContentPane().removeAll();
		getContentPane().add(equationPanel); 
		
		if(panelNumber == 0) {
			getContentPane().add(recordPanel); 
			panelNumber = 1;
		}
		else {
			getContentPane().add(calculationButtonPanel);
			panelNumber = 0;
		}
			
		revalidate();
		repaint();
	}
	
	
}
