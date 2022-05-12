package operationmanagement;

public class ButtonOnCalculator {
	public enum Button {
		CE(0, "CE"),
		C(1, "C"),
		DELETION(2, "←"),
		DIVISION(3, "÷"),
		SEVEN(4, "7"),
		EIGHT(5, "8"),
		NINE(6, "9"),
		MULTIPLICATION(7, "×"),
		FOUR(8, "4"),
		FIVE(9, "5"),
	    SIX(10, "6"),
	    SUBTRACTION(11, "－"),
	    ONE(12, "1"),
	    TWO(13, "2"),
	    THREE(14, "3"),
	    ADDITION(15, "＋"),
	    PLUS_MINUS(16, "±"),
	    ZERO(17, "0"),
	    POINT(18, "."),
	    EQUAL(19, "＝");
		
	    private final int index; 
	    private final String symbol;
	    
	    Button(int index, String symbol) {
	        this.index = index;
	        this.symbol = symbol;
	    }
	    
	    public int getIndex() {return index;}
	    public String getSymbol() {return symbol;}
	    
	}
}
