import java.awt.Graphics;
import java.awt.Image;
import java.net.MalformedURLException;
import java.net.URL;

import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JPanel;

public class ImageFrame extends JFrame {
	private Image background;
	
	public ImageFrame() {
		setTitle("Image");
		setBounds(200, 200, 500, 500);  
		setVisible(false);
	}
	public void showImage(String url) throws MalformedURLException {
		background = new ImageIcon(new URL(url)).getImage();
		setVisible(true);
	}
	public void paint(Graphics g) {//그리는 함수
		g.drawImage(background, 0, 0, null);//background를 그려줌
}
}
