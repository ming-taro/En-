package ui.search_box;

import java.awt.BorderLayout;
import java.awt.Image;
import java.awt.Toolkit;

import javax.swing.JFrame;

import utility.Constants;

public class SearchFrame extends JFrame {
	private SearchPanel searchPanel;
	
	public SearchFrame() {
		setSize(Constants.SEARCH_FRAME_WIDTH, Constants.SEARCH_FRAME_HEIGHT);
		setTitle("도로명 주소 검색");
		setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
		setLayout(new BorderLayout());
		setLocationRelativeTo(null);   //윈도우 가운데에 frame 띄우기
		Toolkit kit = Toolkit.getDefaultToolkit();
		Image img = kit.getImage("image\\house.png");  //frame 아이콘
		setIconImage(img);
		setVisible(false);
	}
	public void setVisible() {
		setVisible(true);
		setLocationRelativeTo(null);   //윈도우 가운데에 frame 띄우기
		setComponent();
	}
	public void setComponent() {
		searchPanel = new SearchPanel();
		
		getContentPane().removeAll();
		getContentPane().add(searchPanel);
		getContentPane().revalidate();
		getContentPane().repaint();
	}
}
