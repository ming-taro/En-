package view;
import java.awt.*;
import java.awt.event.*;
import java.util.ArrayList;
import javax.swing.*;
import utility.*;

public class CalculatorFrame extends JFrame implements ActionListener, ComponentListener{
	private RecordPanel recordPanel;               //계산기록 출력 패널 -> 계산수식 출력 패널
	private ExpressionPanel expressionPanel;                   //프레임 -> 계산수식, 현재 입력값 출력 패널
	private CalculatorButtonPanel calculationButtonPanel;  //프레임 -> 계산기 버튼 패널
	private int panelNumber;  
	private Dimension frameSize;
	
	public CalculatorFrame(ExpressionPanel equationPanel, CalculatorButtonPanel calculationButtonPanel, ArrayList<String> recordList) {
		setPreferredSize(new Dimension(Constants.WIDTH, Constants.HEIGHT));
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

		this.expressionPanel = equationPanel;
		this.calculationButtonPanel = calculationButtonPanel;
		
		equationPanel.setActionListener(this);          //계산식 출력 패널의 기록보기 버튼에 event연결
		equationPanel.setComponentListener(this);
		getContentPane().add(equationPanel, BorderLayout.NORTH);            //계산기 frame에 계산식 출력 패널 추가
		getContentPane().add(calculationButtonPanel, BorderLayout.CENTER);   //계산기 frame에 계산버튼 패널 추가
		
		recordPanel = new RecordPanel(recordList);
		panelNumber = Constants.BUTTON_PANEL_MODE;
	}
	public int getPanelNumber() {
		return panelNumber;
	}
	public void ConvertPanel() {
		getContentPane().removeAll();
		getContentPane().add(expressionPanel, BorderLayout.NORTH); 
		
		if(panelNumber == Constants.BUTTON_PANEL_MODE) {
			getContentPane().add(recordPanel, BorderLayout.CENTER);       //계산기록보기 패널로 전환
			recordPanel.setRecordList();
			setFocusable(false);
			panelNumber = Constants.RECORD_PANEL_MODE;
		}
		else {
			getContentPane().add(calculationButtonPanel, BorderLayout.CENTER);    //계산버튼 입력 패널로 전환
			requestFocus();
			setFocusable(true);
			panelNumber = Constants.BUTTON_PANEL_MODE;
		}
			
		revalidate();
		repaint();
	}
	@Override
	public void actionPerformed(ActionEvent event) {
		ConvertPanel();
	}
	@Override
	public void componentResized(ComponentEvent e) {    //frame 최소크기 설정
		frameSize = this.getSize();
		
        if(frameSize.width < Constants.MIN_WIDTH) {
        	frameSize.width = Constants.MIN_WIDTH;
        }
        if(frameSize.height < Constants.MIN_HEIGHT) {
        	frameSize.height = Constants.MIN_HEIGHT;
        }
        setSize(frameSize);
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
