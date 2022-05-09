import javax.swing.JFrame;

public class ImageFrame extends JFrame {
	public ImageFrame() {
		setTitle("Image Search");
		setBounds(200, 200, 500, 500);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setVisible(false);
	}
	public void showImage() {
		setVisible(true);
		
	}
}
