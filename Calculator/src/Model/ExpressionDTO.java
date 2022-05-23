package Model;

public class ExpressionDTO {
	private String firstValue;
	private String operator;
	private String secondValue;
	private String result;
	
	public ExpressionDTO() {
		InitValue();
	}
	public ExpressionDTO(String firstValue, String operator, String secondValue, String result) {
		this.firstValue = firstValue;
		this.operator = operator;
		this.secondValue = secondValue;
		this.result = result;
	}
	public void InitValue() {
		firstValue = "";
		secondValue = "";
		operator = "";
		result = "";
	}
	public void setFirstValue(String number) {
		firstValue = number;
	}
	public void setSecondValue(String number) {
		secondValue = number;
	}
	public void setOperator(String operator) {
		this.operator = operator;
	}
	public String getFirstValue() {
		return firstValue;
	}
	public String getSecondValue() {
		return secondValue;
	}
	public String getOperator() {
		return operator;
	}
	public void setResult(String result) {
		this.result = result;
	}
	public String getResult() {
		return result;
	}
	@Override
    public String toString() {
        return firstValue + " " + operator + " " + secondValue + " = ";
    }
}
