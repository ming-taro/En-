import java.awt.*;
import java.awt.event.*;
import java.io.IOException;

import javax.swing.*;

import org.json.simple.parser.ParseException;

public class MainPage extends JPanel{
	private JButton searchButton;
	private JButton searchRecordButton;
	private JTextField searchField;
	private MyListener listener;
	private PanelManager panelManager;
	
	public MainPage(PanelManager panelManager) {
		searchField = new JTextField("");   //�˻��� �Է�â
		searchButton = new JButton();
		searchRecordButton = new JButton("�˻� ���");
		listener = new MyListener();
		this.panelManager = panelManager;
		
		setLayout(null);
		setSize(800, 600);
		setComponents();
	}
	public void setComponents() {
		Font font = new Font("SansSerif", Font.BOLD, 20);

		JPanel searchPanel = new JPanel();
		searchPanel.setLayout(null);
		searchPanel.setBackground(Color.white);
		searchPanel.setBounds(0, 0, 800, 600);
		
		searchField.setBounds(150, 270, 450, 50); //�˻�â
		searchField.setFont(font);
		
		searchButton.setBounds(600, 270, 50, 50); //�˻���ư
		
		searchRecordButton.setBounds(620, 480, 130, 40);  //�˻���� ��ư
		searchRecordButton.setFont(font);

		add(searchPanel);
		searchPanel.add(searchField);
		searchPanel.add(searchButton);
		searchPanel.add(searchRecordButton);
		
		searchField.addActionListener(listener);
		searchButton.addActionListener(listener);
		searchRecordButton.addActionListener(listener);
	}
	
	private class MyListener implements ActionListener{

		@Override
		public void actionPerformed(ActionEvent event){
			if(event.getSource() == searchField && searchField.getText().equals("")) {
				
			}
			else if(event.getSource() == searchField || event.getSource() == searchButton) {   //�˻��� �ʵ� enter or �˻���ư Ŭ��
				System.out.print(searchField.getText());
				try {
					panelManager.ChangeToSearchResult(searchField.getText());
				} catch (IOException | ParseException e) {
					e.printStackTrace();
				}
			}
			else {
				System.out.println("�����ȸ��ư"); 
			}
			searchField.setText("");
		}
	}
}
