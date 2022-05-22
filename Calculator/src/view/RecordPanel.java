package view;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.Image;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;

import utility.Constants;

public class RecordPanel extends JPanel implements ActionListener{
	private JPanel resultPanel;       //계산기록 출력 패널
	private JScrollPane recordPanelScroll;    //계산기록 출력 패널 -> 계산수식 출력 패널에 달  스크롤
	private JTextArea recordTextArea;
	private ArrayList<String> recordList;     //계산기록 출력 패널 -> 계산수식 출력 패널 -> 계산기록 리스트
	private JButton deletionButton;           //계산기록 출력 패널 -> 휴지통 버튼
	private JPanel deletionButtonPanel;       //휴지통 버튼을 담을 패널
	
	public RecordPanel(ArrayList<String> recordList) {
		this.recordList = recordList;

		resultPanel = new JPanel();
		recordTextArea = new JTextArea();
		recordTextArea.setFont(new Font("SansSerif", Font.BOLD, Constants.BUTTON_FONT_SIZE));
		
		ImageIcon icon = new ImageIcon("image\\wastebasket.png");
		Image image = icon.getImage();
		Image changeImage = image.getScaledInstance(Constants.BUTTON_SIZE, Constants.BUTTON_SIZE, Image.SCALE_SMOOTH);
		ImageIcon changeIcon = new ImageIcon(changeImage);

		deletionButton = new JButton(changeIcon);

		setDeletionButtonPanel();
		setRecordPanel();
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
		
		deletionButtonPanel.setBackground(Color.white);
		deletionButtonPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
		deletionButtonPanel.add(deletionButton);
	}
	public void setRecordPanel() {
		JLabel noRecordLabel = new JLabel("<html><br>아직 기록이 없음</html>");
		
		recordPanelScroll = new JScrollPane(resultPanel);
		resultPanel.setBackground(Color.white);
		setLayout(new BorderLayout());

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
		revalidate();
		repaint();
		
	}
	public void setRecordList() {
		recordTextArea.setText("");  //먼저 이전에 저장된 계산기록을 초기화 
		
		for(int index = recordList.size() - 1; index >= 0; index--) {
			recordTextArea.append(recordList.get(index) + "\n\n");
		}

		resultPanel.add(recordTextArea);
	}
	@Override
	public void actionPerformed(ActionEvent e) {
		recordList.clear();
		recordTextArea.setText("");
	}
}
