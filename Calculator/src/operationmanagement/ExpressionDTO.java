package operationmanagement;

public class ExpressionDTO {
	private Double firstValue;
	private Double secondValue;
	private char operator;
	public void setFirstValue(double number) {
		firstValue = number;
	}
	public void setSecondValue(double number) {
		secondValue = number;
	}
	public void setOperator(char operator) {
		this.operator = operator;
	}
	public Double getFirstValue() {
		return firstValue;
	}
	public Double getSecondValue() {
		return secondValue;
	}
	public char getOperator() {
		return operator;
	}
}
