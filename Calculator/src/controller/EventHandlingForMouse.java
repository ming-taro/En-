package controller;

import java.awt.Color;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;

import javax.swing.JButton;

public class EventHandlingForMouse implements MouseListener{
	private Color buttonColor;
	
	@Override
	public void mouseClicked(MouseEvent event) {     //버튼 클릭 후 아직 버튼위에 마우스가 위치할 때
		JButton button = (JButton) event.getSource();
		
		if(button.getText().equals("=")) button.setBackground(new Color(69, 153, 219));
		else button.setBackground(new Color(209, 209, 209));
	}
	@Override
	public void mousePressed(MouseEvent event) {
	}
	@Override
	public void mouseReleased(MouseEvent event) {    //마우스로 버튼 클릭시
		JButton button = (JButton) event.getSource();
		
		button.setBackground(buttonColor);
	}
	@Override  
	public void mouseEntered(MouseEvent event) {      //버튼위에 마우스가 위치하는 경우
		JButton button = (JButton) event.getSource();
		
		buttonColor = button.getBackground();
		
		if(button.getText().equals("=")) button.setBackground(new Color(69, 153, 219));
		else button.setBackground(new Color(209, 209, 209));
	}
	@Override
	public void mouseExited(MouseEvent event) {       //마우스가 해당 버튼을 벗어나는 경우
		JButton button = (JButton) event.getSource();
		
		button.setBackground(buttonColor);
	}
}
