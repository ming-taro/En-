import java.awt.Color;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

import javax.swing.*;

public class SearchRecord extends JPanel implements ActionListener{
	private JButton homeButton;
	private JButton resetButton;
	private PanelManager panelManager;
	private ArrayList<String> searchRecordList;
	private SearchRecord searchRecord;
	
	public SearchRecord(PanelManager panelManager) {
		homeButton = new JButton();
		resetButton = new JButton("초기화");
		searchRecordList = new ArrayList<String>();
		this.panelManager = panelManager;
		
		homeButton.setBounds(10, 10, 50, 50);
		resetButton.setBounds(350, 430, 100, 50);
		
		setLayout(null);
		setBounds(0, 0, 800, 600);
		setBackground(Color.white);
		
		add(homeButton);
		add(resetButton);
		
		homeButton.addActionListener(this);
		resetButton.addActionListener(this);
	}
	public void setSearchRecord() {
		sear.crea
	}

	@Override
	public void actionPerformed(ActionEvent event) {
		if(event.getSource() == homeButton) {   //뒤로가기
			panelManager.ChangeToMainPage(); 
		}
		else {  //초기화
			
		}
		
	}
}
