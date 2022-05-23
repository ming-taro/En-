package view;

import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.EmptyBorder;

import Model.ExpressionDTO;
import utility.Constants;

public class RecordPanel extends JPanel implements ActionListener{
	private JPanel resultPanel;               //계산기록 출력 패널
	private JPanel recordButtonPanel;         //계산식 버튼을 부착할 패널
	private JScrollPane recordPanelScroll;    //계산기록 출력 패널 -> 계산수식 출력 패널에 달  스크롤
	private JButton[] recordButton;
	private ArrayList<ExpressionDTO> recordList;     //계산기록 출력 패널 -> 계산수식 출력 패널 -> 계산기록 리스트
	private JButton deletionButton;           //계산기록 출력 패널 -> 휴지통 버튼
	private JPanel deletionButtonPanel;       //휴지통 버튼을 담을 패널
	
	public RecordPanel(ArrayList<ExpressionDTO> recordList) {
		this.recordList = recordList;

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
	}
	private void setDeletionButtonPanel() {
		deletionButtonPanel = new JPanel();
		
		setDeletionButton();
		
		deletionButtonPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		deletionButtonPanel.add(deletionButton);
	}
	public void setRecordPanel() {
		JLabel noRecordLabel = new JLabel("<html><br>아직 기록이 없음</html>");

		removeAll();
		if(recordList.size() == 0) {
			noRecordLabel.setFont(new Font("SansSerif", Font.PLAIN, 15));
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
	public JLabel getExpressionLabel(int index) {
		JLabel expressionLabel;
		expressionLabel = new JLabel(recordList.get(index).toString());
		expressionLabel.setFont(new Font("SansSerif", Font.PLAIN, 13));  
		expressionLabel.setPreferredSize(new Dimension((int)getPreferredSize().getWidth() - 50, 13));
		expressionLabel.setHorizontalAlignment(JLabel.RIGHT);
		
		return expressionLabel;
	}
	public void setRecordList() {
		int listSize = recordList.size();
		JLabel expressionLabel, resultLabel;
		
		recordButtonPanel.setLayout(new GridLayout(listSize, 0, 0, 40));
		
		recordButtonPanel.removeAll();
		recordButton = new JButton[listSize];
		
		for(int index = listSize - 1; index >= 0; index--) {
			recordButton[index] = new JButton();                          //계산식 버튼
			
			expressionLabel = getExpressionLabel(index);

			resultLabel = new JLabel(recordList.get(index).getResult());
			resultLabel.setFont(new Font("SansSerif", Font.BOLD, 25)); 
			resultLabel.setPreferredSize(new Dimension((int)getPreferredSize().getWidth() - 50, 25));
			resultLabel.setHorizontalAlignment(JLabel.RIGHT);

			recordButton[index].setLayout(new BorderLayout());
			recordButton[index].add(expressionLabel, BorderLayout.NORTH);
			recordButton[index].add(resultLabel, BorderLayout.SOUTH);

			recordButton[index].setHorizontalAlignment(SwingConstants.RIGHT);
			recordButton[index].setBorderPainted(false);

			recordButtonPanel.add(recordButton[index]);                         //버튼패널에 계산식버튼 부착
		}
		recordButtonPanel.revalidate();
		recordButtonPanel.repaint();
		
		resultPanel.add(recordButtonPanel);
		resultPanel.revalidate();
		resultPanel.repaint();
	}
	@Override
	public void actionPerformed(ActionEvent e) {      //휴지통 버튼 클릭 이벤트
		recordList.clear();
		setRecordPanel();
	}
}
