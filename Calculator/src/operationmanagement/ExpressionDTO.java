package operationmanagement;

public class ExpressionDTO {
	private String firstValue;
	private String secondValue;
	private char operator;
	private String expression;
	private String result;
	
	public ExpressionDTO() {
		InitValue();
	}
	public void InitValue() {
		firstValue = "";
		secondValue = "";
		operator = ' ';
		result = "";
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
	public void setResult(String result) {
		this.result = result;
	}
	public String getResult() {
		return result;
	}
	public void setExpression() {
		expression = firstValue +  operator + secondValue + "=";
	}
	public String getExpression() {
		return expression;
	}
	@Override
    public String toString() {
        return firstValue + " " + operator + " " + secondValue + " =\n" + result;
    }
}
