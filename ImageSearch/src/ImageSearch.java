import java.awt.Color;

import javax.swing.*;

public class ImageSearch {

	public static void main(String[] args) {
		//PanelManager panelManager = new PanelManager();
		//panelManager.setVisible(true);
		//MainPage mainPage = new MainPage();
		JFrame frame = new JFrame();
		frame.setTitle("Image Search");
		frame.setBounds(450, 150, 800, 600);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.setVisible(true);
		frame.setLayout(null);
		SearchResult panel = new SearchResult();
		frame.setContentPane(panel);
	
		/*JPanel top = new JPanel();
		top.setBounds(0, 0, 800, 600);
		top.setLayout(null);
		top.setBackground(Color.red);
		
		JPanel bottom = new JPanel();
		bottom.setBounds(0, 100, 800, 50);
		bottom.setLayout(null);
		JButton btn = new JButton("¾Æ¿Á");
		btn.setBounds(0,0,100,30);
		bottom.add(btn);
		top.add(bottom);
		
		frame.add(top);*/
		
		/*JFrame frame = new JFrame();
		frame.setTitle("Image Search");
		frame.setBounds(450, 150, 800, 600);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		
		frame.add(mainPage);
		frame.setVisible(true);*/
	}

}
