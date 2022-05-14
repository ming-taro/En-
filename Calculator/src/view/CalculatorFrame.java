package view;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

import javax.swing.*;

import utility.*;

public class CalculatorFrame extends JFrame implements ActionListener{
	private JPanel recordPanel;
	private EquationPanel equationPanel;
	private CalculationButtonPanel calculationButtonPanel;
	private JButton deletionButton;
	private int panelNumber;   //recordpanel : 0, calculationButtonPanel = 1
	
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
		deletionButton = new JButton();
		setRecordPanel();
		
		panelNumber = 0;
	}
	public void setRecordPanel() {
		JPanel equationPanel = new JPanel();
		JPanel deletionButtonPanel = new JPanel();
		
		equationPanel.setBackground(Color.white);
		deletionButtonPanel.setBackground(Color.white);
		
		deletionButtonPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		deletionButton.setPreferredSize(new Dimension(Constants.RECORD_BUTTON_SIZE,Constants.RECORD_BUTTON_SIZE));  //계산기록 버튼
		deletionButtonPanel.add(deletionButton);
		
		recordPanel.setLayout(new BorderLayout());
		recordPanel.add(equationPanel, "Center");
		recordPanel.add(deletionButtonPanel, "South");
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
