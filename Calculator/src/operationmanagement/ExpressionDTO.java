package operationmanagement;

public class ExpressionDTO {
	private String firstValue;
	private String secondValue;
	private char operator;
	private String result;
	
	public ExpressionDTO() {
		firstValue = "";
		secondValue = "";
	}
	public void setFirstValue(String number) {
		firstValue = number;
	}
	public void setSecondValue(String number) {
		secondValue = number;
	}
	public void setOperator(char operator) {
		this.operator = operator;
	}
	public String getFirstValue() {
		return firstValue;
	}
	public String getSecondValue() {
		return secondValue;
	}
	public char getOperator() {
		return operator;
	}
	
	@Override
    public String toString() {
        return firstValue + " " + operator + " " + secondValue + " =\n" + result;
    }
}
