package view;

import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.EmptyBorder;

import Model.ExpressionDTO;
import controller.EventHandlingForMouse;
import controller.ExpressionCheck;
import controller.FormatOfExpression;
import utility.Constants;

public class RecordPanel extends JPanel implements ActionListener{
	private JPanel resultPanel;               //계산기록 출력 패널
	private JPanel recordButtonPanel;         //계산식 버튼을 부착할 패널
	private JScrollPane recordPanelScroll;    //계산기록 출력 패널 -> 계산수식 출력 패널에 달  스크롤
	private JButton[] recordButton;
	private ArrayList<ExpressionDTO> recordList;     //계산기록 출력 패널 -> 계산수식 출력 패널 -> 계산기록 리스트
	private JButton deletionButton;           //휴지통 버튼 -> 계산기록 초기화
	private JPanel deletionButtonPanel;       //휴지통 버튼을 담을 패널
	private FormatOfExpression formatOfExpression;
	private ActionListener recordButtonListener;
	private EventHandlingForMouse mouseListener;
	
	public RecordPanel(ExpressionCheck expressionCheck, ArrayList<ExpressionDTO> recordList, ActionListener recordButtonListener, EventHandlingForMouse mouseListener) {
		this.recordList = recordList;
		this.recordButtonListener = recordButtonListener;
		this.mouseListener = mouseListener;
		
		formatOfExpression = new FormatOfExpression(new ExpressionDTO(), expressionCheck);
		resultPanel = new JPanel();
		recordButtonPanel = new JPanel();
		
		ImageIcon icon = new ImageIcon("image\\wastebasket.png");
		Image image = icon.getImage();
		Image changeImage = image.getScaledInstance(Constants.BUTTON_SIZE, Constants.BUTTON_SIZE, Image.SCALE_SMOOTH);
		ImageIcon changeIcon = new ImageIcon(changeImage);

		deletionButton = new JButton(changeIcon);
		recordPanelScroll = new JScrollPane(resultPanel);
		
		recordPanelScroll.setHorizontalScrollBarPolicy(JScrollPane.HORIZONTAL_SCROLLBAR_NEVER);
		recordPanelScroll.setBorder(new EmptyBorder(0, 0, 0, 0));
		
		setLayout(new BorderLayout());
		setDeletionButtonPanel();
		setRecordPanel();
	}
	public void setBackgroundColor(Color color) {
		int listSize = recordList.size();
		
		setBackground(color);
		resultPanel.setBackground(color);
		deletionButtonPanel.setBackground(color);;
		recordButtonPanel.setBackground(color);
		
		for(int index = 0; index < listSize; index++) {
			recordButton[index].setBackground(color);
		}
	}
	private void setDeletionButton() {
		deletionButton.setPreferredSize(new Dimension(Constants.BUTTON_SIZE,Constants.BUTTON_SIZE));  //계산기록 버튼
		deletionButton.setBorderPainted(false);
		deletionButton.setFocusPainted(false);
		deletionButton.setContentAreaFilled(false);
		deletionButton.addActionListener(this);
		deletionButton.addMouseListener(mouseListener);
	}
	private void setDeletionButtonPanel() {
		deletionButtonPanel = new JPanel();
		
		setDeletionButton();
		
		deletionButtonPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		deletionButtonPanel.add(deletionButton);
	}
	public void setRecordPanel() {
		JLabel noRecordLabel = new JLabel("<html><br>계산 기록이 없습니다.</html>");
		noRecordLabel.setHorizontalAlignment(JLabel.CENTER);

		removeAll();
		if(recordList.size() == 0) {
			noRecordLabel.setFont(new Font("돋움", Font.PLAIN, 15));
			add(noRecordLabel, BorderLayout.NORTH);
		}
		else {
			add(recordPanelScroll, "Center");
			add(deletionButtonPanel, "South");
			setRecordList();
		}

		setBackgroundColor(new Color(230, 230, 230));
		revalidate();
		repaint();
		
	}
	public JLabel getExpressionLabel(ExpressionDTO expressionDTO) {
		JLabel expressionLabel;
		String expression;
		String firstValue, secondValue;
		
		formatOfExpression.setExpressionDTO(expressionDTO);   //계산식을 출력할 DTO
		expression = formatOfExpression.getExpression();
		
		firstValue = formatOfExpression.setNumber(expressionDTO.getFirstValue());
		secondValue = formatOfExpression.setNumber(expressionDTO.getSecondValue());
		
		expressionLabel = new JLabel(firstValue + " " + expressionDTO.getOperator() + " " + secondValue + " =");
		expressionLabel.setFont(new Font("SansSerif", Font.PLAIN, 13)); 
		
		if(expressionLabel.getPreferredSize().getWidth() > getPreferredSize().getWidth() - 50){
			expressionLabel = new JLabel();
			expressionLabel.setLayout(new GridLayout(2, 0));
			
			JLabel firstValueLabel = new JLabel(firstValue + "    " + expressionDTO.getOperator());
			firstValueLabel.setHorizontalAlignment(JLabel.RIGHT);
			firstValueLabel.setFont(new Font("SansSerif", Font.PLAIN, 13)); 
			
			JLabel secondValueLabel = new JLabel(secondValue + " =");
			secondValueLabel.setHorizontalAlignment(JLabel.RIGHT);
			secondValueLabel.setFont(new Font("SansSerif", Font.PLAIN, 13)); 
			
			expressionLabel.add(firstValueLabel);
			expressionLabel.add(secondValueLabel);
			expressionLabel.setPreferredSize(new Dimension((int)getPreferredSize().getWidth() - 50, 40));
		}
		else expressionLabel.setPreferredSize(new Dimension((int)getPreferredSize().getWidth() - 50, 20));
		
		expressionLabel.setHorizontalAlignment(JLabel.RIGHT);
		
		return expressionLabel;
	}
	public JLabel getResulLabel(ExpressionDTO expressionDTO) {
		JLabel resultLabel;
		String result = formatOfExpression.setNumber(expressionDTO.getResult());
		
		if(result.length() > 21) {
			int index = result.indexOf("e") + 2;
			
			resultLabel = new JLabel();
			resultLabel.setLayout(new GridLayout(2, 0));
			
			JLabel firstPart = new JLabel(result.substring(0, index));
			firstPart.setHorizontalAlignment(JLabel.RIGHT);
			firstPart.setFont(new Font("SansSerif", Font.BOLD, 22)); 
			
			JLabel secondPart = new JLabel(result.substring(index));
			secondPart.setHorizontalAlignment(JLabel.RIGHT);
			secondPart.setFont(new Font("SansSerif", Font.BOLD, 22)); 
			
			resultLabel.add(firstPart);
			resultLabel.add(secondPart);
			resultLabel.setPreferredSize(new Dimension((int)getPreferredSize().getWidth() - 50, 60));
		}
		else {
			resultLabel = new JLabel(formatOfExpression.formatNumber(result));
			resultLabel.setFont(new Font("SansSerif", Font.BOLD, 22)); 
			resultLabel.setPreferredSize(new Dimension((int)getPreferredSize().getWidth() - 50, 30));
		}
		
		resultLabel.setHorizontalAlignment(JLabel.RIGHT);

		return resultLabel;
	}
	public void setRecordList() {
		int listSize = recordList.size();
		JLabel expressionLabel, resultLabel;
		
		recordButtonPanel.setLayout(new GridLayout(listSize, 0, 0, 40));
		recordButtonPanel.removeAll();
		recordButton = new JButton[listSize];
		
		for(int index = listSize - 1; index >= 0; index--) {
			recordButton[index] = new JButton();                          //계산식 버튼
			
			expressionLabel = getExpressionLabel(recordList.get(index));
			resultLabel = getResulLabel(recordList.get(index));
			
			recordButton[index].setLayout(new BorderLayout());
			recordButton[index].add(expressionLabel, BorderLayout.NORTH);
			recordButton[index].add(resultLabel, BorderLayout.SOUTH);

			recordButton[index].setHorizontalAlignment(SwingConstants.RIGHT);
			recordButton[index].setBorderPainted(false);
			recordButton[index].addActionListener(recordButtonListener);
			recordButton[index].addMouseListener(mouseListener);

			recordButtonPanel.add(recordButton[index]);                         //버튼패널에 계산식버튼 부착
		}
		recordButtonPanel.revalidate();
		recordButtonPanel.repaint();
		
		resultPanel.add(recordButtonPanel);
		resultPanel.revalidate();
		resultPanel.repaint();
	}
	public int getResultButtonIndex(JButton button) {
		int index;
		
		for(index = 0; index < recordList.size(); index ++) {
			if(recordButton[index] == button) break;
		}
		
		return index;
	}
	@Override
	public void actionPerformed(ActionEvent event) {      //휴지통 버튼 클릭 이벤트
		if(event.getSource() == deletionButton) {
			recordList.clear();
			setRecordPanel();
			return;
		}
	}
}
