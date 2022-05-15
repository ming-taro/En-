package view;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

import javax.swing.*;

import utility.*;

public class CalculatorFrame extends JFrame implements ActionListener{
	private JPanel equationRecordPanel;       //계산기록 출력 패널
	private JPanel recordPanel;               //계산기록 출력 패널 -> 계산수식 출력 패널
	private EquationPanel equationPanel;                    //프레임 -> 계산수식, 현재 입력값 출력 패널
	private CalculationButtonPanel calculationButtonPanel;  //프레임 -> 계산기 버튼 패널
	private JScrollPane recordPanelScroll;  //계산기록 출력 패널 -> 계산수식 출력 패널에 달  스크롤
	private JButton deletionButton;           //계산기록 출력 패널 -> 휴지통 버튼
	private int panelNumber;   //equationRecordPanel : 0, calculationButtonPanel = 1
	private ArrayList<String> recordList;     //계산기록 출력 패널 -> 계산수식 출력 패널 -> 계산기록 리스트
	private JTextArea recordTextArea;
	
	public CalculatorFrame(EquationPanel equationPanel, CalculationButtonPanel calculationButtonPanel, ArrayList<String> recordList) {
		setPreferredSize(new Dimension(Constants.WIDTH, Constants.HEIGHT));
		setSize(Constants.WIDTH, Constants.HEIGHT);
		setTitle("Calculator");
		setDefaultCloseOperation(EXIT_ON_CLOSE);
		setLayout(new GridLayout(2, 0));
		Toolkit toolkit = Toolkit.getDefaultToolkit();
		Image img = toolkit.getImage("calculator.png");
		setIconImage(img); 
		setVisible(true);

		this.equationPanel = equationPanel;
		this.calculationButtonPanel = calculationButtonPanel;
		this.recordList = recordList;
		
		equationPanel.setActionListener(this);          //계산식 출력 패널의 기록보기 버튼에 event연결
		getContentPane().add(equationPanel);            //계산기 frame에 계산식 출력 패널 추가
		getContentPane().add(calculationButtonPanel);   //계산기 frame에 계산버튼 패널 추가
		
		equationRecordPanel = new JPanel();
		
		ImageIcon icon = new ImageIcon("image\\wastebasket.png");
		Image image = icon.getImage();
		Image changeImage = image.getScaledInstance(Constants.RECORD_BUTTON_SIZE, Constants.RECORD_BUTTON_SIZE, Image.SCALE_SMOOTH);
		ImageIcon changeIcon = new ImageIcon(changeImage);
		deletionButton = new JButton(changeIcon);
		deletionButton.setBorderPainted(false);
		deletionButton.setFocusPainted(false);
		deletionButton.setContentAreaFilled(false);
		deletionButton.addActionListener(this);
		recordPanel = new JPanel();
		setequationRecordPanel();

		recordTextArea = new JTextArea();
		recordTextArea.setFont(new Font("SansSerif", Font.BOLD, Constants.BUTTON_FONT_SIZE));
		
		panelNumber = 0;
	}
	public void setequationRecordPanel() {
		JPanel deletionButtonPanel = new JPanel();
		recordPanelScroll = new JScrollPane(recordPanel);
		
		recordPanel.setBackground(Color.white);
		deletionButtonPanel.setBackground(Color.white);
		
		deletionButtonPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		deletionButton.setPreferredSize(new Dimension(Constants.RECORD_BUTTON_SIZE,Constants.RECORD_BUTTON_SIZE));  //계산기록 버튼
		deletionButtonPanel.add(deletionButton);
		
		equationRecordPanel.setLayout(new BorderLayout());
		equationRecordPanel.add(recordPanelScroll, "Center");
		equationRecordPanel.add(deletionButtonPanel, "South");
	}
	public void setRecordList() {
		recordTextArea.setText("");  //먼저 이전에 저장된 계산기록을 초기화 
		
		for(int index = recordList.size() - 1; index >= 0; index--) {
			recordTextArea.append(recordList.get(index) + "\n\n");
		}

		recordPanel.add(recordTextArea);
	}
	@Override
	public void actionPerformed(ActionEvent event) {
		if(event.getSource() == deletionButton) {
			recordList.clear();
			recordTextArea.setText("");
			return;
		}
		
		getContentPane().removeAll();
		getContentPane().add(equationPanel); 
		
		if(panelNumber == 0) {
			getContentPane().add(equationRecordPanel); 
			setRecordList();
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
