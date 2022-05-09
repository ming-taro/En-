import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class MainPage{
	private JButton searchButton;
	private JButton searchRecordButton;
	private JTextField searchField;
	private MyListener listener;
	private PanelManager panelManager;
	
	public MainPage(JFrame frame) {
		searchField = new JTextField();   //�˻��� �Է�â
		searchButton = new JButton();
		searchRecordButton = new JButton("�˻� ���");
	}
	public void setComponents() {
		Font font = new Font("SansSerif", Font.BOLD, 20);
		//setLayout(null);

		JPanel searchPanel = new JPanel();
		searchPanel.setLayout(null);
		searchPanel.setBackground(Color.white);
		
		searchField.setBounds(150, 270, 450, 50); //�˻�â
		searchField.setFont(font);
		
		searchButton.setBounds(600, 270, 50, 50); //�˻���ư
		
		searchRecordButton.setBounds(620, 480, 130, 40);  //�˻���� ��ư
		searchRecordButton.setFont(font);

		searchPanel.add(searchField);
		searchPanel.add(searchButton);
		searchPanel.add(searchRecordButton);
		
		searchField.addActionListener(listener);
		searchButton.addActionListener(listener);
		searchRecordButton.addActionListener(listener);
	}
	
	private class MyListener implements ActionListener{

		@Override
		public void actionPerformed(ActionEvent event) {
			
			if(event.getSource() == searchField || event.getSource() == searchButton) {   //�˻��� �ʵ� enter or �˻���ư Ŭ��
				panelManager.ChangeToSearchResult();
			}
			else {
				System.out.println("�����ȸ��ư"); 
			}
		}
	}
}
