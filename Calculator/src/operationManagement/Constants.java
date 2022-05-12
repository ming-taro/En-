package operationManagement;

public class Constants {
	public static final int HEIGHT = 700;
	public static final int WIDTH = 500;
	public static final int RESULT_PANEL = 200;
	public static final int BUTTON_PANEL = 500;
	
	public enum ButtonnOnCalculator {
		CE("CE"),
	    C("C"),
	    DELETION("←"),
	    DIVISION("÷"),
	    SEVEN("7"),
	    EIGHT("8"),
	    NINE("9"),
	    MULTIPLICATION("×"),
	    FOUR("4"),
	    FIVE("5"),
	    SIX("6"),
	    SUBTRACTION("－"),
	    ONE("1"),
	    TWO("2"),
	    THREE("3"),
	    ADDITION("＋"),
	    PLUS_MINUS("±"),
	    ZERO("0"),
	    POINT("."),
	    EQUAL("=")
	    ;

	    private final String selectedButton;

	    ButtonnOnCalculator(String selectedButton) {
	        this.selectedButton = selectedButton;
	    }

	    public String selectedButton() {
	        return selectedButton;
	    }
	}
}
