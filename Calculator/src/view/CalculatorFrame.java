package view;
import java.awt.*;
import java.awt.event.*;
import java.util.ArrayList;
import javax.swing.*;
import utility.*;

public class CalculatorFrame extends JFrame implements ActionListener, ComponentListener{
	private JPanel mainPanel;
	private RecordPanel recordPanel;               //계산기록 출력 패널 -> 계산수식 출력 패널
	private ExpressionPanel expressionPanel;               //프레임 -> 계산수식, 현재 입력값 출력 패널
	private CalculatorButtonPanel calculationButtonPanel;  //프레임 -> 계산기 버튼 패널
	private int panelNumber;  
	private Dimension frameSize;
	
	public CalculatorFrame(ExpressionPanel expressionPanel, CalculatorButtonPanel calculationButtonPanel, RecordPanel recordPanel) {
		mainPanel = new JPanel();
		mainPanel.setLayout(new BorderLayout());
		
		this.expressionPanel = expressionPanel;
		this.calculationButtonPanel = calculationButtonPanel;
		this.recordPanel = recordPanel;
		
		expressionPanel.setActionListener(this);          //계산식 출력 패널의 기록보기 버튼에 event연결
		expressionPanel.setComponentListener(this);
		
		mainPanel.add(expressionPanel, BorderLayout.NORTH);
		mainPanel.add(calculationButtonPanel, BorderLayout.CENTER);

		panelNumber = Constants.BUTTON_PANEL_MODE;
		
		frameSize = new Dimension(Constants.WIDTH, Constants.HEIGHT);
		setSize(Constants.WIDTH, Constants.HEIGHT);
		setTitle("Calculator");
		setDefaultCloseOperation(EXIT_ON_CLOSE);
		setLayout(new BorderLayout());
		setLocationRelativeTo(null);   //윈도우 가운데에 계산기 띄우기
		Toolkit kit = Toolkit.getDefaultToolkit();
		Image img = kit.getImage("image\\calculator.png");  //계산기 frame 아이콘
		setIconImage(img);
		addComponentListener(this);
		setVisible(true);
		
		getContentPane().add(mainPanel, BorderLayout.CENTER);
	}
	public int getPanelNumber() {
		return panelNumber;
	}
	private void switchToRecordPanel() {                     //계산기록보기 패널로 전환
		mainPanel.add(recordPanel, BorderLayout.CENTER);     
		recordPanel.setRecordPanel();
    	recordPanel.setBackgroundColor(new Color(240, 240, 240));
		panelNumber = Constants.RECORD_PANEL_MODE;
	}
	private void swtichToCalculationButtonPanel() {          //계산버튼 입력 패널로 전환
		mainPanel.add(calculationButtonPanel, BorderLayout.CENTER);  
		panelNumber = Constants.BUTTON_PANEL_MODE;
	}
	public void switchPanel() {
		mainPanel.removeAll();
		mainPanel.add(expressionPanel, BorderLayout.NORTH);
		
		if(panelNumber == Constants.BUTTON_PANEL_MODE) switchToRecordPanel();
		else swtichToCalculationButtonPanel();
			
		mainPanel.revalidate();
		mainPanel.repaint();
	}
	@Override
	public void actionPerformed(ActionEvent event) {
		switchPanel();
	}
	public void setFrameSize() {
		if(frameSize.width < Constants.MIN_WIDTH) {     //frame 최소크기 설정   
            setSize(Constants.MIN_WIDTH, frameSize.height);
        }
		if(frameSize.height < Constants.MIN_HEIGHT) {
        	setSize(frameSize.width, Constants.MIN_HEIGHT);
        }
	}
	public void setRecordPanel() {
		if(frameSize.width >= Constants.MAX_WIDTH) {                //frame을 옆으로 늘리면 기록창 보이기
			recordPanel.setRecordPanel();
        	recordPanel.setPreferredSize(new Dimension(Constants.RECORD_PANEL_WIDTH, frameSize.height));
        	expressionPanel.setInvisibleForRecordButton();
        	getContentPane().add(recordPanel, BorderLayout.EAST);   
        }
		if(frameSize.width < Constants.MAX_WIDTH) {
        	getContentPane().removeAll();
        	recordPanel.setPreferredSize(new Dimension(frameSize.width - 10, frameSize.height));
        	getContentPane().add(mainPanel, BorderLayout.CENTER);
        	revalidate();
    		repaint();
        	expressionPanel.setVisibleForRecordButton();
        }
	}
	@Override
	public void componentResized(ComponentEvent e) {    
		frameSize = this.getSize();
		
		expressionPanel.increaseInputLabel();    //창 크기 조절에 따라 입력숫자 크기 조절
		expressionPanel.decreaseInputLabel();
		
		setFrameSize();
		setRecordPanel();
        
        if(panelNumber == Constants.RECORD_PANEL_MODE) switchPanel();  //기록버튼을 누른 후(기록창 뜸) 창 크기 조절시 다시 계산버튼으로 돌아옴
	}
	@Override
	public void componentMoved(ComponentEvent e) {
	}
	@Override
	public void componentShown(ComponentEvent e) {
	}
	@Override
	public void componentHidden(ComponentEvent e) {
	}
}
