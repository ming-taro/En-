package utility;

public class Constants {
	public static final int HEIGHT = 600;
	public static final int WIDTH = 400;
	
	public static final int MIN_HEIGHT = 500;
	public static final int MIN_WIDTH = 300; 
	public static final int MAX_WIDTH = 570; 
	public static final int RECORD_PANEL_WIDTH = 320;

	public static final int BUTTON_PANEL_MODE = 0;
	public static final int RECORD_PANEL_MODE = 1;
	
	public static final int RESULT_PANEL = 200;
	public static final int BUTTON_PANEL = 500;
	public static final int BUTTON_FONT_SIZE = 20;
	public static final int BUTTON_SIZE = 25;
	public static final int INPUT_FONT_SIZE = 45;
	public static final int EXPRESSION_FONT_SIZE = 14;
	public static final String THOUSAND_SEPARATOR_REGEX = "\\B(?=(\\d{3})+(?!\\d))";
	
	public static final int EQUAL_SIGN = 10;    //키보드 아스크코드값
	public static final int BACKSPACE = 8;
	public static final int DIVISION = 47;
	public static final int ESC = 27;
	
	public static final boolean IS_CALCULATION_OVER = true;
	public static final boolean IS_NOT_CALCULATION_OVER = false;
	
	public static final boolean IS_FIRST_INPUT = true;
	public static final boolean IS_NOT_FIRST_INPUT = false;
	
	public static final boolean IS_VALUE = false;
	
	public static final boolean IS_ALREADY_ENTERED_OPERATOR = true;
	public static final boolean IS_NOT_ENTERED_OPERATOR = false;
	
	public static final boolean IS_ALREADY_ENTERED_SECOND_VALUE = true;
	public static final boolean IS_NOT_ENTERED_SECOND_VALUE = false;
	
	public static final boolean IS_ZERO = true;
	public static final boolean IS_NOT_ZERO = false;
	
	public static final boolean IS_THERE_CALCULATION_RESULT = true;
	public static final boolean IS_THERE_NO_CALCULATION_RESULT = false;
	
	public static final boolean IS_THERE_OPERATOR = true;
	public static final boolean IS_THERE_NO_OPERATOR = false;

	public static final boolean IS_THERE_SECOND_VALUE = true;
	public static final boolean IS_THERE_NO_SECOND_VALUE = false;
	
	public static final boolean IS_RANGE_EXCEEDED = true;
	public static final boolean IS_RANGE_NOT_EXCEEDED = false;

	public static final boolean IS_POINT_ENTERED = true;
	public static final boolean IS_POINT_NOT_ENTERED = false;

	public static final boolean IS_OPERATOR_CHANGED = true;
	public static final boolean IS_OPERATOR_NOT_CHANGED = false;

	public static final boolean IS_DIVIDED_BY_ZERO = true;
	public static final boolean IS_NOT_DIVIDED_BY_ZERO = false;
	
	public static final boolean IS_RESULT_UNDEFINED= true;
	public static final boolean IS_RESULT_DEFINED = false;

	public static final boolean IS_NEGATE_OPERATION = true;
	public static final boolean IS_NOT_NEGATE_OPERATION = false;
	
	public static final boolean IS_DELETION_BUTTON_PRESSED = true;
	public static final boolean IS_NOT_DELETION_BUTTON_PRESSED = false;
	
	public static final boolean IS_CALCULATION_RECORD_LOADED = true;
	public static final boolean IS_CALCULATION_RECORD_NOT_LOADED = false;
	
	public static final boolean IS_FIRST_VALUE_RESULT = true;
	public static final boolean IS_NOT_FIRST_VALUE_RESULT = false;
}
