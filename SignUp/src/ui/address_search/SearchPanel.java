package ui.address_search;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

import javax.swing.*;
import javax.swing.border.EmptyBorder;

import api.RoadAddress;
import ui.UICreator;
import utility.Constants;

public class SearchPanel extends JPanel implements UICreator, ActionListener{
	private JPanel guidePanel;
	private JPanel inputPanel;
	private JPanel searchResultPanel;
	private JTextField inputTextField;
	private JButton searchButton;
	private RoadAddress roadAddress;
	private JButton[] roadAddressButton;
	private JScrollPane searchResultPanelScroll;
	private ActionListener actionListener;
	
	public SearchPanel() {
		roadAddress = new RoadAddress();
		
		setComponent();
	}
	
	@Override
	public void setComponent() {
		guidePanel = new JPanel();
		inputPanel = new JPanel();
		
		JLabel guideLabel = new JLabel();
		Font font = new Font("고딕", Font.BOLD, 15);
		
		guideLabel = new JLabel("<html><br><br>찾으시려는 도로명주소+건물번호/건물명을<br>입력해주세요. "
				+ "(예: 여의나루로 4길)<br><br><br><br></html>");
		guideLabel.setFont(font);
		
		guidePanel.setLayout(new FlowLayout(FlowLayout.CENTER));
		guidePanel.add(guideLabel);
		guidePanel.setPreferredSize(new Dimension(Constants.SEARCH_PANEL_WIDTH, 100));
		guidePanel.setBackground(new Color(255, 0, 0, 0));
		
		inputTextField = new JTextField();
		inputTextField.setPreferredSize(new Dimension(250, 50));     //검색어 입력 필드
		inputTextField.setFont(font);
		
		searchButton = new JButton("검색");
		searchButton.setPreferredSize(new Dimension(70, 50));
		
		inputPanel.setLayout(new FlowLayout(FlowLayout.CENTER));
		inputPanel.add(inputTextField);
		inputPanel.add(searchButton);
		inputPanel.setPreferredSize(new Dimension(Constants.SEARCH_PANEL_WIDTH, 60));
		inputPanel.setBackground(new Color(255, 0, 0, 0));
		
		inputTextField.addActionListener(this);
		searchButton.addActionListener(this);
		
		add(guidePanel, BorderLayout.NORTH);
		add(inputPanel, BorderLayout.CENTER);
	}
	
	private void setNoResult(JPanel searchResultPanel) {
		Font font = new Font("고딕", Font.BOLD, 15);
		JLabel guideLabel = new JLabel("검색 결과가 없습니다.");
		guideLabel.setFont(font);
		searchResultPanel.add(guideLabel);
	}
	
	private void setSearchResultPanel() {
		searchResultPanelScroll = new JScrollPane();
		searchResultPanelScroll.setPreferredSize(new Dimension(Constants.SEARCH_PANEL_WIDTH + 50, 300));
		searchResultPanelScroll.setViewportView(searchResultPanel);
		searchResultPanelScroll.setHorizontalScrollBarPolicy(JScrollPane.HORIZONTAL_SCROLLBAR_NEVER);
		searchResultPanelScroll.setBorder(new EmptyBorder(0, 0, 0, 0));
		
		removeAll();
		add(guidePanel, BorderLayout.NORTH);
		add(inputPanel, BorderLayout.CENTER);
		add(searchResultPanelScroll, "South");
		revalidate();
		repaint();
	}
	
	private JPanel setRoadAddressButton(JButton roadAddressButton) {
		JPanel buttonPanel = new JPanel();    //레이아웃에 따라 버튼크기가 달라지지 않도록 패널에 버튼을 한번 넣어 크기를 고정시킨 후, 
										      //결과패널에 버튼을 직접 넣지 않고 크기가 고정된 패널을 넣는다
		roadAddressButton.setPreferredSize(new Dimension(Constants.SEARCH_PANEL_WIDTH, 60));
		roadAddressButton.setHorizontalAlignment(SwingConstants.LEFT);
		roadAddressButton.setBorderPainted(false);
		buttonPanel.setBackground(new Color(255, 0, 0, 0));
		buttonPanel.setPreferredSize(new Dimension(Constants.SEARCH_PANEL_WIDTH, 70));
		buttonPanel.add(roadAddressButton);
		
		return buttonPanel;
	}
	
	private void setRoadAddress() {
		ArrayList<String> searchResult = roadAddress.getSearchResult(inputTextField.getText());  //도로명 주소 검색결과
		String roadAddress;
		int searchNumber = searchResult.size();
 
		searchResultPanel = new JPanel();
		searchResultPanel.setBackground(new Color(255, 0, 0, 0));
		
		if(searchNumber == 0) {
			setNoResult(searchResultPanel);
			return;
		}
		
		roadAddressButton = new JButton[searchNumber];
		searchResultPanel.setLayout(new GridLayout(searchNumber, 0));
		
		for(int index = 0; index < searchNumber; index++) {
			roadAddress = searchResult.get(index);
			roadAddressButton[index] = new JButton(roadAddress);   //버튼에 검색결과값을 텍스트로 추가
			roadAddressButton[index].addActionListener(actionListener);            //도로명 주소 클릭시 회원가입 화면의 주소칸에 자동 입력
			searchResultPanel.add(setRoadAddressButton(roadAddressButton[index])); //결과패널에 버튼 추가
		}
		setSearchResultPanel();
	}
	@Override
	public void paintComponent(Graphics g) {
		Dimension panelSize = getSize();
		int width = panelSize.width;
		int height = panelSize.height;
		
		ImageIcon icon = new ImageIcon("image\\white.jpg");
	    g.drawImage(icon.getImage(), 0, 0, width, height, null);
	    setOpaque(false);
	    super.paintComponent(g);
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		setRoadAddress();
	}

	@Override
	public void setActionListener(ActionListener actionListener) {
		this.actionListener = actionListener;
	}
}
